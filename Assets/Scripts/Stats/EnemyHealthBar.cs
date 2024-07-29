using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Image HealthImg; ///< The UI image representing the enemy's health bar.
    public Image HealthEffect; ///< The UI image for health effect animation.
    public Transform enemyTransform; ///< Reference to the enemy's transform.
    public Vector3 offset; ///< Offset to position the health bar above the enemy.

    public float MaxHealth = 100f; ///< The maximum health value.
    public float currentHealth; ///< The current health value.
    public float fade_timer = 0.5f; ///< Time duration for the health effect fade.
    private Coroutine Hpcoroutine; ///< Coroutine reference for health effect updates.

    /// <summary>
    /// Initializes the health bar and updates its display.
    /// </summary>
    private void Start()
    {
        currentHealth = MaxHealth;
        UpdateHp();
    }

    /// <summary>
    /// Updates the position of the health bar to follow the enemy.
    /// </summary>
    private void Update()
    {
        if (enemyTransform != null)
        {
            // Update the health bar position to follow the enemy
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(enemyTransform.position + offset);
            transform.position = screenPosition;
        }
    }

    /// <summary>
    /// Sets the health value and updates the health bar.
    /// </summary>
    /// <param name="health">The new health value to set.</param>
    public void SetHp(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, MaxHealth);
        UpdateHp();
        if (currentHealth <= 0)
        {
            // Handle enemy death (e.g., disable or destroy the enemy)
        }
    }

    /// <summary>
    /// Increases the enemy's health by the specified amount.
    /// </summary>
    /// <param name="amount">The amount of health to increase.</param>
    public void IncreaseHealth(float amount)
    {
        SetHp(currentHealth + amount);
    }

    /// <summary>
    /// Decreases the enemy's health by the specified amount.
    /// </summary>
    /// <param name="amount">The amount of health to decrease.</param>
    public void DecreaseHealth(float amount)
    {
        SetHp(currentHealth - amount);
    }

    /// <summary>
    /// Updates the health bar's fill amount and starts the health effect coroutine.
    /// </summary>
    private void UpdateHp()
    {
        HealthImg.fillAmount = currentHealth / MaxHealth;

        if (Hpcoroutine != null)
        {
            StopCoroutine(Hpcoroutine);
        }

        Hpcoroutine = StartCoroutine(UpdateHealthEffect());
    }

    /// <summary>
    /// Gradually updates the health effect to smoothly transition the health bar fill.
    /// </summary>
    /// <returns>An IEnumerator for coroutine management.</returns>
    public IEnumerator UpdateHealthEffect()
    {
        float effectLength = HealthEffect.fillAmount - HealthImg.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < fade_timer && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            HealthEffect.fillAmount = Mathf.Lerp(HealthImg.fillAmount + effectLength, HealthImg.fillAmount, elapsedTime / fade_timer);
            yield return null;
        }

        HealthEffect.fillAmount = HealthImg.fillAmount;
    }
}
