using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PDifficultManager : MonoBehaviour
{
    public static PDifficultManager p_instance; //Instance to make is available in other scripts without reference
    [SerializeField] private PDifficultDataScriptable PdifficultDataScriptable;
    [SerializeField] private Text difficultquestions, diffquestioncountText;          //image element to show the image
    [SerializeField] public GameObject diffstar0, diffstar1, diffstar2, diffstar3, diffwrongPanel, diffcorrectPanel;
    [SerializeField] private GameObject diffgameOverPanel, diffWalkpanel;
    [SerializeField] private float timeLimit = 1800f;
    [SerializeField] private TextMeshProUGUI diffscoreText, diffcorrectMessage, PDifficultHintText;
    [SerializeField] private PWordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private PWordData[] optionsWordList;    //list of options word in the game\
    [SerializeField] public List<AudioSource> DifficultsoundEffect = new List<AudioSource>();


    private PDifficultGameStatus PDifficultGameStatus = PDifficultGameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[10];               //array which store char of each options

    private List<PDifficultQuestionData> questions;
    private List<int> askedQuestions;
    private PDifficultQuestionData diff;
    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question
    private int diffscoreCount = 0;
    private int diffquestionCount = 0;
    private bool checkAnswer = false;
    private float currentTime;
    public Button diffCheckButton;
    private List<PDifficultQuestionData> usedQuestions = new List<PDifficultQuestionData>();


    //public Text DiffTimerText { get { return difftimerText; } }
    public Text DiffQuestionCountText { get { return diffquestioncountText; } }

    public TextMeshProUGUI DiffScoreText { get { return diffscoreText; } }

    public TextMeshProUGUI DiffCorrectAnswerMessage { get { return diffcorrectMessage; } }
    public GameObject DiffGameOverPanel { get { return diffgameOverPanel; } }

    public GameObject DiffWrongPanel { get { return diffwrongPanel; } }

    public GameObject DiffCorrectPanel { get { return diffcorrectPanel; } }

    public GameObject DiffWalkpanel { get { return diffWalkpanel; } }
    
    private void Awake()
    {
        if (p_instance == null)
            p_instance = this;
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

      
        if (PDifficultGameStatus == PDifficultGameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }

       
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value); //set the time value
        //DiffTimerText.text = time.ToString("mm':'ss"); //convert time to Time format

        if (currentTime <= 0)
        {
            //Game Over
            PDifficultGameStatus = PDifficultGameStatus.Next;
            diffgameOverPanel.SetActive(true);
        }
    }


    private void ModernFisherYatesShuffle(List<PDifficultQuestionData> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            PDifficultQuestionData temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void SetQuestion(){
        PDifficultGameStatus = PDifficultGameStatus.Playing;

        // Generate a list of questions that have not been used before
        List<PDifficultQuestionData> unusedQuestions = new List<PDifficultQuestionData>();
        foreach (PDifficultQuestionData question in PdifficultDataScriptable.Pdifficultquestions)
        {
            if (!usedQuestions.Contains(question))
            {
                unusedQuestions.Add(question);
            }
        }

        // Shuffle the list of unused questions using the modern Fisher-Yates shuffle
        ModernFisherYatesShuffle(unusedQuestions);

        // If all questions have been used, reset the used questions HashSet
        if (unusedQuestions.Count == 0)
        {
            usedQuestions.Clear();
            foreach (PDifficultQuestionData question in PdifficultDataScriptable.Pdifficultquestions)
            {
                unusedQuestions.Add(question);
            }
            ModernFisherYatesShuffle(unusedQuestions);
        }

        // Select the first question from the shuffled list of unused questions
        PDifficultQuestionData selectedQuestion = unusedQuestions[0];
        usedQuestions.Add(selectedQuestion);

        // Set the text of the question
        difficultquestions.text = selectedQuestion.diffquestions;

        // Set the answerWord string variable
        answerWord = selectedQuestion.answer;

        PDifficultHintText.text = selectedQuestion.dhints;

        // Increment the question count and update the text
        diffquestionCount += 1;
        DiffQuestionCountText.text = "Q :" + diffquestionCount + "/5";

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
        PDifficultGameStatus = PDifficultGameStatus.Checking;
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
            DiffScoreText.text = diffscoreCount + "/5";
            diffcorrectPanel.gameObject.SetActive(true);
            DifficultsoundEffect[1].Play();

            Invoke("SetNextQuestion", 0.5f);  
            Invoke("DiffDismissMessagePanel", 1.5f);

        }
        else{
            
            diffwrongPanel.gameObject.SetActive(true);
            diffcorrectMessage.text = answerWord;
            DifficultsoundEffect[0].Play();

            Invoke("SetNextQuestion", 0.5f);  
            Invoke("DiffDismissMessagePanel", 1.5f);
        }

        if (diffscoreCount == 5)
        {
            diffstar3.gameObject.SetActive(true);
        }
        else if (diffscoreCount > 3 && diffscoreCount < 5)
        {
            diffstar2.gameObject.SetActive(true);
        }
        else if (diffscoreCount == 0)
        {
            diffstar0.gameObject.SetActive(true);
        }
        else if (diffscoreCount < 2)
        {
            diffstar1.gameObject.SetActive(true);
        }
    }
    void SetNextQuestion(){
        
        PDifficultGameStatus = PDifficultGameStatus.Playing;
   
        //move to next question
        currentQuestionIndex++;
        if (currentQuestionIndex < PdifficultDataScriptable.Pdifficultquestions.Count && diffquestionCount < 10){
            
            SetQuestion();
        
        }else {
            if (diffscoreCount > 4)
            {
                Invoke("ActivateDiffWalkPanel", 1f);
            }
            else
            {
                Invoke("DiffActivateGameOverPanel", 0.9f);
            }
            PDifficultGameStatus = PDifficultGameStatus.Next;
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

    public void DiffActivateGameOverPanel()
    {
        diffgameOverPanel.gameObject.SetActive(true);
    }


    void ActivateDiffWalkPanel()
    {
        DiffWalkpanel.gameObject.SetActive(true);

    }

    public void SelectedOption(PWordData value){
        //if DifficultGameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (PDifficultGameStatus == PDifficultGameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            PDifficultGameStatus = PDifficultGameStatus.Checking;
            
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

    public void DiffRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}


[System.Serializable]
public class PDifficultQuestionData{
    public string diffquestions;
    public string answer;
    public string dhints;
}

public enum PDifficultGameStatus{
    Next,
    Playing,
    Checking
}