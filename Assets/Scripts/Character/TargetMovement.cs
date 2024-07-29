using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the movement of a target between predefined waypoints and handles animations.
/// </summary>
public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints; // Array of waypoints for the target to follow
    [SerializeField]
    private float moveSpeed = 2f; // Speed at which the target moves between waypoints
    private int waypointIndex = 0; // Index of the current waypoint

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    /// <summary>
    /// Initializes the Rigidbody2D, SpriteRenderer, Animator, and sets the initial position of the target.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is empty!");
            return;
        }

        transform.position = waypoints[waypointIndex].transform.position;
        Debug.Log("Initialized at waypoint: " + waypointIndex);
    }

    /// <summary>
    /// Starts the coroutine to follow the waypoints when the right mouse button is clicked.
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(FollowPathCoroutine());
        }
    }

    /// <summary>
    /// Coroutine to move the target between waypoints and handle animation.
    /// </summary>
    private IEnumerator FollowPathCoroutine()
    {
        while (waypointIndex <= waypoints.Length - 1)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            float distance = Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position);
            Debug.Log("Distance to waypoint " + waypointIndex + ": " + distance);

            if (distance < 0.1f)
            {
                waypointIndex += 1;
                Debug.Log("Moving to next waypoint: " + waypointIndex);
            }

            yield return null;
        }
        animator.SetBool("isWalking", false);
        Debug.Log("Reached final waypoint");
    }

    /// <summary>
    /// Starts the walking animation and stops the attacking animation.
    /// </summary>
    public void OnWalk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// Stops the walking and attacking animations.
    /// </summary>
    public void OnWalkStop()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// Triggers the irritation animation.
    /// </summary>
    void OnIrritation()
    {
        animator.SetTrigger("isIrritated");
    }

    /// <summary>
    /// Triggers the death animation.
    /// </summary>
    void OnDeath()
    {
        animator.SetTrigger("isDead");
    }
}
