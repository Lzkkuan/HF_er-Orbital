using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    public void OnHit(int damage, Vector2 knockback);
}
