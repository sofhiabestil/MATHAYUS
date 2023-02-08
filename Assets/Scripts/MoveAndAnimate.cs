using UnityEngine;

public class MoveAndAnimate : MonoBehaviour
{
    public float speed = 1.0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        animator.SetFloat("Speed", 1.0f);
    }
}
