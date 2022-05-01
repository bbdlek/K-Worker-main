using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    [SerializeField]
    Sprite[] doorImage = new Sprite[3];
    [SerializeField]
    bool SceneOrTel = false;
    public bool MoveSceneOrTel
    {
        get { return SceneOrTel; }
    }

    [SerializeField]
    Vector2 teleportPos;
    public Vector2 TeleportPos
    {
        get { return teleportPos; }
    }
    [SerializeField]
    int nextScene;
    public int NextScene
    {
        get { return nextScene; }
    }

    SpriteRenderer rend;
    Sprite selectSprite;

    public bool DoorOpen
    {
        get;
        set;
    }

    public int DoorNum
    {
        get;
        set;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        if (SceneOrTel)
            selectSprite = doorImage[2];
        else
            selectSprite = doorImage[1];
    }

    public void ActiveDoor()
    {
        DoorOpen = true;
        rend.sprite = selectSprite;
        gameObject.layer = 0;
    }

   public void DisabledDoor()
    {
        DoorOpen = false;
        rend.sprite = doorImage[0];
        gameObject.layer = 8;
    }
}
