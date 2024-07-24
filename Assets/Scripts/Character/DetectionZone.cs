using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public Collider2D detectedObj;
    public float viewRadius;
    public LayerMask playerLayerMask;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if (collider != null)
        {
            detectedObj = collider;
        }
        else
        {
            detectedObj = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
