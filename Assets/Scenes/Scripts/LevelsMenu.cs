using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelsMenu : MonoBehaviour
{
    public void Loadlevel(int index)
    {
        SceneManager.LoadScene(index);
    }


    public void LoadStringlevel(string namaScene)
    {
        SceneManager.LoadScene(namaScene);
    }
}

