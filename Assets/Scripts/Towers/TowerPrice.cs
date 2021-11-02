using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPrice : MonoBehaviour
{
    public int buildCost = 100;

    public int GetBuildCost()
    {
        return buildCost;
    }

    public int GetSellPrice()
    {
        return buildCost / 2;
    }
}
