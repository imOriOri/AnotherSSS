using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Tilemap platform, Hover;
    [SerializeField]
    private TileBase tile;
    [SerializeField]
    private int mapWidth, mapHeight;
    [SerializeField]
    private int interval;
    [SerializeField]
    [Range(0, 100)]
    private int cliff_pro = 5;

    int[,] currentMap;

    public void MapGenerate()
    {
        currentMap = RandomWalkTopSmoothed(GenerateArray(mapWidth, mapHeight), Random.Range(0, 10000), interval, cliff_pro);
        RenderMap(currentMap, platform, tile);
    }

    public static int[,] GenerateArray(int width, int height/*, bool empty*/)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                map[x, y] = 0;
            }
        }
        return map;
    }

    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // 1 = tile, 0 = no tile
                if (map[x, y] == 1)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }

    public static int[,] RandomWalkTopSmoothed(int[,] map, float seed, int minSectionWidth, int cliff_pro)
    {
        System.Random rand = new System.Random(seed.GetHashCode());

        int lastHeight = Random.Range(0, map.GetUpperBound(1));

        bool cliff = false;
        int nextMove = 0;
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth)
            {
                lastHeight--;
                sectionWidth = 0;
            }
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth)
            {
                lastHeight++;
                sectionWidth = 0;
            }
            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--)
            {
                map[x, y] = 1;
            }

            //³¶¶°·¯Áö
            if (x < map.GetUpperBound(0) - 1 && !cliff)
            {
                if (Random.Range(0, 100) < cliff_pro)
                {
                    cliff = true;
                    x += 1;
                }
            }
            else 
            {
                cliff = false;
            }
        }

        //Return the modified map
        return map;
    }
}