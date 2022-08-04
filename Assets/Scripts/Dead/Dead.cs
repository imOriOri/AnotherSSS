using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dead : MonoBehaviour
{
    public float speed;
    public string message = "낙사하였습니다.";
    public TMP_Text text;

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
    }
}
