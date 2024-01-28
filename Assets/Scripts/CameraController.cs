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

    [SerializeField] private Transform peekButton;
    [SerializeField] private Transform backFromPeekButton;

    public static bool lookAtDeck;

    private void Awake()
    {
        lookAtDeck = true;
        backFromPeekButton.gameObject.SetActive(false);
    }

    public void MoveToDeck()
    {
        if (!lookAtDeck)
        {
            cam.position = DeckPos.position;
            cam.rotation = DeckPos.rotation;
            lookAtDeck = true;

            backFromPeekButton.gameObject.SetActive(false);
            peekButton.gameObject.SetActive(true);
        }
    }

    public void MoveToTable()
    {
        if (lookAtDeck)
        {
            cam.position = TablePos.position;
            cam.rotation = TablePos.rotation;
            lookAtDeck = false;

            backFromPeekButton.gameObject.SetActive(true);
            peekButton.gameObject.SetActive(false);
        }
    }

    

}
