using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private Tilemap platform, barrier;
    [SerializeField]
    private TileBase tile;

    public GameObject spawn;
    public GameObject finish;
    [SerializeField]
    public List<GameObject> mapObjects = new List<GameObject>();

    int[,] currentMap;

    GameObject player;
    Camera mainCam;
    CameraFollow camFollow;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y + 1.2f);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camFollow = mainCam.GetComponent<CameraFollow>();
    }

    public void MapGenerate(int width, int height, int interval, int cliff_pro, int maxCliffGap)
    {
        currentMap = RandomWalkTopSmoothed(GenerateArray(width, height), Random.Range(0, 10000), interval, cliff_pro, maxCliffGap, width, height);
        RenderMap(currentMap, platform, tile);
    }

    public int[,] GenerateArray(int width, int height/*, bool empty*/)
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

    public void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
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

        barrier.ClearAllTiles();

        for (int y = 0; y < 30; y++) 
        {
            barrier.SetTile(new Vector3Int(-1, y, 0), tile);
        }
        for (int y = 0; y < 30; y++)
        {
            barrier.SetTile(new Vector3Int(map.GetUpperBound(0), y, 0), tile);
        }
    }

    public int[,] RandomWalkTopSmoothed(int[,] map, float seed, int minSectionWidth, int cliff_pro, int maxCliffGap, int mapWidth, int mapHeight)
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

            for (int y = lastHeight; y >= 0; y--)
            {
                map[x, y] = 1;
            }

            //낭떠러지
            if (x > 3 && x < mapWidth - maxCliffGap - 3 && !cliff)
            {
                if (Random.Range(0, 100) < cliff_pro)
                {
                    cliff = true;
                    x += Random.Range(2, maxCliffGap + 1);//낭떠러지 구멍 크기
                }
            }
            else 
            {
                cliff = false;
            }
        }


        for (int i = 0; i < mapObjects.Count; i++)
        {
            //DestroyImmediate(mapObjects[i]); //On Test
            Destroy(mapObjects[i]); //Playing
        }
        mapObjects.Clear();

        bool spawned = false;

        GameObject d = null;
        for (int i = 0; i < mapHeight; i++) 
        {
            if (map[0, i] == 0 && !spawned)
            {

                d = Instantiate(spawn, new Vector3(0.5f, i + 0.5f, 0), Quaternion.identity);
                mapObjects.Add(d);//스폰
                spawned = true;
            }
        }

        player.transform.position = new Vector2(d.transform.position.x, d.transform.position.y + 0.5f);//플레이어를 스폰으로 이동


        GameObject k = Instantiate(finish, new Vector3(mapWidth - 1.5f, mapHeight, 0), Quaternion.identity);
        mapObjects.Add(k);

        camFollow.limitMaxX = mapWidth - 1.5f - 17.3f;//카메라제한

        StartCoroutine("CameraReturn");

        return map;
    }

    IEnumerator CameraReturn() 
    {
        camFollow.smoothSpeed = 4;

        yield return new WaitForSeconds(0.7f);

        camFollow.smoothSpeed = 1;
    }
}