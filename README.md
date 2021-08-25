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

A \	B \	C \	D \	E \	F \	G \	H \	I \	J \	K \	L \	M \	N \	O \	P \	Q \	R \	S \	T \	U \	V \	W \	X \	Y \	Z  
1 \	3 \	3 \	2 \	1 \	4 \	2 \	4 \	1 \	8 \	5 \	1 \	3 \	1 \	1 \	3 \	10 \	1 \	1 \	1 \	1 \	4 \	4 \	8 \	4 \	10

For example, if we wanted to score the word Code, we would note that in general Scrabble rules, the C is worth 3 points, the o is worth 1 point, the d is worth 2 points, and the e is worth 1 point. Summing these, we get that Code is worth 3 + 1 + 2 + 1 = 7 points.