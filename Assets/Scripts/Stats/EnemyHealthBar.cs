using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Image HealthImg;
    public Image HealthEffect;
    public Transform enemyTransform;  // Reference to the enemy's transform
    public Vector3 offset;            // Offset to position the health bar above the enemy

    public float MaxHealth = 100f;
    public float currentHealth;
    public float fade_timer = 0.5f;
    private Coroutine Hpcoroutine;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = MaxHealth;
        UpdateHp();
    }

    private void Update()
    {
        if (enemyTransform != null)
        {
            // Update the health bar position to follow the enemy
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(enemyTransform.position + offset);
            transform.position = screenPosition;
        }
    }

    public void SetHp(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, MaxHealth);
        UpdateHp();
        if (currentHealth <= 0)
        {
            // Handle enemy death
        }
    }

    public void IncreaseHealth(float amount)
    {
        SetHp(currentHealth + amount);
    }
    public void DecreaseHealth(float amount)
    {
        SetHp(currentHealth - amount);
    }

    private void UpdateHp()
    {
        HealthImg.fillAmount = currentHealth / MaxHealth;

        if (Hpcoroutine != null)
        {
            StopCoroutine(Hpcoroutine);
        }

        Hpcoroutine = StartCoroutine(UpdateHealthEffect());
    }

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
