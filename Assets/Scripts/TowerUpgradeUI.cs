using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public Button rangeButton;
    public Button damageButton;

    public RawImage[] rangeUpgradePath;
    public RawImage[] damageUpgradePath;

    public Color greyedOut;

    private Animator animator;

    public void OpenUI()
    {
        animator.SetTrigger("TowerSelectOpen");
    }

    public void CloseUI()
    {
        animator.SetTrigger("TowerSelectClose");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
