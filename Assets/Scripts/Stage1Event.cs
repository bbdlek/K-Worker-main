using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Event : MonoBehaviour
{
    [SerializeField]
    QuestManager questManager;
    [SerializeField]
    Door door;
    [SerializeField]
    GameObject cutScene;
    [SerializeField]
    GameObject cutScene2;
    [SerializeField]
    GameObject cutScene3;
    [SerializeField]
    GameObject FightScene;
    [SerializeField]
    GameObject boss;

    [SerializeField]
    GameObject[] hpBar;

    [SerializeField]
    GameObject Line;

    [SerializeField] private GameObject player;

    private Timer timer;

    bool QuestFirst = false;
    bool BossFirst = false;
    bool BossDie = false;
    bool cut2 = false;
    bool fight = false;
    bool boosS = false;
    Boss1 csBoss;

    void Start()
    {
        cutScene.SetActive(true);
        csBoss = boss.GetComponent<Boss1>();
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        if(!QuestFirst && questManager.QuestClear)
        {
            player.GetComponent<TutorialUI>().OnUI();
            QuestFirst = true;
            Line.SetActive(false);
        }

        else if(!BossDie && csBoss.IsDie)
        {
            BossDie = true;
            hpBar[0].SetActive(false);
            hpBar[1].SetActive(false);
            cutScene3.SetActive(true);
            door.ActiveDoor();
        }

        else if(cut2)
        {
            cut2 = false;
            cutScene2.SetActive(true);
        }
        else if(fight)
        {
            fight = false;
            FightScene.SetActive(true);
        }
        else if(boosS)
        {
            boosS = false;
            hpBar[0].SetActive(true);
            hpBar[1].SetActive(true);
            boss.SetActive(true);
        }

        if (timer.countEnded)
        {
            //시간 끝
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!BossFirst && QuestFirst)
            {
                player.GetComponent<TutorialUI>().OffUI();
                BossFirst = true;
                door.DisabledDoor();
                Line.SetActive(true);
                UIManager.instance.QuestUIObject.SetActive(false);
                StartCoroutine("spawnBoss");
            }
        }
    }

    IEnumerator spawnBoss()
    {
        cut2 = true;
        yield return new WaitForSeconds(1.5f);
        fight = true;
        yield return new WaitForSeconds(1.5f);
        boosS = true;
    }

    
}
