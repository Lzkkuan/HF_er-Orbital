using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's attack mechanics, including knockback and facing direction.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    private Vector3 position;
    private int attackPower;

    /// <summary>
    /// The force applied for knockback when attacking.
    /// </summary>
    public int knockbackForce;

    /// <summary>
    /// Initializes the position of the player's attack.
    /// </summary>
    void Start()
    {
        position = transform.localPosition;
    }

    /// <summary>
    /// Updates the player's attack position based on the facing direction.
    /// </summary>
    /// <param name="isFacingRight">Indicates if the player is facing right.</param>
    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            transform.localPosition = position;
        }
        else
        {
            transform.localPosition = new Vector3(-position.x, position.y, position.z);
        }
    }

    /// <summary>
    /// Handles collision with damageable objects and applies damage and knockback.
    /// </summary>
    /// <param name="collider">The Collider2D component of the object being hit.</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Damageable damageable = collider.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector3 _position = transform.parent.position;
            Vector2 direction = collider.transform.position - _position;

            attackPower = 20;
            damageable.OnHit(attackPower, direction * knockbackForce);
        }
    }

    void Update()
    {
        // Update logic (if any) should be placed here.
    }
}
