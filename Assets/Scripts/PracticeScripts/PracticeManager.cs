using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeManager : MonoBehaviour{
    
    [SerializeField] private PracticeUIHandler practicehandler;
    [SerializeField] private PracticeDataTable practiceData;
    
    private List<PracticeQuestion> questions;
    private PracticeQuestion SelectedQuestion;
    private int scoreCount = 0;
    private int questionCount = 0;

    
    private PracticeGameStatus practicegameStatus = PracticeGameStatus.Next;

    public PracticeGameStatus PracticeGameStatus { get { return practicegameStatus; } }
    

    public void Start(){

        scoreCount = 0;
        questionCount = 0;
        questions = new List<PracticeQuestion>();

       for (int i = 0; i < practiceData.questions.Count; i++){
        
        questions.Add(practiceData.questions[i]);
        
        }
       
        SelectQuestion();
        practicegameStatus = PracticeGameStatus.Playing;

    }

     void ModernFisherYatesShuffle(List<PracticeQuestion> list){
        for (int i = list.Count - 1; i > 0; i--){
        
        int j = UnityEngine.Random.Range(0, i + 1);
        PracticeQuestion temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
    
    }

        void SelectQuestion(){

        ModernFisherYatesShuffle(questions);
        SelectedQuestion = questions[0];

        practicehandler.SetQuestion(SelectedQuestion);

        questionCount += 1;
        practicehandler.QuestionCountText.text = "Q :" + questionCount + "/5";

        questions.RemoveAt(0);
    }

   public bool Answer(string answered){
        bool CorrectAnswer = false;

        if (answered == SelectedQuestion.CorrectAnswer){

            CorrectAnswer = true;
            scoreCount += 1;

            practicehandler.ScoreText.text = scoreCount + "/5";
            Invoke("ActivateCorrectPanel", 1f);

        }
        else{
            Invoke("ActivateWrongPanel", 1f);
            Invoke("CorrectMessage", 1f);
            
        }
        if (practicegameStatus == PracticeGameStatus.Playing){

            Invoke("DismissMessagePanel", 4.0f);

            if (questions.Count > 0 && questionCount < 5){

                Invoke("SelectQuestion", 4f);

            }else{
                if (scoreCount > 4)
                { 
                    Invoke("ActivateWalkPanel", 3f);
                    //Invoke("ActivateGameOverPanel", 2f);
                    
                }
                else
                {
                    Invoke("ActivateGameOverPanel", 3f);
                }
                
                practicegameStatus = PracticeGameStatus.Next;
            }

        }

        if (scoreCount == 5)
        {
            practicehandler.star3.gameObject.SetActive(true);
        }
        else if (scoreCount > 3 && scoreCount < 5)
        {
            practicehandler.star2.gameObject.SetActive(true);
        }
        else if (scoreCount == 0)
        {
            practicehandler.star0.gameObject.SetActive(true);
        }
        else if (scoreCount < 2)
        {
            practicehandler.star1.gameObject.SetActive(true);
        }

        return CorrectAnswer;
    }

    void DismissMessagePanel(){
    practicehandler.correctPanel.gameObject.SetActive(false);
    practicehandler.wrongPanel.gameObject.SetActive(false);

    }

    void ActivateGameOverPanel(){
    practicehandler.GameOverPanel.gameObject.SetActive(true);
    practicehandler.Paverageconfetti.gameObject.SetActive(true);
    practicehandler.PracticesoundEffect[2].Play();

    }


    void ActivateWalkPanel()
    {
        practicehandler.Walkpanel.gameObject.SetActive(true);

    }

    void ActivateCorrectPanel()
    {
        practicehandler.correctPanel.gameObject.SetActive(true);
        practicehandler.PracticesoundEffect[1].Play();
    }

    void ActivateWrongPanel()
    {
        practicehandler.wrongPanel.gameObject.SetActive(true);
        practicehandler.PracticesoundEffect[0].Play();
    }

    void CorrectMessage()
    {
        practicehandler.CorrectAnswerMessage.text = SelectedQuestion.CorrectAnswer;
    }

}

[System.Serializable]
public class PracticeQuestion{
    public string QuestionInfo;
    public List<string> options;
    public string CorrectAnswer;
    public string Hints;
}

[System.Serializable]
public enum PracticeGameStatus
{
    Next,
    Playing
}