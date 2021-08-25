import os

from cs50 import SQL
from flask import Flask, flash, redirect, render_template, request, session
from flask_session import Session
from tempfile import mkdtemp
from werkzeug.exceptions import default_exceptions, HTTPException, InternalServerError
from werkzeug.security import check_password_hash, generate_password_hash
from datetime import datetime

from helpers import apology, login_required, lookup, usd

# Configure application
app = Flask(__name__)

# Ensure templates are auto-reloaded
app.config["TEMPLATES_AUTO_RELOAD"] = True


# Ensure responses aren't cached
@app.after_request
def after_request(response):
    response.headers["Cache-Control"] = "no-cache, no-store, must-revalidate"
    response.headers["Expires"] = 0
    response.headers["Pragma"] = "no-cache"
    return response


# Custom filter
app.jinja_env.filters["usd"] = usd

# Configure session to use filesystem (instead of signed cookies)
app.config["SESSION_FILE_DIR"] = mkdtemp()
app.config["SESSION_PERMANENT"] = False
app.config["SESSION_TYPE"] = "filesystem"
Session(app)

# Configure CS50 Library to use SQLite database
db = SQL("sqlite:///finance.db")

# Make sure API key is set
if not os.environ.get("API_KEY"):
    raise RuntimeError("API_KEY not set")


@app.route("/", methods=["GET", "POST"])
@login_required
def index():
    """Show portfolio of stocks"""

    if request.method == "GET":
        # Create table for portfolio if it doesn't exist yet
        db.execute("CREATE TABLE IF NOT EXISTS portfolio (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC)")

        # Create table for history if it doesn't exist yet
        db.execute("CREATE TABLE IF NOT EXISTS history (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC, price NUMERIC NOT NULL, date DATETIME NOT NULL)")

        # Show empty portfolio if the user doesn't have a portfolio yet
        try:
            db.execute("SELECT * FROM portfolio WHERE user_id = ?", session["user_id"])
        except:
            # Get the user's cash
            cash_list = db.execute("SELECT cash FROM users WHERE id = ?", session["user_id"])
            cash = cash_list[0]["cash"]

            return render_template("/index.html", cash=cash)

        # Load all shares the user has bought
        wallet = db.execute("SELECT * FROM portfolio WHERE user_id = ?", session["user_id"])

        # Get the user's cash
        cash_list = db.execute("SELECT cash FROM users WHERE id = ?", session["user_id"])
        cash = cash_list[0]["cash"]

        # Get the shares' current prices
        prices = []
        for share in wallet:

            values = lookup(share["symbol"])
            price = values["price"]

            # Append the current shares' price
            prices.append(price)

        return render_template("/index.html", wallet=wallet, cash=cash, prices=prices)


@app.route("/buy", methods=["GET", "POST"])
@login_required
def buy():
    """Buy shares of stock"""

    # Create table for portfolio if it doesn't exist yet
    db.execute("CREATE TABLE IF NOT EXISTS portfolio (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC)")

    # Create table for history if it doesn't exist yet
    db.execute("CREATE TABLE IF NOT EXISTS history (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC, price NUMERIC NOT NULL, date DATETIME NOT NULL)")

    # If the user submits a purchase
    if request.method == "POST":

        # Set variables for the input symbol and the current user
        symbol = request.form.get("symbol").upper()
        user = session["user_id"]

        # Ensure symbol is not blank and valid
        if not symbol or lookup(symbol) == None:
            return apology("Invalid symbol", 403)

        shares = int(request.form.get("shares"))
        if shares < 1:
            return apology("Must buy at least 1 share", 403)

        # Check if the user has enough cash
        current_cash_list = db.execute("SELECT cash FROM users WHERE id = ?", user)
        # "cash_list" is a list, "cash" is an integer
        current_cash = current_cash_list[0]["cash"]

        # Current share's values and total cost
        values = lookup(symbol)
        total = values["price"] * shares

        if current_cash < total:
            return apology("Not enough cash", 403)

        else:
            # Check if the user already owns such shares and update portfolio
            if not db.execute("SELECT * FROM portfolio WHERE user_id = ? AND symbol = ?", user, symbol):
                db.execute("INSERT INTO portfolio (user_id, symbol, name, shares) VALUES (?, ?, ?, ?)",
                           user, symbol.upper(), values["name"], shares)

            else:
                # Load old number of shares, compute and update portfolio
                old_shares = db.execute("SELECT shares FROM portfolio WHERE user_id = ? AND symbol = ?", user, symbol)
                new_shares = old_shares[0]["shares"] + shares
                db.execute("UPDATE portfolio SET shares = ? WHERE user_id = ? AND symbol = ?", new_shares, user, symbol)

            # Add purchase to history
            db.execute("INSERT INTO history (user_id, symbol, name, shares, price, date) VALUES (?, ?, ?, ?, ?, ?)",
                       user, symbol.upper(), values["name"], shares, values["price"], datetime.now())

            # Compute new balance
            new_balance = current_cash - total
            db.execute("UPDATE users SET cash = ? WHERE id = ?", new_balance, user)

            return redirect("/")

    else:
        return render_template("/buy.html")


