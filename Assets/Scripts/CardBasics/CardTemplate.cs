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
    public CardTemplate nextCard;
    public ECardTypes cardType;

    public UnityEvent onDoSomething;
    public void addCard(CardTemplate card)
    {
        nextCard = card;
    }

    public void OnEnable()
    {
        onDoSomething.Invoke();
    }
}
