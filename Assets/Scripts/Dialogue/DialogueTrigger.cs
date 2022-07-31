using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    bool talked = false;
    bool playerDetected = false;
    public Dialogue dialogueManager;
    public bool shopKeeper;
    public LayerMask exceptLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            talked = true;
            playerDetected = true;
            dialogueManager.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talked = false;
            playerDetected = false;
            dialogueManager.ToggleIndicator(playerDetected);
        }
    }

    private void Update()
    {
        if (talked && Input.GetKeyDown(KeyCode.E))
        {
            talked = false;
            dialogueManager.StartDialogue();
        }
        else if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            //�������� �ִ� ä�� E�� �ٽ� ������ ��ȭ ���� ���� â�� ����
            if (shopKeeper) 
            {
            }
        }
    }
}
