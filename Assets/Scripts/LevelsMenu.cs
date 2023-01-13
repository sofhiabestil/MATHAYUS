using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelsMenu : MonoBehaviour
{
    public void Loadlevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
