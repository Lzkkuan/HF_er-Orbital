using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    DetectionZone dz;
    Animator animator;
    public float speed;
    public float attackRange; // 攻击范围
    public int damage; // 攻击伤害
    public float attackCooldown = 1f; // 攻击冷却时间
    private float lastAttackTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dz = GetComponent<DetectionZone>(); // 获取子对象上的DetectionZone组件
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown; // 初始化为可以立即攻击
    }

    private void FixedUpdate()
    {
        if (dz.detectedObj != null)
        {
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

    private void AttackPlayer()
    {
        animator.SetBool("isAttacking", true);
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            if (dz.detectedObj != null)
            {
                HealthBar healthBar = dz.detectedObj.GetComponentInChildren<HealthBar>();
                if (healthBar != null)
                {
                    healthBar.DecreaseHp(damage);
                }
                else
                {
                    Debug.LogWarning("HealthBar component not found on detected object.");
                }
            }
            else
            {
                Debug.LogWarning("Detected object is null.");
            }
        }
    }


    public void OnWalk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    public void OnWalkStop()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    void OnIrritation()
    {
        animator.SetTrigger("isIrritated");
    }

    void OnDeath()
    {
        animator.SetTrigger("isDead");
    }
}