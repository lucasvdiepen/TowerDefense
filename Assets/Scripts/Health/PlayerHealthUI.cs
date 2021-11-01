using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public void UpdateUI(float health)
    {
        healthText.text = health.ToString();
    }
}
