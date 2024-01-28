using System;
using System.Drawing;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HandManager : MonoBehaviour
{

    public static HandManager instance;

    private Camera cam;
    [SerializeField] private CameraController controller;
    [SerializeField] private DeckManager deck;

    [SerializeField] private LayerMask Cardmask;
    [SerializeField] private LayerMask Slotmask;
    private RaycastHit hit;


    [SerializeField] InputReader input;

    [SerializeField] private GameObject highlitedObject;
    [SerializeField] private GameObject ObjectInHand;

    [SerializeField] private bool isHandFree = true;
    [SerializeField] private bool isHolding;

    private const float RaycastRange = 100f;

    float heightpx;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        input = InputReader.instance;
        input.LeftClick += Interact;

        heightpx = Screen.height / 3;
    }


    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        PointerHandler(ray);

        if (isHolding)
        {
            Dragging(ray);
        }
    }


    private void PointerHandler(Ray ray)
    {
        LayerMask mask = Cardmask;

        RaycastHit hit;
        if (isHolding)
        {
            mask = Slotmask;
        }
        Physics.Raycast(ray, out hit, RaycastRange, mask);
        if (hit.collider == null)
        {
            if (highlitedObject != null)
            {
                if (highlitedObject.GetComponent<Highlightable>())
                {
                    Highlightable prev = highlitedObject.GetComponent<Highlightable>();
                    prev.DeHighlightMe();
                }
                highlitedObject = null;
            }

            highlitedObject = null;
            return;
        }
        else
        {
            GameObject hited = hit.collider.gameObject;

            if (hited == highlitedObject)
            {
                return;
            }
            else
            {
                if (highlitedObject != null)
                {
                    if (highlitedObject.GetComponent<Highlightable>())
                    {
                        Highlightable prev = highlitedObject.GetComponent<Highlightable>();
                        prev.DeHighlightMe();
                    }
                }
                highlitedObject = hited;
                if (highlitedObject.GetComponent<Highlightable>())
                {
                    highlitedObject.GetComponent<Highlightable>().HighlightMe();
                }

            }

        }

    }


    private void Interact()
    {

        if (highlitedObject.layer == LayerMask.NameToLayer("Card"))
        {
            TakeCard();
        }
    }

    private void TakeCard()
    {
        Debug.Log("Chuj");
        ObjectInHand = highlitedObject;
        highlitedObject = null;
        isHandFree = false;
        isHolding = true;

        input.LeftClick -= Interact;
        input.LeftClickRelase += DropCard;
    }

    private void Dragging(Ray ray)
    {

        Physics.Raycast(ray, out hit, RaycastRange, Slotmask);
        if(hit.collider == null)
        {
            
            DropCard();
            return;
        }
        Vector3 pos = hit.point + new Vector3(0f, 0.05f, 0f);
        ObjectInHand.transform.position = Vector3.Lerp(ObjectInHand.transform.position, pos, Time.deltaTime * 10f);
        ObjectInHand.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
        if (Input.mousePosition.y > heightpx)
        {
            controller.MoveToTable();
        }

    }

    private void DropCard()
    {

        controller.MoveToDeck();
        Debug.Log("Drop Card outif");
        if (highlitedObject)
        {
            Debug.Log("Drop Card is highlited");
            Debug.Log("highlitedObject " + highlitedObject.name);
            if (highlitedObject.GetComponent<SetController>().CanCardBePlaced(ObjectInHand))
            {
                Debug.Log("Drop Card isSlot and Set");
                SetController setController = highlitedObject.GetComponent<SetController>();
                
                PutToSlot(setController);
                
            }
            else
            {
                Debug.Log("Drop Card isNotSlot and notSet");
                returnToHand();
            }
        }
        else
        {
            Debug.Log("Drop Card is not higlighted");
            returnToHand();
        }


        isHolding = false;
        ObjectInHand = null;
        
        if (highlitedObject != null)
        {
            highlitedObject.GetComponent<Highlightable>().DeHighlightMe();
        }
        highlitedObject = null;
        input.LeftClick += Interact;
        input.LeftClickRelase -= DropCard;

    }

    private void returnToHand()
    {
        ObjectInHand.transform.position = ObjectInHand.transform.parent.position;
        ObjectInHand.transform.rotation = ObjectInHand.transform.parent.rotation;
    }

    private void PutToSlot(SetController setController)
    {
        ObjectInHand.transform.SetParent(highlitedObject.transform);
        ObjectInHand.transform.localPosition = Vector3.zero;
        ObjectInHand.transform.localEulerAngles = Vector3.zero;
        setController.AddCard(ObjectInHand);
        deck.CardWasTaken(ObjectInHand);
    }

    public GameObject GetCard()
    {
        return ObjectInHand;
    }
}









