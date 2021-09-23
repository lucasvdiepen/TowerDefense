using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    private int health = 100;

    private void Start()
    {
        health = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy taken damage: " + damage);
        health -= damage;
        if(health <= 0)
        {
            //Enemy died
        }
    }
}
