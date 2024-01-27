using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI funPointsText;
    public RawImage sprite;
    public RawImage frontSprite;
    public CardTemplate card;

    private float maxHoverDistance = 0.3f;
    public float moveByDistance = 0.0f;
    public float currentHoverDistance = 0.0f;
    public float hoverSpeed;
    public bool isHovered = false;
    public bool isRotating = false;

    public float defaultRotation;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.nameText;
        descriptionText.text = card.descriptionText;
        sprite.texture = card.cardSprite;
        frontSprite.texture = card.cardFrontSprite;
        funPointsText.text = card.funScore.ToString();
        

        currentHoverDistance = 0.0f;
        defaultRotation = transform.rotation.eulerAngles.x;
        rotationSpeed = 140.0f;
        hoverSpeed = 5.0f;

        card.InvokeAction();
    }

    private void FixedUpdate()
    {
        if (isHovered)
        {
            if (currentHoverDistance < maxHoverDistance)
            {
                Vector3 newPosition = transform.position;

                moveByDistance = maxHoverDistance * hoverSpeed * Time.deltaTime;

                currentHoverDistance += moveByDistance;

                if (currentHoverDistance > maxHoverDistance)
                {

                    moveByDistance = currentHoverDistance - maxHoverDistance;
                    currentHoverDistance = maxHoverDistance;
                }

                newPosition.y += moveByDistance;
                transform.position = newPosition;
            }
        }
        else
        {
            if (currentHoverDistance > 0)
            {
                Vector3 newPosition = transform.position;

                moveByDistance = maxHoverDistance * hoverSpeed * Time.deltaTime;

                currentHoverDistance -= moveByDistance;

                if (currentHoverDistance < 0)
                {
                    moveByDistance = -currentHoverDistance;
                    currentHoverDistance = 0.0f;
                }

                newPosition.y -= moveByDistance;
                transform.position = newPosition;
            }
        }

        if (isRotating)
        {
            if (transform.rotation.eulerAngles.x < 90.0f)
            {
                if (transform.rotation.eulerAngles.x + rotationSpeed * Time.deltaTime > 90.0f)
                {
                    transform.Rotate(90.0f - transform.rotation.eulerAngles.x , 0, 0);
                }
                else
                {
                    transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
                }
            }
        }
        else
            if (transform.rotation.eulerAngles.x > defaultRotation)
        {
            if (transform.rotation.eulerAngles.x - rotationSpeed * Time.deltaTime < defaultRotation)
            {
                transform.Rotate(defaultRotation - transform.rotation.eulerAngles.x, 0, 0);
            }
            else
            {
                transform.Rotate(-(rotationSpeed * Time.deltaTime), 0, 0);
            }
        }
        
    }

    public void startHovering()
    {
        isHovered = true;
    }

    public void stopHovering()
    {
        isHovered = false;
    }

    public void resetHover()
    {
        isHovered = false;
        currentHoverDistance = 0.0f;
    }

    public void startRotating()
    {
        isRotating = true;
    }

    public void stopRotating()
    {
        isRotating = false;
    }
}
