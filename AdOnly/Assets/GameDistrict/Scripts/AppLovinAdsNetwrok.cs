using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

public class AppLovinAdsNetwrok : MonoBehaviour,IGDAdNetWork
{
    
    protected string NetWorkID
    {
        get
        { 
            string id = RemoteSettings.GetString("UnityAdNetworkId","");
            if(string.IsNullOrEmpty(id))
            {
                id = AdNetworkSettings.Instance.UnityAdNerworkId;
            }
            return id;
        }
    }
    protected bool IsInterstitilaAvilable
    {
        get
        {
            return 
                AppLovin.HasPreloadedInterstitial();
        }
    }
    protected bool IsRewardVideoAvilable
    {
        get
        {
            return 
                AppLovin.IsIncentInterstitialReady();
        }
    }
    protected ShowResult ShowResultStatus
    {
        get;
        set;
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
    public int Priority 
    { 
        get
        {
            return AdNetworkSettings.Instance.ApplovinAdPeriority;
        } 
    }
    public bool IsRewardVideoAvailable()
    {
        return IsRewardVideoAvilable;
    }
    public bool IsInterstitialAvailable()
    {
        return IsInterstitilaAvilable;
    }
    public void Inititialize(string netWorkID)
    {
        AppLovin.SetSdkKey(AdNetworkSettings.Instance.AppLovinAdNetworkId);
        AppLovin.SetUnityAdListener(gameObject.name);//(FindObjectOfType<GameDistrictAdNetWorkManager>().name);
        AppLovin.InitializeSdk();
        AppLovin.PreloadInterstitial();
        AppLovin.LoadRewardedInterstitial();
    }

    public void ShowInterstitial(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdId = null)
    {
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
        if(IsInterstitialAvailable())
        {
            AppLovin.ShowInterstitial();
        }else
        {
            FailedCallback(true);
            DestroyCallBacks();
        }
    }

    public void ShowRewardVideo(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdId = null)
    {
        this.SuccessCallback =SuccessCallback;
        this.FailedCallback = FailedCallback;
        if(IsRewardVideoAvailable())
        {
            AppLovin.ShowRewardedInterstitial();
        }
    }
    void onAppLovinEventReceived(string ev)
    {
        if (ev.Contains("REWARDAPPROVED")) 
        {
            if(SuccessCallback != null)
            {
                SuccessCallback(true);
                DestroyCallBacks();
            }
            
        }
        else if(ev.Contains("LOADEDINTER")) 
        {
            // An interstitial ad was successfully loaded.
            if(SuccessCallback != null)
            {
                SuccessCallback(true);
                DestroyCallBacks();
            }
        }
        else if(ev.Contains("LOADREWARDEDFAILED")) 
        {
            // A rewarded video failed to load.
             if(FailedCallback != null)
            {
                FailedCallback(true);
                DestroyCallBacks();
            }
        }
        else if(ev.Contains("HIDDENREWARDED")) 
        {
            // A rewarded video has been closed.  Preload the next rewarded video.
            AppLovin.LoadRewardedInterstitial();
        }
        else if(ev.Contains("HIDDENINTER")) 
        {
            // A rewarded video has been closed.  Preload the next rewarded video.
            AppLovin.PreloadInterstitial();
        }
    }
    protected void DestroyCallBacks()
    {
        SuccessCallback = null;
        FailedCallback = null;
    }
    public void ShowBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback, AdPosition adPosition, string AdID = null)
    {
        throw new NotImplementedException();
    }

    public void DestroyBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdID = null)
    {
        throw new NotImplementedException();
    }
}
