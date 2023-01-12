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
    void SelectQuestion(){

        int val = UnityEngine.Random.Range(0, questions.Count);
        SelectedQuestion = questions[val];

        practicehandler.SetQuestion(SelectedQuestion);
        questionCount += 1;
        practicehandler.QuestionCountText.text = "Q : " + questionCount + " /10";

        questions.RemoveAt(val);
    }

   public bool Answer(string answered){
        bool CorrectAnswer = false;

        if (answered == SelectedQuestion.CorrectAnswer){

            CorrectAnswer = true;
            scoreCount += 1;
            practicehandler.ScoreText.text = scoreCount + "/10";

        }else{

        }
         if (practicegameStatus == PracticeGameStatus.Playing){
            
            if (questions.Count > 0){

                Invoke("SelectQuestion", 0.4f);

            }else{
                practicegameStatus = PracticeGameStatus.Next;
                practicehandler.GameOverPanel.SetActive(true);
                
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