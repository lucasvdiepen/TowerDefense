using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth = 100;
    private float health = 100;

    private void Start()
    {
        health = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log("Enemy taken damage: " + damage);
        health -= damage;
        if(health <= 0)
        {
            //Enemy died
            FindObjectOfType<EnemySpawner>().DestroyEnemy(GetComponent<EnemyID>().GetID());
        }
    }
}
