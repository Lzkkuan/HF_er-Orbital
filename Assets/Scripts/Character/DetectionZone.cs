using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D detectedObj;
    public float viewRadius;
    public LayerMask playerLayerMask;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Collider2D collider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
       if (collider != null) {
        detectedObj = collider;
       }
       
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
