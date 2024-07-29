using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles detection of objects within a specified radius, specifically detecting player objects.
/// </summary>
public class DetectionZone : MonoBehaviour
{
    /// <summary>
    /// The detected object within the detection zone.
    /// </summary>
    public Collider2D detectedObj;

    /// <summary>
    /// The radius within which objects are detected.
    /// </summary>
    public float viewRadius;

    /// <summary>
    /// The layer mask used to filter which objects can be detected.
    /// </summary>
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

    /// <summary>
    /// Draws a wireframe sphere in the editor to visualize the detection zone.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
