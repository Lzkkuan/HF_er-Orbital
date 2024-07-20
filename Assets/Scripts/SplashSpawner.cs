using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject splashPrefab;
    public GameObject sealPrefab;
    public Vector2 waterAreaMin;
    public Vector2 waterAreaMax;

    private GameObject currentSplash;
    private Vector2 splashPosition;

    private void Start()
    {
        InvokeRepeating("SpawnSplash", 0f, 3f); // Adjust the repeat rate as needed
    }

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

    private void Update()
    {
        if (currentSplash != null && Input.GetMouseButtonDown(1)) // Right-click
        {
            Instantiate(sealPrefab, splashPosition, Quaternion.identity);
            Destroy(currentSplash); // Remove splash after spawning the seal
        }
    }
}
