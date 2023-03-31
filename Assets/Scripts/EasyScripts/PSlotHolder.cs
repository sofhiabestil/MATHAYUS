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
    public Button PresetButton;
    private bool allHoldersFilled = false;
    public bool filled = false;
    public Text EasyScoreText;
    public Vector2 initialPosition;
    public AudioSource PracticeCongrats;


    [SerializeField] private GameObject pgameoverpanel, walkingpanel, PeasyConfetti;
    [SerializeField] public GameObject star0, star1, star2, star3;

    public GameObject WalkingPanel { get { return walkingpanel; } }

    public GameObject PGameOverPanel { get { return pgameoverpanel; } }

    public Text TextScore { get { return EasyScoreText; } }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<PDragAndDrop>().id == id)
            {
                Debug.Log("Correct");
                // eventData.pointerDrag.GetComponent<PDragAndDrop>().PSetScore(Pscore);
            }
            else
            {
                Debug.Log("Wrong");
            }
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            filled = true;
        }

        // Check if all holders are filled
        PSlotHolder[] slotHolders = FindObjectsOfType<PSlotHolder>();
        bool allHoldersFilled = true;
        foreach (PSlotHolder slotHolder in slotHolders)
        {
            if (!slotHolder.filled)
            {
                allHoldersFilled = false;
                Pscore = 0;
                break;
            }
        }

        if (allHoldersFilled)
        {
            checkAnswer();
        }
    }

    void Start()
    {
        PcheckButton.interactable = false;
        PresetButton.onClick.AddListener(ResetSlots); // Add listener to reset button

    }
    public void ResetSlots()
    {
        PcheckButton.interactable = false; // Disable check button
        PDragAndDrop[] dragObjects = FindObjectsOfType<PDragAndDrop>();
        PSlotHolder[] slotHolders = FindObjectsOfType<PSlotHolder>();

        foreach (PDragAndDrop dragObject in dragObjects)
        {
            dragObject.ResetObjects(); // Reset position of drag objects
        }
        Pscore = 0; // Reset score
        TextScore.text = Pscore + "/7"; // Update score text
        star0.SetActive(false); // Deactivate all stars

        // Set all slot holders to not filled
        foreach (PSlotHolder slotHolder in slotHolders)
        {
            slotHolder.filled = false;
            Pscore = 0; // Reset score
            TextScore.text = Pscore + "/7"; // Update score text
        }
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

                                if (Pscore == 7)
                                {
                                    star3.gameObject.SetActive(true);
                                    Invoke("ActivateWalkingPanel", 1f);


                                }
                                else if (Pscore >= 5 && Pscore <= 6)
                                {
                                    star2.gameObject.SetActive(true);
                                    Invoke("ActivateGameOverPanel", 1f);
                                }
                                else if (Pscore <= 4 && Pscore != 0)
                                {
                                    star1.gameObject.SetActive(true);
                                    star2.gameObject.SetActive(false);
                                    star3.gameObject.SetActive(false);
                                    Invoke("ActivateGameOverPanel", 1f);
                                }
                                else if (Pscore == 0)
                                {
                                    Invoke("ActivateGameOverPanel", 1f);
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
                            PcheckButton.onClick.AddListener(delegate
                            {
                                dragObject.GetComponent<Image>().color = Color.red;
                                PcheckButton.onClick.RemoveAllListeners();
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

    void ActivateGameOverPanel()
    {
        PGameOverPanel.gameObject.SetActive(true);
        WalkingPanel.gameObject.SetActive(false);
        PracticeCongrats.Play();
        PeasyConfetti.gameObject.SetActive(true);
    }

    void ActivateWalkingPanel()
    {
        WalkingPanel.gameObject.SetActive(true);
        PGameOverPanel.gameObject.SetActive(false);
    }


}



