using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Event : MonoBehaviour
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
    GameObject boss3;
    [SerializeField]
    GameObject boss1;
    [SerializeField]
    GameObject boss2;

    [SerializeField]
    GameObject[] hpBar;

    /*[SerializeField]
    GameObject Line;*/

    [SerializeField] private GameObject player;

    private Timer timer;

    bool QuestFirst = false;
    bool BossFirst = false;
    bool BossDie = false;
    bool cut2 = false;
    bool fight = false;
    bool boosS = false;
    Boss3 csBoss3;
    Boss2 csBoss2;
    Boss1 csBoss1;

    void Start()
    {
        cutScene.SetActive(true);
        csBoss1 = boss1.GetComponent<Boss1>();
        csBoss2 = boss2.GetComponent<Boss2>();
        csBoss3 = boss3.GetComponent<Boss3>();
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        if(!QuestFirst && questManager.QuestClear && csBoss1.IsDie && csBoss2.IsDie)
        {
            QuestFirst = true;
            timer.timerStarted = false;
            //Line.SetActive(false);
        }
        else if(!BossDie && csBoss3.IsDie)
        {
            BossDie = true;
            hpBar[0].SetActive(false);
            hpBar[1].SetActive(false);
            //cutScene3.SetActive(true);
            //door.ActiveDoor();
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
            boss3.SetActive(true);
        }
        
        if (timer.countEnded)
        {
            if (!DBManager.instance.hasChance)
                UIManager.instance.endPanel.SetActive(true);
            else UIManager.instance.endPanel2.SetActive(true);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!BossFirst && QuestFirst)
            {
                BossFirst = true;
                door.DisabledDoor();
                //Line.SetActive(true);
                UIManager.instance.QuestUIObject.SetActive(false);
                StartCoroutine("spawnBoss");
                FindObjectOfType<EnemySpawner>().enableSpawn = false;
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
