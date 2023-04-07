using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Endgame_SFX_manager : MonoBehaviour
{
    /**public TextMeshProUGUI toggleMusictxt;

    // Start is called before the first frame update
    void Start()
    {
        if (endgame_soundSFX_script.AudioInstance.Audio.isPlaying)
        {
            toggleMusictxt.text = "OFF";
        }
        else
        {
            toggleMusictxt.text = "ON";
        }
    }


    public void MusicToggle()
    {
        if (endgame_soundSFX_script.AudioInstance.Audio.isPlaying)
        {
            endgame_soundSFX_script.AudioInstance.Audio.Pause();
            toggleMusictxt.text = "ON";
        }
        else
        {
            endgame_soundSFX_script.AudioInstance.Audio.Play();
            toggleMusictxt.text = "OFF";
        }
    }


    //[SerializeField] AudioClip[] sounds;
    public AudioSource sfxSource;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        myAudioSource = GetComponent<AudioSource>();
    }*/
    public static Endgame_SFX_manager Instance;
    public Endgame_Sound[] sfxSound;
    public AudioSource sfxSource;

    public void Awake()
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

   /** private void start()
    {
        PlaySFX("wow");
    }*/

    public void PlaySFX(string name)
    {
        Endgame_Sound s = Array.Find(sfxSound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            //sfxSource.clip = s.clip;
            sfxSource.PlayOneShot(s.clip);
        }
    }

}
