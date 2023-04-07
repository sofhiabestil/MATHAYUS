using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ave_sound_manager : MonoBehaviour
{
    private Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;
    private bool isOn = true;

    public AudioSource asrc;
    public AudioClip sfx1;

    //Start is called before the first frame update
    void Start()
    {
        soundOnImage = button.image.sprite;
        //asrc = GetComponent<AudioSource>();
        //asrc.Play();
        //Invoke("button1Play", 1.5f);
    }

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
        asrc.Play(1);
    }
}
