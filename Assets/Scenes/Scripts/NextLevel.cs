using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public LevelsMenu levelmenu;
   
    public void UnlockNextLevel(string key)
    {
        PlayerPrefs.SetInt(key, 1);
        levelmenu.LoadStringlevel("Levels Menu");
    }
    
    public void OnApplicationQuit()
    {
    int numberOfLevels = 2;
        // Lock all unlocked levels
        for (int i = 1; i <= numberOfLevels; i++)
        {
            string key = "level_" + i.ToString();
            if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1)
            {
                PlayerPrefs.SetInt(key, 0);
            }
        }
    }
}
