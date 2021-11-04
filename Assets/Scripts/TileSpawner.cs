using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tile;

public class TileSpawner : MonoBehaviour
{
    public GameObject pathTile;
    public GameObject waypointTile;
    public GameObject spawnpointTile;
    public GameObject buildableTile;
    public GameObject endpointTile;
    public GameObject[] defaultTiles;

    public TextAsset jsonFile;

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
        public TileType tileType { get; set; }

        public int count { get; set; }

        public Tile(Vector2 _position, TileType _tileType, int _count = -1)
        {
            position = _position;
            tileType = _tileType;
            count = _count;
        }
    }

    [System.Serializable]
    public class MapData
    {
        public string game;
        public TileData[][] data;
    }

    [System.Serializable]
    public class TileData
    {
        public string name;
        public int count = -1;
    }

    private List<Tile> tiles = new List<Tile>();

    private void Start()
    {
        LoadFromJson();

        //Test path
        /*tiles.Add(new Tile(new Vector2(-1, 0), TileType.Spawnpoint));
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
        tiles.Add(new Tile(new Vector2(2, 2), TileType.Buildable));
        tiles.Add(new Tile(new Vector2(3, 2), TileType.Default));
        tiles.Add(new Tile(new Vector2(4, 2), TileType.Default));
        tiles.Add(new Tile(new Vector2(5, 2), TileType.Default));*/

        //Change the last waypoint
        SetEndpoint();

        SpawnTiles();
    }

    private void SetEndpoint()
    {
        int highestWaypointNumber = -1;
        int index = -1;

        int endpointIndex = -1;

        for (int i = 0; i < tiles.Count; i++)
        {
            Tile currentTile = tiles[i];

            //if tiles has an endpoint don't set the last waypoint to endpoint
            if (currentTile.tileType == TileType.Endpoint) endpointIndex = i;

            if(currentTile.tileType == TileType.Waypoint)
            {
                if(currentTile.count > highestWaypointNumber)
                {
                    highestWaypointNumber = currentTile.count;
                    index = i;
                }
            }
        }

        if(endpointIndex >= 0)
        {
            tiles[endpointIndex].count = highestWaypointNumber + 1;
        }
        else
        {
            tiles[index].tileType = TileType.Endpoint;
        }
    }

    private void LoadFromJson()
    {
        //MapData mapData = JsonUtility.FromJson<MapData>(jsonFile.text);

        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonFile.text);

        for(int y = 0; y < mapData.data.Length; y++)
        {
            for(int x = 0; x < mapData.data[y].Length; x++)
            {
                tiles.Add(new Tile(new Vector2(x, y), (TileType)Enum.Parse(typeof(TileType), mapData.data[y][x].name), mapData.data[y][x].count));
            }
        }
    }

    private void SpawnTiles()
    {
        int highestX = -1;
        int highestY = -1;

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
            else if(tile.tileType == TileType.Endpoint)
            {
                List<TileDirection> pathDirections = GetNearPath(tile.position);
                if (pathDirections.Contains(TileDirection.Top)) tile.rotation = new Vector2(0, 0);
                if (pathDirections.Contains(TileDirection.Left)) tile.rotation = new Vector2(0, 270);
                if (pathDirections.Contains(TileDirection.Right)) tile.rotation = new Vector2(0, 90);
                if (pathDirections.Contains(TileDirection.Bottom)) tile.rotation = new Vector2(0, 180);

                tileObject = endpointTile;
            }
            else if(tile.tileType == TileType.Buildable)
            {
                tileObject = buildableTile;
            }
            else if (tile.tileType == TileType.Default)
            {
                tileObject = defaultTiles[UnityEngine.Random.Range(0, defaultTiles.Length)];
            }

            //Spawn tile
            GameObject newTile = Instantiate(tileObject, new Vector3(tile.position.x, 0, tile.position.y), Quaternion.Euler(tile.rotation.x, tile.rotation.y, 0));

            //Set waypoint id
            if (tile.tileType == TileType.Waypoint || tile.tileType == TileType.Endpoint)
            {
                newTile.GetComponent<Waypoint>().id = tile.count;
            }

            if (tile.position.x > highestX) highestX = (int)tile.position.x;
            if (tile.position.y > highestY) highestY = (int)tile.position.y;
        }

        FindObjectOfType<CamaraMovement>().UpdateMap(highestX + 1, highestY + 1);

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
