using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    List<Door> Doors = new List<Door>();
    

    void Awake()
    {
        StartCoroutine("ConferNum");     
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator ConferNum()
    {
        for(int i = 0; i< Doors.Count; i++)
        {
            Doors[i].DoorNum = i;
        }
        yield return null;
    }

    public void OpenDoor(int index)
    {
        Doors[index].ActiveDoor();
    }

    public void MoveByDoor(int index)
    {
        if (Doors[index].DoorOpen)
        {
            if (Doors[index].MoveSceneOrTel)
            {
                //SceneManager.LoadScene(Doors[index].NextScene);
                //AdMobManager.instance.ShowRewardAds();
                if(DBManager.instance.noAdsPurchased) SceneManager.LoadScene(SceneManager.sceneCount + 1);
                else AdMobManager.instance.ShowInterstitialAd();
            }

            else
            {
                player.transform.position = Doors[index].TeleportPos;
            }
        }   
    }
}
