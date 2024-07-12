using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HPImg;
    public Image HPEffect;

    public float MaxHp = 100f; 
    public float currentHp;
    public float fade_timer_time = 0.5f;
    private Coroutine damage_coroutine;

    // Start is called before the first frame update
    private void Start()
    {
        currentHp = MaxHp;
        UpdateHealthBar();
    }


    public void SetHealth(float health)
    {
        currentHp = Mathf.Clamp(health, 0f, MaxHp);
        UpdateHealthBar();
        if(currentHp <= 0){
            //die
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
            HPEffect.fillAmount = Mathf.Lerp(HPImg.fillAmount + effectLength, HPImg.fillAmount, elapsedTime/fade_timer_time);
            yield return null;
        }

        HPEffect.fillAmount = HPImg.fillAmount;
    }
}
