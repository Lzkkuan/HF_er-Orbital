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
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (waypointIndex < waypoints.Length)
        {
            Vector2 targetPosition = waypoints[waypointIndex].transform.position;
            Vector2 direction = targetPosition - (Vector2)transform.position;

            // 设置角色朝向
            sr.flipX = direction.x < 0;

            animator.SetBool("IsWalking", true);

            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime);
                
                yield return null;
            }

            // 更新到下一个路径点
            waypointIndex += 1;
            Debug.Log("Moving to next waypoint: " + waypointIndex);
        }

        animator.SetBool("IsWalking", false);
        Debug.Log("Reached final waypoint");
    }
}
