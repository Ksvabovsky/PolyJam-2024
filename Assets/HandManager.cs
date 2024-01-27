using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HandManager : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private CameraController controller;

    [SerializeField] private LayerMask Cardmask;
    [SerializeField] private LayerMask Slotmask;
    private RaycastHit hit;


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
                Highlightable prev = highlitedObject.GetComponent<Highlightable>();
                prev.DeHighlightMe();
                highlitedObject = null;
            }

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
        Vector3 pos = hit.point + new Vector3(0f, 0.05f, 0f);
        ObjectInHand.transform.position = Vector3.Lerp(ObjectInHand.transform.position, pos, Time.deltaTime * 10f);
        ObjectInHand.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
        if (Input.mousePosition.y > 200f)
        {
            controller.MoveToTable();
        }

    }

    private void DropCard()
    {

        controller.MoveToDeck();
        if (highlitedObject)
        {
            if (highlitedObject.GetComponent<SlotScript>())
            {
                PutToSlot();
            }
            else
            {
                returnToHand();
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
        ObjectInHand.transform.SetParent(highlitedObject.transform);
        ObjectInHand.transform.localPosition = Vector3.zero;
        ObjectInHand.transform.localEulerAngles = Vector3.zero;
    }

}









