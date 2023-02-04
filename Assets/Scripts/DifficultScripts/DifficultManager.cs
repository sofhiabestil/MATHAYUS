using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultManager : MonoBehaviour
{
    public static DifficultManager instance; //Instance to make is available in other scripts without reference
    [SerializeField] private DifficultDataScriptable difficultDataScriptable;
    [SerializeField] private Text difficultquestions, diffquestioncountText, diffscoreText, diffcorrectMessage, difftimerText;           //image element to show the image
    [SerializeField] public GameObject diffstar0, diffstar1, diffstar2, diffstar3, diffwrongPanel, diffcorrectPanel;
    [SerializeField] private GameObject diffgameOverPanel;
    [SerializeField] private float timeLimit = 1800f;
    [SerializeField] private WordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private WordData[] optionsWordList;    //list of options word in the game\
    [SerializeField] public List<AudioSource> DifficultsoundEffect = new List<AudioSource>();


    private DifficultGameStatus DifficultGameStatus = DifficultGameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[10];               //array which store char of each options

    private List<DifficultQuestionData> dquestions;
    private List<int> askedQuestions;
    private DifficultQuestionData diff;
    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question
    private int diffscoreCount = 0;
    private int diffquestionCount = 0;
    private bool checkAnswer = false;
    private float currentTime;



    public Text DiffTimerText { get { return difftimerText; } }
    public Text DiffQuestionCountText { get { return diffquestioncountText; } }

    public Text DiffScoreText { get { return diffscoreText; } }

    public Text DiffCorrectAnswerMessage { get { return diffcorrectMessage; } }
    public GameObject DiffGameOverPanel { get { return diffgameOverPanel; } }

    public GameObject DiffWrongPanel { get { return diffwrongPanel; } }

    public GameObject DiffCorrectPanel { get { return diffcorrectPanel; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start() {
       
        currentTime = timeLimit;

        askedQuestions = new List<int>();
        selectedWordsIndex = new List<int>();   //create a new list at start        
        SetQuestion();                                  //set question
        
    }

    private void Update()
    {
        if (DifficultGameStatus == DifficultGameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value); //set the time value
        DiffTimerText.text = time.ToString("mm':'ss"); //convert time to Time format

        if (currentTime <= 0)
        {
            //Game Over
            DifficultGameStatus = DifficultGameStatus.Next;
            diffgameOverPanel.SetActive(true);
        }
    }


 private void ModernFisherYatesShuffle(List<DifficultQuestionData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i);
            DifficultQuestionData temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }


    void SetQuestion(){
        DifficultGameStatus = DifficultGameStatus.Playing;      //set DifficultGameStatus to playing 

        // Set the text of the question
        difficultquestions.text = difficultDataScriptable.difficultquestions[currentQuestionIndex].diffquestions;

        // Set the answerWord string variable
        answerWord = difficultDataScriptable.difficultquestions[currentQuestionIndex].answer;

        // Increment the question count and update the text
        diffquestionCount += 1;
        DiffQuestionCountText.text = "Q :" + diffquestionCount + "/10";

        // Reset the answers and options value to orignal
        ResetQuestion();

        // Clear the list for new question
        selectedWordsIndex.Clear();

        // Clear the array
        Array.Clear(wordsArray, 0, wordsArray.Length);

        // Add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {
            wordsArray[i] = char.ToUpper(answerWord[i]);
        }

        // Add the dummy char to wordsArray
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
        }

        // Randomly Shuffle the words array
        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray();

        // Set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }
    }
    //Method called on Reset Button click and on new question
    public void ResetQuestion(){
        //activate all the answerWordList gameobject and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++){
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetWord('_');
        }

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++){
            answerWordList[i].gameObject.SetActive(false);
        }

        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++){
            optionsWordList[i].gameObject.SetActive(true);
        }

        currentAnswerIndex = 0;
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void CheckAnswer(){
        DifficultGameStatus = DifficultGameStatus.Checking;
        correctAnswer = true;

        for (int i = 0; i < answerWord.Length; i++)
        {
            if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
            {
                
                correctAnswer = false;
                break;
            }
        }

        if (correctAnswer){

            diffscoreCount += 1;
            DiffScoreText.text = diffscoreCount + "/10";
            diffcorrectPanel.gameObject.SetActive(true);
            DifficultsoundEffect[1].Play();

            Invoke("SetNextQuestion", 0.5f);  
            Invoke("DiffDismissMessagePanel", 0.8f);

        }
        else{
            
            diffwrongPanel.gameObject.SetActive(true);
            diffcorrectMessage.text = answerWord;
            DifficultsoundEffect[0].Play();

            Invoke("SetNextQuestion", 0.5f);  
            Invoke("DiffDismissMessagePanel", 0.8f);
        }

        if (diffscoreCount == 10)
        {
            diffstar3.gameObject.SetActive(true);
        }
        else if (diffscoreCount > 4 && diffscoreCount < 10)
        {
            diffstar2.gameObject.SetActive(true);
        }
        else if (diffscoreCount == 0)
        {
            diffstar0.gameObject.SetActive(true);
        }
        else if (diffscoreCount < 5)
        {
            diffstar1.gameObject.SetActive(true);
        }
    }
    void SetNextQuestion(){
        
        DifficultGameStatus = DifficultGameStatus.Playing;
   
        //move to next question
        currentQuestionIndex++;
        if (currentQuestionIndex < difficultDataScriptable.difficultquestions.Count && diffquestionCount < 10){
            
            SetQuestion();
        
        }else {
            
            Invoke("DiffActivateGameOverPanel", 0.9f);
            DifficultGameStatus = DifficultGameStatus.Next;
        }
   
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DiffDismissMessagePanel()
    {
        diffcorrectPanel.gameObject.SetActive(false);
        diffwrongPanel.gameObject.SetActive(false);
    }

    void DiffActivateGameOverPanel()
    {
        diffgameOverPanel.gameObject.SetActive(true);
    }

    public void SelectedOption(WordData value){
        //if DifficultGameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (DifficultGameStatus == DifficultGameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            DifficultGameStatus = DifficultGameStatus.Checking;
            
        }
    }

    public void ResetLastWord(){
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

            currentAnswerIndex--;
            answerWordList[currentAnswerIndex].SetWord('_');
        }
    }
    void ResetQuestionsList()
    {
        askedQuestions.Clear();
    }

    void OnEndGame(){
    ResetQuestionsList();
    }

}


[System.Serializable]
public class DifficultQuestionData{
    public string diffquestions;
    public string answer;
}

public enum DifficultGameStatus{
    Next,
    Playing,
    Checking
}