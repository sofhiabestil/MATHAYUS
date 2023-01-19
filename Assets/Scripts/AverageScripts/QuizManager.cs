using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private List<QuizDataScriptable> quizData;
    [SerializeField] private float timeLimit = 30f;

    private List<Question> questions;

    //current question data
    private Question selectedQuestion;

    private int scoreCount = 0;
    private float currentTime;
    private int lifeRemaining = 3;
    private int questionCount = 0;

    private GameStatus gameStatus = GameStatus.Next;

    public GameStatus GameStatus { get { return gameStatus; } }

    // Start is called before the first frame update
    public void StartGame(int index)
    {
        scoreCount = 0;
        currentTime = timeLimit;
        lifeRemaining = 3;
        questions = new List<Question>();
        questionCount = 0;

        for (int i = 0; i < quizData[index].questions.Count; i++)
        {
            //set the questions data
            questions.Add(quizData[index].questions[i]);
        }

        SelectQuestion();
        gameStatus = GameStatus.Playing;
    }

    void ModernFisherYatesShuffle(List<Question> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {

            int j = UnityEngine.Random.Range(0, i + 1);
            Question temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    void SelectQuestion()
    {

        ModernFisherYatesShuffle(questions);
        selectedQuestion = questions[0];

        quizUI.SetQuestion(selectedQuestion);

        questionCount += 1;
        quizUI.QuestionCountText.text = "Q :" + questionCount + "/10";

        questions.RemoveAt(0);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value); //set the time value
        quizUI.TimerText.text = time.ToString("mm':'ss"); //convert time to Time format

        if (currentTime <= 0)
        {
            //Game Over
            gameStatus = GameStatus.Next;
            quizUI.GameOverPanel.SetActive(true);
        }
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;

        if (answered == selectedQuestion.correctAns)
        {
            //Yes
            correctAns = true;
            scoreCount += 1;
            quizUI.ScoreText.text = scoreCount + "/10";
            quizUI.correctPanel.gameObject.SetActive(true);
            /*quizUI.PracticesoundEffect[1].Play();*/

        }
        else
        {
            //No
            //lifeRemaining--;
            //quizUI.ReduceLife(lifeRemaining);
            quizUI.wrongPanel.gameObject.SetActive(true);
            quizUI.CorrectAnswerMessage.text = selectedQuestion.correctAns;

            if (lifeRemaining <= 0)
            {
                gameStatus = GameStatus.Next;
                quizUI.GameOverPanel.SetActive(true);
            }
        }

        if (gameStatus == GameStatus.Playing)
        {
            Invoke("DismissMessagePanel", 0.8f);

            if (questions.Count > 0)
            {
                //call SelectQuestion method again after 1s
                Invoke("SelectQuestion", 0.4f);
            }
            else
            {
                Invoke("ActivateGameOverPanel", 1f);
                gameStatus = GameStatus.Next;
                /*quizUI.GameOverPanel.SetActive(true);*/
            }

        }


        //Test
        if (scoreCount == 10)
        {
            quizUI.star3.gameObject.SetActive(true);
        }
        else if (scoreCount > 4 && scoreCount < 10)
        {
            quizUI.star2.gameObject.SetActive(true);
        }
        else if (scoreCount == 0)
        {
            quizUI.star0.gameObject.SetActive(true);
        }
        else if (scoreCount < 5)
        {
            quizUI.star1.gameObject.SetActive(true);
        }

        //Test

        //return the value of correct bool
        return correctAns;
    }

    void DismissMessagePanel()
    {
        quizUI.correctPanel.gameObject.SetActive(false);
        quizUI.wrongPanel.gameObject.SetActive(false);
    }

    void ActivateGameOverPanel()
    {
        quizUI.GameOverPanel.gameObject.SetActive(true);
    }

}

[System.Serializable]
public class Question
{
    public string questionInfo; //question text
    public QuestionType questionType;  //type
    public Sprite questionImg; //image for Image Type
    public AudioClip questionClip; //audio for audio type
    public UnityEngine.Video.VideoClip questionVideo; //video for video type
    public List<string> options; //options to select
    public string correctAns; //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO,
    AUDIO
}

[System.Serializable]
public enum GameStatus
{
    Next,
    Playing
}
