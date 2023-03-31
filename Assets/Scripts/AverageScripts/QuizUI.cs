using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI questionText, scoreText, timerText, questioncountText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, averageMenuPanel, gameMenuPanel, walkpanel;
    [SerializeField] public  GameObject star0, star1, star2, star3, wrongPanel, correctPanel, Averageconfetti, Akeepitup,Awelldone,Aawesome;
    [SerializeField] private Image questionImage;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private List<Button> options, uiButtons;
    [SerializeField] public List<AudioSource> PracticesoundEffect = new List<AudioSource>();
    [SerializeField] public List<GameObject> JumpNico = new List<GameObject>();
    /*[SerializeField] private Color correctCol, wrongCol, normalCol;*/

    private Question question;
    private bool answered;
    private float audioLength;

    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    public TextMeshProUGUI TimerText { get { return timerText; } }
    //public TextMeshProUGUI QuestionNum { get { return questionNum; } }

    public TextMeshProUGUI QuestionCountText { get { return questioncountText; } }

    public GameObject GameOverPanel { get { return gameOverPanel; } }

    public GameObject Walkpanel { get { return walkpanel; } }

    public GameObject WrongPanel { get { return wrongPanel; } }

    public GameObject CorrectPanel { get { return correctPanel; } }

    public GameObject AverageConfetti { get { return Averageconfetti; } }


    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        for (int i = 0; i < uiButtons.Count; i++)
        {
            Button localBtn = uiButtons[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;

        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for (int i = 0; i < options.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
            options[i].name = answerList[i]; //set the name of button
            /*options[i].image.color = normalCol; //set color of button to normal*/
        }

        answered = false;

    }


    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
    }

    private void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.Playing)
        {
            if (!answered)
            {
                answered = true;
                bool val = quizManager.Answer(btn.name);

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



        switch (btn.name)
        {
            case "Skip":
                quizManager.StartGame(0);
                averageMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;
            /*case "Days":
                quizManager.StartGame(0);
                averageMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;*/
            case "Months":
                quizManager.StartGame(1);
                averageMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;
            /*case "Mix":
                quizManager.StartGame(2);
                mainMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;*/
            case "AverageDataQuestion":
                quizManager.StartGame(2);
                averageMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;
           /* case "SkipButton":
                quizManager.StartGame(3);
                averageMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;*/
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

    /*public void ReduceLife(int index)
    {
        lifeImageList[index].color = wrongCol;
    }*/
}
