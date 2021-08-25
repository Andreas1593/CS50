using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public SceneFader fader;

    public Button[] levelButtons;

    public GameObject resetUI;

    private void Start()
    {

        // Get the highest level reached so far or 1 by default
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {

            // Disable all level buttons that have a higher index than the highest level reached
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }

        }
    }

    // Called by level buttons
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }

    // Reset game progress
    public void ResetProgress()
    {

        // Enable reset confirmation UI
        resetUI.SetActive(!resetUI.activeSelf);
    }

    public void ConfirmReset()
    {
        PlayerPrefs.DeleteAll();
        resetUI.SetActive(!resetUI.activeSelf);

        Start();

        Debug.Log("Progress deleted");
    }

    public void DenyReset()
    {
        resetUI.SetActive(!resetUI.activeSelf);

        Debug.Log("Progress not deleted");
    }

}
