using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    public void Continue()
    {

        // If the player hasn't unlocked the next level yet...
        if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
        {
            // ...save the highest reached level in 'levelReached' in the Player preferences
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }

        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {

        // If the player hasn't unlocked the next level yet...
        if (PlayerPrefs.GetInt("levelReached") < levelToUnlock)
        {
            // ...save the highest reached level in 'levelReached' in the Player preferences
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }

        sceneFader.FadeTo(menuSceneName);
    }

}
