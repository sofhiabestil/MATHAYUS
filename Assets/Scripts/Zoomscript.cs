using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomscript : MonoBehaviour
{

    public SpriteRenderer targetSize;
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = targetSize.bounds.size.x / targetSize.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = targetSize.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = targetSize.bounds.size.y / 2 * differenceInSize;
        }

    }

}
