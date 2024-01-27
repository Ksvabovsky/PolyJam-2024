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
    Vector3 mousePosition;
    bool canDrag = false;
    bool newObject = true;
    public Vector3 cardFirstPosition;


    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        //Rysuje raycasta od myszki
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        Debug.DrawRay(transform.position, mousePosition - transform.position, Color.blue);
        if (Input.GetMouseButtonDown(0))
        {
            canDrag = true;
            mousePosition = Input.mousePosition - GetMousePos();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                /*if(hit.transform.Equals(objectToDrag))
                {
                    newObject = false;
                }
                else { newObject = true; }*/

                
                Debug.Log(hit.transform.name);
                objectToDrag = hit.transform;

            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            }
            if (newObject)
            {
                newObject = false;
                cardFirstPosition = hit.transform.position;
            }
        }else if(Input.GetMouseButtonUp(0))
        {
            canDrag = false;
            objectToDrag = null;
        }

        if(canDrag && objectToDrag != null)
        {
            Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            newPosition.z = cardFirstPosition.z;
            newPosition.y = cardFirstPosition.y;
            objectToDrag.position = newPosition;
            
        }

    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

}
