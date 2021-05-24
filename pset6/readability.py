from cs50 import get_string

# Function to count letters in a text
def count_letters(text):
    letters = 0
    for i in range(0, len(text)):
        if text[i].isalpha():
            letters += 1
    return letters
    
# Function to count words in a text. Number of words = number of whitespaces + 1
def count_words(text):
    words = 1
    for i in range(0, len(text)):
        if ord(text[i]) == 32:
            words += 1
    return words
    
# Function to count sentences in a text. Each sentence ends with ".", "!" or "?"
def count_sentences(text):
    sentences = 0
    for i in range(0, len(text)):
        if ord(text[i]) == 33 or ord(text[i]) == 46 or ord(text[i]) == 63:
            sentences += 1
    return sentences


# Prompt user for a text    
text = get_string("Text: ")

# Count letters, words and sentences in the text
letters = count_letters(text)
words = count_words(text)
sentences = count_sentences(text)

# Coleman-Liau index is computed as 0.0588 * L - 0.296 * S - 15.8
# L = average number of letters per 100 words
# S = average number of sentences per 100 words
L = letters * 100 / words
S = sentences * 100 / words

index = round(0.0588 * L - 0.296 * S - 15.8)

# Print grade of the text according to the index
if index >= 16:
    print("Grade 16+")
elif index < 1:
    print("Before Grade 1")
else:
    print("Grade " + str(index))