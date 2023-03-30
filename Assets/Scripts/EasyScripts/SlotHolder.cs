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
    public Button checkButton, resetButton;
    private bool allHoldersFilled = false;
    public bool filled = false;
    public Text EasyScoreText;

    [SerializeField] private GameObject gameoverpanel, EasyConfetti, EasyWalkingPanel;
    //[SerializeField] private GameObject wrongAnswer, correctAnswer;
    [SerializeField] public GameObject star0, star1, star2, star3;

    public GameObject GameOverPanel { get { return gameoverpanel; } }

    //public GameObject MonthPanel1 { get { return monthpanel1; } }
    
    //public GameObject MonthPanel2 { get { return monthpanel2; } }

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
        resetButton.onClick.AddListener(ResetSlots); // Add listener to reset button

    }

    public void ResetSlots()
    {
        checkButton.interactable = false; // Disable check button
        DragAndDrop[] dragObjects = FindObjectsOfType<DragAndDrop>();
        SlotHolder[] slotHolders = FindObjectsOfType<SlotHolder>();

        foreach (DragAndDrop dragObject in dragObjects)
        {
            dragObject.ResetObjects(); // Reset position of drag objects
        }
        score = 0; // Reset score
        EasyScoreText.text = score + "/7"; // Update score text
        star0.SetActive(false); // Deactivate all stars

        // Set all slot holders to not filled
        foreach (SlotHolder slotHolder in slotHolders)
        {
            slotHolder.filled = false;
        }
    }
    private int maxPanels = 2; // total number of panels
    private int currentPanel = 0;
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
                            checkButton.onClick.AddListener(delegate
                            {

                                dragObject.GetComponent<Image>().color = Color.green;

                                score++;
                                EasyScoreText.text = score + "/6";

                                if (score == 7)
                                {
                                    star3.gameObject.SetActive(true);
                                    Invoke("ActivateEasyWalkingPanel", 1f);


                                }
                                else if (score >= 5 && score <= 6)
                                {
                                    star2.gameObject.SetActive(true);
                                    Invoke("EasyActivateGameOverPanel", 1f);
                                }
                                else if (score <= 4 && score != 0)
                                {
                                    star1.gameObject.SetActive(true);
                                    star2.gameObject.SetActive(false);
                                    star3.gameObject.SetActive(false);
                                    Invoke("EasyActivateGameOverPanel", 1f);
                                }
                                else if (score == 0)
                                {
                                    Invoke("EasyActivateGameOverPanel", 1f);
                                    star2.gameObject.SetActive(false);
                                    star3.gameObject.SetActive(false);
                                    star1.gameObject.SetActive(false);
                                    star0.gameObject.SetActive(true);
                                }


                            });

                        }
                        else
                        {
                            // Wait until checkButton is clicked before changing color
                            checkButton.onClick.AddListener(delegate
                            {
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


    /*void ActivateMonthPanel2(){   
        MonthPanel2.gameObject.SetActive(true);
        MonthPanel1.gameObject.SetActive(false);

    }
    void ActivateMonthPanel1()
    {
        MonthPanel2.gameObject.SetActive(false);
        MonthPanel1.gameObject.SetActive(true);
    }*/

    void EasyActivateGameOverPanel(){
        GameOverPanel.gameObject.SetActive(true);
        EasyConfetti.gameObject.SetActive(true);
       // MonthPanel2.gameObject.SetActive(false);
        //MonthPanel1.gameObject.SetActive(false);

    }
    void ActivateEasyWalkingPanel()
    {
        EasyWalkingPanel.gameObject.SetActive(true);
    }

}


