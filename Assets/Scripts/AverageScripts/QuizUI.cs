using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private TextMeshProUGUI questionText, scoreText, timerText, questioncountText, correctMessage;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, averageMenuPanel, gameMenuPanel;
    [SerializeField] public  GameObject star0, star1, star2, star3, wrongPanel, correctPanel;
    [SerializeField] private Image questionImage;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private List<Button> options, uiButtons;
    /*[SerializeField] private Color correctCol, wrongCol, normalCol;*/

    private Question question;
    private bool answered;
    private float audioLength;

    public TextMeshProUGUI ScoreText { get { return scoreText; } }

    public TextMeshProUGUI TimerText { get { return timerText; } }
    //public TextMeshProUGUI QuestionNum { get { return questionNum; } }

    public TextMeshProUGUI QuestionCountText { get { return questioncountText; } }

    public TextMeshProUGUI CorrectAnswerMessage { get { return correctMessage; } }

    public GameObject GameOverPanel { get { return gameOverPanel; } }

    public GameObject WrongPanel { get { return wrongPanel; } }

    public GameObject CorrectPanel { get { return correctPanel; } }

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

               /* if (val)
                {
                    btn.image.color = correctCol;
                }
                else
                {
                    btn.image.color = wrongCol;
                }*/
            }
        }

        switch (btn.name)
        {
            case "Days":
                quizManager.StartGame(0);
                averageMenuPanel.SetActive(false);
                gameMenuPanel.SetActive(true);
                break;
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
        }
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
