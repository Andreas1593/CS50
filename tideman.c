#include <cs50.h>
#include <stdio.h>
#include <string.h>

// Max number of candidates
#define MAX 9

// preferences[i][j] is number of voters who prefer i over j
int preferences[MAX][MAX];

// locked[i][j] means i is locked in over j
bool locked[MAX][MAX];

// Each pair has a winner, loser
typedef struct
{
    int winner;
    int loser;
}
pair;

// Array of candidates
string candidates[MAX];
pair pairs[MAX * (MAX - 1) / 2];

int pair_count;
int candidate_count;

// Function prototypes
bool vote(int rank, string name, int ranks[]);
void record_preferences(int ranks[]);
void add_pairs(void);
void sort_pairs(void);
void lock_pairs(void);
void print_winner(void);
bool cycle(int start, int end);

int main(int argc, string argv[])
{
    // Check for invalid usage
    if (argc < 2)
    {
        printf("Usage: tideman [candidate ...]\n");
        return 1;
    }

    // Populate array of candidates
    candidate_count = argc - 1;
    if (candidate_count > MAX)
    {
        printf("Maximum number of candidates is %i\n", MAX);
        return 2;
    }
    for (int i = 0; i < candidate_count; i++)
    {
        candidates[i] = argv[i + 1];
    }

    // Clear graph of locked in pairs
    for (int i = 0; i < candidate_count; i++)
    {
        for (int j = 0; j < candidate_count; j++)
        {
            locked[i][j] = false;
        }
    }

    pair_count = 0;
    int voter_count = get_int("Number of voters: ");

    // Query for votes
    for (int i = 0; i < voter_count; i++)
    {
        // ranks[i] is voter's ith preference
        int ranks[candidate_count];

        // Query for each rank
        for (int j = 0; j < candidate_count; j++)
        {
            string name = get_string("Rank %i: ", j + 1);

            if (!vote(j, name, ranks))
            {
                printf("Invalid vote.\n");
                return 3;
            }
        }

        record_preferences(ranks);

        printf("\n");
    }

    add_pairs();
    sort_pairs();
    lock_pairs();
    print_winner();
    return 0;
}



// Update ranks given a new vote
bool vote(int rank, string name, int ranks[])
{
    // Search for valid candidates and update ranks[]
    for (int i = 0; i < candidate_count; i++)
    {
        if (strcmp(name, candidates[i]) == 0)
        {
            ranks[rank] = i;
            return true;
        }
    }

    return false;
}



// Update preferences given one voter's ranks
void record_preferences(int ranks[])
{
    // Voter's ith preference wins vs each following preference, count 1
    for (int i = 0; i < (candidate_count - 1); i++)
    {
        for (int j = (i + 1); j < candidate_count; j++)
        {
            preferences[ranks[i]][ranks[j]]++;
        }
    }

    return;
}



// Record pairs of candidates where one is preferred over the other
void add_pairs(void)
{

    // Loop all pairs and save them in pairs[] when one is preferred
    for (int i = 0; i < candidate_count; i++)
    {
        for (int j = 0; j < candidate_count; j++)
        {
            if (preferences[i][j] > preferences[j][i])
            {
                pairs[pair_count].winner = i;
                pairs[pair_count].loser = j;
                pair_count++;
            }
        }
    }

    return;
}



// Sort pairs in decreasing order by strength of victory
void sort_pairs(void)
{

    // Temporary variable for swap
    pair temp[MAX];

    // Two loops for bubble sort; strenght = margin between winner - loser

    // Iterate bubble search (pair_count - 1) [means (n - 1)] times
    for (int i = (pair_count - 1); i > 0; i--)
    {

        // Swap (i - 1) times during each iteration
        for (int j = 0; j < i; j++)
        {

            // preferences[pairs[j].winner][pairs[j].loser] is the number of how many voters preferred the winner over the loser
            // preferences[pairs[j].loser][pairs[j].winner] is the number of how many voters preferred the loser over the winner

            if ((preferences[pairs[j].winner][pairs[j].loser] - preferences[pairs[j].loser][pairs[j].winner]) <
                (preferences[pairs[j + 1].winner][pairs[j + 1].loser] - preferences[pairs[j + 1].loser][pairs[j + 1].winner]))
            {

                // Swap technique via temp variable
                temp[1] = pairs[j];
                pairs[j] = pairs[j + 1];
                pairs[j + 1] = temp[1];
            }
        }
    }

    return;
}



// Lock pairs into the candidate graph in order, without creating cycles
void lock_pairs(void)
{
    for (int i = 0; i < pair_count; i++)
    {
        
        // Call the cycle function and lock if it's not going to create a cycle
        if (!cycle(pairs[i].winner, pairs[i].loser))
        {
            locked[pairs[i].winner][pairs[i].loser] = true;
        }
    }
    return;
}



// Print the winner of the election
void print_winner(void)
{

    // The winner is the one with no arrows pointing at him (source), means the winner must be on the right side of the pairs array
    // and all fields must be false when iterating through every sender
    for (int i = 0; i < pair_count; i++)
    {
        
        // Count the number of fields with false value per coloumn
        int falses = 0;
        
        for (int j = 0; j < pair_count; j++)
        {
            if (locked[pairs[j].winner][pairs[i].loser] == false)
            {
                falses++;
                
                // If the whole column is false then it's the source
                if (falses == pair_count)
                {
                    printf("%s\n", candidates[i]);
                    break;
                }
                
            }
        }
    }
    return;
}



// Check for cycles in the candidate graph; start = winner, end = loser
bool cycle(int start, int end)
{
    
    // If the start and end of the chain is identical it's a loop (base case)
    if (start == end)
    {
        return true;
    }
    
    for (int i = 0; i < candidate_count; i++)
    {
        
        // If the loser's pointing at a candidate, there's potential for a cycle
        if (locked[end][i])
        {
            
            // Recursion (start remains the same start of the cycle, i becomes the next end)
            if (cycle(start, i))
            {
                return true;
            }
        }
    }
    return false;
}