using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float moveSpeed = 2f;
    private int waypointIndex = 0;
    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is empty!");
            return;
        }
        
        transform.position = waypoints[waypointIndex].transform.position;
        animator = GetComponentInChildren<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Debug.Log("Initialized at waypoint: " + waypointIndex);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            sr.flipX = false;
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (waypointIndex <= waypoints.Length - 1)
        {
            animator.SetBool("IsWalking", true);
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
        animator.SetBool("IsWalking", false);
        Debug.Log("Reached final waypoint");
    }
}
