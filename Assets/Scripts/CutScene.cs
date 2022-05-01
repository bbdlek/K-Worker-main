using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutScene : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Sprite[] changeImage;

    [SerializeField]
    GameObject[] texts;

    Image sourceImage;

    int Level = 0;

    void Start()
    {
        sourceImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Level >= changeImage.Length)
        {
            FindObjectOfType<Timer>().TimerStart();
            gameObject.SetActive(false);
        }

        else
        {
            texts[Level+1].SetActive(true);
            texts[Level].SetActive(false);
            sourceImage.sprite = changeImage[Level];
        }

        Level += 1;
    }
}
