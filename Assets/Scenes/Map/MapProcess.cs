using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MapProcess : MonoBehaviour//���� �������
{
    public static int level = 0;
    int solarterm;
    string lastSolar;
    public bool shopOpened;
    int shopWait = 2;
    bool isShop;

    [SerializeField]
    MapRoutine[] mapRoutines;
    [SerializeField]
    public GameObject shopKeeper;
    Light2D playerLight;
    

    // Start is called before the first frame update
    void Awake()
    {
        mapRoutines = Resources.LoadAll<MapRoutine>("MapData");
        playerLight = GameObject.FindGameObjectWithTag("Player").GetComponent<Light2D>();
    }

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel() 
    {
        isShop = false;

        if (!shopOpened)
        {
            Night(false);
            SelectMap();
        }
        else 
        {
            shopWait--;
            shopOpened = false;
            if (shopWait < 0)
            {
                isShop = true;
                Debug.Log("������� ��Ÿ����!");
                Night(true);
                gameObject.GetComponent<MapGenerator>().MapGenerate(40, 5, 37, 0, 2);//���� ��

                GameObject d = Instantiate(shopKeeper, new Vector2(18, 10), Quaternion.identity);
                gameObject.GetComponent<MapGenerator>().mapObjects.Add(d);

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
        level++;//3������ ���Ⱑ �ٲ� ���� Level 1���� ����
        PlayerPrefs.SetInt("level",level);
        
        Debug.Log(level);

        switch(level) //����
        {
            case 18:
                    SceneManager.LoadScene("Spring");
                return;
            case 36:
                    SceneManager.LoadScene("Summer");
                return;
            case 54:
                    SceneManager.LoadScene("Autumn");
                return;
            case 72:
                    SceneManager.LoadScene("Winter");
                return;
        }

        MapRoutine currentRoutine = null;

        if (level < 73)
        {
            for (int i = 0; i < mapRoutines.Length; i++)
            {
                switch (mapRoutines[i].SolarTerm[0])
                {
                    case "����":
                        if (level <= 18)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level;
                        }
                        break;
                    case "����":
                        if (level <= 36 && level >= 19)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level - 18;
                        }
                        break;
                    case "����":
                        if (level <= 54 && level >= 37)
                        {
                            currentRoutine = mapRoutines[i];
                            solarterm = level - 36;
                        }
                        break;
                    case "�Ե�":
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
            //���� �� �� ���� �� ���� �ѱ�
        }

        lastSolar = currentRoutine.SolarTerm[solarterm];

        Debug.Log(currentRoutine.SolarTerm[solarterm]);

        if (Random.Range(0, 2) == 0 && level % 2 == 0)//50%Ȯ�� ¦����° �� ��� �ϼ���
        {
            Night(true);
            gameObject.GetComponent<MapGenerator>().MapGenerate(currentRoutine.mapWidth[solarterm], 3, currentRoutine.mapWidth[solarterm], currentRoutine.cliff_Pro[solarterm], currentRoutine.maxCliffGap[solarterm]);
        }
        else 
        {
            Night(false);
            gameObject.GetComponent<MapGenerator>().MapGenerate(currentRoutine.mapWidth[solarterm], currentRoutine.mapHeight[solarterm], currentRoutine.minSectionWidth[solarterm], currentRoutine.cliff_Pro[solarterm], currentRoutine.maxCliffGap[solarterm]);   
        }
    }

    void Night(bool night) 
    {//true -> night
        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(!night);
        }

        if (!isShop) 
        {
            playerLight.enabled = night;
        }
    }
}
