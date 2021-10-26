using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectUI : MonoBehaviour
{
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
