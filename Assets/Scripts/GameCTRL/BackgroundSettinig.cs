using UnityEngine;

public class BackgroundSetting : MonoBehaviour
{
    public Sprite backgroundSprite;

    void Start()
    {
        GameObject background = new GameObject("Background");
        SpriteRenderer spriteRenderer = background.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = backgroundSprite;
        spriteRenderer.sortingLayerName = "Background"; // Ensure this sorting layer exists
        spriteRenderer.sortingOrder = -10;

        background.transform.position = new Vector3(0, 0, 10); // Adjust position as needed
    }
}
