#include "helpers.h"
#include <math.h>

// Convert image to grayscale
void grayscale(int height, int width, RGBTRIPLE image[height][width])
{
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            // Cast BYTES to float in order to round the result
            image[i][j].rgbtBlue = round(((float) image[i][j].rgbtBlue + (float) image[i][j].rgbtGreen + (float) image[i][j].rgbtRed) / 3);
            image[i][j].rgbtGreen = image[i][j].rgbtBlue;
            image[i][j].rgbtRed = image[i][j].rgbtBlue;
        }
    }
    return;
}


// Reflect image horizontally
void reflect(int height, int width, RGBTRIPLE image[height][width])
{
    BYTE buffer = 0;
    for (int i = 0; i < height; i++)
    {

        // Swap the outermost pixels until reaching the middle
        for (int j = 0; j < (width / 2); j++)
        {
            {
                // Last column is [width - 1]
                buffer = image[i][j].rgbtBlue;
                image[i][j].rgbtBlue = image[i][(width - 1) - j].rgbtBlue;
                image[i][(width - 1) - j].rgbtBlue = buffer;

                buffer = image[i][j].rgbtGreen;
                image[i][j].rgbtGreen = image[i][(width - 1) - j].rgbtGreen;
                image[i][(width - 1) - j].rgbtGreen = buffer;

                buffer = image[i][j].rgbtRed;
                image[i][j].rgbtRed = image[i][(width - 1) - j].rgbtRed;
                image[i][(width - 1) - j].rgbtRed = buffer;
            }
        }
    }
    return;
}


// Blur image
void blur(int height, int width, RGBTRIPLE image[height][width])
{
    RGBTRIPLE temp[height][width];

    // Loop twice to iterative through every pixel
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            // Total value of all pixel in the 3x3 grid
            int totalb = 0;
            int totalg = 0;
            int totalr = 0;
            // Number of pixels of the 3x3 which are inside the image
            BYTE pinside = 0;

            // Loop twice to iterative through the 3x3 grid of every pixel; k = height, l = width
            // Reference point is the pixel at height i width j
            for (int k = (i - 1); k <= (i + 1); k++)
            {
                for (int l = (j - 1); l <= (j + 1); l++)
                {

                    // If the pixel's not outside the image
                    if (!((k < 0) || (k >= height) || (l < 0) || (l >= width)))
                    {

                        // Add the surrounding pixel's value and count the number of pixels not outside the image
                        totalb += image[k][l].rgbtBlue;
                        totalg += image[k][l].rgbtGreen;
                        totalr += image[k][l].rgbtRed;
                        pinside++;
                    }
                }
            }

            // Store rounded values in temp image
            temp[i][j].rgbtBlue = round((float) totalb / (float) pinside);
            temp[i][j].rgbtGreen = round((float) totalg / (float) pinside);
            temp[i][j].rgbtRed = round((float) totalr / (float) pinside);
        }
    }

    // Copy temp image to actual image
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            image[i][j].rgbtBlue = temp[i][j].rgbtBlue;
            image[i][j].rgbtGreen = temp[i][j].rgbtGreen;
            image[i][j].rgbtRed = temp[i][j].rgbtRed;
        }
    }

    return;
}


