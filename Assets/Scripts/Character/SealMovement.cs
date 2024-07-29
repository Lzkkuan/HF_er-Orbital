using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement and behavior of the seal, including its response to being hunted.
/// </summary>
public class SealMovement : MonoBehaviour
{
    private Animator animator;
    private bool isHunted = false;
    private HuntManager huntManager;
    [SerializeField]
    private float moveSpeed = 2f; // Seal's movement speed
    private DetectionZone dz;

    /// <summary>
    /// Initializes components and finds the HuntManager instance.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        huntManager = FindObjectOfType<HuntManager>();
        dz = GetComponent<DetectionZone>();
    }

    /// <summary>
    /// Updates seal behavior based on whether it is hunted and user input.
    /// </summary>
    void Update()
    {
        if (isHunted && Input.GetKeyDown(KeyCode.Space)) // Space key
        {
            // Check if the mouse click is on the seal
            Destroy(gameObject);
            huntManager.ShowHuntMessageAtPosition(transform.position);
            HealthBar healthBar = dz.detectedObj.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                healthBar.IncreaseHp(10);
            }
            else
            {
                Debug.LogWarning("HealthBar component not found on detected object.");
            }
        }

        if (!isHunted)
        {
            MoveRight();
        }
    }

    /// <summary>
    /// Moves the seal to the right at the specified speed.
    /// </summary>
    void MoveRight()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Triggers the irritation animation for the seal.
    /// </summary>
    void OnIrritation()
    {
        animator.SetTrigger("isIrritated");
    }

    /// <summary>
    /// Triggers the death animation for the seal and marks it as hunted.
    /// </summary>
    void OnDeath()
    {
        animator.SetTrigger("isDead");
        isHunted = true;
    }
}
