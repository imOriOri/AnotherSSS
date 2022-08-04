using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHold : MonoBehaviour
{
    public GameObject dialoguePrefab;
    public Transform canvas;
    public Vector3 offset;
    public float smooth;
    GameObject cureentDial;
    Camera mainCam;
    Transform playerPos;

    public List<string> dialogues;

    public bool isTalk = false;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Talk();
    }

    void Talk() 
    {
        cureentDial = Instantiate(dialoguePrefab, transform.position, Quaternion.identity);
        cureentDial.transform.SetParent(canvas);

        Dialogue cur = cureentDial.GetComponent<Dialogue>();
        cur.dialogues = dialogues;
        cur.StartDialogue();
        cur.dHolder = GetComponent<DialogueHold>();

        isTalk = true;
    }

    private void Update()
    {
        if (!isTalk)
            return;

        Transform tail = cureentDial.transform.GetChild(0);
        Vector3 bossPos = mainCam.WorldToScreenPoint(transform.position);
        float width = (cureentDial.GetComponent<RectTransform>().sizeDelta.x / 2.0f);

        tail.position = new Vector2(bossPos.x,tail.position.y);

        bossPos = mainCam.WorldToScreenPoint(transform.position + offset);
        Vector2 destPos = mainCam.WorldToScreenPoint(new Vector3(playerPos.position.x, transform.position.y, 0) + offset);
        cureentDial.transform.position = Vector3.Lerp(cureentDial.transform.position, new Vector3(Mathf.Clamp(destPos.x, tail.position.x - width + 25, tail.position.x + width - 25), bossPos.y, 0), smooth);
    }
}
