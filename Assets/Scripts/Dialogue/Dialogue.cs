using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject window;
    public GameObject indicator;

    public List<string> dialogues;

    public TMP_Text dialogueText;

    private int index;
    private int charIndex;

    private bool started;

    public float writingSpeed;
    public float waitForNext;

    private void ToggleWindow(bool show) 
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }


    public void StartDialogue()
    {
        if (started)
            return;

        started = true;
        ToggleWindow(true);
        ToggleIndicator(false);

        GetDialogue(0);
    }

    private void GetDialogue(int i) 
    {
        index = i;
        charIndex = 0;

        dialogueText.text = string.Empty;

        StartCoroutine("Writing");
    }

    public void EnDialogue() 
    {
        index = 0;
        ToggleWindow(false);
    }

    IEnumerator Writing() 
    {
        string currentDialogue = dialogues[index];

        dialogueText.text += currentDialogue[charIndex];
        charIndex++;

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
        ToggleIndicator(false);
        ToggleWindow(false);
    }
}
