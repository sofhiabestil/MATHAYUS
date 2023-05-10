using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConfetti : MonoBehaviour
{
    public GameObject confetti;

    public void Spawn()
    {
        GameObject gameObject = Instantiate(confetti);
        Destroy(gameObject, 3);
    }
}
