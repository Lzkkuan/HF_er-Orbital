using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    private Animator animator;
    private SpriteRenderer sr;
    private Vector2 movementInput;

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
        movementInput = new Vector2(xInput, yInput).normalized;
        UpdateAnimation(xInput, yInput);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 currentPosition = rb.position;
        Vector2 newPosition = currentPosition + movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void UpdateAnimation(float xInput, float yInput) {
        bool isWalking = (xInput != 0 || yInput != 0);
        animator.SetBool("IsWalking", isWalking);

        // Flip sprite based on movement direction
        if (xInput > 0)
        {
            sr.flipX = false;
            gameObject.BroadcastMessage("IsFacingRight", true);
        }
        else if (xInput < 0)
        {
            sr.flipX = true;
            gameObject.BroadcastMessage("IsFacingRight", false);
        }
    }

    void OnFire() {
        animator.SetTrigger("Attack");
    }

}
