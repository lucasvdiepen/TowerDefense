using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Spawnpoint,
        Waypoint,
        Path,
        Buildable,
        Endpoint,
        Default
    }

    public TileType tileType;

    private GameObject placedObject;

    public bool IsBuildable()
    {
        if (tileType == TileType.Buildable) return true;
        return false;
    }

    public bool CanBuild()
    {
        return IsBuildable() && placedObject == null;
    }

    public void SelectTile()
    {
        if (IsBuildable() && placedObject != null)
        {
            //Select tile and show tower upgrade path
            placedObject.GetComponent<TowerSelect>().Select();
        }
    }

    public GameObject GetPlacedObject()
    {
        return placedObject;
    }

    public void BuildTile(GameObject tower)
    {
        if (CanBuild())
        {
            //Check if player has enough gold
            if(FindObjectOfType<GoldManager>().Purchase(tower.GetComponent<TowerPrice>().GetBuildCost()))
            {
                //Build tile
                placedObject = Instantiate(tower, transform.position, Quaternion.identity);
            }
        }
    }
}
