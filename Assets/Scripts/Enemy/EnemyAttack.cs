using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 5;

    public void DealDamage()
    {
        FindObjectOfType<PlayerHealth>().TakeDamage(damage);
    }
}
