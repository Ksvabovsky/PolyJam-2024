using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetController : MonoBehaviour
{

    List<GameObject> cards = new List<GameObject>();
    private int scoreMultiplier = 1;
    private int setScore = 0;

    public void CompleteSet()
    {
        foreach(GameObject card in cards)
        {
            //card.activateSynergy.invoke();
        }

        foreach(GameObject card in cards)
        {
            //setScore += card.GetComponent<CardTemplate>();.funScore;
        }

        ActivateSynergies();
    }

    public void ActivateSynergies()
    {
       setScore *= scoreMultiplier;
    }

    public bool HasSynergy()
    {
        CardTemplate firstCardData = cards[cards.Count - 2].GetComponent<CardTemplate>();
        CardTemplate secondCardData = cards[cards.Count - 1].GetComponent<CardTemplate>();
        if (firstCardData.synergyType != ECardTypes.Default)
          {       
            if (firstCardData.synergyType == secondCardData.cardType)
            {
                scoreMultiplier++;
            }
        }

        return true;
    }
}
