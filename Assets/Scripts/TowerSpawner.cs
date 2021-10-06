using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    public GameObject[] towers;
    public GameObject[] previewTowers;
    public GameObject[] buttons;

    private bool isSelected = false;
    private int selectedTowerId = -1;

    private GameObject previewTower;

    private void Update()
    {
        PreviewTower();

        if(Input.GetMouseButtonDown(0))
        {
            ClickTowerButton();
        }

        if(Input.GetMouseButtonUp(0))
        {
            Deselect();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Deselect();
        }
    }

    private void DestroyPreviewTower()
    {
        if (previewTower != null) Destroy(previewTower);
    }

    private void SpawnPreviewTower(int towerId, Vector3 position)
    {
        previewTower = Instantiate(previewTowers[selectedTowerId], position, Quaternion.identity);
    }

    private void PreviewTower()
    {
        if(isSelected)
        {
            GameObject tile = GetPointingTile();

            if(tile != null)
            {
                if (previewTower != null)
                {
                    if (previewTower.transform.position != tile.transform.position)
                    {
                        DestroyPreviewTower();

                        SpawnPreviewTower(selectedTowerId, tile.transform.position);
                    }
                }
                else
                {
                    SpawnPreviewTower(selectedTowerId, tile.transform.position);
                }
            }
            else
            {
                DestroyPreviewTower();
            }
        }
        else
        {
            DestroyPreviewTower();
        }
    }

    private void PlaceTower()
    {
        if(isSelected)
        {
            
        }
    }

    private GameObject GetPointingTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if(hitInfo.transform.CompareTag("Tile"))
            {
                return hitInfo.transform.gameObject;
            }
        }

        return null;
    }

    private void ClickTowerButton()
    {
        GameObject button = CheckButtonHit();
        if(button != null)
        {
            isSelected = true;

            for(int i = 0; i < buttons.Length; i++)
            {
                if (button.transform.name == buttons[i].transform.name)
                {
                    selectedTowerId = i;
                    break;
                }
            }

            FindObjectOfType<MouseFollow>().Select(button.GetComponent<RawImage>().texture);
        }
    }

    private GameObject CheckButtonHit()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var result in raycastResults)
            {
                foreach (GameObject button in buttons)
                {
                    if (button.name == result.gameObject.transform.name)
                    {
                        return button.gameObject;
                    }
                }
            }
        }

        return null;
    }

    public void Deselect()
    {
        isSelected = false;
        selectedTowerId = -1;
    }
}
