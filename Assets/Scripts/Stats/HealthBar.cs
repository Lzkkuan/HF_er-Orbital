using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HPImg;
    public Image HPEffect;

    public Image StrengthImg;
    public Image StrengthEffect;

    public Animator PlayerAnimator;
    public GameObject DeathPanel;

    public float MaxHp;
    public float currentHp;
    public float fade_timer_time = 0.5f;
    private Coroutine damage_coroutine;
    private Coroutine strength_coroutine;

    [Range(1, 5)]
    public float strength = 1;

    // Start is called before the first frame update
    private void Start()
    {
        UpdateMaxHp();
        currentHp = MaxHp;
        DeathPanel.SetActive(false);
        UpdateHealthBar();
        UpdateStrengthBar();
    }

    public void SetHealth(float health)
    {
        currentHp = Mathf.Clamp(health, 0f, MaxHp);
        UpdateHealthBar();
        if (currentHp <= 0)
        {
            Debug.Log("start 2s");
            PlayerAnimator.SetBool("IsDead", true);
            StartCoroutine(ShowDeathPanelAfterDelay(2f));
        }
    }

    public void IncreaseHp(float amount)
    {
        SetHealth(currentHp + amount);
    }

    public void DecreaseHp(float amount)
    {
        SetHealth(currentHp - amount);
    }

    private void UpdateHealthBar()
    {
        HPImg.fillAmount = currentHp / MaxHp;

        if (damage_coroutine != null)
        {
            StopCoroutine(damage_coroutine);
        }

        damage_coroutine = StartCoroutine(UpdateHpEffect());
    }

    public IEnumerator UpdateHpEffect()
    {
        float effectLength = HPEffect.fillAmount - HPImg.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < fade_timer_time && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            HPEffect.fillAmount = Mathf.Lerp(HPImg.fillAmount + effectLength, HPImg.fillAmount, elapsedTime / fade_timer_time);
            yield return null;
        }

        HPEffect.fillAmount = HPImg.fillAmount;
    }

    private IEnumerator ShowDeathPanelAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Wait for the specified delay
        DeathPanel.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("2s done");
    }

    public void SetHp(float health)
    {
        SetHealth(health);
    }

    private void UpdateMaxHp()
    {
        MaxHp = 20 * strength;
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
        UpdateHealthBar();
    }

    public void SetStrength(int newStrength)
    {
        strength = Mathf.Clamp(newStrength, 1, 5);
        UpdateMaxHp();
        UpdateStrengthBar();
    }

    private void UpdateStrengthBar()
    {
        StrengthImg.fillAmount = strength / 5f;

        if (strength_coroutine != null)
        {
            StopCoroutine(strength_coroutine);
        }

        strength_coroutine = StartCoroutine(UpdateStrengthEffect());
    }

    public IEnumerator UpdateStrengthEffect()
    {
        float effectLength = StrengthEffect.fillAmount - StrengthImg.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < fade_timer_time && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            StrengthEffect.fillAmount = Mathf.Lerp(StrengthImg.fillAmount + effectLength, StrengthImg.fillAmount, elapsedTime / fade_timer_time);
            yield return null;
        }

        StrengthEffect.fillAmount = StrengthImg.fillAmount;
    }
}
