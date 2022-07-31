using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProcess : MonoBehaviour//���� �������
{
    public int level;
    int solarterm;

    [SerializeField]
    MapRoutine[] mapRoutines;

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
        level++;//3������ ���Ⱑ �ٲ� ���� Level 1���� ����

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
        for (int i = solarterm; i > 0; i -= 3)
        {
            solarterm = n;
            n++;
        }

        Debug.Log(currentRoutine.SolarTerm[solarterm]);

        gameObject.GetComponent<MapGenerator>().MapGenerate(currentRoutine.mapWidth[solarterm], currentRoutine.mapHeight[solarterm], currentRoutine.minSectionWidth[solarterm], currentRoutine.cliff_Pro[solarterm], currentRoutine.maxCliffGap[solarterm]);
    }
}
