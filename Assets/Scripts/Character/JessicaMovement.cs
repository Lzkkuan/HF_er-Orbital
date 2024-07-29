using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement and animations of the character Jessica.
/// </summary>
public class JessicaMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private DetectionZone dz;
    private Animator animator;

    /// <summary>
    /// The movement speed of the character.
    /// </summary>
    public float speed;

    /// <summary>
    /// The minimum distance at which the character will follow the detected object.
    /// </summary>
    public float minFollowDistance;

    /// <summary>
    /// Initializes the components required for the character's movement and animations.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dz = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame.
    /// Handles the movement and direction of the character based on the detected object.
    /// </summary>
    private void FixedUpdate()
    {
        if (dz.detectedObj != null)
        {
            Vector2 direction = (dz.detectedObj.transform.position - transform.position);
            float distance = direction.magnitude;

            // Only move if the distance is greater than the minimum follow distance
            if (distance <= dz.viewRadius && distance > minFollowDistance)
            {
                rb.AddForce(direction.normalized * speed);
                if (direction.x > 0)
                {
                    sr.flipX = false;
                }
                else if (direction.x < 0)
                {
                    sr.flipX = true;
                }
                OnWalk();
            }
            else
            {
                OnWalkStop();
            }
        }
    }

    /// <summary>
    /// Sets the walking animation.
    /// </summary>
    public void OnWalk()
    {
        animator.SetBool("IsWalking", true);
    }

    /// <summary>
    /// Stops the walking animation.
    /// </summary>
    public void OnWalkStop()
    {
        animator.SetBool("IsWalking", false);
    }
}
