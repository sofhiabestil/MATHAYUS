using UnityEngine;
using UnityEngine.UI;

public class ActivatePanel : MonoBehaviour
{
    public GameObject Confetti, GameOverPanel;
    public AudioSource Congrats;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(ActivateObjects);
    }

    void ActivateObjects()
    {
        Confetti.SetActive(true);
        Congrats.Play();
        GameOverPanel.SetActive(true);
    }
}