/**
 * UIContoller script 
 * - is to access the audio manager script from this panel.
 * - This is where the function for Music and Sound Slider.
 * 
 * @Sofhia Bestil
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    //public AudioMixer mixer;
    //private float value;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

   /* public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }*/


/* 
 * This method is for cotrolling the music and sound slider
*/

    /*public void Start()
    {
        mixer.GetFloat("volume", value);
        _musicSlider.value = value;
    }*/
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

  /*  public void MusicVolume()
    {
        // volume is the name of the expose parameter in AudioMixer
        mixer.SetFloat("volume", _musicSlider.value);
        //using _musicSlider.value we can use this value to change the volume of the mixer.
    }*/

    /*public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }*/

    

}
