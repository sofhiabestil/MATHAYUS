using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeUIHandler : MonoBehaviour{
    
    [SerializeField] private PracticeManager practicemanager;
    [SerializeField] private Text QuestionText, HintText, scoreText, questioncountText;
    [SerializeField] private List<Button> options;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] public  GameObject star0, star1, star2, star3;

    private PracticeQuestion question;
    private bool answered;

    public Text ScoreText { get { return scoreText; } }

    public Text QuestionCountText { get { return questioncountText; } }

    public GameObject GameOverPanel { get { return gameOverPanel; } }
    
    void Awake(){
         
         for(int i =  0; i < options.Count; i++){
            
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => Onclick(localBtn));
         }
    }

    public void SetQuestion(PracticeQuestion question){
        
        this.question = question;  
        QuestionText.text = question.QuestionInfo;
        HintText.text = question.Hints;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);
        for(int i = 0; i < options.Count;  i++){
            options[i].GetComponentInChildren<Text>().text = answerList[i]; 
            options[i].name = answerList[i];
        }
        answered = false;

    }

    private void Onclick(Button btn){

        if(!answered){

            answered = true;
            bool val = practicemanager.Answer(btn.name);

        }
    }

    public void RetryButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}