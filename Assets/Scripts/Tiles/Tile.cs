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
        Default
    }

    public TileType tileType;

    private GameObject placedObject;

    public bool IsBuildable()
    {
        if (tileType == TileType.Buildable) return true;
        return false;
    }

    public void SelectTile()
    {
        if(IsBuildable())
        {
            //Select tile and show tower upgrade path
        }
    }

    public void BuildTile()
    {
        if(IsBuildable())
        {
            //Build tile
        }
    }
}
