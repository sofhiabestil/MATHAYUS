using UnityEngine;
using UnityEngine.UI;

public class ActivatePanel : MonoBehaviour
{
    public GameObject Confetti, GameOverPanel;
    public AudioSource Congrats, Esoundwelldone, Esoundawesome;
    public Button button;
    SlotHolder slotholder;

    void Start()
    {
        button.onClick.AddListener(ActivateObjects);
        slotholder = FindObjectOfType<SlotHolder>();
    }

    void ActivateObjects()
    {
        Confetti.SetActive(true);
        Congrats.Play();
        GameOverPanel.SetActive(true);

        int easyScore = slotholder.Easyscores;
        Debug.Log("Easy score: " + easyScore);

        if (easyScore == 12)
        {
            Esoundawesome.Play();
        }
        else if (easyScore >= 6 && easyScore <= 11)
        {
            Esoundwelldone.Play();
        }
    }

}
