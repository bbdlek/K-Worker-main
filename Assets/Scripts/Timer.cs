using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerTxt;
    public float time = 60f;
    [SerializeField] private float selectCountdown = 60f;
    public bool countEnded;
    public bool timerStarted;


    private void Awake()
    {
        selectCountdown = time;
        countEnded = false;
    }

    public void TimerStart()
    {
        selectCountdown = time;
        timerStarted = true;
    }

    private void Update()
    {
        if (timerStarted)
        {
            if (Mathf.Floor(selectCountdown) <= 0) countEnded = true;
            else
            {
                selectCountdown -= Time.deltaTime;
                timerTxt.text = Mathf.Floor(selectCountdown).ToString();
            }    
        }
    }
}
