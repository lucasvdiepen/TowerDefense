using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [HideInInspector] public bool isSelected = false;

    private TowerRange towerRangeScript;

    private void Start()
    {
        towerRangeScript = GetComponent<TowerRange>();
    }

    public void Select()
    {
        if(!isSelected)
        {
            isSelected = true;

            towerRangeScript.Show();

            //Show upgrade popup
            FindObjectOfType<TowerUpgradeUI>().OpenUI();
        }
    }

    public void Deselect()
    {
        towerRangeScript.Hide();

        //Hide upgrade popup
        FindObjectOfType<TowerUpgradeUI>().CloseUI();

        isSelected = false;
    }
}
