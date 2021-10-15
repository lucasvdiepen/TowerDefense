using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    public Transform rangeImage;

    public void UpdateRangeImage(float range)
    {
        float newRangeScale = 1.293423f / 5f * range;
        rangeImage.localScale = new Vector3(newRangeScale, newRangeScale, newRangeScale);
    }

    public void Show()
    {
        rangeImage.gameObject.SetActive(true);
    }

    public void Hide()
    {
        rangeImage.gameObject.SetActive(false);
    }
}
