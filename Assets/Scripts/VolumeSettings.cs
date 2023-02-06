// MATHAYUS 3

/**
 * This class will only be doing saving 
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    //This is the field that need to fill out in UI of Volume settings
    //lagayan nang mixer,
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    // the expose parameter
    public const string MIXER_MUSIC = "Music Volume";
    public const string MIXER_SFX = "Sound Effects Volume";

    // whenever the player changes the value of the slider on value changed is called and we're going to go on value 
    // change.add a listener so add a function to 
    // the function requires float parameter
    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }


    // FOR  able to save these values so the player doesn't have to set them every single time they play and we'll save them between scenes
    /**
     * When you exit the scene or the obj is disabled we'll go ahead and save here we'll go PlayerPrefs
     * Now with values being saved let's go ahead in our audio manager 
     */
    void OnDisable()
    {
        //were saving 
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }


    //set the float of our music channe; and to do that we have to tap on our music channel
    // we have to set the default volume into logarithmic value because the mixer is logarithmic value.
    void SetMusicVolume(float value)
    {
        // multiply this value by 20 so that we're able to reach minus 90 decibels
        //which is zero volume 
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }

    /**
     * Now we want to be able to save these values so the player doesn't have to set them every single time they play
     * and we'll save them between scenes as well so our canvas will be doing the saving and our audio manager will be 
     * loading the channels volumes 
     */

}
