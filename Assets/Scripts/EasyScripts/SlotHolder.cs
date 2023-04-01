using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotHolder : MonoBehaviour, IDropHandler
{
    public int id;
    public int scores, TotalScore;
    public Button checkButton, resetButton;
    private bool allHoldersFilled = false;
    public bool filled = false;
    public Text EasyScoreText;
    public AudioSource EasyCongrats;


    [SerializeField] private GameObject gameoverpanel, EasyConfetti, EasyWalkingPanel, panel1,panel2;
    [SerializeField] public GameObject star0, star1, star2, star3, Ekeepitup, Ewelldone, Eawesome;


    private int panel1Score = 0;
    private int panel2Score = 0;
    private bool inPanel1 = true;
    public GameObject GameOverPanel { get { return gameoverpanel; } }

    public void OnDrop(PointerEventData eventData)
    {
       
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                Debug.Log("Correct");
            }
            else
            {
                Debug.Log("Wrong");
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            filled = true;
        }
        CheckAnswer();

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

        if (inPanel1)
        {
            panel1Score = 0; // Reset panel 1 score
            EasyScoreText.text = panel2Score + "/" + "6"; // Update score text
        }
        else
        {
            panel2Score = 0; // Reset panel 2 score
            EasyScoreText.text = panel1Score + "/" + ""; // Update score text
        }

        // Deactivate all stars
        star0.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        // Set all slot holders to not filled
        foreach (SlotHolder slotHolder in slotHolders)
        {
            slotHolder.filled = false;
        }
    }
    private void CheckAnswer()
    {
        if (inPanel1)
        {
            // Check answer for panel 1
            DragAndDrop[] dragObjects = panel1.GetComponentsInChildren<DragAndDrop>();
            SlotHolder[] slotHolders = panel1.GetComponentsInChildren<SlotHolder>();

            bool allHoldersFilled = true;
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
                checkButton.onClick.AddListener(delegate
                {
                    int score = 0;
                    foreach (DragAndDrop dragObject in dragObjects)
                    {
                        RectTransform dragObjectRect = dragObject.GetComponent<RectTransform>();
                        foreach (SlotHolder slotHolder in slotHolders)
                        {
                            if (dragObjectRect.anchoredPosition == slotHolder.GetComponent<RectTransform>().anchoredPosition)
                            {
                                if (dragObject.id == slotHolder.id)
                                {
                                    dragObject.GetComponent<Image>().color = Color.green;
                                    score++;
                                }
                                else
                                {
                                    dragObject.GetComponent<Image>().color = Color.red;
                                }
                            }
                        }
                    }

                    panel1Score = score;
                    EasyScoreText.text = " " + panel1Score;
                    Invoke("ActivatePanel2", 1f);
                    inPanel1 = false;
                    
                    checkButton.onClick.RemoveAllListeners();
                    checkButton.interactable = false;

                });

                checkButton.interactable = true;
            }
            else
            {
                if (emptySlotExists)
                {
                    Debug.Log("Not all holders are filled");
                }
            }
        }
        else
        {
            // Check answer for panel 2
            DragAndDrop[] dragObjects = panel2.GetComponentsInChildren<DragAndDrop>();
            SlotHolder[] slotHolders = panel2.GetComponentsInChildren<SlotHolder>();

            bool allHoldersFilled = true;
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
                checkButton.onClick.AddListener(delegate
                {
                    int score = 0;
                    foreach (DragAndDrop dragObject in dragObjects)
                    {
                        RectTransform dragObjectRect = dragObject.GetComponent<RectTransform>();
                        foreach (SlotHolder slotHolder in slotHolders)
                        {
                            if (dragObjectRect.anchoredPosition == slotHolder.GetComponent<RectTransform>().anchoredPosition)
                            {
                                if (dragObject.id == slotHolder.id)
                                {
                                    dragObject.GetComponent<Image>().color = Color.green;
                                    score++;
                                }
                                else
                                {
                                    dragObject.GetComponent<Image>().color = Color.red;
                                }
                            }
                        }
                    }
                    panel2Score = score;
                    TotalScore = (panel1Score + panel2Score);
                    EasyScoreText.text = TotalScore + "/12";
                    inPanel1 = false;

                    if (TotalScore == 12)
                    {
                        star3.gameObject.SetActive(true);
                        Eawesome.gameObject.SetActive(true);
                        Ewelldone.gameObject.SetActive(false);
                        Ekeepitup.gameObject.SetActive(false);
                        Invoke("ActivateEasyWalkingPanel", 1f);
                    }
                    else if (TotalScore >= 6 && TotalScore <= 11)
                    {
                        star2.gameObject.SetActive(true);
                        Eawesome.gameObject.SetActive(false);
                        Ewelldone.gameObject.SetActive(true);
                        Ekeepitup.gameObject.SetActive(false);
                        Invoke("ActivateEasyWalkingPanel", 1f);
                    }
                    else if (TotalScore <= 5 && TotalScore != 0)
                    {
                        star1.gameObject.SetActive(true);
                        Eawesome.gameObject.SetActive(false);
                        Ewelldone.gameObject.SetActive(false);
                        Ekeepitup.gameObject.SetActive(true);     
                        Invoke("EasyActivateGameOverPanel", 1f);
                    }
                    else if (TotalScore == 0)
                    {
                        star0.gameObject.SetActive(true);
                        Eawesome.gameObject.SetActive(false);
                        Ewelldone.gameObject.SetActive(false);
                        Ekeepitup.gameObject.SetActive(true);
                        Invoke("EasyActivateGameOverPanel", 1f);
                    }


                    checkButton.onClick.RemoveAllListeners();
                    checkButton.interactable = false;
                });

                checkButton.interactable = true;
            }
            else
            {
                if (emptySlotExists)
                {
                    Debug.Log("Not all holders are filled");
                }
                checkButton.interactable = false; // reset check button
            }
        }

  }
        void EasyActivateGameOverPanel(){
        GameOverPanel.gameObject.SetActive(true);
        EasyConfetti.gameObject.SetActive(true);
        EasyCongrats.Play();

    }
    void ActivateEasyWalkingPanel()
    {
        EasyWalkingPanel.gameObject.SetActive(true);
    }

    void ActivatePanel2(){
        panel1.SetActive(false);
        panel2.SetActive(true);
    }


}

/*              
*/
