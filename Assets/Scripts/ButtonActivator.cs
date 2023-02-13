using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    public Button[] buttons;
    public int activeButtonIndex = 0;


    public void ActivateNextButton()
    {
        buttons[activeButtonIndex].interactable = false;
        buttons[activeButtonIndex].gameObject.SetActive(false);
        activeButtonIndex = (activeButtonIndex + 1) % buttons.Length;
        buttons[activeButtonIndex].gameObject.SetActive(true);
        buttons[activeButtonIndex].interactable = true;
    }
}