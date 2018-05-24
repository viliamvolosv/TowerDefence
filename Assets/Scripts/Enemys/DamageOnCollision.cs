using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOnCollision : MonoBehaviour
{
    public int DamageAmmount = 1;

    protected void OnCollisionEnter(Collision c)
    {
        Debug.Log("OnCollisionEnter");
        var damageable = c.gameObject.GetComponent<Damageable>();
        if (damageable == null)
        {
            return;
        }

        damageable.TakeDamage(DamageAmmount);
    }
}