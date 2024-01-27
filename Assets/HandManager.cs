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

            /*Pa�aszek, jesli to czytasz to znaczy jedn� z dw�ch rzeczy
             albo mi si� uda�o i odszed�em z tego �wiata
            albo tylko zrzek�em si� twojego zadania na twoj� cz��
            tak czy siak �ycz� ci powodzenia i mi�ej zabawy

            ps.
            branie .position z raycasta chrzani w jaki� spos�b poruszanie kart� bo nie da si� zablokowa�
            wzgl�dem jednej zz osi a mia�em pomys� �eby zablokowac y
            i wy�apywa� kart� colliderami kt�re trzeba umie�ci� nad "placeForCard" (poki co nazywaja sie tylko place)
            jak wy�apie to powinno snapowa� do takiego miejsca ale wcia� je�li przytrzynujesz myszke
            i wyjdziesz raycastem poza tego collidera place'a to spowrotem poruszasz kart� myszk�
            
            Odno�nie samej karty jakby by�y rozkminy, s� 2 klasy, scriptable object - CardTemplate
            (klikasz prawym -> create -> cards -> templates -> cardsSO) jest od danych
            (jak np sprite karty, jej szczeg�lny obrazek) i jest CardDisplay kt�ry
            skleja prefaba z CardTemplatem, no i jest przyk�adowa Karta w prefabach, j� duplikujecie
            i si� na niej wzorujecie, no chyba �e w czasie jak bede mimimi to na inny pomys� wpadniecie
            dla mnie bez problemu, a i dlaczego nie pisze tego np na mesie? Przed chwil� si� zczai�em 
            �e bym m�g� ale mi sie nie chce. Powodzeniaaaaaaa
            */
            Vector3 newPosition = hit.point;
            //newPosition.z = cardFirstPosition.z + (newPosition.y-cardFirstPosition.y)*1.15f;
            newPosition.y = yCardPos;
            Debug.Log(newPosition.y);
            objectToDrag.position = newPosition;

            objectToDrag.GetComponent<CardDisplay>().startRotating();
        }

    }

}
