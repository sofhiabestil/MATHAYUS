using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
           // reference     call name
    public TextMeshProUGUI textDisplay;
    // array of sentence
    public string[] elements; 
    public int index=0;
    public float typingSpeed;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Type());
        DisplayText();
    }

    void Update()
    {
        if(textDisplay.text == elements[0])
        {

            //For abling continue button
            continueButton.SetActive(true);
        }
    }

    void DisplayText()
    {
         foreach(char letter in elements[index].ToCharArray())
         {
             textDisplay.text += letter;
             //typing speed
             yield return new WaitForSeconds(0.02f); 
         }

        /**for (int i=0; i<elements.Length; i++)
        {
            textDisplay.text += elements[i];

        }*/
    }

    // Update is called once per frame
    public void NextSentence()
    {
        //For disabling continue button
        continueButton.SetActive(false);

        // -1 because array is start in zero(0) 
        if (index < elements.Length-1) 
        {
            // incrementing index by 1
            index++;
            // reset the text display so sentences don't stack
            textDisplay.text = "";
            // to make sentence slowly displays itself
            StartCoroutine(Type());
        }
        else
        {
            // reset the text when the dialogue is complete 
            textDisplay.text = "";
            //hide continue button if dialogue ended
            continueButton.SetActive(false);
        }
    }

    


}
