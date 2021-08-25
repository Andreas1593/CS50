import csv
import sys

def main():

    # Ensure correct usage
    if len(sys.argv) != 3:
        sys.exit("Usage: python dna.py DATABASE.csv DNA.txt")

    database = []

    # Open CSV file with names and their STR counts inside
    with open(sys.argv[1]) as f:
        reader = csv.DictReader(f)

        # Copy each person and their STR counts into 'database' as dict
        for row in reader:
            database.append(row)

    # Open TXT file with DNA sequence inside
    with open(sys.argv[2]) as dna:
        sequence = dna.read()

    # List of the highest STR counts within sequence
    sequence_str = []

    # Compute highest STR counts via functions and save inside sequence_str
    # STR types start at fieldnames[1] since fieldnames[0] is the name
    for i in range(1, len(reader.fieldnames)):
        sequence_str.append(max_continuous_str(sequence, reader.fieldnames[i]))


    # Compare every person's STR counts with the sequence's STR counts
    # Iterate over every person
    for i in range(0, len(database)):

        # Variable to keep track of whether all STR counts are equal
        match = True

        # Iterate over every STR of the person
        for j in range(1, len(database[i])):

            # Convert person's STR counts to an integer!!
            if int(list(database[i].values())[j]) != sequence_str[j - 1]:
                match = False
                
        # Print the person's name if it's a match and stop searching
        if match == True:
            print(database[i]['name'])
            break

    if match == False:
        print("No match")


# Function to return the highest number of continuous STRs within a DNA sequence
def max_continuous_str(sequence, STR):
    
    max_str_counts = []
    
    # Iterate over the sequence one position at a time
    for i in range(0, len(sequence)):
        count = 0
        
        # Iterate over the sequence per STR block
        for j in range(i, len(sequence), len(STR)):
            
            # Count continuous STR sequences and store the amount in max_str_counts finally
            if sequence[j : (j + len(STR))] == STR:
                count += 1
    
            else:
    
                # Save the number of continuous STRs from position i
                max_str_counts.append(count)
                break
                
    # Return the highest value from the list
    max_str = max(max_str_counts)

    return max_str


if __name__ == "__main__":
    main()