using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnImpact : MonoBehaviour
{

    [SerializeField] private float minHitSpeed = 1;
    [SerializeField] private Health healthScript;
    private void OnEnable()
    {
        if (healthScript == null)
            healthScript = GetComponent<Health>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health myHealth))
        {
            myHealth.AddDamage(collision.relativeVelocity.sqrMagnitude);
            Debug.Log(collision.relativeVelocity.sqrMagnitude);
        }
        if (collision.otherCollider.TryGetComponent(out Health otherHealth))
        {
            otherHealth.AddDamage(collision.relativeVelocity.sqrMagnitude);
        }
    }
}
