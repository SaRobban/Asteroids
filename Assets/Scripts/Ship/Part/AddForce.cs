using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private float force = 10;
    // Start is called before the first frame update
    void Start()
    {
        ShipComponent shipComponent = GetComponent<ShipComponent>();
        shipComponent.A_OnKey += Fire;
    }

    void Fire()
    {
        GameMaster.instance.shipMaster.rootRB.AddForceAtPosition(transform.up*force, transform.position);
    }
}