// Detect edges
void edges(int height, int width, RGBTRIPLE image[height][width])
{
    RGBTRIPLE temp[height][width];

    // Loop twice to iterative through every pixel
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            // Gx / Gy for all colour channels
            float Gxb = 0, Gxg = 0, Gxr = 0;
            float Gyb = 0, Gyg = 0, Gyr = 0;

            // Loop twice to iterative through the 3x3 grid of every pixel; k = height, l = width
            // Reference point is the pixel at height i width j
            for (int k = (i - 1); k <= (i + 1); k++)
            {
                for (int l = (j - 1); l <= (j + 1); l++)
                {

                    // If the pixel's not outside the image
                    if (!((k < 0) || (k >= height) || (l < 0) || (l >= width)))
                    {

                        // Pixel top left of 3x3 grid
                        if (k == (i - 1) && l == (j - 1))
                        {
                            Gxb -= image[k][l].rgbtBlue;
                            Gxg -= image[k][l].rgbtGreen;
                            Gxr -= image[k][l].rgbtRed;

                            Gyb -= image[k][l].rgbtBlue;
                            Gyg -= image[k][l].rgbtGreen;
                            Gyr -= image[k][l].rgbtRed;
                        }

                        // Pixel top middle of 3x3 grid
                        if (k == (i - 1) && l == j)
                        {
                            Gyb -= 2 * image[k][l].rgbtBlue;
                            Gyg -= 2 * image[k][l].rgbtGreen;
                            Gyr -= 2 * image[k][l].rgbtRed;
                        }

                        // Pixel top right of 3x3 grid
                        if (k == (i - 1) && l == (j + 1))
                        {
                            Gxb += image[k][l].rgbtBlue;
                            Gxg += image[k][l].rgbtGreen;
                            Gxr += image[k][l].rgbtRed;

                            Gyb -= image[k][l].rgbtBlue;
                            Gyg -= image[k][l].rgbtGreen;
                            Gyr -= image[k][l].rgbtRed;
                        }

                        // Pixel middle left of 3x3 grid
                        if (k == i && l == (j - 1))
                        {
                            Gxb -= 2 * image[k][l].rgbtBlue;
                            Gxg -= 2 * image[k][l].rgbtGreen;
                            Gxr -= 2 * image[k][l].rgbtRed;
                        }

                        // Pixel middle right of 3x3 grid
                        if (k == i && l == (j + 1))
                        {
                            Gxb += 2 * image[k][l].rgbtBlue;
                            Gxg += 2 * image[k][l].rgbtGreen;
                            Gxr += 2 * image[k][l].rgbtRed;
                        }

                        // Pixel bottom left of 3x3 grid
                        if (k == (i + 1) && l == (j - 1))
                        {
                            Gxb -= image[k][l].rgbtBlue;
                            Gxg -= image[k][l].rgbtGreen;
                            Gxr -= image[k][l].rgbtRed;

                            Gyb += image[k][l].rgbtBlue;
                            Gyg += image[k][l].rgbtGreen;
                            Gyr += image[k][l].rgbtRed;
                        }

                        // Pixel bottom middle of 3x3 grid
                        if (k == (i + 1) && l == j)
                        {
                            Gyb += 2 * image[k][l].rgbtBlue;
                            Gyg += 2 * image[k][l].rgbtGreen;
                            Gyr += 2 * image[k][l].rgbtRed;
                        }

                        // Pixel bottom right of 3x3 grid
                        if (k == (i + 1) && l == (j + 1))
                        {
                            Gxb += image[k][l].rgbtBlue;
                            Gxg += image[k][l].rgbtGreen;
                            Gxr += image[k][l].rgbtRed;

                            Gyb += image[k][l].rgbtBlue;
                            Gyg += image[k][l].rgbtGreen;
                            Gyr += image[k][l].rgbtRed;
                        }
                    }
                }
            }

            // Store the new pixel's values in temp, but cap at 255
            if (round(sqrt(pow(Gxb, 2) + pow(Gyb, 2))) > 255)
            {
                temp[i][j].rgbtBlue = 255;
            }
            else
            {
                temp[i][j].rgbtBlue = round(sqrt(pow(Gxb, 2) + pow(Gyb, 2)));
            }

            if (round(sqrt(pow(Gxg, 2) + pow(Gyg, 2))) > 255)
            {
                temp[i][j].rgbtGreen = 255;
            }
            else
            {
                temp[i][j].rgbtGreen = round(sqrt(pow(Gxg, 2) + pow(Gyg, 2)));
            }

            if (round(sqrt(pow(Gxr, 2) + pow(Gyr, 2))) > 255)
            {
                temp[i][j].rgbtRed = 255;
            }
            else
            {
                temp[i][j].rgbtRed = round(sqrt(pow(Gxr, 2) + pow(Gyr, 2)));
            }
        }
    }
    
    // Copy temp image to actual image
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            image[i][j].rgbtBlue = temp[i][j].rgbtBlue;
            image[i][j].rgbtGreen = temp[i][j].rgbtGreen;
            image[i][j].rgbtRed = temp[i][j].rgbtRed;
        }
    }

    return;
}
