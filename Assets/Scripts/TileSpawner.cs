using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject pathTile;

    private enum TileType
    {
        Spawnpoint,
        Waypoint,
        Path,
        Buildable,
        Default
    }

    private enum TileDirection
    {
        Left,
        Right,
        Top,
        Down
    }

    private Vector2[] vectorDirections = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    private class Tile
    {
        public Vector2 position { get; private set; }
        public Vector2 rotation { get; set; }
        public TileType tileType { get; private set; }
        public TileDirection startDirection { get; set; }
        public TileDirection endDirection { get; set; }

        public GameObject tile { get; private set; }

        public Tile(Vector2 _position, TileType _tileType)
        {
            position = _position;
            tileType = _tileType;
        }

        public void SetTile(GameObject _tile)
        {
            tile = _tile;
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
        tiles.Add(new Tile(new Vector2(2, 1), TileType.Path));

        SpawnTiles();
    }

    private void SpawnTiles()
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            if(tiles[i].tileType == TileType.Path)
            {
                foreach(Vector2 vectorDirection in vectorDirections)
                {
                    Tile nearTile = GetTile(tiles[i].position + vectorDirection);
                    if(nearTile != null && nearTile.tileType == TileType.Path || nearTile.tileType == TileType.Waypoint || nearTile.tileType == TileType.Spawnpoint)
                    {
                        if(vectorDirection == Vector2.left || vectorDirection == Vector2.right)
                        {
                            tiles[i].rotation = new Vector2(0, 90);
                        }

                        if(vectorDirection == Vector2.up || vectorDirection == Vector2.down)
                        {
                            tiles[i].rotation = new Vector2(0, 0);
                        }
                    }
                }

                tiles[i].SetTile(Instantiate(pathTile, new Vector3(tiles[i].position.x, 0, tiles[i].position.y), Quaternion.Euler(tiles[i].rotation.x, tiles[i].rotation.y, 0)));
            }
        }
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
