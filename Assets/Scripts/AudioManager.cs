/*
 * AudioManager Script 
 * - is for playing the Background music once the game open
 * - 
 * @Sofhia Bestil
*/

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


// Muting the Music & Sound Icon button 

    public void ToggleMusic()
    {
        //musicSource is mute(boolean) equals musicSource is not mute
        musicSource.mute = !musicSource.mute;
    }

    // Muting the Music Icon button
    /*public void ToggleSFX()
    {
        //sfxSource is mute(boolean) equals sfxSource is not mute
        sfxSource.mute = !sfxSource.mute;
    }*/

// Method for Volume Slider
/* 
* The function for music slider
* - it get the value of music slider that inside of the Setting Menu
*/
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }


}