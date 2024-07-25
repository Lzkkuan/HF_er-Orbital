using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JessicaMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    DetectionZone dz;
    Animator animator;
    public float speed;
    public float minFollowDistance; // New variable for minimum follow distance

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dz = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (dz.detectedObj != null) {
            Vector2 direction = (dz.detectedObj.transform.position - transform.position);
            float distance = direction.magnitude;

            // Only move if the distance is greater than the minimum follow distance
            if (distance <= dz.viewRadius && distance > minFollowDistance) {
                rb.AddForce(direction.normalized * speed);
                if (direction.x > 0) {
                    sr.flipX = false;
                }
                else if (direction.x < 0) {
                    sr.flipX = true;
                }
                OnWalk();
            }
            else {
                OnWalkStop();
            }
        }
    }

    public void OnWalk() {
        animator.SetBool("IsWalking", true);
    }
    
    public void OnWalkStop() {
        animator.SetBool("IsWalking", false);
    }
}
