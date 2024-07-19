using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class WaterTileAnimator : MonoBehaviour
{
    public Tilemap waterTilemap;
    public float shakeDuration = 0.5f;
    public float shakeStrength = 0.1f;
    public int shakeVibrato = 10;
    public float shakeRandomness = 90f;
    public TileBase waterTile; // The specific tile to be animated

    void Start()
    {
        if (waterTilemap == null)
        {
            Debug.LogError("Water Tilemap is not assigned.");
            return;
        }

        AnimateWaterTiles();
    }

    void AnimateWaterTiles()
    {
        BoundsInt bounds = waterTilemap.cellBounds;
        TileBase[] allTiles = waterTilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = waterTilemap.GetTile(tilePosition);

                if (tile != null && tile == waterTile)
                {
                    Vector3 worldPosition = waterTilemap.CellToWorld(tilePosition) + new Vector3(0.5f, 0.5f, 0);
                    Debug.Log($"Tile found at {tilePosition} (world position: {worldPosition})");

                    GameObject tileGameObject = new GameObject("WaterTile_" + x + "_" + y);
                    tileGameObject.transform.position = worldPosition;
                    tileGameObject.transform.SetParent(transform);

                    SpriteRenderer spriteRenderer = tileGameObject.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = waterTilemap.GetSprite(tilePosition);
                    spriteRenderer.sortingOrder = 1; // Adjust sorting order if necessary

                    // Shake the tile's position
                    tileGameObject.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, true)
                        .SetLoops(-1, LoopType.Restart);

                    // Optionally hide the original tile
                    waterTilemap.SetTile(tilePosition, null);
                }
            }
        }
    }
}
