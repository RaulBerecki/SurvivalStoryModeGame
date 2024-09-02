using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BiomeGenerator : MonoBehaviour
{
    public int mapWidth = 200;
    public int mapHeight = 200;
    public float biomeScale = 20f;

    public Tilemap tilemap;
    public Tile grassTile;
    public Tile waterTile;
    public Tile stoneTile;
    public Tile dirtTile;
    // Start is called before the first frame update
    void Start()
    {
        GenerateBiome();
    }

    void GenerateBiome()
    {
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Tile selectedTile = ChooseTileForPosition(x, y);
                tilemap.SetTile(tilePosition, selectedTile);
            }
        }
        PlaceDirtTransition();
    }
    Tile ChooseTileForPosition(int x, int y)
    {
        float xCoord=(float)x/mapWidth*biomeScale;
        float yCoord= (float)y/mapHeight*biomeScale;
        float noiseValue=Mathf.PerlinNoise(xCoord, yCoord);
        if (noiseValue < 0.3f)
        {
            return waterTile;
        }
        else if(noiseValue < 0.6f)
        {
            return grassTile;
        }
        else
        {
            return stoneTile;
        }
    }
    void PlaceDirtTransition()
    {
        for(int x = 0;x < mapWidth; x++)
        {
            for (int y = 0;y < mapHeight; y++)
            {
                Vector3Int gridPosition = new Vector3Int(x, y, 0);

                TileBase currentTile = tilemap.GetTile(gridPosition);
                if (currentTile==grassTile)
                {
                    if (IsToWater(gridPosition))
                    {
                        tilemap.SetTile(gridPosition, dirtTile);
                    }
                }
            }
        }
    }
    bool IsToWater(Vector3Int gridPosition)
    {
        Vector3Int[] adjacentPositions = new Vector3Int[]
        {
            gridPosition + new Vector3Int(1, 0, 0),
            gridPosition + new Vector3Int(-1, 0, 0),
            gridPosition + new Vector3Int(0, 1, 0),
            gridPosition + new Vector3Int(0, -1, 0)
        };

        foreach (var position in adjacentPositions)
        {
            if (tilemap.GetTile(position) == waterTile)
            {
                return true;
            }
        }

        return false;
    }
}
