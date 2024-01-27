using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public float letterTick = 0.1f;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    Queue<string> dialogueQueue = new Queue<string>();
    // Update is called once per frame
    public void StartDialogue(Dialogue dialogueList)
    {
        animator.SetBool("IsOpen", true);
        dialogueQueue.Clear();

        foreach (string sentence in dialogueList.sentences)
        {
            dialogueQueue.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        
        string sentance = dialogueQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentance, letterTick));
    }

    IEnumerator TypeSentence(string sentence, float talkTick)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(talkTick);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
