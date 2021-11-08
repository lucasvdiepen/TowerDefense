using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWavesUI : MonoBehaviour
{
    public TextMeshProUGUI wavesText;
    public TextMeshProUGUI wavesToWinText;

    public void SetWavesText(int waves)
    {
        wavesText.text = "Waves: " + (waves + 1);
    }

    public void SetWavesToWinText(int waves)
    {
        wavesToWinText.text = "Waves to win: " + waves;
    }
}
