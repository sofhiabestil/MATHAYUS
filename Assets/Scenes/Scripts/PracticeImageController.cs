using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeImageController : MonoBehaviour
{
    public GameObject PracticeGameOverPanel, PracticeWalkingPanel, PracticeConfetti;
    public AudioSource PracticeCongrats;
    public float panelDuration = 2.0f; // duration in seconds
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= panelDuration)
        {
            PracticeWalkingPanel.SetActive(false);
            PracticeCongrats.Play();
            PracticeConfetti.SetActive(true);
            PracticeGameOverPanel.SetActive(true);
        }
    }
}

