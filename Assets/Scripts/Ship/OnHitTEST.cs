using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitTEST : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.transform.name + " other : " + collision.otherCollider.transform.parent.name);

        if (collision.otherCollider.transform.parent.TryGetComponent(out Health health))
        {
            health.AddDamage(collision.relativeVelocity.sqrMagnitude);
        }
    }
}

