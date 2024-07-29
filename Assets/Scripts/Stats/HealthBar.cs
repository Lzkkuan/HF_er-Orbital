using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the health and strength bars for the player character, including handling damage and updating UI effects.
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Image HPImg; // Health bar image
    public Image HPEffect; // Health bar effect image

    public Image StrengthImg; // Strength bar image
    public Image StrengthEffect; // Strength bar effect image

    public Animator PlayerAnimator; // Animator for the player character
    public GameObject DeathPanel; // Panel to show when the player dies

    public float MaxHp; // Maximum health points
    public float currentHp; // Current health points
    public float fade_timer_time = 0.5f; // Duration of the fade effect
    private Coroutine damage_coroutine; // Coroutine for updating health effect
    private Coroutine strength_coroutine; // Coroutine for updating strength effect

    [Range(1, 5)]
    public float strength = 1; // Strength value affecting maximum health

    /// <summary>
    /// Initializes the health and strength bars at the start.
    /// </summary>
    private void Start()
    {
        UpdateMaxHp(); // Set the initial maximum health based on strength
        currentHp = MaxHp; // Set the current health to maximum
        DeathPanel.SetActive(false); // Hide the death panel at the start
        UpdateHealthBar(); // Update the health bar display
        UpdateStrengthBar(); // Update the strength bar display
    }

    /// <summary>
    /// Sets the health to a specified value and updates the health bar.
    /// </summary>
    /// <param name="health">The new health value.</param>
    public void SetHealth(float health)
    {
        currentHp = Mathf.Clamp(health, 0f, MaxHp); // Clamp health within valid range
        UpdateHealthBar(); // Update the health bar display
        if (currentHp <= 0)
        {
            Debug.Log("start 2s"); // Log message for debugging
            PlayerAnimator.SetBool("IsDead", true); // Trigger death animation
            StartCoroutine(ShowDeathPanelAfterDelay(2f)); // Show death panel after delay
        }
    }

    /// <summary>
    /// Increases the health by a specified amount.
    /// </summary>
    /// <param name="amount">The amount to increase health by.</param>
    public void IncreaseHp(float amount)
    {
        SetHealth(currentHp + amount); // Increase current health and update
    }

    /// <summary>
    /// Decreases the health by a specified amount.
    /// </summary>
    /// <param name="amount">The amount to decrease health by.</param>
    public void DecreaseHp(float amount)
    {
        SetHealth(currentHp - amount); // Decrease current health and update
    }

    /// <summary>
    /// Updates the health bar and starts the health effect coroutine.
    /// </summary>
    private void UpdateHealthBar()
    {
        HPImg.fillAmount = currentHp / MaxHp; // Update health bar fill amount

        if (damage_coroutine != null)
        {
            StopCoroutine(damage_coroutine); // Stop any ongoing health effect coroutine
        }

        damage_coroutine = StartCoroutine(UpdateHpEffect()); // Start new health effect coroutine
    }

    /// <summary>
    /// Coroutine to smoothly update the health effect image.
    /// </summary>
    /// <returns>IEnumerator for coroutine.</returns>
    public IEnumerator UpdateHpEffect()
    {
        float effectLength = HPEffect.fillAmount - HPImg.fillAmount; // Calculate effect length
        float elapsedTime = 0f; // Timer for coroutine

        while (elapsedTime < fade_timer_time && effectLength != 0)
        {
            elapsedTime += Time.deltaTime; // Increment timer
            HPEffect.fillAmount = Mathf.Lerp(HPImg.fillAmount + effectLength, HPImg.fillAmount, elapsedTime / fade_timer_time); // Smooth transition
            yield return null; // Wait for the next frame
        }

        HPEffect.fillAmount = HPImg.fillAmount; // Ensure final value is set
    }

    /// <summary>
    /// Coroutine to show the death panel after a specified delay and stop the game.
    /// </summary>
    /// <param name="delay">The delay before showing the death panel.</param>
    /// <returns>IEnumerator for coroutine.</returns>
    private IEnumerator ShowDeathPanelAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Wait for the specified delay
        DeathPanel.SetActive(true); // Show the death panel
        Time.timeScale = 0; // Pause the game
        Debug.Log("2s done"); // Log message for debugging
    }

    /// <summary>
    /// Sets the health to a specified value (alias for SetHealth).
    /// </summary>
    /// <param name="health">The new health value.</param>
    public void SetHp(float health)
    {
        SetHealth(health); // Alias for SetHealth method
    }

    /// <summary>
    /// Updates the maximum health based on the current strength value.
    /// </summary>
    private void UpdateMaxHp()
    {
        MaxHp = 20 * strength; // Calculate maximum health
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp); // Clamp current health within valid range
        UpdateHealthBar(); // Update the health bar display
    }

    /// <summary>
    /// Sets the strength value and updates the strength and health bars.
    /// </summary>
    /// <param name="newStrength">The new strength value.</param>
    public void SetStrength(int newStrength)
    {
        strength = Mathf.Clamp(newStrength, 1, 5); // Clamp strength within valid range
        UpdateMaxHp(); // Update maximum health based on new strength
        UpdateStrengthBar(); // Update the strength bar display
    }

    /// <summary>
    /// Updates the strength bar and starts the strength effect coroutine.
    /// </summary>
    private void UpdateStrengthBar()
    {
        StrengthImg.fillAmount = strength / 5f; // Update strength bar fill amount

        if (strength_coroutine != null)
        {
            StopCoroutine(strength_coroutine); // Stop any ongoing strength effect coroutine
        }

        strength_coroutine = StartCoroutine(UpdateStrengthEffect()); // Start new strength effect coroutine
    }

    /// <summary>
    /// Coroutine to smoothly update the strength effect image.
    /// </summary>
    /// <returns>IEnumerator for coroutine.</returns>
    public IEnumerator UpdateStrengthEffect()
    {
        float effectLength = StrengthEffect.fillAmount - StrengthImg.fillAmount; // Calculate effect length
        float elapsedTime = 0f; // Timer for coroutine

        while (elapsedTime < fade_timer_time && effectLength != 0)
        {
            elapsedTime += Time.deltaTime; // Increment timer
            StrengthEffect.fillAmount = Mathf.Lerp(StrengthImg.fillAmount + effectLength, StrengthImg.fillAmount, elapsedTime / fade_timer_time); // Smooth transition
            yield return null; // Wait for the next frame
        }

        StrengthEffect.fillAmount = StrengthImg.fillAmount; // Ensure final value is set
    }
}
