using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    private int rangeUpgrade = 0;
    private int damageUpgrade = 0;

    public int maxUpgrade = 4;

    public int GetRangeUpgradeCount()
    {
        return rangeUpgrade;
    }

    public int GetDamageUpgradeCount()
    {
        return damageUpgrade;
    }

    public bool CanRangeUpgrade()
    {
        if (rangeUpgrade >= maxUpgrade) return false;
        return true;
    }

    public bool CanDamageUpgrade()
    {
        if (damageUpgrade >= maxUpgrade) return false;
        return true;
    }

    public void UpgradeRange()
    {
        //Check if player has enough gold

        if(CanRangeUpgrade())
        {
            rangeUpgrade++;
        }
    }

    public void UpgradeDamage()
    {
        //Check if player has enough gold

        if(CanDamageUpgrade())
        {
            damageUpgrade++;
        }
    }
}
