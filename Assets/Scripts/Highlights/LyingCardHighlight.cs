using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyingCardHighlight : Highlightable
{
    GameObject highlight;
    public override void HighlightMe()
    {
        if (CameraController.lookAtDeck)
        {
            highlight = Instantiate(this.gameObject, this.transform.position + Vector3.up * 1.02f, this.transform.rotation);
            highlight.transform.LookAt(Camera.main.transform.position);
            highlight.transform.localScale = Vector3.one * 2f;
            highlight.layer = LayerMask.NameToLayer("Water");
        }
        else
        {
            highlight = Instantiate(this.gameObject, Camera.main.transform.position + Camera.main.transform.forward * 1.1f, Camera.main.transform.rotation);
            highlight.transform.localScale = Vector3.one * 1.2f;
            highlight.layer = LayerMask.NameToLayer("Water");
        }
    }

    public override void DeHighlightMe()
    {
        if (highlight != null)
        {
            Destroy(highlight);
        }
    }



}
