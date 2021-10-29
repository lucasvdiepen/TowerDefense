using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public RectTransform panel;

    public Vector3 openPosition;
    public Vector3 closedPosition;

    private Vector3 panelPosition;

    public float animationSpeed = 1f;

    public Button rangeButton;
    public Button damageButton;

    public Button sellButton;

    public RawImage[] rangeUpgradePath;
    public RawImage[] damageUpgradePath;

    public Texture emptyUpgradePath;
    public Texture filledUpgradePath;

    public Color greyedOut;

    private void OnEnable()
    {
        rangeButton.onClick.AddListener(UpgradeRangeClicked);
        damageButton.onClick.AddListener(UpgradeDamageClicked);
        sellButton.onClick.AddListener(SellClicked);
    }

    private void OnDisable()
    {
        rangeButton.onClick.RemoveAllListeners();
        damageButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();
    }

    public void OpenUI()
    {
        UpdateUI();

        panelPosition = openPosition;
    }

    public void CloseUI()
    {
        panelPosition = closedPosition;
    }

    private void UpgradeRangeClicked()
    {
        TowerUpgrade currentTower = GetCurrentTower();
        if(currentTower != null)
        {
            currentTower.UpgradeRange();

            UpdateUI();
        }
    }

    private void UpgradeDamageClicked()
    {
        TowerUpgrade currentTower = GetCurrentTower();
        if (currentTower != null)
        {
            currentTower.UpgradeDamage();

            UpdateUI();
        }
    }

    private void SellClicked()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        panelPosition = closedPosition;
    }

    private void Update()
    {
        panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, panelPosition, animationSpeed * Time.deltaTime);
    }

    private TowerUpgrade GetCurrentTower()
    {
        return FindObjectOfType<TowerSelector>().GetCurrentTower().GetComponent<TowerUpgrade>();
    }
    private void UpdateUI()
    {
        TowerUpgrade currentTower = GetCurrentTower();

        //Tower image

        //Buttons
        if(currentTower.CanRangeUpgrade())
        {
            rangeButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            rangeButton.GetComponent<Image>().color = greyedOut;
        }

        if (currentTower.CanDamageUpgrade())
        {
            damageButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            damageButton.GetComponent<Image>().color = greyedOut;
        }

        //Upgrade path
        int rangeUpgradeCount = currentTower.GetRangeUpgradeCount();

        for(int i = 0; i < rangeUpgradePath.Length; i++)
        {
            if (i <= (rangeUpgradeCount - 1)) rangeUpgradePath[i].texture = filledUpgradePath;
            else rangeUpgradePath[i].texture = emptyUpgradePath;
        }

        int damageUpgradeCount = currentTower.GetDamageUpgradeCount();

        for (int i = 0; i < damageUpgradePath.Length; i++)
        {
            if (i <= (damageUpgradeCount - 1)) damageUpgradePath[i].texture = filledUpgradePath;
            else damageUpgradePath[i].texture = emptyUpgradePath;
        }
    }
}
