using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

// Sound class to define audio clips
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    // Variable is public, but won't show
    [HideInInspector]
    public AudioSource source;
}
