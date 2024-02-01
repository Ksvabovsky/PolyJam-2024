using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public DeckManager DeckManager;

    void Start()
    {
        DeckManager = FindObjectOfType<DeckManager>();
    }

    private void Awake()
    {
        DeckManager = FindObjectOfType<DeckManager>();
    }

    public void DoEffect()
    {
        DrawCard();
    }

    public void DrawCard()
    {
        DeckManager.drawCard();
    }
}
