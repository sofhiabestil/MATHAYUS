using UnityEngine;
using UnityEngine.UI;

public class PWordData : MonoBehaviour
{
    [SerializeField] private Text wordText;

    [HideInInspector]
    public char wordValue;

    private Button buttonComponent;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        if (buttonComponent)
        {
            buttonComponent.onClick.AddListener(() => PWordSelected());
        }
    }

    public void SetWord(char value)
    {
        wordText.text = value + "";
        wordValue = value;
    }

    private void PWordSelected()
    {
        PDifficultManager.p_instance.SelectedOption(this);
    }


}
