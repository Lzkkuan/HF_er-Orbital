using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    DetectionZone dz;
    Animator animator;
    public float speed;
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
            if (direction.magnitude <= dz.viewRadius) {
                rb.AddForce(direction * speed);
                if (direction.x > 0) {
                    sr.flipX = false;
                }
                else if (direction.x < 0) {
                    sr.flipX = true;
                }
                OnWalk();
            }
            
        }
        else
        {
            OnWalkStop();
        }
    }

    public void OnWalk() {
        animator.SetBool("isWalking", true);
    }
    
    public void OnWalkStop() {
        animator.SetBool("isWalking", false);
    }

    void OnIrritation() {
        animator.SetTrigger("isIrritated");
    }

    void OnDeath() {
        animator.SetTrigger("isDead");
    }
}
