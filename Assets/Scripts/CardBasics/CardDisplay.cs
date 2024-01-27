using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RawImage sprite;
    public CardTemplate card;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.nameText;
        descriptionText.text = card.descriptionText;
        sprite.texture = card.cardSprite;
    }
}
