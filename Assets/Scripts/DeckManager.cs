using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    List<CardScript> cards;
    List<Transform> DeckCardsPos;


    int numberOfCards;
    float AngleBeetwen;
    Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void laidCards()
    {
        int i = 0;

        foreach (var card in cards)
        {
            card.transform.parent = DeckCardsPos[i];
        }
    }
}
