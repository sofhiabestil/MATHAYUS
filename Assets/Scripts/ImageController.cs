using UnityEngine;

public class ImageController : MonoBehaviour
{
    public GameObject NextPanel, WalkingPanel;
    public float panelDuration = 2.0f; // duration in seconds
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= panelDuration)
        {
            NextPanel.SetActive(true);
            WalkingPanel.SetActive(false);
        }
    }
}
