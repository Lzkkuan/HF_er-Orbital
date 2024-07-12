using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
       position = transform.localPosition; 
    }

    // Update is called once per frame
    void IsFacingRight(bool isFacingRight) {
        if (isFacingRight) {
            transform.localPosition = position;
        }
        else {
            transform.localPosition = new Vector3(-position.x, position.y, position.z);
        }
    }
    
    void Update()
    {
        
    }
}
