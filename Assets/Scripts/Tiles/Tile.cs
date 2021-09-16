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

    public bool IsBuildable()
    {
        if (tileType == TileType.Buildable) return true;

        return false;
    }
}
