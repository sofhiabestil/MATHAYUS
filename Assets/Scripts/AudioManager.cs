using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //method for easily access it from anywhere
    public static AudioManager Instance;


    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //method to play the background music when it open.
    private void Start()
    {
        PlayMusic("Theme");
    }

    //method to play music
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    /***public void PlaySFX(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }*/

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    /*public void ToggleSFX()
    {
        musicSource.mute = !sfxSource.mute;
    }*/

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}