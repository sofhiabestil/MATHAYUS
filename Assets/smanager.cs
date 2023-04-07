using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class smanager : MonoBehaviour
{
    //[SerializeField] Image soundOnIcon;
    //[SerializeField] Image soundOffIcon;

    private Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;
    private bool isOn = true;

    public AudioSource asrc;
    public AudioClip sfx1, sfx2;
    
    //Start is called before the first frame update
    void Start()
    {
        soundOnImage = button.image.sprite;
        //asrc = GetComponent<AudioSource>();
        //asrc.Play();
        Invoke("button1Play", 1.5f);
        /**
        if (PlayerPrefs.Haskey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        asrc.pause = muted;*/
    }

    /**void playAudio()
    {
        asrc.Play()
    }*/

    public void ButtonClicked()
    {
        if (isOn)
        {
            button.image.sprite = soundOffImage;
            isOn = false;
            asrc.mute = true;
        }
        else
        {
            button.image.sprite = soundOnImage;
            isOn = true;
            asrc.mute = false;
        }
    }

    public void button1Play()
    {
        asrc.clip = sfx1;
        asrc.Play();
    }

    public void button2Play()
    {
        asrc.clip = sfx2;
        asrc.Play(1);
    }

    /** public void button1Stop()
     {
         asrc.clip = sfx1;
         asrc.Stop();
        // soundOnIcon.enabled = false;
        //soundOffIcon.enabled = true;
     }*/



    /*
    public void OnButtonPress()
    {
        UpdateButtonIcon();

        if (muted == false)
        {
            muted = true;
            asrc.pause = false;
        }
        else
        {
            muted = false;
            asrc.pause = true;
        }

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if(muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }*/
}
