# CS50x
 Harvard University's CS50x
 
## Lab 8: Trivia

Write a webpage that lets users answer trivia questions.

![Trivia Webpage](https://github.com/Andreas1593/CS50x/blob/Week-8/images/trivia.png?raw=true)

### Getting Started

*\[Download Distribution Code\]*

### Implementation Details

Design a webpage using HTML, CSS, and JavaScript to let users answer trivia questions.

- In index.html, add beneath “Part 1” a multiple-choice trivia question of your choosing with HTML.
    - You should use an h3 heading for the text of your question.
    - You should have one button for each of the possible answer choices. There should be at least three answer choices, of which exactly one should be correct.
- Using JavaScript, add logic so that the buttons change colors when a user clicks on them.
    - If a user clicks on a button with an incorrect answer, the button should turn red and text should appear beneath the question that says “Incorrect”.
    - If a user clicks on a button with the correct answer, the button should turn green and text should appear beneath the question that says “Correct!”.
- In index.html, add beneath “Part 2” a text-based free response question of your choosing with HTML.
    - You should use an h3 heading for the text of your question.
    - You should use an input field to let the user type a response.
    - You should use a button to let the user confirm their answer.
- Using JavaScript, add logic so that the text field changes color when a user confirms their answer.
    - If the user types an incorrect answer and presses the confirmation button, the text field should turn red and text should appear beneath the question that says “Incorrect”.
    - If the user types the correct answer and presses the confirmation button, the input field should turn green and text should appear beneath the question that says “Correct!”.

Optionally, you may also:

- Edit styles.css to change the CSS of your webpage!
- Add additional trivia questions to your trivia quiz if you would like!

## Problem Set 8: Homepage

*\[Disclaimer: The website itself makes no sense and the HTML and CSS structure might be questionable - I just wanted to play around with HTML, CSS and JavaScript, but I think I could learn some things from that\]*

Build a simple homepage using HTML, CSS, and JavaScript.

### Background

The internet has enabled incredible things: we can use a search engine to research anything imaginable, communicate with friends and family members around the globe, play games, take courses, and so much more. But it turns out that nearly all pages we may visit are built on three core languages, each of which serves a slightly different purpose:

1. HTML, or HyperText Markup Language, which is used to describe the content of websites;
2. CSS, Cascading Style Sheets, which is used to describe the aesthetics of websites; and
3. JavaScript, which is used to make websites interactive and dynamic.

Create a simple homepage that introduces yourself, your favorite hobby or extracurricular, or anything else of interest to you.

### Getting Started

*\[Download Distribution Code\]*

You can immediately start a server to view the site by running
```
$ http-server
```
in the terminal window and clicking on the link that appears.

### Specification

Implement in your homepage directory a website that must:

- Contain at least four different .html pages, at least one of which is index.html (the main page of your website), and it should be possible to get from any page on your website to any other page by following one or more hyperlinks.
- Use at least ten (10) distinct HTML tags besides <html>, <head>, <body>, and <title>. Using some tag (e.g., <p>) multiple times still counts as just one (1) of those ten!
- Integrate one or more features from Bootstrap into your site. Bootstrap is a popular library (that comes with lots of CSS classes and more) via which you can beautify your site. See Bootstrap’s documentation to get started. In particular, you might find some of Bootstrap’s components of interest. To add Bootstrap to your site, it suffices to include
```
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>
```
in your pages’ <\head>, below which you can also include
```
<link href="styles.css" rel="stylesheet">
```
to link your own CSS.

- Have at least one stylesheet file of your own creation, styles.css, which uses at least five (5) different CSS selectors (e.g. tag (example), class (.example), or ID (#example)), and within which you use a total of at least five (5) different CSS properties, such as font-size, or margin; and
- Integrate one or more features of JavaScript into your site to make your site more interactive. For example, you can use JavaScript to add alerts, to have an effect at a recurring interval, or to add interactivity to buttons, dropdowns, or forms. Feel free to be creative!
- Ensure that your site looks nice on browsers both on mobile devices as well as laptops and desktops.

### Testing

If you want to view how your site looks while you work on it, there are two options:

1. Within CS50 IDE, navigate to your homepage directory (remember how?) and then execute
```
$ http-server
```
2. Within CS50 IDE, right-click (or Ctrl+click, on a Mac) on the homepage directory in the file tree at left. From the options that appear, select Serve, which should open a new tab in your browser (it may take a second or two) with your site therein.

Recall also that by opening Developer Tools in Google Chrome, you can simulate visiting your page on a mobile device by clicking the phone-shaped icon to the left of Elements in the developer tools window, or, once the Developer Tools tab has already been opened, by typing Ctrl+Shift+M on a PC or Cmd+Shift+M on a Mac, rather than needing to visit your site on a mobile device separately!