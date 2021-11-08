using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public void UpdateUI(float health)
    {
        if (health < 0) health = 0;
        healthText.text = health.ToString();
    }
}
