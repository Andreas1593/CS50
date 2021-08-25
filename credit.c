#include <stdio.h>
#include <cs50.h>
#include <math.h>

int main(void)
{

    // Checksum for credit card numbers; ask user for credit card number

    int cs = 0;
    long a = get_long("Number: ");

    // Get every other digit from right to left via 10^i and multiply by 2; add every single, digit not number

    for (int i = 1; i <= 16; i += 2)
    {
        int number = (((a / (long int) pow(10, i)) % 10) * 2);

        // If the number has two digits, add 1 + second digit (because max. 9 * 2 = 18); otherwise just add the number

        if (number >= 10)
        {
            cs += (number % 10) + 1;
        }
        else
        {
            cs += number;
        }
    }

    // Add sum of the digits that weren't multiplied

    for (int i = 0; i <= 16; i += 2)
    {
        int number = (a / (long int) pow(10, i)) % 10;
        cs += number;
    }

    // Valid credit card number if checksum's last digit is 0; then check which card exactly

    if (cs % 10 == 0)
    {

        // 15 digits, starts with 34 or 37

        if ((a / (long int) pow(10, 13) == 34) || (a / (long int) pow(10, 13) == 37))
        {
            printf("AMEX\n");
        }

        // 16 digits, starts with 51, 52, 53, 54 or 55

        else if (a >= 5100000000000000 && a <= 5599999999999999)
        {
            printf("MASTERCARD\n");
        }

        // 13 or 16 digits, starts with 4

        else if ((a / (long int) pow(10, 12) == 4) || (a / (long int) pow(10, 15) == 4))
        {
            printf("VISA\n");
        }

        // Invalid if it's none of those

        else
        {
            printf("INVALID\n");
        }
    }

    // Invalid in any other case

    else
    {
        printf("INVALID\n");
    }
}