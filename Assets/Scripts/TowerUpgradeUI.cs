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
        //animator.SetTrigger("TowerSelectOpen");
        animator.SetBool("OpenTowerSelect", true);
    }

    public void CloseUI()
    {
        //animator.SetTrigger("TowerSelectClose");
        animator.SetBool("CloseTowerSelect", true);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenAnimationFinished()
    {
        animator.SetBool("OpenTowerSelect", false);
    }

    public void CloseAnimationFinished()
    {
        animator.SetBool("CloseTowerSelect", false);
    }
}
