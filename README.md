# CS50
 Harvard University's CS50

## Lab 2: Scrabble

Determine which of two Scrabble words is worth more.
```
$ ./scrabble
Player 1: COMPUTER
Player 2: science
Player 1 wins!
```

## Background

In the game of Scrabble, players create words to score points, and the number of points is the sum of the point values of each letter in the word.

A&emsp;B&emsp;C&emsp;D&emsp;E&emsp;F&emsp;G&emsp;H&emsp;I&emsp;J&emsp;K&emsp;L&emsp;M&emsp;N&emsp;O&emsp;P&emsp;Q&emsp;R&emsp;S&emsp;T&emsp;U&emsp;V&emsp;W&emsp;X&emsp;Y&emsp;Z
1&emsp;3&emsp;3&emsp;2&emsp;1&emsp;4&emsp;2&emsp;4&emsp;1&emsp;8&emsp;5&emsp;1&emsp;3&emsp;1&emsp;1&emsp;3&emsp;10&emsp;1&emsp;1&emsp;1&emsp;1&emsp;4&emsp;4&emsp;8&emsp;4&emsp;10

For example, if we wanted to score the word Code, we would note that in general Scrabble rules, the C is worth 3 points, the o is worth 1 point, the d is worth 2 points, and the e is worth 1 point. Summing these, we get that Code is worth 3 + 1 + 2 + 1 = 7 points.