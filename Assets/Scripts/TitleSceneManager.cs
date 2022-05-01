using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private Text titleTxt;

    [SerializeField] private float blinkDuration;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            titleTxt.DOKill();
            SceneManager.LoadScene("01.Stage1");
        }
    }

    private void Start()
    {
        if(titleTxt != null)
            titleTxt.DOFade(0.0f, blinkDuration).SetEase(Ease.InCubic).SetLoops(-1, LoopType.Yoyo);
    }
}
