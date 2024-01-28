using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public float letterTick = 0.1f;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public AudioSource audioSource;
    Queue<string> dialogueQueue = new Queue<string>();
    public CharacterTemplate currentTalkingCharacter;
    public DialogueContainer dialogueContainer;

    public void StartClownMoodDialogue(EClownMood clownMood)
    {
        StartDialogue(dialogueContainer.GetDialogueBasedOnMood(clownMood));
    }

    public void StartDialogue(Dialogue dialogueList)
    {
        animator.SetBool("IsOpen", true);
        dialogueQueue.Clear();

        foreach (string sentence in dialogueList.sentences)
        {
            dialogueQueue.Enqueue(sentence);
        }
        currentTalkingCharacter = dialogueList.character;
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
            if (char.IsLetterOrDigit(letter))
            {
                audioSource.clip = currentTalkingCharacter.GetVoiceClip();
                audioSource.pitch = Random.Range(1.5f, 1.8f);
                audioSource.Play();
            }
            yield return new WaitForSeconds(talkTick);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
