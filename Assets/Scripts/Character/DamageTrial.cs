using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrial : MonoBehaviour
{
    public int damage = 30;
    public float attackCooldown = 1f; // 攻击冷却时间
    private float lastAttackTime;
    private Animator animator;

    private void Start()
    {
        lastAttackTime = -attackCooldown; // 初始化为可以立即攻击
        animator = GetComponentInParent<Animator>(); // 获取父对象上的Animator组件
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("isAttacking"); // 触发攻击动画
            StartCoroutine(PerformAttack(collision));
        }
    }

    private IEnumerator PerformAttack(Collider2D collision)
    {
        // 等待攻击动画的执行
        yield return new WaitForSeconds(0.5f); // 假设攻击动画持续0.5秒

        if (collision != null && collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<HealthBar>().DecreaseHp(damage);
        }
    }
}
