using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth = 100;
    private float health = 100;

    private bool isDead = false;

    public float CurrentHealth 
    {
        get { return health; } 
    }

    private void Awake()
    {
        health = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        HandleDamage();

        if(health <= 0 && !isDead)
        {
            //Dead
            HandleDeath();
        }
    }

    protected virtual void HandleDamage()
    {

    }

    protected virtual void HandleDeath()
    {
        isDead = true;
    }
}
