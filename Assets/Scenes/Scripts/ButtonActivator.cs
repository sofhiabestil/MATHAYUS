using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonActivator : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] texts;
    public int activeButtonIndex = 0;
    public int activeTextIndex = 0;

    public void ActivateNextButton()
    {
        buttons[activeButtonIndex].interactable = false;
        buttons[activeButtonIndex].gameObject.SetActive(false);
        activeButtonIndex = (activeButtonIndex + 1) % buttons.Length;
        buttons[activeButtonIndex].gameObject.SetActive(true);
        buttons[activeButtonIndex].interactable = true;
    }

    public void ActivateNextText()
    {
        texts[activeTextIndex].gameObject.SetActive(false);
        activeTextIndex = (activeTextIndex + 1) % texts.Length;
        texts[activeTextIndex].gameObject.SetActive(true);
    }
}