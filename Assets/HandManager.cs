using UnityEngine;

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

    private const float RaycastRange = 100f;

    private void Start()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        HandleSlotHighlighting(ray);
        HandleCardInteraction(ray);
        HandleCardDragging(ray);
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
        Debug.DrawLine(ray.origin, hit.point, Color.green);

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
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
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