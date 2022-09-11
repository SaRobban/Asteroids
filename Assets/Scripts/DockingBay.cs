using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingBay : MonoBehaviour
{
    Rigidbody2D otherRb;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Vector2.Dot(transform.up, collision.otherCollider.transform.up) > 0.707f)
        {
            //And both are dockeble
            Debug.Log("Dock");
            if (otherRb == null)
            {
                if (collision.collider.transform.parent.TryGetComponent(out Rigidbody2D rb))
                {
                    otherRb = rb;
                }
            }
            else
            {
                Vector2 f = (transform.position - collision.transform.position ).normalized * 10;
                otherRb.AddForceAtPosition(f, collision.transform.position);
                Debug.DrawRay(collision.transform.position, f, Color.green);
            }
        }

    }
}
