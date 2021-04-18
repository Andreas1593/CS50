#include <cs50.h>
#include <stdio.h>

int main(void)
{
    // Prompt for start size
    int start;
    do
    {
        start = get_int("Start size: ");
    }
    while (start < 9);
    // Prompt for end size
    int end;
    do
    {
        end = get_int("End size: ");
    }
    while (end < start);
    // Calculate number of years until we reach threshold
    int population = start;
    int y;
    for (y = 0; population < end; y++)
    {
        population += population / 3 - population / 4;
    }
    // Print number of years
    printf("Years: %i\n", y);
}