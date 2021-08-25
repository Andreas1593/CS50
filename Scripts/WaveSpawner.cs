using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
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

        // If all waves passed (level won), display complete level UI and disable wave spawner
        if (waveIndex == waves.Length && GameManager.GameIsOver == false)
        {
            gameManager.WinLevel();
            this.enabled = false;
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
    }

    // Use coroutine to pause between each enemy being spawned
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        // Get current wave to be spawned
        Wave wave = waves[waveIndex];

        // Set enemies alive to the amount of enemies that are going to be spawned
        EnemiesAlive = wave.count;

        // Increment number of enemies per wave
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
