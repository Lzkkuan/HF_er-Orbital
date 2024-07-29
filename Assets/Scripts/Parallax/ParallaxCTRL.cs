using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the parallax effect for background elements, creating a sense of depth as the camera moves.
/// </summary>
public class ParallaxCTRL : MonoBehaviour
{
    private float length; // Length of the sprite, used to calculate the repeating effect
    private float startpos; // Initial position of the sprite
    public GameObject cam; // Reference to the camera
    public float parallaxEffect; // The intensity of the parallax effect

    private void Start()
    {
        // Initialize the starting position and length of the sprite
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Calculate sprite length
    }

    private void Update()
    {
        // Calculate the new position based on the camera's position and parallax effect
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x) * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // Adjust the starting position if the sprite has moved past the camera's view
        // If the right border fades out from the left, reset the start position
        if (temp > startpos + length)
        {
            startpos += length;
        }
        // If the left border fades out from the right, reset the start position
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
