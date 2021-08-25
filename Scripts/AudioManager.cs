using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Singleton pattern
    public static AudioManager instance;

    void Awake()
    {

        // Create a new audio manager if none exists
        if (instance == null)
        {
            instance = this;
        }
        // If there's already an audio manager, remove the new (second) one
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Loop over each sound in the audio manager
        foreach (Sound s in sounds)
        {

            // Add audio source to the current sound element 's'
            s.source = gameObject.AddComponent<AudioSource>();
            // Add audio clip to the current sound element's source
            s.source.clip = s.clip;

            // Control volume, pitch and loop via game inspector
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }


    public void Play(string name)
    {

        // Find sound in the 'sounds' array (similar to SQL syntax)
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {

        // Find sound in the 'sounds' array (similar to SQL syntax)
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

}
