using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AverageCelebrationPanel : MonoBehaviour
{
    public GameObject Confetti, GameOverPanel;
    public AudioSource Congrats, Asoundwelldone, Asoundawesome;
    public Button button;
    QuizManager quizManager;

    void Start()
    {
        button.onClick.AddListener(ActivateObjects);
        quizManager = FindObjectOfType<QuizManager>(); // find QuizManager in the scene
    }

    void ActivateObjects()
    {
        Confetti.SetActive(true);
        Congrats.Play();
        GameOverPanel.SetActive(true);

        int scorecount = quizManager.scoreCount;
        Debug.Log("Easy score: " + scorecount);

        if (scorecount == 10)
        {
            Asoundawesome.Play();
        }
        else if (scorecount >= 4 && scorecount <= 10)
        {
            Asoundwelldone.Play();
        }
    }
}
