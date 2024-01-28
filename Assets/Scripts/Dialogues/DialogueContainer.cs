using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Dialogue> dyingHappyDialogues = new List<Dialogue>();
    [SerializeField] List<Dialogue> cryingHappyDialogues = new List<Dialogue>();
    [SerializeField] List<Dialogue> happyDialogues = new List<Dialogue>();
    [SerializeField] List<Dialogue> annoyedDialogues = new List<Dialogue>();
    [SerializeField] List<Dialogue> angryDialogues = new List<Dialogue>();
    //   \/ To ten ze skoœnymi oczami
    [SerializeField] List<Dialogue> npcDialogues = new List<Dialogue>();

    public Dialogue GetDialogueBasedOnMood(EClownMood mood)
    {
        Dialogue randomizedDialogue;
        int randomIndex;

        switch(mood)
        {
            case EClownMood.DyingLaugh:
                randomIndex = Random.Range(0, dyingHappyDialogues.Count);
                randomizedDialogue = dyingHappyDialogues[randomIndex];
                break;
            case EClownMood.CryingLaugh:
                randomIndex = Random.Range(0, cryingHappyDialogues.Count);
                randomizedDialogue = cryingHappyDialogues[randomIndex];
                break;
            case EClownMood.Laugh:
                randomIndex = Random.Range(0, happyDialogues.Count);
                randomizedDialogue = happyDialogues[randomIndex];
                break;
            case EClownMood.Annoyed:
                randomIndex = Random.Range(0, annoyedDialogues.Count);
                randomizedDialogue = annoyedDialogues[randomIndex];
                break;
            case EClownMood.Angry:
                randomIndex = Random.Range(0, angryDialogues.Count);
                randomizedDialogue = angryDialogues[randomIndex];
                break;
            case EClownMood.Npc:
                randomIndex = Random.Range(0, npcDialogues.Count);
                randomizedDialogue = npcDialogues[randomIndex];
                break;
            default: 
                randomizedDialogue = null;
                break;
        }

        return randomizedDialogue;
    }
}
