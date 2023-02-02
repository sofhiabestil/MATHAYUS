/*
 * Main Menu
 * - is for exiting the game application
 * - 
 * @Sofhia Bestil
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**To close the game
    - the method should set as public to make it visble 
    on the function section of MainMenu*/
    public void QuitGame() 
    {
        Debug.Log("Quit on the Game!");
        Application.Quit();
    } 
}
