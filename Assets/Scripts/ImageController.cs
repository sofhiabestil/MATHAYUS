using UnityEngine;

public class ImageController : MonoBehaviour
{
    public GameObject NextPanel, WalkingPanel;
    public GameObject targetObject;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetObject.transform.position, Time.deltaTime);

        if (Vector2.Distance(transform.position, targetObject.transform.position) < 0.1f)
        {
            NextPanel.SetActive(true);
            WalkingPanel.SetActive(false);
        }
    }
}