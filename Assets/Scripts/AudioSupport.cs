using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSupport : MonoBehaviour
{
    public static AudioSupport instance;

    public AudioMixer masterMixer;

    [SerializeField] private AudioClip[] bgmClips; 
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public void MusicControl(float sound)
    {
        DBManager.instance.musicVolume = sound;
        if (sound == -40f) masterMixer.SetFloat("Music", -80);
        else masterMixer.SetFloat("Music", sound);
    }
    
    public void SFXcontrol(float sound)
    {
        DBManager.instance.sfxVolume = sound;
        if (sound == -40f) masterMixer.SetFloat("SFX", -80);
        else masterMixer.SetFloat("SFX", sound);
    }

    public void SaveSound()
    {
        PlayerPrefs.SetFloat("MusicVolume", DBManager.instance.musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", DBManager.instance.sfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadSound()
    {
        masterMixer.SetFloat("Music", DBManager.instance.musicVolume);
        masterMixer.SetFloat("SFX", DBManager.instance.sfxVolume);
    }

}
