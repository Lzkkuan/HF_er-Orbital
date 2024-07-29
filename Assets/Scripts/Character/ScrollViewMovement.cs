using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Controls the movement, animation, and interactions of the player character in a scrolling environment.
/// </summary>
public class ScrollMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    public float Yaxis;
    private Animator animator;
    private SpriteRenderer sr;
    private bool isGrounded;

    /// <summary>
    /// Panel displayed when the character dies.
    /// </summary>
    public GameObject Deathpanel;

    private GameObject JumperDown;

    // Maintain a set of current collisions
    private HashSet<Collider2D> currentCollisions = new HashSet<Collider2D>();

    /// <summary>
    /// Initializes the components and hides the Death panel.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Deathpanel.SetActive(false);
    }

    /// <summary>
    /// Updates character state and movement each frame.
    /// </summary>
    private void Update()
    {
        CheckGroundStatus();

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
            animator.SetBool("IsDead", true);
            Deathpanel.SetActive(true);
        }
    }

    /// <summary>
    /// Handles movement based on player input.
    /// </summary>
    /// <param name="movementInput">The movement vector based on player input.</param>
    private void HandleMovement(Vector2 movementInput)
    {
        rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
    }

    /// <summary>
    /// Triggers the standing animation and transitions to walking after a delay.
    /// </summary>
    private void Stand()
    {
        animator.SetTrigger("IsStanding");
        StartCoroutine(StandToWalk(0.5f));
    }

    /// <summary>
    /// Updates the character's animation state based on movement input.
    /// </summary>
    /// <param name="xInput">The horizontal input from the player.</param>
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

    /// <summary>
    /// Handles collisions with ground and jumper objects.
    /// </summary>
    /// <param name="collision">The collision details.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("enter ground");
            currentCollisions.Add(collision.collider);
        }
        else if (collision.gameObject.CompareTag("jumper") && collision.gameObject.transform.position.y - 1 < transform.position.y)
        {
            Debug.Log("enter jumper");
            currentCollisions.Add(collision.collider);
        }
    }

    /// <summary>
    /// Handles the exit of collisions with ground and jumper objects.
    /// </summary>
    /// <param name="collision">The collision details.</param>
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

    /// <summary>
    /// Checks if the character is grounded by iterating through current collisions.
    /// </summary>
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

    /// <summary>
    /// Determines if the character is on a jumper object.
    /// </summary>
    /// <returns>True if on a jumper object; otherwise, false.</returns>
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

    /// <summary>
    /// Coroutine to transition from standing to walking after a delay.
    /// </summary>
    /// <param name="delay">The delay before transitioning to walking.</param>
    private IEnumerator StandToWalk(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        animator.SetTrigger("StandToWalk");
        animator.SetBool("IsStanding", false);
    }

    /// <summary>
    /// Triggers the attack animation.
    /// </summary>
    void OnFire()
    {
        animator.SetTrigger("Attack");
    }
}
