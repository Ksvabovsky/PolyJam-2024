using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BoardManager : MonoBehaviour
{
    public List<GameObject> setControllers = new List<GameObject>();
    public int goalScore = 50;
    public int globalScore;

    public int UpdateGlobalScore()
    {
        int sum = 0;
        foreach (GameObject setObject in setControllers) {

            SetController setController = setObject.GetComponent<SetController>();
            sum += setController.CalculatePoints();
        }
        globalScore = sum;
        return sum;
    }

    public float CalculatePercentageScore()
    {
        if (globalScore == 0) return 0;
        return (float)globalScore / (float)goalScore;
    }

    public EClownMood GetClownMood()
    {
        float funninessPercentage = CalculatePercentageScore();
        Debug.Log("Board-CalculatePercentage Percentage: " + funninessPercentage);
        if (funninessPercentage < 0.20)
        {
            return EClownMood.Angry;
        } else if (funninessPercentage < 0.40)
        {
            return EClownMood.Annoyed;
        } else if (funninessPercentage < 0.60)
        {
            return EClownMood.Laugh;
        } else if (funninessPercentage < 0.80)
        {
            return EClownMood.CryingLaugh;
        } else
        {
            return EClownMood.DyingLaugh;
        }
    }

    public void EndGame()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartClownMoodDialogue(GetClownMood());
    }
}
