using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MissionSpriteAnim : MonoBehaviour
{
    [SerializeField] private GameObject MissionImg;

    [SerializeField] private Timer timer;
    
    public void GameStart()
    {
        MissionImg.SetActive(true);
        StartCoroutine(ObjDestroy());
    }

    IEnumerator ObjDestroy()
    {
        yield return new WaitForSeconds(1f);
        timer.TimerStart();
        gameObject.SetActive(false);
    }
}
