using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // To close the game
    public void QuitGame() //the function should set as public to make it visble on the function section of MainMenu
    {
        Debug.Log("Quit on the Game!");
        Application.Quit();
    }

    
}
