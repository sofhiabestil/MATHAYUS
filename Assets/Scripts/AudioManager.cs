/**
 *  This class will only be doing the loading
 *  
 *  @Sofhia Bestil 02/06/2023
 */

/**
* Now we want to be able to save these values so the player doesn't have to set them every single time they play
* and we'll save them between scenes as well so our canvas will be doing the saving and our audio manager will be 
* loading the channels volumes 
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    // we need key in order to show player props where to save it 
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    // FOR  able to save these values so the player doesn't have to set them every single time they play and we'll save them between scenes
    //Volume saved in VolumeSettings.cs
    void LoadVolume()
    {
        // value 0 to 1
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
}
