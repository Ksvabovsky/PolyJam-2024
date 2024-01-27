using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HandManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private LayerMask Cardmask;
    [SerializeField] private LayerMask Slotmask;
    private RaycastHit hit;
    private RaycastHit slothit;
    private Transform objectToDrag;
    private bool isDragging = false;
    private bool canBePlaced = false;
    private bool canHighlight = false;
    private SlotScript lastHighlightedSlot = null;
    private GameObject currentSlotToPlaceCard = null;

    [SerializeField] InputReader input;

    [SerializeField] private GameObject highlitedObject;
    [SerializeField] private GameObject ObjectInHand;

    [SerializeField] private bool isHandFree = true;
    [SerializeField] private bool isHolding;

    private const float RaycastRange = 100f;

    private void Awake()
    {
        input = InputReader.instance;
    }

    private void Start()
    {
        cam = Camera.main;
        
        input.LeftClick += Interact;
    }



    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        PointerHandler(ray);

        if(isHolding)
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
                Highlightable prev = highlitedObject.GetComponent<Highlightable>();
                prev.DeHighlightMe();
                highlitedObject = null;
            }
           
            return;
        }
        else {
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
        
        if(highlitedObject.layer == LayerMask.NameToLayer("Card"))
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
        Vector3 pos = hit.point + new Vector3(0f,0.05f,0f);
        ObjectInHand.transform.position = Vector3.Lerp(ObjectInHand.transform.position,pos,Time.deltaTime * 10f); 
        ObjectInHand.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);


    }

    private void DropCard()
    {
        if (highlitedObject)
        {
            if (highlitedObject.GetComponent<SlotScript>())
            {
                ObjectInHand.transform.SetParent(highlitedObject.transform);
                ObjectInHand.transform.localPosition = Vector3.zero;
                ObjectInHand.transform.localEulerAngles = Vector3.zero;
            }
        }

        isHolding = false;
        ObjectInHand = null;
        highlitedObject = null;
        input.LeftClick += Interact;

    }

    private void returnToHand()
    {

    }

    private void PutToSlot()
    {

    }

















    private void HandleSlotHighlighting(Ray ray)
    {
        if (Physics.Raycast(ray, out slothit, RaycastRange, Slotmask))
        {
            SlotInteraction();
        }
        else
        {
            ClearHighlightedSlot();
        }
    }

    private void SlotInteraction()
    {
        SlotScript currentSlot = slothit.transform.GetComponent<SlotScript>();
        if (currentSlot == null) return;

        HighlightCurrentSlot(currentSlot);
        UpdateSlotPlacement(currentSlot);
    }

    private void HighlightCurrentSlot(SlotScript currentSlot)
    {
        if (canHighlight)
        {
            currentSlot.HighlightMe();
        }
    }

    private void UpdateSlotPlacement(SlotScript currentSlot)
    {
        canBePlaced = true;
        currentSlotToPlaceCard = slothit.collider.gameObject;

        if (lastHighlightedSlot != null && lastHighlightedSlot != currentSlot)
        {
            lastHighlightedSlot.DishighlightMe();
        }

        lastHighlightedSlot = currentSlot;
    }

    private void ClearHighlightedSlot()
    {
        if (lastHighlightedSlot != null)
        {
            canBePlaced = false;
            lastHighlightedSlot.DishighlightMe();
            lastHighlightedSlot = null;
        }
    }

    private void HandleCardInteraction(Ray ray)
    {
        if (Physics.Raycast(ray, out hit, RaycastRange, Cardmask))
        {
            highlitedObject = hit.collider.gameObject;
            ProcessCardRaycastHit(ray);
        }
        else if (objectToDrag != null)
        {
            objectToDrag.GetComponent<CardDisplay>().stopHovering();
            objectToDrag = null;
        }
    }

    private void ProcessCardRaycastHit(Ray ray)
    {
        //Debug.DrawLine(ray.origin, hit.point, Color.green);

        CardScript cardScript = hit.transform.GetComponent<CardScript>();
        if (cardScript != null && cardScript.CanBeDragged())
        {
            PrepareCardForDragging(cardScript);
        }
        else
        {
            objectToDrag = null;
            canHighlight = false;
        }

        HandleMouseEvents(cardScript);
    }

    private void PrepareCardForDragging(CardScript cardScript)
    {
        objectToDrag = hit.transform;
        canHighlight = true;
    }

    private void HandleMouseEvents(CardScript cardScript)
    {
        if (Input.GetMouseButtonDown(0) && objectToDrag != null)
        {
            StartDragging(cardScript);
        }
        else if (Input.GetMouseButtonUp(0) && objectToDrag != null)
        {
            StopDragging(cardScript);
        }
        else
        {
            UpdateHoverState();
        }
    }

    private void StartDragging(CardScript cardScript)
    {
        objectToDrag.GetComponent<CardDisplay>().resetHover();
        if (cardScript.CanBeDragged())
            isDragging = true;
    }

    private void StopDragging(CardScript cardScript)
    {
        canHighlight = false;
        PlaceCardIfPossible(cardScript);
        isDragging = false;
        objectToDrag.GetComponent<CardDisplay>().resetHover();
        objectToDrag.GetComponent<CardDisplay>().stopRotating();
        objectToDrag = null;
    }

    private void PlaceCardIfPossible(CardScript cardScript)
    {
        if (canBePlaced)
        {
            cardScript.PlaceTheCard();
            if (currentSlotToPlaceCard != null)
            {
                objectToDrag.transform.SetParent(currentSlotToPlaceCard.transform);
                objectToDrag.transform.position = currentSlotToPlaceCard.transform.position;
            }
        }
        else
        {
            objectToDrag.position = cardScript.originalPosition;
        }
    }

    private void UpdateHoverState()
    {
        if (!isDragging && !canBePlaced)
            hit.transform.GetComponent<CardDisplay>().startHovering();
        else
            hit.transform.GetComponent<CardDisplay>().stopHovering();
    }

    private void HandleCardDragging(Ray ray)
    {
        if (isDragging && objectToDrag != null && Physics.Raycast(ray, out hit, RaycastRange, Cardmask))
        {
            //Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            MoveDraggingCard();
        }
    }

    private void MoveDraggingCard()
    {
        Vector3 newPosition = hit.point;
        newPosition.y = objectToDrag.transform.GetComponent<CardScript>().originalPosition.y;
        objectToDrag.position = newPosition;

        objectToDrag.GetComponent<CardDisplay>().startRotating();
    }
}