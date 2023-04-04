using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DifficultCelebratePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Confetti, GameOverPanel;
    public AudioSource Congrats, Dsoundwelldone, Dsoundawesome;
    public Button button;
    DifficultManager diffManager;

    void Start()
    {
        button.onClick.AddListener(ActivateObjects);
        diffManager = FindObjectOfType<DifficultManager>(); // find QuizManager in the scene
    }

    void ActivateObjects()
    {
        Confetti.SetActive(true);
        Congrats.Play();
        GameOverPanel.SetActive(true);

        int scorecount = diffManager.diffscoreCount;
        Debug.Log("Easy score: " + scorecount);

        if (scorecount == 10)
        {
            Dsoundawesome.Play();
        }
        else if (scorecount >= 4 && scorecount <= 10)
        {
            Dsoundwelldone.Play();
        }
    }
}
