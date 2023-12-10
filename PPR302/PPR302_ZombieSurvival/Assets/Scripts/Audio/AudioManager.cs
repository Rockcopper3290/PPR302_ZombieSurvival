using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    private void Start()
    {
        //when I add background music place it here
        //play("background music")
    }

    public void PlayAudioClip (string clipName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == clipName);

        //if audio clip name was not found or was misspellt, return to prevent error
        // and display error message
        if (s == null)
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        s.source.Play();
    }
}

