using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetController : MonoBehaviour
{

    List<GameObject> cards = new List<GameObject>();
    private int scoreMultiplier = 1;
    private int setScore = 0;

    public void AddCard(GameObject card)
    {
        cards.Add(card);
        InitializeCard(card);
    }

    public bool CanCardBePlaced(GameObject newCard)
    {
        List<ECardTypes> cardOrder = new List<ECardTypes> {
            ECardTypes.Person,
            ECardTypes.Action,
            ECardTypes.Location,
            ECardTypes.Connector
        };

        CardTemplate lastCardProperties = cards.Last().GetComponent<CardTemplate>();
        CardTemplate newCardProperties = newCard.GetComponent<CardTemplate>();

        int connectorAmount = cards.FindAll(x => x.GetComponent<CardTemplate>().cardType == ECardTypes.Connector).Count;

        if (connectorAmount == 0) return false;

        if((int)newCardProperties.cardType == (((int)lastCardProperties.cardType + 1) % cardOrder.Count)) {
            return true;
        }

        return false;
    }

    public int CheckSetSynergies() 
    {
        for(int i = 1; i < cards.Count; i++)
        {
            int synergyAmount = CountCardSynergy(cards[i-1], cards[i]);
            scoreMultiplier += synergyAmount;
        } 

        return 0;
    }

    public int CountCardSynergy(GameObject card1, GameObject card2)
    {
        CardTemplate card1Properties = card1.GetComponent<CardTemplate>();
        CardTemplate card2Properties = card2.GetComponent<CardTemplate>();
        int synergies = 0;

        if (card1Properties.synergyType.Contains(card2Properties.cardType))
        {
            synergies++;
        }
        if (card2Properties.synergyType.Contains(card1Properties.cardType))
        {
            synergies++;
        }

        return synergies;
    }
    
    public void InitializeCard(GameObject card)
    {
        card.GetComponent<CardTemplate>().InvokeAction();
    }















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
    {/*
        CardTemplate firstCardData = cards[cards.Count - 2].GetComponent<CardTemplate>();
        CardTemplate secondCardData = cards[cards.Count - 1].GetComponent<CardTemplate>();
        if (firstCardData.synergyType != ECardTypes.Default)
          {       
            if (firstCardData.synergyType == secondCardData.cardType)
            {
                scoreMultiplier++;
            }
        }
        */
        return true;
    }
}
