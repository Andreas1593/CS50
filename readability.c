#include <stdio.h>
#include <cs50.h>
#include <string.h>
#include <math.h>

int count_letters(string text);
int count_words(string text);
int count_sentences(string text);

int main(void)
{
    
    // Prompt user for text
    string text = get_string("Text: ");
    
    // Call functions to count letters, words and sentences
    int letters = count_letters(text);
    int words = count_words(text);
    int sentences = count_sentences(text);
    
    // Compute L (average number of letters per 100 words) and S (average number of sentences per 100 words)
    float L = letters * 100.0 / words;
    float S = sentences * 100.0 / words;
    
    // Compute Coleman-Liau index and round it to the nearest integer
    int index = round(0.0588 * L - 0.296 * S - 15.8);
    
    // Print Grade of the text
    if (index > 15)
    {
        printf("Grade 16+\n");
    }
    else if (index < 1)
    {
        printf("Before Grade 1\n");
    }
    else
    {
        printf("Grade %i\n", index);
    }
}

// Function counting letters in the text
int count_letters(string text)
{
    int letters = 0;
    for (int i = 0, n = strlen(text); i < n; i++)
    {
        if ((text[i] >= 'a' && text[i] <= 'z') || (text[i] >= 'A' && text[i] <= 'Z'))
        {
            letters += 1;
        }
    }
    return letters;
}

// Function counting words in the text. Number of words = number of whitespaces + 1
int count_words(string text)
{
    int words = 1;
    for (int i = 0, n = strlen(text); i < n; i++)
    {
        if (text[i] == 32)
        {
            words += 1;
        }
    }
    return words;
}

// Function counting sentences in the text
int count_sentences(string text)
{
    int sentences = 0;
    for (int i = 0, n = strlen(text); i < n; i++)
    {
        if (text[i] == 33 || text[i] == 46 || text[i] == 63)
        {
            sentences += 1;
        }
    }
    return sentences;
}