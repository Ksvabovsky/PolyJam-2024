using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardTemplate", menuName = "Cards/Templates/CardSO", order = 1)]
public class CardTemplate : ScriptableObject
{
    public int id;
    public string nameText;
    public string descriptionText;
    public Texture cardSprite;
    public Texture cardFrontSprite;
    public CardTemplate nextCard;

    public int funScore;
    public ECardTypes cardType;
    public List<ECardSynergies> synergyType;

    public UnityEvent acionInvoker;
    public void AddCard(CardTemplate card)
    {
        nextCard = card;
    }

    public void InvokeAction()
    {
        acionInvoker.Invoke();
    }
}
