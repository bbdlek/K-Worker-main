using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private Vector3 uiPos;

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        uiObject.transform.position = mainCam.WorldToScreenPoint(transform.position + uiPos);
    }

    public void OffUI()
    {
        if(uiObject.activeInHierarchy)
            uiObject.SetActive(false);
    }

    public void OnUI()
    {
        if(!uiObject.activeInHierarchy)
            uiObject.SetActive(true);
    }
}
