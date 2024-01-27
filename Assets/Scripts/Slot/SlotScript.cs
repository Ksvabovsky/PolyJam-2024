using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    [SerializeField]GameObject highlight;


    public void HighlightMe()
    {
        highlight.SetActive(true);
    }

    public void DishighlightMe()
    {
        highlight.SetActive(false);
    }
}
