using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    public float speed;
    public string message = "낙사하였습니다.";
    public TMP_Text text;

    public Image[] buttons;
    public TMP_Text[] buttonTexts;

    private void Start()
    {   
        text.text = "";
        StartCoroutine("showMessage");
    }

    IEnumerator showMessage() 
    {
        for (int i = 0; i < message.Length; i++) 
        {
            yield return new WaitForSeconds(speed);
            text.text += message[i];  
        }

        yield return new WaitForSeconds(0.2f);

        for (int y = 0; y < 200; y += 10)
        {
            text.transform.localPosition = new Vector3(0, y, 0);
            yield return new WaitForSeconds(0.05f);
        }


        buttonTexts[0].text = "현재 계절부터 시작";
        buttonTexts[1].text = "시작화면으로";

        for (int c = 0; c <= 10; c++)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].color = new Color(1, 1, 1, c / 10.0f);
                buttonTexts[i].color = new Color(0, 0, 0, c / 10.0f);
                
            }

            yield return new WaitForSeconds(0.1f);
        }

        buttons[0].GetComponent<Button>().interactable = true;
        buttons[1].GetComponent<Button>().interactable = true;
    }

    public void Retry() 
    {
        int lvl = MapProcess.level;

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

    public void GoToTitle() 
    {
        SceneManager.LoadScene("Title");
    }
}
