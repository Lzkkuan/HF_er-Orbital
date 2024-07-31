using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement and attack behavior of an enemy character.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private DetectionZone dz;
    private Animator animator;

    /// <summary>
    /// The movement speed of the enemy.
    /// </summary>
    public float speed;

    /// <summary>
    /// The range within which the enemy can attack.
    /// </summary>
    public float attackRange;

    /// <summary>
    /// The damage dealt by the enemy's attack.
    /// </summary>
    public int damage;

    /// <summary>
    /// The cooldown time between attacks.
    /// </summary>
    public float attackCooldown = 1f;

    private float lastAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dz = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown; // Initialize to allow immediate attack
    }

    private void FixedUpdate()
    {
        if (dz.detectedObj != null)
        {
            Debug.Log("detecting:" + dz.detectedObj);
            Vector2 direction = (dz.detectedObj.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(dz.detectedObj.transform.position, transform.position);

            if (distance <= attackRange)
            {
                rb.velocity = Vector2.zero;
                AttackPlayer();
            }
            else
            {
                FollowPlayer(direction);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            OnWalkStop();
        }
    }

    /// <summary>
    /// Follows the player in the specified direction.
    /// </summary>
    /// <param name="direction">The direction to move towards.</param>
    private void FollowPlayer(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
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

    /// <summary>
    /// Attacks the player if within range and the cooldown has elapsed.
    /// </summary>
    private void AttackPlayer()
    {
        animator.SetBool("isAttacking", true);
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            if (dz.detectedObj != null)
            {
                HealthBar healthBar = dz.detectedObj.GetComponent<HealthBar>();
                if (healthBar != null)
                {
                    healthBar.DecreaseHp(damage);
                }
                else
                {
                    Debug.LogWarning("HealthBar component not found on:" + dz.detectedObj + "noooooo");
                }
            }
            else
            {
                Debug.LogWarning("Detected object is null.");
            }
        }
    }

    /// <summary>
    /// Sets the walking animation and stops the attacking animation.
    /// </summary>
    public void OnWalk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// Stops the walking and attacking animations.
    /// </summary>
    public void OnWalkStop()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// Triggers the irritation animation.
    /// </summary>
    void OnIrritation()
    {
        animator.SetTrigger("isIrritated");
    }

    /// <summary>
    /// Triggers the death animation.
    /// </summary>
    void OnDeath()
    {
        animator.SetTrigger("isDead");
    }
}
