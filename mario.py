# Prompt user for height of the pyramid until valid input
while True:
    height = input("Height: ")

    # Check if the input is a number, cast to an integer if so
    if height.isdecimal():
        height = int(height)

        # Check if the number is between 1 and 8, inclusive
        if height < 9 and height > 0:
            break

# Print every row of the pyramid
for i in range(1, height + 1):

    # First half of the row and two whitespaces
    print(" " * (height - i) + "#" * i, end = "  ")

    # Second half of the row without trailing whitespace
    print("#" * i)