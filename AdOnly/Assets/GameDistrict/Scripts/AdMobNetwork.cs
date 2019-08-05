using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AdMobNetwork : MonoBehaviour, IGDAdNetWork
{
    public  BannerView BannerView;
	public  InterstitialAd InterstitialAd;
	public  RewardBasedVideoAd RewardBasedVideoAd;

    public string InterstitialAdID
    {
        get
        {
            string id = AdNetworkSettings.Instance.AdMobInterstitialID;
            if(string.IsNullOrEmpty(id))
            {
                id = RemoteSettings.GetString("AdMobInterstitialID","");
            }
            return id;
        }
    }
    public string RewardVideoAdId
    {
        get
        {
            string id = AdNetworkSettings.Instance.AdMobRewardVideoID;
            if(string.IsNullOrEmpty(id))
            {
                id = RemoteSettings.GetString("AdMobRewardVideoID","");
            }
            return id;
        }
    }
    public string BannerAdId
    {
        get
        {
            string id = AdNetworkSettings.Instance.AdMobBannerID;
            if(string.IsNullOrEmpty(id))
            {
                id = RemoteSettings.GetString("AdMobBannerID","");
            }
            return id;
        }
    }
    public int Priority 
    { 
        get
        {
            return AdNetworkSettings.Instance.AdmobPeriority;
        } 
    }
    protected Action<bool> SuccessCallback
    {
        get;
        set;
    }
    protected Action<bool> FailedCallback
    {
        get;
        set;
    }

    public void Inititialize(string NetworkID)
    {
        MobileAds.Initialize(AdNetworkSettings.Instance.AdMobNetworkID);
        
        CreateAndLoadVideo();
        RewardBasedVideoAd.OnAdFailedToLoad += OnRewardedVideoFailed;
        RewardBasedVideoAd.OnAdStarted += OnRewardedVideoStarted;
        RewardBasedVideoAd.OnAdClosed += OnRewardedVideoClosed;
        RewardBasedVideoAd.OnAdRewarded += OnRewardedVideoRewarded;
        RewardBasedVideoAd.OnAdCompleted += OnRewardedVideoCompleted;

        CreateAndLoadInterstitial();
        InterstitialAd.OnAdClosed += OnInterstitialClosed;
        InterstitialAd.OnAdLoaded += OnInterstitialLoaded;
        InterstitialAd.OnAdFailedToLoad += OnInterstitialFailedToLoad;
        
    }
    protected void CreateAndLoadVideo()
    {
        RewardBasedVideoAd =  RewardBasedVideoAd.Instance;
        AdRequest request = new AdRequest.Builder().Build();
        RewardBasedVideoAd.LoadAd(request, RewardVideoAdId);

    }
    protected void CreateAndLoadInterstitial()
    {
        InterstitialAd = new InterstitialAd(InterstitialAdID);
        AdRequest request = new AdRequest.Builder().Build();
        InterstitialAd.LoadAd(request);
    }
    
    private void OnInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if(SuccessCallback != null)
        {
            SuccessCallback(true);
        }
        DestroyCallBacks();
    }

    private void OnInterstitialLoaded(object sender, EventArgs e)
    {
        //InterstitialAd.Show();
    }

    private void OnInterstitialClosed(object sender, EventArgs e)
    {
        if(SuccessCallback != null)
        {
            SuccessCallback(true);
        }
        SuccessCallback = null;
        FailedCallback = null;
    }

    public void CreateBanner(AdPosition adPosition)
    {
        BannerView = new BannerView(BannerAdId,AdSize.Banner,adPosition);

        BannerView.OnAdClosed += OnBannerClosed;
        BannerView.OnAdLoaded += OnBannerLoaded;
        BannerView.OnAdFailedToLoad += OnBannerFialedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        BannerView.LoadAd(request);

        
    }
    private void OnBannerFialedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        if(FailedCallback != null)
        {
            FailedCallback(true);
            FailedCallback = null;
        }
    }

    private void OnBannerLoaded(object sender, EventArgs e)
    {
        if(SuccessCallback != null)
        {
            SuccessCallback(true);
            DestroyCallBacks();
            BannerView.Show();
        }
    }

    private void OnBannerClosed(object sender, EventArgs e)
    {
        if(SuccessCallback != null)
        {
            SuccessCallback(true);
            DestroyCallBacks();
        }
    }
    public bool IsInterstitialAvailable()
    {
        return 
            InterstitialAd.IsLoaded();
    }

    public bool IsRewardVideoAvailable()
    {
        return 
            RewardBasedVideoAd.IsLoaded();
    }
    protected void ShowRewardVideoInternal()
    {
        if(IsRewardVideoAvailable())
        {
            RewardBasedVideoAd.Show();
        }else
        {
            Debug.Log("Not Found Video Ad:");
        }
    }
    public void ShowInterstitial(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdId = null)
    {
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
        if(IsInterstitialAvailable())
        {
            InterstitialAd.Show();
        }else
        {
            Debug.Log("Interstitial Not Found");
        }
       
    }

    public void ShowRewardVideo(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdId = null)
    {
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
        if(IsRewardVideoAvailable())
        {
            ShowRewardVideoInternal();
        }
        
    }
    public void OnRewardedVideoFailed(object sender, AdFailedToLoadEventArgs args)
	{
        if(FailedCallback != null)
        {
            FailedCallback(true);
            DestroyCallBacks();
        }
        MonoBehaviour.print(
            "Failed:"+args.Message);
	}

	public void OnRewardedVideoOpen(object sender, EventArgs args)
	{
        //TO DO
	}

	public void OnRewardedVideoStarted(object sender, EventArgs args)
	{
        //TO DO
	}

	public void OnRewardedVideoClosed(object sender, EventArgs args)
	{
        //TO DO
	}
	public void OnRewardedVideoRewarded(object sender, Reward args)
	{
      
        if(SuccessCallback != null)
        {
            SuccessCallback(true);
            SuccessCallback = null;
        }
	}
    public void OnRewardedVideoCompleted(object sender, EventArgs args)
	{
        //TO DO
	}
    protected void DestroyCallBacks()
    {
        SuccessCallback = null;
        FailedCallback = null;
    }
    public void ShowBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback, AdPosition adPosition, string AdID = null)
    {
        CreateBanner(adPosition);
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
    }

    public void DestroyBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdID = null)
    {
        BannerView.Destroy();
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
    }
}
