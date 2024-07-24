using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    public float Yaxis;
    private Animator animator;
    private SpriteRenderer sr;
    private bool isGrounded;

    public GameObject Deathpanel;

    // Maintain a set of current collisions
    private HashSet<Collider2D> currentCollisions = new HashSet<Collider2D>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Deathpanel.SetActive(false);
    }

    private void Update()
    {
        CheckGroundStatus();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
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
            animator.SetBool("IsDead", true);
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
        currentCollisions.Add(collision.collider);

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("enter ground");
        }
        else if (collision.gameObject.CompareTag("jumper"))
        {
            Debug.Log("enter jumper");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentCollisions.Remove(collision.collider);

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("leave ground");
        }
        else if (collision.gameObject.CompareTag("jumper"))
        {
            Debug.Log("leave jumper");
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = false;
        foreach (Collider2D col in currentCollisions)
        {
            if (col.CompareTag("Ground") || col.CompareTag("jumper"))
            {
                isGrounded = true;
                break;
            }
        }
    }

    private bool IsOnJumper()
    {
        foreach (Collider2D col in currentCollisions)
        {
            if (col.CompareTag("jumper"))
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator StandToWalk(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        animator.SetTrigger("StandToWalk");
        animator.SetBool("IsStanding", false);
    }

    void OnFire()
    {
        animator.SetTrigger("Attack");
    }
}
