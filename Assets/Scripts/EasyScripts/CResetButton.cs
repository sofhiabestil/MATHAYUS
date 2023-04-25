using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResetButton : MonoBehaviour
{
    private RectTransform rectTrans;
    public Canvas myCanvas;
    private CanvasGroup canvasGroup;
    public int id;
    public Vector2 startingPosition;
    public List<DragObjectData> objectsData = new List<DragObjectData>();
    public GameObject[] objectsToReset;
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        foreach (GameObject obj in objectsToReset)
        {
            DragObjectData data = new DragObjectData();
            data.id = obj.GetComponent<DragAndDrop>().id;
            data.startingPosition = obj.GetComponent<RectTransform>().anchoredPosition;
            objectsData.Add(data);
        }
    }

    public void ResetObjects()
    {
        foreach (GameObject obj in objectsToReset)
        {
            RectTransform rect = obj.GetComponent<RectTransform>();
            int id = obj.GetComponent<DragAndDrop>().id;
            DragObjectData data = objectsData.Find(x => x.id == id);
            rect.anchoredPosition = data.startingPosition;
        }
    }
}
