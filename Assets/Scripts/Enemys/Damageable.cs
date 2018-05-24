using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int StartHp = 10;
    private int _currentHp;

    private void Start()
    {
        _currentHp = StartHp;
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
    }
}