using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public void InitStart() 
    {
        MapProcess.level = 0;
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadScene("Game");
    }

    public void Retry()
    {
        int lvl = PlayerPrefs.GetInt("level");

        if (lvl < 19)
        {
            MapProcess.level = 0;
        }
        else if (lvl < 37)
        {
            MapProcess.level = 18;
        }
        else if (lvl < 55)
        {
            MapProcess.level = 36;
        }
        else if (lvl < 73)
        {
            MapProcess.level = 54;
        }

        SceneManager.LoadScene("Game");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
