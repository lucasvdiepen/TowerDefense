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

    public Material towerPreviewMaterial;

    public Color redPreviewColor;
    public Color greenPreviewColor;

    public LayerMask spawnLayerMask;

    public float yOffset = 0.20f;
    
    [HideInInspector]
    public bool isSelected = false;

    private int selectedTowerId = -1;

    private GameObject previewTower;
    private GameObject selectedTile;

    private string previousButton = "";

    private TowerPreviewFade towerPreviewFade;

    private void Start()
    {
        towerPreviewFade = GetComponent<TowerPreviewFade>();
    }

    private void Update()
    {
        PreviewTower();

        if(Input.GetMouseButtonDown(0))
        {
            PlaceTower();

            Deselect();

            ClickTowerButton();
        }

        if(Input.GetMouseButtonUp(0))
        {
            GameObject buttonHit = CheckButtonHit();
            if (buttonHit != null)
            {
                if(buttonHit.transform.name != previousButton)
                {
                    Deselect();
                }
                else
                {
                    //Hide mouse follow image
                    FindObjectOfType<MouseFollow>().Deselect();
                }
            }
            else
            {
                PlaceTower();

                Deselect();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Deselect();
        }
    }

    private void DestroyPreviewTower()
    {
        if (previewTower != null)
        {
            selectedTile = null;

            towerPreviewFade.StopFade();

            Destroy(previewTower);
        }
    }

    private void SetPreviewColor(bool canBuild)
    {
        if(canBuild) towerPreviewMaterial.color = greenPreviewColor;
        else towerPreviewMaterial.color = redPreviewColor;
    }

    private bool CanBuild()
    {
        if(selectedTile != null)
        {
            return selectedTile.GetComponent<Tile>().CanBuild();
        }

        return false;
    }

    private void SpawnPreviewTower(int towerId, GameObject tile)
    {
        selectedTile = tile;

        SetPreviewColor(CanBuild());

        previewTower = Instantiate(previewTowers[selectedTowerId], new Vector3(tile.transform.position.x, tile.transform.position.y + yOffset, tile.transform.position.z), Quaternion.identity);

        towerPreviewFade.StartFade();
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

                        SpawnPreviewTower(selectedTowerId, tile);
                    }
                }
                else
                {
                    SpawnPreviewTower(selectedTowerId, tile);
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
            if(selectedTile != null)
            {
                selectedTile.GetComponent<Tile>().BuildTile(towers[selectedTowerId], yOffset);
            }
        }
    }

    private GameObject GetPointingTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, spawnLayerMask))
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

            previousButton = button.transform.name;

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
        FindObjectOfType<MouseFollow>().Deselect();
    }
}