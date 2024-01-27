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
    bool canDrag = false;
    bool newObject = true;
    public Vector3 cardFirstPosition;


    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        //Debug.DrawRay(transform.position, mousePosition - transform.position, Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            canDrag = true;
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

            }

        }
        else if(Input.GetMouseButtonUp(0))
        {
            canDrag = false;
            objectToDrag = null;
        }
        if(canDrag && objectToDrag != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);

                Debug.Log(hit.transform.name);
                objectToDrag = hit.transform;

            }
            Vector3 newPosition = hit.point;
            newPosition.z = cardFirstPosition.z + (newPosition.y-cardFirstPosition.y)*1.15f;
            objectToDrag.position = newPosition;


        }

    }

}
