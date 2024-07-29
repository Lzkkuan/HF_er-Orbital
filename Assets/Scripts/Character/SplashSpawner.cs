using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject splashPrefab; ///< The prefab for the splash effect.
    public GameObject sealPrefab; ///< The prefab for the seal object.
    public Vector2 waterAreaMin; ///< The minimum coordinates of the water area.
    public Vector2 waterAreaMax; ///< The maximum coordinates of the water area.

    private GameObject currentSplash; ///< The currently active splash object.
    private Vector2 splashPosition; ///< The position where the splash will be instantiated.

    /// <summary>
    /// Starts the process of spawning splashes at regular intervals.
    /// </summary>
    private void Start()
    {
        InvokeRepeating("SpawnSplash", 0f, 3f); // Adjust the repeat rate as needed
    }

    /// <summary>
    /// Spawns a splash at a random position within the defined water area.
    /// </summary>
    void SpawnSplash()
    {
        if (currentSplash == null)
        {
            splashPosition = new Vector2(
                Random.Range(waterAreaMin.x, waterAreaMax.x),
                Random.Range(waterAreaMin.y, waterAreaMax.y)
            );

            currentSplash = Instantiate(splashPrefab, splashPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// Checks for right-click input to spawn a seal and remove the current splash.
    /// </summary>
    private void Update()
    {
        if (currentSplash != null && Input.GetMouseButtonDown(1)) // Right-click
        {
            Instantiate(sealPrefab, splashPosition, Quaternion.identity);
            Destroy(currentSplash); // Remove splash after spawning the seal
        }
    }
}
