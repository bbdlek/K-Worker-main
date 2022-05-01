using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    DoorManager doorManager;
    [SerializeField]
    Door[] doors;

    [SerializeField]
    public int ConditionsOfSuccess = 12;

    int questNum = 0;
    public int QuestNumber
    {
        get { return questNum; }
        set { if (value <= ConditionsOfSuccess)
                questNum = value;
        }
    }
    float scoreSum = 0;
    public float ScoreSum
    {
        get { return scoreSum; }
        set { scoreSum = value; }
    }

    bool questClear = false;
    public bool QuestClear
    {
        get { return questClear; }
    }

    void Awake()
    {
            
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(!questClear && questNum >= ConditionsOfSuccess)
        {
            questClear = true;
            if(doors != null)
            {
                StartCoroutine("doorOn");
            }
        }
    }

    IEnumerator doorOn()
    {
        for(int i = 0; i< doors.Length; i++)
        {
            doorManager.OpenDoor(doors[i].DoorNum);
        }
        yield return null;
    }
}

