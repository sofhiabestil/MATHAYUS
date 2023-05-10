using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;
    public Button[] buttons;

    private void Start()
    {
        // Add click event listeners to each button
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Use closure to capture current value of i
            buttons[i].onClick.AddListener(() => ShowPanel(index));
        }

        // Show the first panel and hide the rest
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void ShowPanel(int index)
    {
        // Hide the current panel
        panels[index].SetActive(false);

        // Show the next panel (if there is one)
        if (index < panels.Length - 1)
        {
            panels[index + 1].SetActive(true);
        }
    }
}
