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

    /// <summary>
    /// The AudioSource for the normal music.
    /// </summary>
    public AudioSource normalMusic;

    /// <summary>
    /// The AudioSource for the bottle music.
    /// </summary>
    public AudioSource bottleMusic;

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if (collider != null && !IsSelfOrChild(collider.transform) && collider.CompareTag("Player"))
        {
            detectedObj = collider;
            if (normalMusic.isPlaying)
            {
                normalMusic.Stop();
                bottleMusic.Play();
            }
        }
        else
        {
            if (detectedObj != null)
            {
                detectedObj = null;
                if (bottleMusic.isPlaying)
                {
                    bottleMusic.Stop();
                    normalMusic.Play();
                }
            }
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

    /// <summary>
    /// Checks if the given transform belongs to the same GameObject or its children.
    /// </summary>
    /// <param name="transform">The transform to check.</param>
    /// <returns>True if the transform belongs to the same GameObject or its children, otherwise false.</returns>
    private bool IsSelfOrChild(Transform transform)
    {
        return transform == this.transform || transform.IsChildOf(this.transform);
    }
}
