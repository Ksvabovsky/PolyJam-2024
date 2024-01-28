using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawPileController : MonoBehaviour
{
    public List<GameObject> drawPileCards = new List<GameObject>();

    public static DrawPileController instance;

    public CardPoolsTemplate cardPool;

    private Vector3 growDirection = Vector3.zero;
    private Vector3 currentGrow = Vector3.zero;

    private void Awake()
    {
        instance = this;
    }

    public void addCard(GameObject card)
    {
        drawPileCards.Add(card);
        card.transform.SetParent(transform, true);
        card.transform.Rotate(-90.0f, 0, 0);
        card.transform.position = currentGrow;
        currentGrow += growDirection;
    }

    public GameObject getCard()
    {
        Debug.Log(drawPileCards);
        GameObject copy = drawPileCards.Last();
        Debug.Log(copy+" getCard");
        drawPileCards.RemoveAt(drawPileCards.Count - 1 );
        //currentGrow -= growDirection;
        return copy;
    }


    void Start()
    {
        currentGrow = transform.position;
        growDirection.y = 0.01f;

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
