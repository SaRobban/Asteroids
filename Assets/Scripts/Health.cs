using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healt = 10;
    [SerializeField] private float armor = 1;

    public void AddDamage(float dmg)
    {
        dmg -= armor;
        if (dmg > 0)
            healt -= dmg;

        CheckDeath();
    }

    public void CheckDeath()
    {
        if (healt < 0)
            Destroy(gameObject);
    }
}
