using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, Damageable
{
    Rigidbody2D rb;
    Collider2D physicsCollider;
    public int health;
    public HealthBarEnemy healthBar; // Reference to the health bar

    // Start is called before the first frame update
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (healthBar != null)
            {
                healthBar.SetHp(health); // Update the health bar
            }
            if (health <= 0)
            {
                gameObject.BroadcastMessage("OnDeath");
                Targetable = false;
            }
            else
            {
                gameObject.BroadcastMessage("OnIrritation");
            }
        }
    }

    bool targetable;
    public bool Targetable
    {
        get
        {
            return targetable;
        }
        set
        {
            targetable = value;
            if (!targetable)
            {
                rb.simulated = false;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetHp(health); // Initialize the health bar
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
    }

    public void onDestruction()
    {
        Destroy(gameObject);
    }
}
