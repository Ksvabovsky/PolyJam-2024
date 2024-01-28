using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetController : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();
    // \/zmienna nie u¿ywana
    public event Action<int> AddScore;
    public event Action OnChange;
    private int scoreMultiplier = 1;
    private int setScore = 0;

    public void AddCard(GameObject card)
    {
        cards.Add(card);
        InitializeCard(card);
        InvokeOnChange();
    }

    public void InvokeOnChange()
    {
        OnChange?.Invoke();
    } 

    public void InvokeAddScore(int score)
    {
        AddScore.Invoke(score);
    }

    public bool CanCardBePlaced(GameObject newCard)
    {
        Debug.Log("can it?");
        List<ECardTypes> cardOrder = new List<ECardTypes> {
            ECardTypes.Person,
            ECardTypes.Action,
            ECardTypes.Location,
            ECardTypes.Connector
        };
        Debug.Log("CanCarBePlaced 1");
        if(cards.Count == 0)
        {
            if(newCard.GetComponent<CardDisplay>().card.cardType == ECardTypes.Person)
            {
                return true;
            } else
            {
                return false;
            }
        }

        CardTemplate lastCardProperties = cards.Last().GetComponent<CardDisplay>().card;
        CardTemplate newCardProperties = newCard.GetComponent<CardDisplay>().card;

        int connectorAmount = cards.FindAll(x => x.GetComponent<CardDisplay>().card.cardType == ECardTypes.Connector).Count;

        if (connectorAmount == 1) return false;
        Debug.Log("Card Check " + (int)newCardProperties.cardType + " " + (((int)lastCardProperties.cardType + 1) % cardOrder.Count));
        if((int)newCardProperties.cardType == (((int)lastCardProperties.cardType + 1) % cardOrder.Count)) {
            return true;
        }

        return false;
    }

    public int CalculatePoints()
    {
        CheckSetSynergies();
        int sum = 0;
        foreach (GameObject cardObject in cards)
        {
            CardTemplate card = cardObject.GetComponent<CardDisplay>().card;
            sum += card.funScore;
        }
        sum *= scoreMultiplier; 
        return sum;
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
        CardTemplate card1Properties = card1.GetComponent<CardDisplay>().card;
        CardTemplate card2Properties = card2.GetComponent<CardDisplay>().card;
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
        card.GetComponent<CardDisplay>().card.InvokeAction();
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
