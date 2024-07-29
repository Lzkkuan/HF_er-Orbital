using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the GameObject along a series of waypoints and updates its animation and sprite direction accordingly.
/// </summary>
public class FollowThePath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints; // Array of waypoints for the object to follow
    [SerializeField]
    private float moveSpeed = 2f; // Speed at which the object moves
    private int waypointIndex = 0; // Index of the current waypoint
    private Animator animator; // Animator component for controlling animations
    private SpriteRenderer sr; // SpriteRenderer component for flipping sprite

    private void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is empty!"); // Log error if no waypoints are provided
            return;
        }

        transform.position = waypoints[waypointIndex].transform.position; // Initialize position at the first waypoint
        animator = GetComponentInChildren<Animator>(); // Get the Animator component
        sr = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        Debug.Log("Initialized at waypoint: " + waypointIndex);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveCoroutine()); // Start moving when right mouse button is clicked
        }
    }

    /// <summary>
    /// Coroutine to move the GameObject along the waypoints.
    /// </summary>
    /// <returns>IEnumerator for the coroutine</returns>
    private IEnumerator MoveCoroutine()
    {
        while (waypointIndex < waypoints.Length)
        {
            Vector2 targetPosition = waypoints[waypointIndex].transform.position; // Target position of the current waypoint
            Vector2 direction = targetPosition - (Vector2)transform.position; // Direction to the target position

            // Set character facing direction
            sr.flipX = direction.x < 0;

            animator.SetBool("IsWalking", true); // Start walking animation

            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime); // Move towards the target position

                yield return null; // Wait for the next frame
            }

            // Update to the next waypoint
            waypointIndex += 1;
            Debug.Log("Moving to next waypoint: " + waypointIndex);
        }

        animator.SetBool("IsWalking", false); // Stop walking animation
        Debug.Log("Reached final waypoint");
    }
}
