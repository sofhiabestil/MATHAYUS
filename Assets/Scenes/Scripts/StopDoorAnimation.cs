using UnityEngine;

public class StopDoorAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StopDoor()
    {
        animator.enabled = false;
    }
}