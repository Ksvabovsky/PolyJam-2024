using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DeckManager : MonoBehaviour
{

    [SerializeField] private DrawPileController pile;
    [SerializeField] private Transform slotsParent;
    

    [SerializeField] List<GameObject> cards;
    [SerializeField] List<Transform> DeckCardsPos;

    [SerializeField] GameObject obj;

    [SerializeField] List<GameObject> drawed;


    [SerializeField] int numberOfCards;
    [SerializeField] int firstFreeSlot;

    [SerializeField] float offset;
    Vector3 ParentStartPos;



    // Start is called before the first frame update
    void Start()
    {
        pile = DrawPileController.instance;

        firstFreeSlot = 0;
        ParentStartPos = slotsParent.position;
        StartCoroutine(drawCards(7));
        
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
            
        }
    }

    IEnumerator drawCards(int number)
    {
        for(int i = 0;i < number; i++)
        {
            obj = pile.getCard();
            drawed.Add(obj);
        }

        while(drawed.Count > 0)
        {
            GameObject card = drawed[0];
            drawed.RemoveAt(0);
            if (firstFreeSlot < 9)
            {
                card.transform.parent = DeckCardsPos[firstFreeSlot];
                cards.Add(card);
                StartCoroutine(MoveToPos(card.transform, DeckCardsPos[firstFreeSlot]));
                firstFreeSlot++;
                numberOfCards++;
                StartCoroutine(ChangedCardsCount());
            }
            else
            {
                BurnCard();
            }
            yield return new WaitForSeconds(0.5f);

        }
        yield break;
    }


    public void BurnCard()
    {

    }

    IEnumerator MoveToPos(Transform card, Transform pos)
    {
        float elapsedTime = 0f;


        while (elapsedTime < 1f) {
            elapsedTime += Time.deltaTime *2f;

            card.position = Vector3.Lerp(card.position, pos.position,Mathf.Clamp01(elapsedTime));
            card.rotation = Quaternion.Lerp(card.rotation, pos.rotation, Mathf.Clamp01(elapsedTime));
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ChangedCardsCount()
    {
        float elapsedTime = 0f;

        Vector3 targetPos = ParentStartPos + (numberOfCards - 1) * new Vector3(offset,0f,0f);


        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime*2;

            slotsParent.position = Vector3.Lerp(slotsParent.position, targetPos, Mathf.Clamp01(elapsedTime));
            yield return new WaitForEndOfFrame();
        }
    }

    public void CardWasTaken(GameObject card)
    {
        int index = cards.IndexOf(card);
        cards.RemoveAt(index);
        numberOfCards--;
        firstFreeSlot = cards.Count;

        for(int i = 0;i < cards.Count;i++)
        {
            cards[i].transform.parent = DeckCardsPos[i].transform;
            cards[i].transform.localPosition = Vector3.zero;
        }

        StartCoroutine(ChangedCardsCount());
        Debug.Log("Boobs");
    }

    public void RERoll()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }
        cards.Clear();
        firstFreeSlot = 0;
        numberOfCards = 0;

        StartCoroutine(drawCards(8));
    }
}