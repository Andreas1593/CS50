using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoundsSurvived : MonoBehaviour
{

    public Text roundsText;

    // Show survived rounds when game over
    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    // Coroutine to create a counting animation
    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        // Display number of rounds survived slightly after fading in other elements
        yield return new WaitForSeconds(.7f);

        FindObjectOfType<AudioManager>().Play("Counting");

        // Count from 0 to the number of rounds survived
        while (round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();

            // Time between displaying every number while counting
            yield return new WaitForSeconds(.05f);
        }

        FindObjectOfType<AudioManager>().Stop("Counting");

    }

}
