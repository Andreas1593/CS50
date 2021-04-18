#include <stdio.h>
#include <cs50.h>
#include <string.h>
#include <ctype.h>

int sum(string arr[99], int n);


int main(int argc, string argv[])
{

    // Check if key was prompted
    if (argc != 2)
    {
        printf("Usage: ./substitution KEY\n");
        return 1;
    }
    
    
    // Initialise the key's lenght
    int klen = strlen(argv[1]);


    // Check key lenght
    if (klen != 26)
    {
        printf("Key must contain 26 characters.\n");
        return 1;
    }


    // Check for non-alphabetical characters
    for (int i = 0; i < klen; i++)
    {
        if (!isalpha(argv[1][i]))
        {
            printf("Key must only contain alphabetic characters.\n");
            return 1;
        }

        // Make the letters uppercase (required for next step / repeated characters)
        argv[1][i] = toupper(argv[1][i]);
    }


    // Check for repeated characters
    for (int i = 0; i < klen; i++)
    {
        
        // 2nd loop to compare one letter at a time with every letter of the key
        for (int j = 0; j < klen; j++)
        {
            
            // Print error if a letter has the same value as any other letter of the key exept the current letter
            if ((argv[1][i] == argv[1][j]) && (i != j))
            {
                printf("Key must not contain repeated characters.\n");
                return 1;
            }
        }
    }


    // Ask user for plain text
    string pt = get_string("plaintext: ");

    // Print ciphertex
    printf("ciphertext: ");

    // Cipher letter by letter
    for (int i = 0; i < strlen(pt); i++)
    {
        
        // Check if the plain text's character is a letter
        if (isalpha(pt[i]))
        {
    
            // Check if the plain text's letter is uppercase
            if (isupper(pt[i]))
            {
    
                // Determine the position of the plain text's letter within the key and print uppercase
                int pos = pt[i] - 65;
                printf("%c", toupper(argv[1][pos]));
            }
    
            // Check if the plain text's letter is lowercase
            else if (islower(pt[i]))
            {
    
                // Determine the position of the plain text's letter within the key and print lowercase
                int pos = pt[i] - 97;
                printf("%c", tolower(argv[1][pos]));
            }
            
        }
        
        else
        {
            printf("%c", pt[i]);
        }

    }
    printf("\n");
}


// Function to return the sum of elements in a 2D array
int sum(string arr[99], int n)
{
    int sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += arr[1][i];
    }
    return sum;
}