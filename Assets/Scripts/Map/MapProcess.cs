using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProcess : MonoBehaviour//게임 진행관련
{
    public int level;
    int solarterm;
    string lastSolar;
    public bool shopOpened;
    int shopWait = 2;

    [SerializeField]
    MapRoutine[] mapRoutines;
    [SerializeField]
    public GameObject shopKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        mapRoutines = Resources.LoadAll<MapRoutine>("MapData");
    }

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel() 
    {
        if (!shopOpened)
        {
            SelectMap();
        }
        else 
        {
            shopWait--;
            shopOpened = false;
            if (shopWait < 0)
            {
                Debug.Log("밤상인이 나타났다!");
                gameObject.GetComponent<MapGenerator>().MapGenerate(40, 5, 37, 0, 2);//상점 맵
                Invoke("SpawnNpc", 1f);
                shopWait = 2;
            }
            else 
            {
                SelectMap();
            }
        }
    }

    void SelectMap() 
    {
        level++;//3단위로 절기가 바뀜 맵은 Level 1부터 시작

        MapRoutine currentRoutine = null;

        if (level < 73)
        {
            for (int i = 0; i < mapRoutines.Length; i++)
            {
                switch (mapRoutines[i].SolarTerm[0])
                {
                    case "입춘":
                        if (level <= 18)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level;
                        }
                        break;
                    case "입하":
                        if (level <= 36 && level >= 19)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level - 18;
                        }
                        break;
                    case "입추":
                        if (level <= 54 && level >= 37)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level - 36;
                        }
                        break;
                    case "입동":
                        if (level <= 72 && level >= 55)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level - 54;
                        }
                        break;
                }
            }
        }

        int n = 0;
        for (int i = solarterm; i > 0; i -= 3)//0 1 2 3 4 5
        {
            solarterm = n;
            n++;
        }

        if (lastSolar != currentRoutine.SolarTerm[solarterm])
        {
            shopOpened = true;
            //절기 두 개 끝날 시 상점 켜기
        }

        lastSolar = currentRoutine.SolarTerm[solarterm];

        Debug.Log(currentRoutine.SolarTerm[solarterm]);

        gameObject.GetComponent<MapGenerator>().MapGenerate(currentRoutine.mapWidth[solarterm], currentRoutine.mapHeight[solarterm], currentRoutine.minSectionWidth[solarterm], currentRoutine.cliff_Pro[solarterm], currentRoutine.maxCliffGap[solarterm]);
    }

    void SpawnNpc() 
    {
        GameObject d = Instantiate(shopKeeper, new Vector2(18, 10), Quaternion.identity);
        gameObject.GetComponent<MapGenerator>().mapObjects.Add(d);
    }
}
