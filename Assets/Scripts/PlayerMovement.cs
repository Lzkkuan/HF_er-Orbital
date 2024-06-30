using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    private Animator animator;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        HandleMovement(xInput, yInput);
        UpdateAnimation(xInput, yInput);
    }

    private void HandleMovement(float xInput, float yInput)
    {
        Vector2 movementDirection = new Vector2(xInput, yInput).normalized;
        rb.velocity = movementDirection * moveSpeed;
    }

    private void UpdateAnimation(float xInput, float yInput)
    {
        bool isWalking = (xInput != 0 || yInput != 0);
        animator.SetBool("IsWalking", isWalking);

        // Flip sprite based on movement direction
        if (xInput > 0)
        {
            sr.flipX = false;
        }
        else if (xInput < 0)
        {
            sr.flipX = true;
        }
    }
}
