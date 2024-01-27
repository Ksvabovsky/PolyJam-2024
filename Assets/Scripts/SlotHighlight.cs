using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHighlight : Highlightable
{
    [SerializeField] GameObject highlight;

    public override void HighlightMe()
    {
        highlight.SetActive(true);
    }

    public override void DeHighlightMe()
    {
        highlight.SetActive(false);
    }


}
