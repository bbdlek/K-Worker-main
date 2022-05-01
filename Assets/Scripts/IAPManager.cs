using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Security;

public class IAPManager : MonoBehaviour
{
    public static IAPManager instance;
    public bool noAdsPurchased;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("NoAdsPurchased"))
        {
            noAdsPurchased = PlayerPrefs.GetInt("NoAdsPurchased") == 1;
        }
    }
}
