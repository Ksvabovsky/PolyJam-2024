using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    bool canBeDragged = true;
    public Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public bool CanBeDragged()
    {
        return canBeDragged;
    }

    public void PlaceTheCard()
    {
        canBeDragged = false;
    }

}
