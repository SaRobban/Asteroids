using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControll : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float accSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTorque;
    [SerializeField] private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        AddForces();
    }

    void AddForces()
    {
        rb.AddForce(transform.up * accSpeed * InputManager.accelerate);
        // rb.AddTorque(turnSpeed * InputManager.turn);

        rb.AddForceAtPosition(transform.right * accSpeed * InputManager.turn, transform.position);

        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (Mathf.Abs(rb.angularVelocity) > maxTorque)
        {
            if (rb.angularVelocity < 0)
                rb.angularVelocity = -maxTorque;
            else
                rb.angularVelocity = maxTorque;
        }
    }
}
