using UnityEngine;

/// <summary>
/// Updates the size and offset of the BoxCollider2D based on the sprite's bounds.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DynamicCollider : MonoBehaviour
{
    /// <summary>
    /// The BoxCollider2D component attached to the GameObject.
    /// </summary>
    public BoxCollider2D boxCollider;

    /// <summary>
    /// The SpriteRenderer component attached to the GameObject.
    /// </summary>
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateCollider();
    }

    void Update()
    {
        UpdateCollider();
    }

    /// <summary>
    /// Updates the size and offset of the BoxCollider2D based on the bounds of the sprite.
    /// </summary>
    void UpdateCollider()
    {
        if (spriteRenderer.sprite == null)
            return;

        // Get the bounds of the sprite
        Bounds spriteBounds = spriteRenderer.sprite.bounds;

        // Update the size of the BoxCollider2D
        boxCollider.size = spriteBounds.size;

        // Update the offset of the BoxCollider2D
        boxCollider.offset = spriteBounds.center;
    }
}
