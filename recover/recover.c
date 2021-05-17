#include <stdio.h>
#include <stdlib.h>
#include <cs50.h>
#include <stdint.h>

int main(int argc, char *argv[])
{
    // Number of found JPEGs
    int jpgs = 0;
    // Buffer for byte blocks
    uint8_t buffer[512];
    // Memory location for JPEGs
    FILE *image = NULL;
    string filename = malloc(8);

    // Check for invalid input
    if (argc != 2)
    {
        printf("Usage: ./recover image\n");
        return 1;
    }
    
    // Open forensic image
    FILE *f = fopen(argv[1], "r");

    // While not having reached the end of the file
    // Read a 512 byte block into the buffer
    while (fread(&buffer, 512, 1, f) == 1)
    {
        
        // If the block is the start of a new JPEG
        if (buffer[0] == 0xff && buffer[1] == 0xd8 && buffer[2] == 0xff && (buffer[3] & 0xf0) == 0xe0)
        {
            
            // If it's the first JPEG
            if (jpgs == 0)
            {
                image = fopen("000.jpg", "w");
                fwrite(&buffer, 512, 1, image);
                jpgs++;
            }
            
            // If it isn't the first JPEG
            else
            {
                fclose(image);
                sprintf(filename, "%03i.jpg", jpgs);
                
                // Open a file for each following JPEG
                image = fopen(filename, "w");
                fwrite(&buffer, 512, 1, image);
                jpgs++;
            }
        }
        
        // If the block isn't the start of a new JPEG
        else
        {
            // If already found a JPEG
            if (jpgs > 0)
            {
                fwrite(&buffer, 512, 1, image);
            }
        }
    }
    fclose(f);
    fclose(image);
    free(filename);
}