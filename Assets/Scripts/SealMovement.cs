using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{
    Animator animator;
    private bool isHunted = false;
    private HuntManager huntManager;
    [SerializeField]
    private float moveSpeed = 2f; // 海豹移动速度

    void Start()
    {
        animator = GetComponent<Animator>();
        huntManager = FindObjectOfType<HuntManager>();
    }

    void Update()
    {
        if (isHunted && Input.GetKeyDown(KeyCode.Space)) // Space key
        {
            // Check if the mouse click is on the seal
            Destroy(gameObject);
            huntManager.ShowHuntMessageAtPosition(transform.position);
        }

        if (!isHunted)
        {
            MoveRight();
        }
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void OnIrritation()
    {
        animator.SetTrigger("isIrritated");
    }

    void OnDeath()
    {
        animator.SetTrigger("isDead");
        isHunted = true;
    }
}
