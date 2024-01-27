using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPileController : MonoBehaviour
{
    List<GameObject> drawPileCards = new List<GameObject>();

    public CardPoolsTemplate cardPool;

    private Vector3 growDirection = Vector3.zero;
    private Vector3 currentGrow = Vector3.zero;

    public void addCard(GameObject card)
    {
        drawPileCards.Add(card);
        card.transform.SetParent(transform, true);
        card.transform.Rotate(-90.0f, 0, 0);
        card.transform.position = currentGrow;
        currentGrow += growDirection;
    }

    public GameObject getCard(int index)
    {
        GameObject copy = drawPileCards[index];
        drawPileCards.RemoveAt(index);
        currentGrow -= growDirection;
        return copy;
    }


    void Start()
    {
        currentGrow = transform.position;
        growDirection.y = 5.0f;

        List<GameObject> cardPoolCards = cardPool.getPool();


        for (int i = 0; i < cardPoolCards.Count; i++)
        {
            int randIndex = (int)Mathf.Round(Random.Range(i, cardPoolCards.Count - 1));

            if (randIndex != i)
            {
                GameObject tmp = cardPoolCards[randIndex];
                cardPoolCards[randIndex] = cardPoolCards[i];
                cardPoolCards[i] = tmp;
            }

            addCard(cardPoolCards[i]);
        }
    }
}
