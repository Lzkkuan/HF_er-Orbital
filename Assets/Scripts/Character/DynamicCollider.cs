using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DynamicCollider : MonoBehaviour
{
    public BoxCollider2D boxCollider;
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
