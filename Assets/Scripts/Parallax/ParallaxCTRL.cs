using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCTRL : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        //initialize start position
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        //updating position
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x) * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // if right border fading out from left:
        if (temp > startpos + length){ startpos += length; }

        //if left border fading out from right:
        else if (temp < startpos - length){ startpos -= length; }
    }
}
