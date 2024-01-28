using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    List<SetController> SetList = new List<SetController>();
    BoardManager Board;
    public Scrollbar ScoreScrollbar;
    int Score;

    void Start()
    {
        SetList = FindObjectsOfType<SetController>().ToList();
        Board = FindObjectOfType<BoardManager>();
        SubscribeToSets();
        UpdateProgressBar(0f);
    }

    private void Awake()
    {
        
    }

    void SubscribeToSets()
    {
        foreach (SetController set in SetList)
        {
            Debug.Log(set.ToString());
            set.OnChange += RecalculateScore;
        }
    }

    void RecalculateScore()
    {
        Score = Board.UpdateGlobalScore();
        Debug.Log("Current Score " + Score);
        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateProgressBar(CalculatePercentageFromScore());
    }

    float CalculatePercentageFromScore()
    {
        Debug.Log("CalculatePercentage Score: " + Score + " TargetScore: " + Board.goalScore);
        if (Score == 0) return 0;
        return (float)Score / (float)Board.goalScore;
    }

    void UpdateProgressBar(float percentage)
    {
        Debug.Log("UpdateProgressBar " + percentage);
        ScoreScrollbar.size = percentage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
