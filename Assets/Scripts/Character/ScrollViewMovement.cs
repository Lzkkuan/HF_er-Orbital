using UnityEngine;
using System.Collections;

public class ScrollMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;  // Add a field for jump force
    public float Yaxis;
    private Animator animator;
    private SpriteRenderer sr;
    private bool isGrounded;
    
    public GameObject Deathpanel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Deathpanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isGrounded)
        {
            Stand();
        }
        float xInput = Input.GetAxisRaw("Horizontal");
        Vector2 movementInput = new Vector2(xInput, rb.velocity.y);
        HandleMovement(movementInput);
        UpdateAnimation(xInput);
        if (transform.position.y < Yaxis)
        {
            animator.SetBool("IsDead",true);
            Deathpanel.SetActive(true);
        }
    }

    private void HandleMovement(Vector2 movementInput)
    {
        rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
    }

    private void Stand()
    {
        animator.SetTrigger("IsStanding");
        StartCoroutine(StandToWalk(0.5f));
        
    }

    private void UpdateAnimation(float xInput)
    {
        bool isWalking = (xInput != 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator StandToWalk(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Wait for the specified delay
        animator.SetTrigger("StandToWalk");
        animator.SetBool("IsStanding", false);
    }

    void OnFire()
    {
        animator.SetTrigger("Attack");
    }
}
