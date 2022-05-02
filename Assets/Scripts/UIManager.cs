using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField]
    QuestManager questManager;

    [SerializeField]
    Player player;
    [SerializeField]
    Boss boss;

    [SerializeField]
    Slider[] hpBar;
    
    [SerializeField]
    Text scoreNum;
    
    [SerializeField]
    private Text QuestObjectText;

    public GameObject QuestUIObject;
    [SerializeField] private GameObject pausePanel;
    public GameObject endPanel;
    public GameObject noAdsPanel;
    public GameObject volumePanel;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        instance = this;
        /*if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);*/
    }

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(AudioSupport.instance.MusicControl);
        sfxSlider.onValueChanged.AddListener(AudioSupport.instance.SFXcontrol);
        LoadSound();
    }

    void Update()
    {
        scoreNum.text = questManager.ScoreSum.ToString();
        QuestObjectText.text = questManager.QuestNumber + " / " + questManager.ConditionsOfSuccess;
        hpBar[0].value = player.Hp / player.MaxHp;
        hpBar[1].value = boss.Hp / boss.MaxHp;

    }

    #region PausePanel

    public void PauseBtn()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
        Time.timeScale = Time.timeScale != 0 ? 0 : 1;
    }

    public void OptionBtn()
    {
        if(!volumePanel.activeInHierarchy) volumePanel.SetActive(true);
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    public void MainMenuBtn()
    {
        SceneManager.LoadScene("00.Title");
    }

    public void NoAdsBtn()
    {
        //광고 구매 안했으면 광고제거 패널 띄우기
        noAdsPanel.SetActive(true);
        pausePanel.SetActive(false);
        //구매했을시 팝업
    }

    public void QuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        
    }

    #endregion

    #region NoAdsPanel

    public void BuyNoAdsBtn()
    {
        //광고제거 구매
    }

    #endregion
    
    
    #region GameOverPanel
    public void ReplayBtn()
    {
        FindObjectOfType<Timer>().time = 60f;
        FindObjectOfType<Timer>().countEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AdBtn()
    {
        AdMobManager.instance.ShowRewardAds();
    }
    

    #endregion

    #region VolumePanel

    public void SaveVol()
    {
        AudioSupport.instance.SaveSound();
    }

    private void LoadSound()
    {
        musicSlider.value = DBManager.instance.musicVolume;
        sfxSlider.value = DBManager.instance.sfxVolume;
    }

    #endregion

}
