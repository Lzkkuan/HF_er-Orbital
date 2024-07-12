using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, Damageable
{
    Rigidbody2D rb;
    Collider2D physicsCollider;
    public int health;

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
            if (health <= 0) {
                gameObject.BroadcastMessage("OnDeath");
                Targetable = false;
            }
            else {
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
            if (!targetable) {
                rb.simulated = false;
            }

        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void OnHit(int damage, Vector2 knockback) {
        Health -= damage;
        rb.AddForce(knockback);
    }

    public void onDestruction() {
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
