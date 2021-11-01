using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGold : MonoBehaviour
{
    public int gold = 10;

    public void GiveGold()
    {
        FindObjectOfType<GoldManager>().AddGold(gold);
    }
}
