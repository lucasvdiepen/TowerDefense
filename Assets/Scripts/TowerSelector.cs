using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelector : MonoBehaviour
{
    public LayerMask uiLayer;

    private TowerSelect currentTowerScript;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!FindObjectOfType<TowerSpawner>().isSelected)
            {
                if(!CheckUIHit())
                {
                    DeselectTower();

                    currentTowerScript = GetTowerScript();

                    SelectTower();
                }
            }
        }
    }

    public void SelectTower()
    {
        if (currentTowerScript != null) currentTowerScript.Select();
    }

    public void DeselectTower()
    {
        if (currentTowerScript != null) currentTowerScript.Deselect();
    }

    private TowerSelect GetTowerScript()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.transform.CompareTag("Tile"))
            {
                GameObject placedObject = hitInfo.transform.gameObject.GetComponent<Tile>().GetPlacedObject();
                if (placedObject != null) return placedObject.GetComponent<TowerSelect>();
            }
            else if(hitInfo.transform.CompareTag("Tower"))
            {
                return hitInfo.transform.gameObject.GetComponent<TowerSelect>();
            }
        }

        return null;
    }

    private bool CheckUIHit()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0) return true;

        return false;
    }
}
