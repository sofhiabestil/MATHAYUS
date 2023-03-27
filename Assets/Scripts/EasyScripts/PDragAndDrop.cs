using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PDragObjectData
{
    public int id;
    public Vector2 startingPosition;
}

public class PDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler{
    private RectTransform rectTrans;
    public Canvas myCanvas;
    private CanvasGroup canvasGroup;
    public int id;
    public Vector2 startingPosition;
    public List<PDragObjectData> objectsData = new List<PDragObjectData>();
    public GameObject[] objectsToReset;
    private int Pscore = 0;

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
            int id = obj.GetComponent<PDragAndDrop>().id;
            PDragObjectData data = objectsData.Find(x => x.id == id);
            rect.anchoredPosition = data.startingPosition;
           

        }
        Pscore = 0; // Reset score
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Pscore = 0;
    }
}