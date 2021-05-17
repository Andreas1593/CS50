// Implements a dictionary's functionality
#include <stdbool.h>
#include "dictionary.h"
#include <stdio.h>
#include <cs50.h>
#include <ctype.h>
#include <stdlib.h>
#include <string.h>
#include <strings.h>

// Initialize counter for the number of words
unsigned int words = 0;

// Represents a node in a hash table
typedef struct node
{
    char word[LENGTH + 1];
    struct node *next;
}
node;

// Number of buckets in hash table
const unsigned int N = 1000;

// Hash table
node *table[N];

// Returns true if word is in dictionary, else false
bool check(const char *word)
{
    // Hash the word and set a curser pointing to the table
    unsigned int index = hash(word);
    node *cursor = table[index];
    while (cursor != NULL)
    {
        if (strcasecmp(cursor->word, word) == 0)
        {
            return true;
        }
        cursor = cursor->next;
    }
    return false;
}

// Hashes word to a number
unsigned int hash(const char *word)
{
    // Source: djib2 by Dan Bernstein, modified by a reddit user
    unsigned long hash = 5381;
    int c = *word;
    c = tolower(c);
    while (*word != 0)
    {
        hash = ((hash << 5) + hash) + c;
        c = *word++;
        c = tolower(c);
    }
    return hash % N;
}

// Loads dictionary into memory, returning true if successful, else false
bool load(const char *dictionary)
{
    // Open file with a dictionary inside
    FILE *fp = fopen(dictionary, "r");

    // Check if it could be opened
    if (fp == NULL)
    {
        return false;
    }

    // Storage space for the word
    char word[LENGTH + 1];

    // Scan every word of the dictionary
    while (fscanf(fp, "%s", word) != EOF)
    {

        // Allocate memory for the word and check if there's enough memory
        node *n = malloc(sizeof(node));
        if (n == NULL)
        {
            return false;
        }

        // Copy word to the new node and link node to the hash table
        strcpy(n->word, word);
        unsigned int index = hash(word);

        // New node points to the first element
        n->next = table[index];

        // Node-pointer in the hash table points to the new node
        table[index] = n;

        // Count the number of words loaded for the size function
        words++;
    }

    fclose(fp);

    // If the dictionary was loaded successfully
    return true;
}

// Returns number of words in dictionary if loaded, else 0 if not yet loaded
unsigned int size(void)
{
    // TODO
    return words;
}

// Unloads dictionary from memory, returning true if successful, else false
bool unload(void)
{
    // Iterate over the hash table
    for (int i = 0; i < N; i++)
    {

        // Set a cursor and tmp cursor to free every word of the linked list
        node *cursor = table[i];
        node *tmp = cursor;
        while (cursor != NULL)
        {
            cursor = cursor->next;
            free(tmp);
            tmp = cursor;
        }
    }
    return true;
}