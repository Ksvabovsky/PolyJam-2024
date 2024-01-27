using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetController : MonoBehaviour
{

    List<GameObject> cards = new List<GameObject>();

    public void CompleteSet()
    {
        int score = 0;
        foreach(GameObject card in cards)
        {
            //card.activateSynergy.invoke();
        }

        foreach(GameObject card in cards)
        {
            //score += card.score;
        }
    }
}
