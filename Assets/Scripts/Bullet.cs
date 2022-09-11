using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float timeOut = 1;

    public void Fire(Transform cannon)
    {
        transform.position = cannon.position;
        transform.rotation = cannon.rotation;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position += transform.up * step;

    }

    public void Raycast(float step)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, step);
        if (hit.collider == null)
            return;

        if (hit.collider.TryGetComponent(out DamageOnImpact doi))
        {

        }

        gameObject.SetActive(false);
    }
}
