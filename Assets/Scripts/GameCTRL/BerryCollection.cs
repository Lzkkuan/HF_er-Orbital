using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the collection of berries by the player and interacts with the CollectionManager and HealthBar.
/// </summary>
public class BerryCollection : MonoBehaviour
{
    private CollectionManager collectionManager; // Reference to the CollectionManager
    private DetectionZone dz; // Reference to the DetectionZone component
    private bool playerInRange; // Indicates if the player is within range

    /// <summary>
    /// Initializes references and states.
    /// </summary>
    void Start()
    {
        dz = GetComponent<DetectionZone>();
        playerInRange = false;
        collectionManager = FindObjectOfType<CollectionManager>();
    }

    /// <summary>
    /// Checks if the player is in range and handles berry collection on space key press.
    /// </summary>
    void Update()
    {
        // Update playerInRange based on DetectionZone
        if (dz.detectedObj != null)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        // Collect the berry if space key is pressed and the player is in range
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Destroy(gameObject); // Destroy the berry object
            collectionManager.ShowCollectionMessageAtPosition(transform.position); // Show collection message

            // Increase health of the detected object if it has a HealthBar component
            HealthBar healthBar = dz.detectedObj.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                healthBar.IncreaseHp(10); // Increase health by 10
            }
            else
            {
                Debug.LogWarning("HealthBar component not found on detected object.");
            }
        }
    }
}
