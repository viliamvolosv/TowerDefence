using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public int StartHp = 10;
    private int _currentHp;

    public UnityAction<Damageable> OnChangeHpAction;

    public int CurrentHp
    {
        get { return _currentHp; }
        private set
        {
            _currentHp = value;
            if (OnChangeHpAction != null)
                OnChangeHpAction(this);
        }
    }

    private void Start()
    {
        CurrentHp = StartHp;
    }

    public void TakeDamage(int damage)
    {
        CurrentHp = CurrentHp - damage;
    }
}