using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;

    public bool noAdsPurchased;

    public float musicVolume = 0;
    public float sfxVolume = 0;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitPurchased();
        InitVolume();
    }

    private void InitPurchased()
    {
        if(PlayerPrefs.HasKey("NoAdsPurchased"))
        {
            noAdsPurchased = PlayerPrefs.GetInt("NoAdsPurchased") == 1;
        }
        
        PlayerPrefs.Save();
    }

    private void InitVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        } else PlayerPrefs.SetFloat("MusicVolume", 5);
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("SfxVolume");
        } else PlayerPrefs.SetFloat("SFXVolume", 5);
        
        PlayerPrefs.Save();
        AudioSupport.instance.LoadSound();
    }

}
