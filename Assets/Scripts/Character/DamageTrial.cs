using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the attack logic for an entity, dealing damage to the player on collision with an attack cooldown.
/// </summary>
public class DamageTrial : MonoBehaviour
{
    /// <summary>
    /// The amount of damage dealt by the attack.
    /// </summary>
    public int damage = 30;

    /// <summary>
    /// The cooldown time between attacks.
    /// </summary>
    public float attackCooldown = 1f;

    private float lastAttackTime;
    private Animator animator;

    private void Start()
    {
        lastAttackTime = -attackCooldown; // Initialize to allow immediate attack
        animator = GetComponentInParent<Animator>(); // Get the Animator component from the parent object
    }

    /// <summary>
    /// Handles collision detection with the player to perform an attack.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("isAttacking"); // Trigger the attack animation
            StartCoroutine(PerformAttack(collision));
        }
    }

    /// <summary>
    /// Performs the attack after waiting for the attack animation to complete.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    /// <returns>An IEnumerator for coroutine.</returns>
    private IEnumerator PerformAttack(Collider2D collision)
    {
        // Wait for the attack animation to complete
        yield return new WaitForSeconds(0.5f); // Assume the attack animation lasts 0.5 seconds

        if (collision != null && collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<HealthBar>().DecreaseHp(damage);
        }
    }
}
