using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOnCollision : MonoBehaviour
{
    public int DamageAmmount = 1;
    public bool DestroyAfterDamage = false;

    private void OnTriggerEnter(Collider c)
    {
        var damageable = c.gameObject.GetComponent<Damageable>();
        if (damageable == null)
        {
            return;
        }

        damageable.TakeDamage(DamageAmmount);
        if (DestroyAfterDamage)
        {
            Destroy(this.gameObject);
        }
    }
}