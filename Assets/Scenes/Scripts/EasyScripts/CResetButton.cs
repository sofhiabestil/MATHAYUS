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
    public ObjectShuffler objectShuffler;
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
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            GameObject obj = objectsToReset[i];
            RectTransform rect = obj.GetComponent<RectTransform>();
            int id = obj.GetComponent<DragAndDrop>().id;
            DragObjectData data = objectsData.Find(x => x.id == id);

            // Get the corresponding shuffled object position
            Vector3 shuffledObjPosition = objectShuffler.shuffledObjectPositions[i];

            // Get the parent object's position relative to the canvas
            Vector3 parentPos = obj.transform.parent.GetComponent<RectTransform>().TransformPoint(Vector3.zero);

            // Get the shuffled object's position relative to the parent object
            Vector3 shuffledObjPosRelativeToParent = obj.transform.parent.GetComponent<RectTransform>().InverseTransformPoint(shuffledObjPosition);

            // Set the reset object's position to the shuffled object's position relative to the parent object
            rect.anchoredPosition = parentPos + shuffledObjPosRelativeToParent;
        }
    }
}
