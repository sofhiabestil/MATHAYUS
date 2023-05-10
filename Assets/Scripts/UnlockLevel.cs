using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnlockLevel : MonoBehaviour
{
   int levelUnlocked;
   public Button[] buttons;
    
   void Start()
    {
        levelUnlocked = PlayerPrefs.GetInt("levelUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++){
            buttons[i].interactable = false;
        }

        for(int i = 0; i < levelUnlocked; i++){
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int levelIndex){

        SceneManager.LoadScene(levelIndex);
    }

}