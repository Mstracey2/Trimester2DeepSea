using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Set up here to play sounds on start
    void Awake()
    {
        // Make a new audiosource for each sound in the sounds array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            // Is it muted or not?
            if (PlayerPrefs.GetInt("VolumeMuted") == 1)
            {
                s.source.volume = 0;
            }
            else
            {
                s.source.volume = s.volume * PlayerPrefs.GetFloat("VolumeValue");
            }

            s.source.loop = s.loop;
        }
    }

    // Find a referenced sound and play it
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
