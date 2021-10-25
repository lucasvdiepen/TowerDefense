using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectUI : MonoBehaviour
{
    private Animator animator;

    public void OpenUI()
    {
        animator.SetFloat("animationSpeed", 1f);
        animator.SetTrigger("TriggerTowerSelect");
    }

    public void CloseUI()
    {
        animator.SetFloat("animationSpeed", -1f);
        animator.SetTrigger("TriggerTowerSelect");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        OpenUI();
    }
}
