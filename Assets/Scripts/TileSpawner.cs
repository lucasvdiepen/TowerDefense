using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tile;

public class TileSpawner : MonoBehaviour
{
    public GameObject pathTile;
    public GameObject waypointTile;
    public GameObject spawnpointTile;

    public enum TileDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }

    private Vector2[] vectorDirections = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    private class Tile
    {
        public Vector2 position { get; private set; }
        public Vector2 rotation { get; set; }
        public TileType tileType { get; private set; }

        public Tile(Vector2 _position, TileType _tileType)
        {
            position = _position;
            tileType = _tileType;
        }
    }

    private List<Tile> tiles = new List<Tile>();

    private void Start()
    {
        //Test path
        tiles.Add(new Tile(new Vector2(-1, 0), TileType.Spawnpoint));
        tiles.Add(new Tile(new Vector2(0, 0), TileType.Path));
        tiles.Add(new Tile(new Vector2(1, 0), TileType.Path));
        tiles.Add(new Tile(new Vector2(2, 0), TileType.Waypoint));
        tiles.Add(new Tile(new Vector2(2, 1), TileType.Waypoint));
        tiles.Add(new Tile(new Vector2(3, 1), TileType.Path));
        tiles.Add(new Tile(new Vector2(4, 1), TileType.Waypoint));
        tiles.Add(new Tile(new Vector2(4, 0), TileType.Path));
        tiles.Add(new Tile(new Vector2(4, -1), TileType.Path));
        tiles.Add(new Tile(new Vector2(4, -2), TileType.Waypoint));
        tiles.Add(new Tile(new Vector2(5, -2), TileType.Path));

        SpawnTiles();
    }

    private void SpawnTiles()
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            GameObject tileObject = null;
            Tile tile = tiles[i];

            if(tile.tileType == TileType.Path)
            {
                List<TileDirection> pathDirections = GetNearPath(tile.position);

                //Set rotation
                if(pathDirections.Contains(TileDirection.Left) || pathDirections.Contains(TileDirection.Right)) tile.rotation = new Vector2(0, 90);
                if (pathDirections.Contains(TileDirection.Top) || pathDirections.Contains(TileDirection.Bottom)) tile.rotation = new Vector2(0, 0);

                tileObject = pathTile;
            }
            else if(tile.tileType == TileType.Spawnpoint)
            {
                List<TileDirection> pathDirections = GetNearPath(tile.position);

                //Set rotation
                if (pathDirections.Contains(TileDirection.Left) || pathDirections.Contains(TileDirection.Right)) tile.rotation = new Vector2(0, 90);
                if (pathDirections.Contains(TileDirection.Top) || pathDirections.Contains(TileDirection.Bottom)) tile.rotation = new Vector2(0, 0);

                /*if(pathDirections.Count == 1)
                {
                    if (pathDirections.Contains(TileDirection.Left)) tile.rotation = new Vector2(0, 180);
                    if (pathDirections.Contains(TileDirection.Right)) tile.rotation = new Vector2(0, 0);
                    if (pathDirections.Contains(TileDirection.Top)) tile.rotation = new Vector2(0, 90);
                    if (pathDirections.Contains(TileDirection.Bottom)) tile.rotation = new Vector2(0, 270);
                }*/

                tileObject = spawnpointTile;
            }
            else if(tile.tileType == TileType.Waypoint)
            {
                List<TileDirection> pathDirections = GetNearPath(tile.position);

                //Set rotation
                if (pathDirections.Contains(TileDirection.Left) && pathDirections.Contains(TileDirection.Top)) tile.rotation = new Vector2(0, 0);
                if (pathDirections.Contains(TileDirection.Top) && pathDirections.Contains(TileDirection.Right)) tile.rotation = new Vector2(0, 90);
                if (pathDirections.Contains(TileDirection.Right) && pathDirections.Contains(TileDirection.Bottom)) tile.rotation = new Vector2(0, 180);
                if (pathDirections.Contains(TileDirection.Bottom) && pathDirections.Contains(TileDirection.Left)) tile.rotation = new Vector2(0, 270);

                tileObject = waypointTile;
            }

            //Spawn tile
            Instantiate(tileObject, new Vector3(tile.position.x, 0, tile.position.y), Quaternion.Euler(tile.rotation.x, tile.rotation.y, 0));
        }

        //Done spawning
        Destroy(gameObject);
    }

    public List<TileDirection> GetNearPath(Vector2 position)
    {
        List<TileDirection> l = new List<TileDirection>();

        foreach(Vector2 vectorDirection in vectorDirections)
        {
            Tile nearTile = GetTile(position + vectorDirection);

            if(nearTile != null && (nearTile.tileType == TileType.Path || nearTile.tileType == TileType.Spawnpoint || nearTile.tileType == TileType.Waypoint))
            {
                if (vectorDirection == Vector2.left) l.Add(TileDirection.Left);
                if (vectorDirection == Vector2.right) l.Add(TileDirection.Right);
                if (vectorDirection == Vector2.up) l.Add(TileDirection.Top);
                if (vectorDirection == Vector2.down) l.Add(TileDirection.Bottom);
            }
        }

        if(l.Count > 0) return l;
        return null;
    }

    private Tile GetTile(Vector2 position)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].position == position) return tiles[i];
        }

        return null;
    }
}
