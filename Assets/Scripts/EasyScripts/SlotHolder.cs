using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotHolder : MonoBehaviour, IDropHandler
{
    public int id;
    public int score;
    public Button checkButton;
    private bool allHoldersFilled = false;
    public bool filled = false;
 
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                Debug.Log("Correct");
                
                score ++;
                //EasyScoreText.text = score + "/10";
            }
            else
            {
                Debug.Log("Wrong");
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            filled = true;
        }
        checkAnswer();
    }

    void Start()
    {
        checkButton.interactable = false;
    }

    public void checkAnswer()
    {
        DragAndDrop[] dragObjects = FindObjectsOfType<DragAndDrop>();
        SlotHolder[] slotHolders = FindObjectsOfType<SlotHolder>();
        allHoldersFilled = true;

        foreach (SlotHolder slotHolder in slotHolders)
        {
            if (!slotHolder.filled)
            {
                allHoldersFilled = false;
                break;
            }
        }

        if (allHoldersFilled)
        {
            foreach (DragAndDrop dragObject in dragObjects)
            {
                RectTransform dragObjectRect = dragObject.GetComponent<RectTransform>();
                foreach (SlotHolder slotHolder in slotHolders)
                {
                    if (dragObjectRect.anchoredPosition == slotHolder.GetComponent<RectTransform>().anchoredPosition)
                    {
                        if (dragObject.id == slotHolder.id)
                        {
                            // Wait until checkButton is clicked before changing color
                            checkButton.onClick.AddListener(delegate { dragObject.GetComponent<Image>().color = Color.green; });
                        }
                        else
                        {
                            // Wait until checkButton is clicked before changing color
                            checkButton.onClick.AddListener(delegate { dragObject.GetComponent<Image>().color = Color.red; });
                        }
                    }
                }
            }
            checkButton.interactable = true;
        }
        else
        {
            checkButton.interactable = false;
            Debug.Log("Not all holders are filled");
        }
    }
}


