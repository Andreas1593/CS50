using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{

    public Image img;
    public AnimationCurve curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Fade out and load next scene
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    // Coroutine
    IEnumerator FadeIn()
    {

        // Variable for time
        float t = 1f;

        while (t > 0f)
        {
            // Decrease time variable every frame; completely fades in 1 second
            t -= Time.deltaTime;

            // Alpha value for the animation curve
            float a = curve.Evaluate(t);

            // Alpha can't be changed alone, only the whole color
            img.color = new Color(0f, 0f, 0f, a);

            // Wait one frame and continue
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {

        // Variable for time
        float t = 0f;

        while (t < 1f)
        {
            // Increase time variable every frame; completely fades in 1 second
            t += Time.deltaTime;

            // Alpha value for the animation curve
            float a = curve.Evaluate(t);

            // Alpha can't be changed alone, only the whole color
            img.color = new Color(0f, 0f, 0f, a);

            // Wait one frame and continue
            yield return 0;
        }

        // Load new scene
        SceneManager.LoadScene(scene);

        // Reset the wave spawner
        WaveSpawner.EnemiesAlive = 0;
        WaveSpawnerInfinite.EnemiesAlive = 0;
    }

}
