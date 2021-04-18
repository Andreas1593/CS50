#include <stdio.h>
#include <cs50.h>

int main(void)
{
    int row = 0;

    // Ask user for height of pyramid between 1 and 8, inclusive

    int a;
    do
    {
        a = get_int("Height: ");
    }
    while (a < 1 || a > 8);

    // Loop for each row

    for (int i = 0; i < a; i++)
    {
        row += 1;

        // Loop for blanks in a row; one blank per difference of height - row

        for (int j = 0; j < (a - row); j++)
        {
            printf(" ");
        }

        // Loop for hashtags in a row; one hashtag per number of row

        for (int k = 0; k < row; k++)
        {
            printf("#");
        }
        printf("  ");

        // Pyramid going downward

        for (int k = 0; k < row; k++)
        {
            printf("#");
        }
        printf("\n");
    }
}