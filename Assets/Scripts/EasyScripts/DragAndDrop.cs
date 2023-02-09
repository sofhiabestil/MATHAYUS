using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragObjectData
{
    public int id;
    public Vector2 startingPosition;
}

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler{
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
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta / myCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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