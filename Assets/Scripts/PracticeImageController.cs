using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeImageController : MonoBehaviour
{
    public GameObject PracticeGameOverPanel, PracticeWalkingPanel, PracticeConfetti;
    public float panelDuration = 2.0f; // duration in seconds
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= panelDuration)
        {
            PracticeConfetti.SetActive(true);
            PracticeGameOverPanel.SetActive(true);
            PracticeWalkingPanel.SetActive(false);
        }
    }
}

