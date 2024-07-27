using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCollection : MonoBehaviour
{
    // Start is called before the first frame update
    private CollectionManager collectionManager;
    DetectionZone dz;
    private bool playerInRange;
    void Start()
    {
      dz = GetComponent<DetectionZone>();  
      playerInRange = false;
      collectionManager = FindObjectOfType<CollectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dz.detectedObj != null)
        {
            playerInRange = true; 
        }
        else
        {
            playerInRange = false; 
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange == true) // Space key
        {
            Destroy(gameObject);
            collectionManager.ShowCollectionMessageAtPosition(transform.position);
            HealthBar healthBar = dz.detectedObj.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                healthBar.IncreaseHp(10);
            }
            else
            {
                Debug.LogWarning("HealthBar component not found on detected object.");
            }
        }
    }
}