@app.route("/history")
@login_required
def history():
    """Show history of transactions"""

    # Create table for history if it doesn't exist yet
    db.execute("CREATE TABLE IF NOT EXISTS history (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC, price NUMERIC NOT NULL, date DATETIME NOT NULL)")

    # Load the user's history
    history = db.execute("SELECT * FROM history WHERE user_id = ?", session["user_id"])
    return render_template("history.html", history=history)


@app.route("/login", methods=["GET", "POST"])
def login():
    """Log user in"""

    # Forget any user_id
    session.clear()

    # User reached route via POST (as by submitting a form via POST)
    if request.method == "POST":

        # Ensure username was submitted
        if not request.form.get("username"):
            return apology("must provide username", 403)

        # Ensure password was submitted
        elif not request.form.get("password"):
            return apology("must provide password", 403)

        # Query database for username
        rows = db.execute("SELECT * FROM users WHERE username = ?", request.form.get("username"))

        # Ensure username exists and password is correct
        if len(rows) != 1 or not check_password_hash(rows[0]["hash"], request.form.get("password")):
            return apology("invalid username and/or password", 403)

        # Remember which user has logged in
        session["user_id"] = rows[0]["id"]

        # Redirect user to home page
        return redirect("/")

    # User reached route via GET (as by clicking a link or via redirect)
    else:
        return render_template("login.html")


@app.route("/logout")
def logout():
    """Log user out"""

    # Forget any user_id
    session.clear()

    # Redirect user to login form
    return redirect("/")


@app.route("/quote", methods=["GET", "POST"])
@login_required
def quote():
    """Get stock quote."""

    # User submits via form
    if request.method == "POST":

        # Get the symbol and lookup the values
        symbol = request.form.get("symbol")
        values = lookup(symbol)

        # Check for invalid symbol
        if values == None:
            return apology("Invalid symbol", 403)

        # Show the current quote
        return render_template("/quote.html", sentence="A share of " + values["name"] + " (" + values["symbol"] + ") costs " + str(usd(values["price"])) + ".")

    else:
        return render_template("/quote.html")


@app.route("/register", methods=["GET", "POST"])
def register():
    """Register user"""
    if request.method == "GET":
        return render_template("/register.html")

    else:
        # Query database for username
        rows = db.execute("SELECT * FROM users WHERE username = ?", request.form.get("username"))

        # Ensure username was submitted
        if not request.form.get("username"):
            return apology("Must provide username", 403)

        # Ensure password was submitted
        elif not request.form.get("password"):
            return apology("Mst provide password", 403)

        # Ensure username doesn't exist already
        elif len(rows) != 0:
            return apology("Username already exists", 403)

        # Ensure both passwords match
        elif request.form.get("password") != request.form.get("confirmation"):
            return apology("Passwords do not match", 403)

        else:
            # Create new user and store the hashed password in database
            hashed = generate_password_hash(request.form.get("password"))
            user = db.execute("INSERT INTO users (username, hash) VALUES (?, ?)", request.form.get("username"), hashed)

            # Remember session to let the user stay logged in
            session["user_id"] = user
            return redirect("/")


