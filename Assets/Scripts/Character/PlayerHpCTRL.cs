using UnityEngine;

/// <summary>
/// Represents a damageable character in the game, managing health, targetability, and interactions.
/// </summary>
public class PlayerHpCTRL : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D physicsCollider;

    /// <summary>
    /// The current health of the character.
    /// </summary>
    public int health;

    /// <summary>
    /// Reference to the health bar UI element.
    /// </summary>
    public HealthBar healthBar;

    /// <summary>
    /// Gets or sets the health of the character. Updates the health bar and handles death or irritation events.
    /// </summary>
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (healthBar != null)
            {
                healthBar.SetHp(health);
                Debug.Log("health is:" + health);
            }
            if (health <= 0)
            {
                gameObject.BroadcastMessage("OnDeath");
                Targetable = false;
            }
        }
    }

    private bool targetable;

    /// <summary>
    /// Gets or sets whether the character is targetable. Disables Rigidbody2D simulation when not targetable.
    /// </summary>
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

    /// <summary>
    /// Applies damage to the character and applies knockback force.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    /// <param name="knockback">The knockback force to apply to the character.</param>
    public void OnHit(int damage, Vector2 knockback)
    {
        Debug.Log("damage hit");
        Health -= damage;
        rb.AddForce(knockback);
    }

    /// <summary>
    /// Destroys the game object.
    /// </summary>
    public void OnDestruction()
    {
        Destroy(gameObject);
    }
}
