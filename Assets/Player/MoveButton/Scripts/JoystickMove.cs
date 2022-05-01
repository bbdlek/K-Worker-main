using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickMove : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{
    [SerializeField] Sprite[] sourceImage = new Sprite[2];
    [SerializeField] Sprite[] selectImage = new Sprite[2];
    [SerializeField] Image[] objectImage = new Image[2];

    RectTransform touch;
    RectTransform rectTransform;

    float buttonWidth = 0f;
    float buttonHeight = 0f;

    int h = 0;
    public int H
    {
        get { return h; }
    }
    

    bool rightSelected = false;
    bool LeftSelected = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        buttonHeight = rectTransform.sizeDelta.y;
        buttonWidth = rectTransform.sizeDelta.x;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.position.x <= buttonWidth && eventData.position.y <= buttonHeight)
        {
            h = eventData.position.x > buttonWidth / 2 ? 1 : -1;
            if (h > 0 && !rightSelected)
            {
                rightSelected = true;
                LeftSelected = false;
                objectImage[1].sprite = selectImage[1];
                objectImage[0].sprite = sourceImage[0];
            }
            else if (h < 0 && !LeftSelected)
            {
                LeftSelected = true;
                rightSelected = false;
                objectImage[0].sprite = selectImage[0];
                objectImage[1].sprite = sourceImage[1];
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x <= buttonWidth && eventData.position.y <= buttonHeight)
        {
            //Debug.Log(eventData.position);
            h = eventData.position.x > buttonWidth / 2 ? 1 : -1;
            if(h > 0 && !rightSelected)
            {
                rightSelected = true;
                LeftSelected = false;
                objectImage[1].sprite = selectImage[1];
                objectImage[0].sprite = sourceImage[0];
            }
            else if(h < 0 && !LeftSelected)
            {
                LeftSelected = true;
                rightSelected = false;
                objectImage[0].sprite = selectImage[0];
                objectImage[1].sprite = sourceImage[1];
            }
        }
        else
        {
            rightSelected = false;
            LeftSelected = false;
            objectImage[0].sprite = sourceImage[0];
            objectImage[1].sprite = sourceImage[1];
            h = 0;
        }    

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rightSelected = false;
        LeftSelected = false;
        objectImage[0].sprite = sourceImage[0];
        objectImage[1].sprite = sourceImage[1];
        h = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rightSelected = false;
        LeftSelected = false;
        objectImage[0].sprite = sourceImage[0];
        objectImage[1].sprite = sourceImage[1];
        h = 0;
    }

    
}
