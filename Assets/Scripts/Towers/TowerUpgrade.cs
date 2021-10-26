using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    private int rangeUpgrade = 0;
    private int damageUpgrade = 0;

    public int GetRangeUpgradeCount()
    {
        return rangeUpgrade;
    }

    public int GetDamageUpgradeCount()
    {
        return damageUpgrade;
    }
}
