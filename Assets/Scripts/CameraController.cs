using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public bool moveCam;

    [SerializeField] private Transform cam;
    [SerializeField] private Transform DeckPos;
    [SerializeField] private Transform TablePos;

    bool lookAtDeck;

    private void Start()
    {
        lookAtDeck = true;
    }

    public void MoveToDeck()
    {
        if (!lookAtDeck)
        {
            cam.position = DeckPos.position;
            cam.rotation = DeckPos.rotation;
            lookAtDeck = true;
        }
    }

    public void MoveToTable()
    {
        if (lookAtDeck)
        {
            cam.position = TablePos.position;
            cam.rotation = TablePos.rotation;
            lookAtDeck = false;
        }
    }

    

}
