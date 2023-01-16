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

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

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
 * 
*/
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    /*public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }*/

    

}
