using System.Collections.Generic;
using UnityEngine;

public class ObjectShuffler : MonoBehaviour
{
    public List<GameObject> objectsToShuffle;
    public List<Vector3> initialPositions;
    public List<Vector3> shuffledObjectPositions;

    void Start()
    {
        ShuffleObjects();
    }

    public void ShuffleObjects()
    {
        // Store initial positions of objects
        initialPositions = new List<Vector3>();
        foreach (GameObject obj in objectsToShuffle)
        {
            initialPositions.Add(obj.transform.position);
        }

        // Shuffle objects
        shuffledObjectPositions = new List<Vector3>();
        for (int i = 0; i < objectsToShuffle.Count; i++)
        {
            int randomIndex = Random.Range(i, objectsToShuffle.Count);
            GameObject temp = objectsToShuffle[i];
            objectsToShuffle[i] = objectsToShuffle[randomIndex];
            objectsToShuffle[randomIndex] = temp;
            shuffledObjectPositions.Add(objectsToShuffle[i].transform.position); // Store shuffled object positions
        }

        // Restore objects to initial positions
        for (int i = 0; i < objectsToShuffle.Count; i++)
        {
            objectsToShuffle[i].transform.position = initialPositions[i];
        }
    }
}
