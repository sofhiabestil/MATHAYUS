using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button buttonlevel2;
    public Button buttonlevel3;

    void Start()
    {
        CheckLevel();    
    }

   public void CheckLevel(){
        
        int StatusLevel2 = PlayerPrefs.GetInt("Average");
        int StatusLevel3 = PlayerPrefs.GetInt("Difficult");

        if(StatusLevel2 == 1){
            buttonlevel2.interactable = true;
        }else
             buttonlevel2.interactable = false;

        if(StatusLevel3 == 1){
             buttonlevel3.interactable = true;
        }else
             buttonlevel3.interactable = false;    
   }

    public void OnApplicationQuit()
    {
        // Lock all unlocked levels
        PlayerPrefs.SetInt("Average", 0);
        PlayerPrefs.SetInt("Difficult", 0);
    }
}
