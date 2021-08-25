using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{

    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    public Text level;

    // Update is called once per frame
    void Update()
    {

        // Pause game with "Esc" oder "P"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {

        // Don't toggle pause menu when game is over
        if (GameManager.GameIsOver)
            return;

        // Toggle pause menu
        ui.SetActive(!ui.activeSelf);

        // Freeze / unfreeze game
        if (ui.activeSelf)
        {
            // Hint: Changing Time.fixedDeltaTime not required when changing the timeScale to 0
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        // Display current level
        Scene scene = SceneManager.GetActiveScene();
        string levelIndex = scene.name.Substring(5, 2);
        // Parse current level index to an integer
        int levelIndexInt = Int32.Parse(levelIndex);
        level.text = "Level " + levelIndexInt;

    }

    public void Retry()
    {

        // Toggle pause menu and reload current scene
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }

}
