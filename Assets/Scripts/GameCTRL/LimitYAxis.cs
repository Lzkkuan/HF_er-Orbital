using UnityEngine;
using System.Collections;

public class LimitYAxis : MonoBehaviour
{
    private float originalY; // To store the original Y-axis value
    private bool limitActive = false; // To check if the limit should be active

    void Start()
    {
        // Start the coroutine to wait for 5 seconds
        StartCoroutine(ActivateLimitAfterDelay(5f));
    }

    void Update()
    {
        // Check if the limit is active
        if (limitActive)
        {
            // If the object's Y position exceeds the original Y position, reset it
            if (transform.position.y > originalY)
            {
                Vector3 position = transform.position;
                position.y = originalY;
                transform.position = position;
            }
        }
    }

    // Coroutine to wait for a specified amount of time before activating the limit
    private IEnumerator ActivateLimitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        limitActive = true;

        // Store the original Y-axis value when the script starts
        originalY = transform.position.y;
    }
}
