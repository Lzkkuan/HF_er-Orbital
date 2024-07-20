using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
      animator = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void OnIrritation() {
        animator.SetTrigger("isIrritated");
    }

    void OnDeath() {
        animator.SetTrigger("isDead");
    }
    
    void Update()
    {
        
    }
}
