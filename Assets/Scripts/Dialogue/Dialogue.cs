using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject window;

    public List<string> dialogues;

    public TMP_Text dialogueText;

    private int index;
    private int charIndex;

    private bool started;

    public float writingSpeed;
    public float waitForNext;

    public float maxX;//580
    public float minX;//150
    float currentX;

    public RectTransform rt;
    public RectTransform drt;
    public DialogueHold dHolder;

    private void ToggleWindow(bool show) 
    {
        window.SetActive(show);
    }


    public void StartDialogue()
    {
        if (started)
            return;

        started = true;
        ToggleWindow(true);

        GetDialogue(0);
    }

    private void GetDialogue(int i) 
    {
        index = i;
        charIndex = 0;
        currentX = 0;
        rt.sizeDelta = new Vector2(minX + 30, 60);
        drt.sizeDelta = new Vector2(minX, 70);

        dialogueText.text = string.Empty;

        StartCoroutine("Writing");
    }

    public void EnDialogue() 
    {
        index = 0;
        Destroy(gameObject);
        dHolder.isTalk = false;
    }

    IEnumerator Writing() 
    {
        string currentDialogue = dialogues[index];

        dialogueText.text += currentDialogue[charIndex];
        charIndex++;

        currentX += 15;
        if (minX + currentX >= maxX) 
        {
            currentX = maxX - minX;
        }

        rt.sizeDelta = new Vector2(minX + currentX + 30, 50);
        drt.sizeDelta = new Vector2(minX + currentX, 20);

        if (charIndex < currentDialogue.Length)
        {
            yield return new WaitForSeconds(writingSpeed);

            StartCoroutine("Writing");
        }
        else 
        {
            index++;

            if (index < dialogues.Count)
            {
                yield return new WaitForSeconds(waitForNext);
                GetDialogue(index);
            }
            else 
            {
                yield return new WaitForSeconds(waitForNext);
                EnDialogue();
                started = false;
            }
        }
    }

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        drt = dialogueText.GetComponent<RectTransform>();
        ToggleWindow(false);
    }
}
