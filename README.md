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

AemspBemspCemspDemspEemspFemspGemspHemspIemspJemspKemspLemspMemspNemspOemspPemspQemspRemspSemspTemspUemspVemspWemspXemspYemspZ
1emsp3emsp3emsp2emsp1emsp4emsp2emsp4emsp1emsp8emsp5emsp1emsp3emsp1emsp1emsp3emsp10emsp1emsp1emsp1emsp1emsp4emsp4emsp8emsp4emsp10

For example, if we wanted to score the word Code, we would note that in general Scrabble rules, the C is worth 3 points, the o is worth 1 point, the d is worth 2 points, and the e is worth 1 point. Summing these, we get that Code is worth 3 + 1 + 2 + 1 = 7 points.