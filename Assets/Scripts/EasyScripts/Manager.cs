using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour{

    [SerializeField] private Text easytimerText;
    [SerializeField] private float timeLimit = 1800f;
    private float currentTime;

    private EasyGameStatus EasyGameStatus = EasyGameStatus.Playing;
    public Text EasyTimerText { get { return easytimerText; } }


    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeLimit;
    }
    private void Update()
    {
        if (EasyGameStatus == EasyGameStatus.Playing)
        {
            currentTime -= Time.deltaTime;
            SetTimer(currentTime);
        }
    }

    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value); //set the time value
        easytimerText.text = time.ToString("mm':'ss"); //convert time to Time format

        if (currentTime <= 0)
        {
            //Game Over
            EasyGameStatus = EasyGameStatus.Next;
            //diffgameOverPanel.SetActive(true);
        }
    }


}