@app.route("/sell", methods=["GET", "POST"])
@login_required
def sell():
    """Sell shares of stock"""

    # Create table for portfolio if it doesn't exist yet
    db.execute("CREATE TABLE IF NOT EXISTS portfolio (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC)")

    # Create table for history if it doesn't exist yet
    db.execute("CREATE TABLE IF NOT EXISTS history (id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, user_id TEXT NOT NULL, symbol TEXT NOT NULL, name TEXT, shares NUMERIC, price NUMERIC NOT NULL, date DATETIME NOT NULL)")

    user = session["user_id"]
    symbol = request.form.get("symbol")

    # If the user submits a selling
    if request.method == "POST":

        sell_shares = int(request.form.get("shares"))
        if sell_shares < 1:
            return apology("Must sell at least 1 share", 403)

        # Load selected shares from portfolio
        current_shares_list = db.execute("SELECT shares FROM portfolio WHERE user_id = ? AND symbol = ?", user, symbol)
        # "current_shares_list" is a list, "current_shares" is an integer
        current_shares = current_shares_list[0]["shares"]

        # Check if the user has enough shares
        if current_shares < sell_shares:
            return apology("Not enough shares", 403)

        else:
            values = lookup(symbol)
            # Total value of the shares to sell
            total = values["price"] * sell_shares

            # Add selling to history (shares displayed as negative number)
            db.execute("INSERT INTO history (user_id, symbol, name, shares, price, date) VALUES (?, ?, ?, ?, ?, ?)",
                       user, symbol.upper(), values["name"], (0-sell_shares), values["price"], datetime.now())

            # Update portfolio
            new_shares = current_shares - sell_shares
            db.execute("UPDATE portfolio SET shares = ? WHERE user_id = ? AND symbol = ?", new_shares, user, symbol)

            # Get old balance
            old_balance_list = db.execute("SELECT cash FROM users WHERE id = ?", user)
            old_balance = old_balance_list[0]["cash"]

            # Compute new balance
            new_balance = old_balance + total
            db.execute("UPDATE users SET cash = ? WHERE id = ?", new_balance, user)

            return redirect("/")

    else:
        # Load shares from portfolio
        portfolio = db.execute("SELECT symbol FROM portfolio WHERE user_id = ? AND shares > 0", user)

        return render_template("/sell.html", portfolio=portfolio)


@app.route("/password", methods=["GET", "POST"])
@login_required
def password():
    """Change password"""

    if request.method == "POST":

        old_password = request.form.get("old_password")
        new_password = request.form.get("new_password")
        new_password_confirm = request.form.get("new_password_confirm")

        # Check if old password is empty
        if not old_password:
            return apology("Must provide old password", 403)

        # Check if old password incorrect
        hash_old = db.execute("SELECT hash FROM users WHERE id = ?", session["user_id"])
        hash_old = hash_old[0]["hash"]
        if not check_password_hash(hash_old, old_password):
            return apology("Old password incorrect", 403)

        # Check if new password is empty
        if not new_password or not new_password_confirm:
            return apology("Must provide new password", 403)

        # Check if new passwords don't match
        if new_password != new_password_confirm:
            return apology("New passwords don't match", 403)

        else:
            # Save new password
            new_password = generate_password_hash(new_password)
            db.execute("UPDATE users SET hash = ? WHERE id = ?", new_password, session["user_id"])

            return redirect("/")

    else:
        return render_template("/password.html")


def errorhandler(e):
    """Handle error"""
    if not isinstance(e, HTTPException):
        e = InternalServerError()
    return apology(e.name, e.code)


# Listen for errors
for code in default_exceptions:
    app.errorhandler(code)(errorhandler)
