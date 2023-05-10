using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyPanelManager : MonoBehaviour
{
    public GameObject[] panels;

    void Start()
    {
        int randomIndex = Random.Range(0, panels.Length);
        panels[randomIndex].SetActive(true);
    }
}
