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
    public Text EasyScoreText;

    [SerializeField] private GameObject monthpanel1, monthpanel2, gameoverpanel;

    public GameObject GameOverPanel { get { return gameoverpanel; } }

    public GameObject MonthPanel1 { get { return monthpanel1; } }
    
    public GameObject MonthPanel2 { get { return monthpanel2; } }

    //public GameObject WeekPanel { get { return weekpanel; } }


    public void OnDrop(PointerEventData eventData)
    {
       
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                Debug.Log("Correct");
                eventData.pointerDrag.GetComponent<DragAndDrop>().SetScore(score);
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
        bool emptySlotExists = false;

        foreach (SlotHolder slotHolder in slotHolders)
        {
            if (!slotHolder.filled)
            {
                allHoldersFilled = false;
                emptySlotExists = true;
                break;
            }
        }
        
        int currentPanel = 0; // variable to keep track of current panel
        int maxPanels = 3; // total number of panels

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
                            checkButton.onClick.AddListener(delegate {
                                
                                dragObject.GetComponent<Image>().color = Color.green;
                                
                                score++;
                                EasyScoreText.text = score + "/10";

                                if (currentPanel == 0)
                                {
                                    Invoke("ActivateMonthPanel2", 1f);
                                }
                                else if (currentPanel == 1)
                                {
                                    Invoke("ActivateMonthPanel1", 1f);
                                }
                                else if (currentPanel == 2)
                                {
                                    Invoke("EasyActivateGameOverPanel", 1f);
                                }

                                currentPanel++;

                                // Check if current panel is greater than max panels
                                if (currentPanel > maxPanels)
                                {
                                    currentPanel = maxPanels; // Set current panel to max panels
                                }

                            });
                        }
                        else
                        {
                            // Wait until checkButton is clicked before changing color
                            checkButton.onClick.AddListener(delegate {
                                dragObject.GetComponent<Image>().color = Color.red;
                                checkButton.onClick.RemoveAllListeners();
                            });
                        }
                    }
                }
            }
            checkButton.interactable = true;
        }
        else
        {
            checkButton.interactable = false;
            if (emptySlotExists) Debug.Log("Not all holders are filled");
        }

    }

    void ActivateMonthPanel2(){   
        MonthPanel2.gameObject.SetActive(true);
        MonthPanel1.gameObject.SetActive(false);

    }
    void ActivateMonthPanel1(){
        MonthPanel1.gameObject.SetActive(false);

    }

    void EasyActivateGameOverPanel(){
        GameOverPanel.gameObject.SetActive(true);

    }
}


