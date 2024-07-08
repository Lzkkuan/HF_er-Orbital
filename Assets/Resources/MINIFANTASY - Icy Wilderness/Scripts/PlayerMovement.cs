using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    private float xInput;
    private Animator animator;
    public bool IsWalking;
    private SpriteRenderer sr;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>(); 
        sr = GetComponent<SpriteRenderer>();
    }
    

    private void Update() {
        xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical"); // Added vertical input
        IsWalking = (rb.velocity.x != 0 || rb.velocity.y != 0);
        animator.SetBool("IsWalking", IsWalking);
        if(rb.velocity.x > 0) {
            sr.flipX = false;
        }
        if(rb.velocity.x < 0) {
            sr.flipX = true;
        }
        HandleMovement(xInput, yInput);
    }

    private void HandleMovement(float xInput, float yInput) {
        rb.velocity = new Vector2(xInput * moveSpeed, yInput * moveSpeed);
    }

}
