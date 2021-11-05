using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private PlayerHealthUI playerHealthUI;

    private void Start()
    {
        playerHealthUI = FindObjectOfType<PlayerHealthUI>();

        playerHealthUI.UpdateUI(CurrentHealth);
    }

    protected override void HandleDamage()
    {
        base.HandleDamage();

        playerHealthUI.UpdateUI(CurrentHealth);
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();

        Debug.Log("Laat nu het game over scherm zien");

        FindObjectOfType<LoseScreen>().ShowLoseScreen();
    }
}
