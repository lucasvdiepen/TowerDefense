using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreviewFade : MonoBehaviour
{
    public Material towerPreviewMaterial;

    private float timeElapsed = 0;

    public float lowestAlpha = 0.6f;
    public float highestAlpha = 0.8f;

    private float lowestAlphaTemp = 0;
    private float highestAlphaTemp = 0;

    private bool isFading = false;

    public float alphaChangeTime = 1f;

    private void Start()
    {
        lowestAlphaTemp = lowestAlpha;
        highestAlphaTemp = highestAlpha;
    }

    private void Update()
    {
        PreviewTowerAnimation();
    }

    public void StartFade()
    {
        isFading = true;
    }

    public void StopFade()
    {
        isFading = false;
    }

    private void PreviewTowerAnimation()
    {
        if (isFading)
        {
            towerPreviewMaterial.color = new Color(towerPreviewMaterial.color.r, towerPreviewMaterial.color.g, towerPreviewMaterial.color.b, Mathf.Lerp(lowestAlphaTemp, highestAlphaTemp, timeElapsed / alphaChangeTime));

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= alphaChangeTime)
            {
                //Switch lowest and highest
                float tmp = lowestAlphaTemp;
                lowestAlphaTemp = highestAlphaTemp;
                highestAlphaTemp = tmp;

                timeElapsed = 0;
            }
        }
    }
}
