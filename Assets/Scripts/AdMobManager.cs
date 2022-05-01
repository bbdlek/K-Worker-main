using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager instance;
    
    private RewardedAd ReviveAd;
    private InterstitialAd nextStageAd;

    [SerializeField] private bool noAdsPurchased;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    

    private void Start()
    {
        MobileAds.Initialize(status => { });

        InitRewardedAd();
        InitInterstitialAd();

        AdRequest request = new AdRequest.Builder().Build();
        this.ReviveAd.LoadAd(request);
        this.nextStageAd.LoadAd(request);
    }

    #region RewardedAd

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print(
            "HandleRewardedAdFailedToLoad event received with message: "
            + args.LoadAdError.GetMessage());
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        print("HandleRewardedAdOpening event received");
        Time.timeScale = 0f;
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print(
            "HandleRewardedAdFailedToShow event received with message: "
            + args.AdError.GetMessage());
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("HandleRewardedAdClosed event received");
        Time.timeScale = 1f;
        this.CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("Earned");
        FindObjectOfType<Player>().hp = FindObjectOfType<Player>().MaxHp;
    }

    private void InitRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif
        this.ReviveAd = new RewardedAd(adUnitId);
        
        // Called when an ad request has successfully loaded.
        this.ReviveAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.ReviveAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.ReviveAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.ReviveAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.ReviveAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.ReviveAd.OnAdClosed += HandleRewardedAdClosed;
    }
    
    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.ReviveAd = new RewardedAd(adUnitId);

        this.ReviveAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.ReviveAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.ReviveAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.ReviveAd.LoadAd(request);
    }

    public void ShowRewardAds()
    {
        if (this.ReviveAd.IsLoaded())
        {
            this.ReviveAd.Show();
        }
    }

    #endregion

    #region InterstitialAd
    private void InitInterstitialAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        if (nextStageAd != null)
        {
            nextStageAd.Destroy();
        }
        
        this.nextStageAd = new InterstitialAd(adUnitId);
        
        // Called when an ad request has successfully loaded.
        this.nextStageAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.nextStageAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.nextStageAd.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.nextStageAd.OnAdClosed += HandleOnAdClosed;
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        SceneManager.LoadScene("02.Stage2");
    }

    public void ShowInterstitialAd()
    {
        if (nextStageAd != null && nextStageAd.IsLoaded())
        {
            nextStageAd.Show();
        }
    }
    
    #endregion

    #region noAds

    public void noAdsPurchase()
    {
        IAPManager.instance.noAdsPurchased = true;
        PlayerPrefs.SetInt("NoAdsPurchased", 1);
    }

    #endregion
}
