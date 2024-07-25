using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2f;
    private int waypointIndex = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(FollowPathCoroutine());
        }
    }

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

    public void OnWalk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    public void OnWalkStop()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    void OnIrritation()
    {
        animator.SetTrigger("isIrritated");
    }

    void OnDeath()
    {
        animator.SetTrigger("isDead");
    }
}
