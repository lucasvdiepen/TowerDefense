using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    public int startingGold = 500;

    private int gold = 0;

    private void Start()
    {
        gold = startingGold;

        UpdateUI();
    }

    public void UpdateUI()
    {
        goldText.text = "$" + gold;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public bool Purchase(int amount)
    {
        if(gold >= amount)
        {
            gold -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }
}
