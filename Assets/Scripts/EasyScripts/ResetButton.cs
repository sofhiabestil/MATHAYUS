using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private RectTransform rectTrans;
    public Canvas myCanvas;
    private CanvasGroup canvasGroup;
    public int id;
    public Vector2 startingPosition;
    public List<PDragObjectData> objectsData = new List<PDragObjectData>();
    public GameObject[] objectsToReset;
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        foreach (GameObject obj in objectsToReset)
        {
            PDragObjectData data = new PDragObjectData();
            data.id = obj.GetComponent<PDragAndDrop>().id;
            data.startingPosition = obj.GetComponent<RectTransform>().anchoredPosition;
            objectsData.Add(data);
        }
    }

    public void ResetObjects()
    {
        foreach (GameObject obj in objectsToReset)
        {
            RectTransform rect = obj.GetComponent<RectTransform>();
            int id = obj.GetComponent<PDragAndDrop>().id;
            PDragObjectData data = objectsData.Find(x => x.id == id);
            rect.anchoredPosition = data.startingPosition;
        }
    }
}
