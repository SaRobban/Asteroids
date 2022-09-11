using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    public bool posW;
    public bool posS;
    public bool posA;
    public bool posD;
    public bool posE;
    public bool posQ;

    public bool negW;
    public bool negS;
    public bool negA;
    public bool negD;

    [SerializeField] private float force = 10;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        fire.SetActive(false);
        if (posW)
        {
            if (Input.GetKey(KeyCode.W))
            {
                OnFire();
            }
        }
        if (posS)
        {
            if (Input.GetKey(KeyCode.S))
            {
                OnFire();
            }
        }
        if (posA)
        {
            if (Input.GetKey(KeyCode.A))
            {
                OnFire();
            }
        }
        if (posD)
        {
            if (Input.GetKey(KeyCode.D))
            {
                OnFire();
            }
        }

        if (posQ)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                OnFire();
            }
        }

        if (posE)
        {
            if (Input.GetKey(KeyCode.E))
            {
                OnFire();
            }
        }

        //
        if (negW)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForceAtPosition(transform.up * -force, transform.position);
            }
        }
        if (negS)
        {
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForceAtPosition(transform.up * -force, transform.position);
            }
        }
        if (negA)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForceAtPosition(transform.up * -force, transform.position);
            }
        }
        if (negD)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForceAtPosition(transform.up * -force, transform.position);
            }
        }
    }
    // Update is called once per frame
    void OnFire()
    {
        rb.AddForceAtPosition(transform.up * force, transform.position);
        fire.SetActive(true);
    }
}
