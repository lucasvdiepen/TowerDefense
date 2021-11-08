using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int minDamage = 3;
    public int maxDamage = 7;

    public void DealDamage()
    {
        FindObjectOfType<PlayerHealth>().TakeDamage(Random.Range(minDamage, maxDamage));
    }
}
