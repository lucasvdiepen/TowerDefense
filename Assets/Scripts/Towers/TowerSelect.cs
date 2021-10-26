using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    private TowerRange towerRangeScript;

    private void Start()
    {
        towerRangeScript = GetComponent<TowerRange>();
    }

    public void Select()
    {
        towerRangeScript.Show();

        //Show upgrade popup
        FindObjectOfType<TowerUpgradeUI>().OpenUI();
    }

    public void Deselect()
    {
        towerRangeScript.Hide();

        //Hide upgrade popup
        FindObjectOfType<TowerUpgradeUI>().CloseUI();
    }
}
