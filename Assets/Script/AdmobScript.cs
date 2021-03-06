﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
public class AdmobScript : MonoBehaviour
{
    string App_ID = "ca-app-pub-1655687549880781~6314859231";

    //TESTING
    string BannerAdId = "ca-app-pub-3940256099942544/6300978111";
    string InterstitialAdId = "ca-app-pub-3940256099942544/1033173712";
    string RewardedAdId = "ca-app-pub-3940256099942544/5224354917";

    //REAL
    //string BannerAdId = "ca-app-pub-1655687549880781/5811478110";
    //string InterstitialAdId = "ca-app-pub-1655687549880781/8250757439";
    //string RewardedAdId = "ca-app-pub-1655687549880781/2998430751";

    private BannerView bannerView;
    private InterstitialAd interstitial;
    public RewardBasedVideoAd rewardedAd;

    public Text adStats;


    public bool includePreparation;
    /// <summary>
    /// 0 = health
    /// 1 = attack 
    /// 2 = energy
    /// </summary>
    public int selectedPowerUp { get; set; }
    public Booster theBooster;

    void Start()
    {
        if (includePreparation)
        {
            theBooster = FindObjectOfType<Booster>();
        }
        MobileAds.Initialize(App_ID);
        RequestBanner();
        RequestInterstitial();
        StartCoroutine(AutoShowBanner());

        rewardedAd = RewardBasedVideoAd.Instance;

        rewardedAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestRewardVideoNew();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //RequestBanner();
            //RequestInterstitial();
            RequestRewardVideo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //ShowBanner();
            //ShowInterstitial();
            ShowVideoRewardedAd();
        }


    }

    public void RequestBanner()
    {
        //AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        AdSize adSize = new AdSize(320, 50);
        this.bannerView = new BannerView(BannerAdId, adSize, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
    }

    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }

    public void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(InterstitialAdId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void RequestRewardVideo()
    {
        rewardedAd = RewardBasedVideoAd.Instance;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request, RewardedAdId);
    }

    public void ShowVideoRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

    public void RequestRewardVideoNew()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request, RewardedAdId);
    }

    public void ShowRewardVideoNew()
    {
        StopCoroutine(CloseStats());
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            RequestRewardVideoNew();
        }
        else
        {
            adStats.gameObject.SetActive(true);
            StartCoroutine(CloseStats());
        }
    }

    // FOR EVENTS
    //============
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        // adStats.text = "Ad Loaded";
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //adStats.text = "Ad Failed to Load";
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, EventArgs args)
    {
        if (includePreparation)
        {
            switch (selectedPowerUp)
            {
                case 0:
                    {
                        if (theBooster != null)
                        {
                            theBooster.BoostHealth();
                        }
                        break;
                    }
                case 1:
                    {
                        if (theBooster != null)
                        {
                            theBooster.BoostAttack();
                        }
                        break;
                    }
                case 2:
                    {
                        if (theBooster != null)
                        {
                            theBooster.BoostEnergy();
                        }
                        break;
                    }
            }
        }
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        Debug.Log("NOT REWARDED");
    }

    public IEnumerator AutoShowBanner()
    {
        yield return new WaitForSecondsRealtime(10);
        ShowBanner();
    }

    public IEnumerator CloseStats()
    {
        yield return new WaitForSecondsRealtime(2);
        adStats.gameObject.SetActive(false);
    }
}
