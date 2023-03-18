using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PSlotHolder : MonoBehaviour, IDropHandler
{
    public int id;
    public int Pscore;
    public Button PcheckButton;
    private bool allHoldersFilled = false;
    public bool filled = false;
    public Text EasyScoreText;



    [SerializeField] private GameObject pgameoverpanel, walkingpanel;

    public GameObject WalkingPanel { get { return walkingpanel; } }

    public GameObject PGameOverPanel { get { return pgameoverpanel; } }


    public void OnDrop(PointerEventData eventData)
    {
       
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<PDragAndDrop>().id == id)
            {
                Debug.Log("Correct");
                eventData.pointerDrag.GetComponent<PDragAndDrop>().PSetScore(Pscore);
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
        PcheckButton.interactable = false;
    }
    public void checkAnswer()
    {
        PDragAndDrop[] dragObjects = FindObjectsOfType<PDragAndDrop>();
        PSlotHolder[] slotHolders = FindObjectsOfType<PSlotHolder>();
        allHoldersFilled = true;
        bool emptySlotExists = false;

        foreach (PSlotHolder slotHolder in slotHolders)
        {
            if (!slotHolder.filled)
            {
                allHoldersFilled = false;
                emptySlotExists = true;
                break;
            }
        }

        if (allHoldersFilled)
        {
            foreach (PDragAndDrop dragObject in dragObjects)
            {
                RectTransform dragObjectRect = dragObject.GetComponent<RectTransform>();
                foreach (PSlotHolder slotHolder in slotHolders)
                {
                    if (dragObjectRect.anchoredPosition == slotHolder.GetComponent<RectTransform>().anchoredPosition)
                    {
                        if (dragObject.id == slotHolder.id)
                        {
                            // Wait until checkButton is clicked before changing color
                            PcheckButton.onClick.AddListener(delegate
                            {

                                dragObject.GetComponent<Image>().color = Color.green;

                                Pscore++;
                                EasyScoreText.text = Pscore + "/7";

                                if (Pscore > 4)
                                {
                                    Invoke("ActivateWalkingPanel", 1f);
                                    Debug.Log("Activate Walk");
                                }
                            });

                        }
                        else
                        {
                            // Wait until checkButton is clicked before changing color
                            PcheckButton.onClick.AddListener(delegate
                            {
                                dragObject.GetComponent<Image>().color = Color.red;
                                PcheckButton.onClick.RemoveAllListeners();

                                if (Pscore < 3)
                                {
                                    Invoke("ActivateGameOverPanel", 1f);
                                    Debug.Log("Activate Game Over");
                                }
                            });
                        }
                    }
                }
            }
            PcheckButton.interactable = true;
        }
        else
        {
            PcheckButton.interactable = false;
            if (emptySlotExists) Debug.Log("Not all holders are filled");
        }
    }

    void ActivateGameOverPanel(){
        PGameOverPanel.gameObject.SetActive(true);

    }

    void ActivateWalkingPanel(){
        WalkingPanel.gameObject.SetActive(true);

    }
}


