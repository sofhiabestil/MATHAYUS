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
        practicehandler.QuestionCountText.text = "Q :" + questionCount + "/10";

        questions.RemoveAt(0);
    }

   public bool Answer(string answered){
        bool CorrectAnswer = false;

        if (answered == SelectedQuestion.CorrectAnswer){

            CorrectAnswer = true;
            scoreCount += 1;

            practicehandler.ScoreText.text = scoreCount + "/10";
            practicehandler.correctPanel.gameObject.SetActive(true); 
            practicehandler.PracticesoundEffect[1].Play();

        }else{

            practicehandler.wrongPanel.gameObject.SetActive(true);
            practicehandler.CorrectAnswerMessage.text = SelectedQuestion.CorrectAnswer;
            practicehandler.PracticesoundEffect[0].Play();

        }
         if (practicegameStatus == PracticeGameStatus.Playing){

            Invoke("DismissMessagePanel", 0.8f);
            
            if (questions.Count > 0 && questionCount < 10){

                Invoke("SelectQuestion", 0.4f);

            }else{
                Invoke("ActivateGameOverPanel", 1f);
                practicegameStatus = PracticeGameStatus.Next;
                
            }

        }

        if (scoreCount == 10)
        {
            practicehandler.star3.gameObject.SetActive(true);
        }
        else if (scoreCount > 4 && scoreCount < 10)
        {
            practicehandler.star2.gameObject.SetActive(true);
        }
        else if (scoreCount == 0)
        {
            practicehandler.star0.gameObject.SetActive(true);
        }
        else if (scoreCount < 5)
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