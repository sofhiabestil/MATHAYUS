using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotHolder : MonoBehaviour, IDropHandler
{
    public int id;
    //public int Easyscores;
    public Button checkButton, resetButton;
    private bool allHoldersFilled = false;
    public bool filled = false;
    public Text EasyScoreText;
    public AudioSource EasyCongrats, Esoundkeepitup, Esoundwelldone, Esoundawesome;


    [SerializeField] private GameObject gameoverpanel, EasyConfetti, EasyWalkingPanel, panel1,panel2;
    [SerializeField] public GameObject star0, star1, star2, star3, Ekeepitup, Ewelldone, Eawesome;

    public GameObject GameOverPanel { get { return gameoverpanel; } }
    //public int easyscores { get { return Easyscores; } }

    public int Easyscores { get; private set; }
    public void SetEasyScore(int easyscore)
    {
        Easyscores = easyscore;
    }
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
        resetButton.onClick.AddListener(ResetSlots); // Add listener to reset butto

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
        Easyscores = 0; // Reset score
        EasyScoreText.text = Easyscores + "/12"; // Update score text
        star0.SetActive(false); // Deactivate all stars

        // Set all slot holders to not filled
        foreach (SlotHolder slotHolder in slotHolders)
        {
            slotHolder.filled = false;
            Easyscores = 0; // Reset score
            EasyScoreText.text = Easyscores + "/7"; // Update score text
        }
    }


    public void CheckAnswer()
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
            checkButton.onClick.AddListener(delegate
            {
                int easyscore = 0;

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
                                easyscore++;
                               
                            }
                            else
                            {
                                dragObject.GetComponent<Image>().color = Color.red;
                            }
                        }
                    }
                }

                foreach (SlotHolder slotHolder in slotHolders)
                {
                    slotHolder.SetEasyScore(easyscore);
                }
                // Update score and stars
                if (easyscore == 12)
                {
                    star3.gameObject.SetActive(true);
                    Eawesome.gameObject.SetActive(true);
                    Ewelldone.gameObject.SetActive(false);
                    Ekeepitup.gameObject.SetActive(false);
                    Invoke("ActivateEasyWalkingPanel", 1f);
                }
                else if (easyscore >= 6 && easyscore <= 11)
                {
                    star2.gameObject.SetActive(true);
                    Eawesome.gameObject.SetActive(false);
                    Ewelldone.gameObject.SetActive(true);
                    Ekeepitup.gameObject.SetActive(false);
                    Invoke("ActivateEasyWalkingPanel", 1f);
                }
                else if (easyscore <= 5 && easyscore != 0)
                {
                    star1.gameObject.SetActive(true);
                    Invoke("E_ActivateKeepitUp", 0.5f);
                    Eawesome.gameObject.SetActive(false);
                    Ewelldone.gameObject.SetActive(false);
                    Ekeepitup.gameObject.SetActive(true);
                    Invoke("EasyActivateGameOverPanel", 1f);
                }
                else if (easyscore == 0)
                {
                    Invoke("EasyActivateGameOverPanel", 1f);
                    star0.gameObject.SetActive(true);
                    Invoke("E_ActivateKeepitUp", 0.5f);
                    Eawesome.gameObject.SetActive(false);
                    Ewelldone.gameObject.SetActive(false);
                    Ekeepitup.gameObject.SetActive(true);
              
                }

            
                EasyScoreText.text = Easyscores + "/12";
                checkButton.onClick.RemoveAllListeners();
                checkButton.interactable = false;
            });

            checkButton.interactable = true;
        }
        else
        {
            checkButton.interactable = false;
            if (emptySlotExists) Debug.Log("Not all holders are filled");
        }
    }


    void EasyActivateGameOverPanel()
    {
        GameOverPanel.gameObject.SetActive(true);
        EasyConfetti.gameObject.SetActive(true);
        EasyCongrats.Play();

        if(Easyscores == 12)
        {
            Esoundawesome.Play();
        }
        else if (Easyscores >= 6 && Easyscores <= 11)
        {
            Esoundwelldone.Play();
        }
    }
    void ActivateEasyWalkingPanel()
    {
        EasyWalkingPanel.gameObject.SetActive(true);
    }

    void ActivatePanel2(){
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    void E_ActivateAwesome()
    {
        Esoundawesome.Play();

    }

    void E_ActivateWelldone()
    {
        Esoundwelldone.Play();

    }

    void E_ActivateKeepitUp()
    {
        Esoundkeepitup.Play();
    }
}


