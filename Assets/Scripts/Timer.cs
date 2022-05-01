using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerTxt;
    public float time = 60f;
    private float selectCountdown;
    public bool countEnded;

    public void TimerStart()
    {
        selectCountdown = time;
    }

    private void Update()
    {
        if (Mathf.Floor(selectCountdown) <= 0) countEnded = true;
        else
        {
            selectCountdown -= Time.deltaTime;
            timerTxt.text = Mathf.Floor(selectCountdown).ToString();
        }
    }
}
