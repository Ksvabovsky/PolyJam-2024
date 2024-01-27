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

            /*Pa³aszek, jesli to czytasz to znaczy jedn¹ z dwóch rzeczy
             albo mi siê uda³o i odszed³em z tego œwiata
            albo tylko zrzek³em siê twojego zadania na twoj¹ czêœæ
            tak czy siak ¿yczê ci powodzenia i mi³ej zabawy

            ps.
            branie .position z raycasta chrzani w jakiœ sposób poruszanie kart¹ bo nie da siê zablokowaæ
            wzglêdem jednej zz osi a mia³em pomys³ ¿eby zablokowac y
            i wy³apywaæ kartê colliderami które trzeba umieœciæ nad "placeForCard" (poki co nazywaja sie tylko place)
            jak wy³apie to powinno snapowaæ do takiego miejsca ale wcia¿ jeœli przytrzynujesz myszke
            i wyjdziesz raycastem poza tego collidera place'a to spowrotem poruszasz kartê myszk¹
            
            Odnoœnie samej karty jakby by³y rozkminy, s¹ 2 klasy, scriptable object - CardTemplate
            (klikasz prawym -> create -> cards -> templates -> cardsSO) jest od danych
            (jak np sprite karty, jej szczególny obrazek) i jest CardDisplay który
            skleja prefaba z CardTemplatem, no i jest przyk³adowa Karta w prefabach, j¹ duplikujecie
            i siê na niej wzorujecie, no chyba ¿e w czasie jak bede mimimi to na inny pomys³ wpadniecie
            dla mnie bez problemu, a i dlaczego nie pisze tego np na mesie? Przed chwil¹ siê zczai³em 
            ¿e bym móg³ ale mi sie nie chce. Powodzeniaaaaaaa
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
