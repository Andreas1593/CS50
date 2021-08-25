using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    public void Retry()
    {
        // Restart current level
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);

        // Reset the wave spawner
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawnerInfinite.EnemiesAlive = 0;
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}
