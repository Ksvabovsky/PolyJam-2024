using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyingCardHighlight : Highlightable
{
    GameObject highlight;
        public override void HighlightMe()
    {
        highlight = Instantiate(this.gameObject, this.transform.position + Vector3.up * 1.02f, this.transform.rotation);
        highlight.transform.LookAt(Camera.main.transform.position);
        highlight.transform.localScale = Vector3.one *2f;
        highlight.layer = LayerMask.NameToLayer("Water");
    }

    public override void DeHighlightMe()
    {
        if (highlight != null)
        {
            Destroy(highlight);
        }
    }



}
