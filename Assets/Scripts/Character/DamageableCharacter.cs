using UnityEngine;

public class DamageableCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D physicsCollider;
    public int health;
    public HealthBar healthBar;

    // Start is called before the first frame update
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (healthBar != null)
            {
                healthBar.SetHp(health);
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

    private bool targetable;
    public bool Targetable
    {
        get { return targetable; }
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
            healthBar.SetHp(health);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnDestruction()
    {
        Destroy(gameObject);
    }
}
