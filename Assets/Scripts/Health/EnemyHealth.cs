using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected override void HandleDeath()
    {
        base.HandleDeath();

        GetComponent<EnemyGold>().GiveGold();

        FindObjectOfType<EnemySpawner>().DestroyEnemy(GetComponent<EnemyID>().GetID());
    }
}
