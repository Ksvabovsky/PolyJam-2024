using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask mask;
    RaycastHit hit;
    Transform objectToDrag;
    bool isDragging = false;
    bool newObject = true;
    public Vector3 cardFirstPosition;

    private float yCardPos;

    private void Start()
    {
        cam = Camera.main;
    }
    void FixedUpdate()
    {
        //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        //Debug.DrawRay(transform.position, mousePosition - transform.position, Color.blue);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            if (newObject)
            {
                newObject = false;
                cardFirstPosition = hit.transform.position;
            }

            Debug.Log(hit.transform.name);
            objectToDrag = hit.transform;

            if(Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                objectToDrag.GetComponent<CardDisplay>().resetHover();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                objectToDrag.GetComponent<CardDisplay>().resetHover();
                objectToDrag.GetComponent<CardDisplay>().stopRotating();
                objectToDrag = null;
                newObject = true;
            }
            else
            {
                if(!isDragging)
                    hit.transform.GetComponent<CardDisplay>().startHovering();
            }
        }
        else
        {
            if(objectToDrag != null)
            {
                objectToDrag.GetComponent<CardDisplay>().stopHovering();
                objectToDrag = null;
            }
        }

        if(isDragging && objectToDrag != null)
        {
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);

            }
            Vector3 newPosition = hit.point;
            //newPosition.z = cardFirstPosition.z + (newPosition.y-cardFirstPosition.y)*1.15f;
            newPosition.y = yCardPos;
            Debug.Log(newPosition.y);
            objectToDrag.position = newPosition;

            objectToDrag.GetComponent<CardDisplay>().startRotating();
        }

    }

}
