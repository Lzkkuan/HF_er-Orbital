using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Vector3 position;
    private int attackPower;
    public int knockbackForce;

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
    
    private void OnTriggerEnter2D(Collider2D collider) {
        Damageable damageable = collider.GetComponent<Damageable>();
        if (damageable != null) {
            Vector3 _position = transform.parent.position;
            Vector2 direction = collider.transform.position - _position;

            attackPower = 20;
            damageable.OnHit(attackPower, direction * knockbackForce);
        }
    }
    void Update()
    {
        
    }
}
