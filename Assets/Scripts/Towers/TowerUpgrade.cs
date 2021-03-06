using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public Texture towerImage;

    public float damageUpgradeAmount = 1f;
    public float rangeUpgradeAmount = 0.5f;

    public int rangeUpgradePrice = 100;
    public int damageUpgradePrice = 100;

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
        if(CanRangeUpgrade())
        {
            if(FindObjectOfType<GoldManager>().Purchase(rangeUpgradePrice))
            {
                rangeUpgrade++;

                GetComponent<Tower>().UpgradeRangeAmount(rangeUpgradeAmount);
            }
        }
    }

    public void UpgradeDamage()
    {
        if(CanDamageUpgrade())
        {
            if(FindObjectOfType<GoldManager>().Purchase(damageUpgradePrice))
            {
                damageUpgrade++;

                GetComponent<Tower>().UpgradeDamageAmount(damageUpgradeAmount);
            }
        }
    }

    public int GetSellPrice()
    {
        return GetComponent<TowerPrice>().GetSellPrice();
    }

    public void SellTower()
    {
        //Give gold
        FindObjectOfType<GoldManager>().AddGold(GetComponent<TowerPrice>().GetSellPrice());

        GetComponent<TowerSelect>().Deselect();

        Destroy(gameObject);
    }
}
