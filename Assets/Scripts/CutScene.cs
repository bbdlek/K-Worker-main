using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScene : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Sprite[] changeImage;

    [SerializeField]
    GameObject[] texts;

    Image sourceImage;
    
    [SerializeField] private GameObject StartImg;

    [SerializeField] private bool enableSceneChange;
    [SerializeField] private int sceneNum;

    int Level = 0;

    void Start()
    {
        sourceImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Level >= changeImage.Length)
        {
            if(StartImg != null)
                StartImg.SetActive(true);
            gameObject.SetActive(false);
            if (enableSceneChange) SceneManager.LoadScene(sceneNum);
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
