using UnityEngine;
using UnityEngine.UI;

public class ActivatePanel : MonoBehaviour
{
    public GameObject Confetti, GameOverPanel;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(ActivateObjects);
    }

    void ActivateObjects()
    {
        Confetti.SetActive(true);
        GameOverPanel.SetActive(true);
    }
}