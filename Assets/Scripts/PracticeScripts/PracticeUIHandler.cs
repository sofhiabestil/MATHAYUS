using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeUIHandler : MonoBehaviour
{

    [SerializeField] private PracticeManager practicemanager;
    [SerializeField] private TextMeshProUGUI QuestionText, HintText, scoreText, questioncountText, correctMessage;
    [SerializeField] private List<Button> options;
    [SerializeField] public List<AudioSource> PracticesoundEffect = new List<AudioSource>();
    [SerializeField] private GameObject gameOverPanel, walkpanel;
    [SerializeField] public GameObject star0, star1, star2, star3, wrongPanel, correctPanel, Paverageconfetti;
    [SerializeField] public List<GameObject> JumpNico = new List<GameObject>();

    private PracticeQuestion question;
    private bool answered;

    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    public TextMeshProUGUI QuestionCountText { get { return questioncountText; } }

    public TextMeshProUGUI CorrectAnswerMessage { get { return correctMessage; } }

    public GameObject GameOverPanel { get { return gameOverPanel; } }

    public GameObject Walkpanel { get { return walkpanel; } }

    public GameObject WrongPanel { get { return wrongPanel; } }

    public GameObject CorrectPanel { get { return correctPanel; } }

    public GameObject PAverageConfetti { get { return Paverageconfetti; } }

    void Awake()
    {

        for (int i = 0; i < options.Count; i++)
        {

            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => Onclick(localBtn));
        }
    }

    public void SetQuestion(PracticeQuestion question)
    {

        this.question = question;
        QuestionText.text = question.QuestionInfo;
        HintText.text = question.Hints;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
            options[i].name = answerList[i];
        }
        answered = false;

    }

    private void Onclick(Button btn)
    {

        if (!answered)
        {

            answered = true;
            bool val = practicemanager.Answer(btn.name);

            int buttonIndex = options.IndexOf(btn);
            string buttonName = btn.name;

            if (buttonIndex == 0)
            {
                JumpNico[4].SetActive(false);
                PracticesoundEffect[3].Play();
                JumpNico[0].SetActive(true);
                Invoke("ResetJumpNico", 1.5f);
            }
            else if (buttonIndex == 1)
            {
                JumpNico[4].SetActive(false);
                PracticesoundEffect[3].Play();
                JumpNico[1].SetActive(true);
                Invoke("ResetJumpNico", 1.5f);
            }
            else if (buttonIndex == 2)
            {
                JumpNico[4].SetActive(false);
                PracticesoundEffect[3].Play();
                JumpNico[2].SetActive(true);
                Invoke("ResetJumpNico", 1.5f);
            }
            else if (buttonIndex == 3)
            {
                JumpNico[4].SetActive(false);
                PracticesoundEffect[3].Play();
                JumpNico[3].SetActive(true);
                Invoke("ResetJumpNico", 1.5f);
            }
        }
    }

    private void ResetJumpNico()
    {
        for (int i = 0; i < 4; i++)
        {
            JumpNico[i].SetActive(false);
        }
        JumpNico[4].SetActive(true);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
