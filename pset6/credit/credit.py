import re

# Prompt 13 to 16 digit credit card number and convert to integer
number = int(input("Number: "))
checksum = 0

# Multiply every other digit by 2, starting with the number’s second-to-last digit,
# and then add those products’ digits together
for i in range(1, 17, 2):

    # int() truncates the returned integer; diving by 10^i cuts off the digits on the right
    x = (int(number / (10 ** i)) % 10) * 2

    # If the number has two digits, add 1 + second digit (because max. 9 * 2 = 18),
    # otherwise just add the number
    if x >= 10:
        checksum += 1 + (x % 10)
    else:
        checksum += x

# Add the sum to the sum of the digits that weren’t multiplied by 2
for i in range(0, 17, 2):
    checksum += (int(number / (10 ** i)) % 10)

# If the total’s last digit is 0 (or, put more formally, if the total modulo 10 is
# congruent to 0), the number is valid
if checksum % 10 == 0:

    # Convert number back to a string for regex
    number = str(number)

    # AMEX has 15 digits, starts with 34 or 37
    if re.search("(34|37)[0-9]{13}", number):
        print("AMEX")

    # MASTERCARD has 16 digits, starts with 51, 52, 53, 54 or 55
    elif re.search("(51|52|53|54|55)[0-9]{14}", number):
        print("MASTERCARD")

    # VISA has 13 or 16 digits, starts with 4
    elif re.search("4[0-9]{12}|[0-9]{15}", number):
        print("VISA")

    # Any other number is invalid
    else:
        print("INVALID")

# Invalid checksum
else:
    print("INVALID")