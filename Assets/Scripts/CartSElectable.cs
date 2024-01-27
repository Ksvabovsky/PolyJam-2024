using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSElectable : Selectable
{
    public override void HighlightMe()
    {
        this.transform.Translate(new Vector3(0f,0.2f,0f),Space.Self);
    }

    public override void DeHighlightMe()
    {
        this.transform.Translate(new Vector3(0f, -0.2f, 0f), Space.Self);
    }


}
