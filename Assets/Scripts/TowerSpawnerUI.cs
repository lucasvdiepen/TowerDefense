using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnerUI : MonoBehaviour
{
    public RectTransform panel;
    public RectTransform arrow;

    public Button arrowButton;

    public Vector3 openPosition;
    public Vector3 closePosition;

    public float animationSpeed = 1f;

    private Vector3 panelPosition;
    private bool isOpen = false;

    private float arrowOpenRotation = 0f;
    private float arrowCloseRotation = 180f;

    private float arrowRotation = 0;

    private void OnEnable()
    {
        arrowButton.onClick.AddListener(ToggleUI);   
    }

    private void OnDisable()
    {
        arrowButton.onClick.RemoveAllListeners();
    }

    public void OpenUI()
    {
        panelPosition = openPosition;
        arrowRotation = arrowCloseRotation;
        isOpen = true;
    }

    public void CloseUI()
    {
        panelPosition = closePosition;
        arrowRotation = arrowOpenRotation;
        isOpen = false;
    }

    public void ToggleUI()
    {
        if (isOpen) CloseUI();
        else OpenUI();
    }

    private void Start()
    {
        OpenUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) ToggleUI();

        panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, panelPosition, animationSpeed * Time.deltaTime);
        arrow.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(arrow.eulerAngles.z, arrowRotation, animationSpeed * Time.deltaTime)));
    }
}
