using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawnerInfinite : MonoBehaviour
{

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    public int bonusGold = 50;
    private bool bonusReceived = true;


    public float startHealthZombie = 100;
    public float startHealthSkeleton = 250;
    public float startHealthBat = 60;

    public float healthFactor = 1.8f;

    // Reset all enemies' start health at the beginning of the level
    private void Start()
    {
        // Iterate over waves array and access the enemy's startHealth in the 'Enemy' script
        foreach (Wave w in waves)
        {
            GameObject enemy = w.enemy;

            // Check which type of enemy
            if (enemy.name == "Enemy_Zombie_Infinite")
            {
                // Reset start health via 'Enemy' script component
                Enemy e = enemy.GetComponent<Enemy>();
                e.startHealth = startHealthZombie;
            }

            else if (enemy.name == "Enemy_Skeleton_Infinite")
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.startHealth = startHealthSkeleton;
            }

            else if (enemy.name == "Enemy_Bat_Infinite")
            {
                Enemy e = enemy.GetComponent<Enemy>();
                e.startHealth = startHealthBat;
            }

        }

    }

    private void Update()
    {

        // Pause countdown until all enemies died
        if (EnemiesAlive > 0)
        {
            return;
        }

        // Give the player bonus money after every wave
        if (!bonusReceived)
        {
            PlayerStats.Money += bonusGold;
            bonusReceived = true;
        }

        // Spawn wave at 0 and reset countdown; don't exceed 'waves[]' array
        if (countdown <= 0f && waveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        // Count down
        countdown -= Time.deltaTime;

        // Clamp countdown to make sure it won't show negative numbers
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // Format the countdown text
        waveCountdownText.text = string.Format("{0:00.00}", countdown);


        // When all waves have spawned, increase health of all enemys
        if (waveIndex == waves.Length)
        {

            // Iterate over waves array and access the enemy's startHealth in the 'Enemy' script
            foreach (Wave w in waves)
            {
                Enemy enemy = w.enemy.GetComponent<Enemy>();
                enemy.startHealth = enemy.startHealth * healthFactor;
            }

            // Reset wave index
            waveIndex = 0;
        }
    }

    // Use coroutine to pause between each enemy being spawned
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        // Get current wave to be spawned
        Wave wave = waves[waveIndex];

        // Set enemies alive to the amount of enemies that are going to be spawned
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {

            // Spawn the current wave's enemy
            SpawnEnemy(wave.enemy);
            // Wait some time between enemy spawns depending on the wave's spawn rate
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

        bonusReceived = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

}
