using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Disappear", 2f); // Disappear after 2 seconds
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
    
}
