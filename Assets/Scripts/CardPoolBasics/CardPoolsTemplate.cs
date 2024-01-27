using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardPoolsTemplate", menuName = "Cards/Templates/CardPoolSO", order = 1)]
public class CardPoolsTemplate : ScriptableObject
{
    public List<GameObject> cardPool = new List<GameObject>();

    public List<GameObject> cardPoolCopy = new List<GameObject>();

    public List<GameObject> getPool()
    {
        cardPoolCopy = new List<GameObject>();

        foreach (GameObject obj in cardPool)
        {
            Debug.Log(obj);
            cardPoolCopy.Add(Instantiate(obj));
        }

        return cardPoolCopy;
    }
}
