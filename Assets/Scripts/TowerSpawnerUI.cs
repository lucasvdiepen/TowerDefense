using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnerUI : MonoBehaviour
{
    public RectTransform panel;

    public Vector3 openPosition;
    public Vector3 closePosition;

    public float animationSpeed = 1f;

    private Vector3 panelPosition;
    private bool isOpen = false;

    public void OpenUI()
    {
        panelPosition = openPosition;
        isOpen = true;
    }

    public void CloseUI()
    {
        panelPosition = closePosition;
        isOpen = false;
    }

    public void ToggleUI()
    {
        if (isOpen) CloseUI();
        else OpenUI();
    }

    private void Start()
    {
        panelPosition = closePosition;
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) ToggleUI();

        panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, panelPosition, animationSpeed * Time.deltaTime);
    }
}
