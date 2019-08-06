using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameDistrictAdNetWorkManager : MonoBehaviour
{
    
    
    protected static GameDistrictAdNetWorkManager mInstance
    {
        get;
        set;
    }
    public static GameDistrictAdNetWorkManager Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<GameDistrictAdNetWorkManager>();
                if(mInstance == null)
                {
                    mInstance = new GameObject().AddComponent<GameDistrictAdNetWorkManager>();
                }
                DontDestroyOnLoad(mInstance.gameObject);
                
            }
            return mInstance;
        }
    }
    public List<IGDAdNetWork> AdNetworks = new List<IGDAdNetWork>();
    protected UnityAdsNetwrok UnityAdsNetwrok ;
    protected AdMobNetwork AdMobNetwork ;
    protected AppLovinAdsNetwrok AppLovinAdsNetwrok;


    void Awake()
    {
        if(Instance != null && 
           Instance != this
        )
        {
            Destroy(gameObject); 
        }
       Inititialize();

    }
    public void Inititialize()
    {
        AdMobNetwork = gameObject.AddComponent<AdMobNetwork>() as AdMobNetwork;
        UnityAdsNetwrok = gameObject.AddComponent<UnityAdsNetwrok>() as UnityAdsNetwrok;
        AppLovinAdsNetwrok = gameObject.AddComponent<AppLovinAdsNetwrok>() as AppLovinAdsNetwrok;
        
        AdMobNetwork.Inititialize(null);
        UnityAdsNetwrok.Inititialize(null);
        AppLovinAdsNetwrok.Inititialize(null);
        
        AdNetworks.Add(AdMobNetwork);
        AdNetworks.Add(UnityAdsNetwrok);
        AdNetworks.Add(AppLovinAdsNetwrok);
    }
    protected List<IGDAdNetWork> SortedNetwrokList
    {
        get
        {
            return 
                AdNetworks
                    .OrderBy(x => x.Priority)
                    .ToList();
        }
    }
    public void ShowInterstitial(Action<bool> SuccessCallback, Action<bool> FailedCallback)
    {   
        
        
        foreach(IGDAdNetWork netWork in SortedNetwrokList)
        {
            if(netWork.IsInterstitialAvailable())
            {
                LogMessage(netWork,"GDAdNetwork:");
                netWork.ShowInterstitial(SuccessCallback,FailedCallback);
                break;
            }else
            {
                LogMessage(netWork,"Not Available GDAdNetwork:");
            }
        }
    }
    public void ShowRewardVideo(Action<bool> SuccessCallback, Action<bool> FailedCallback)
    {   
        foreach(IGDAdNetWork netWork in SortedNetwrokList)
        {
            if(netWork.IsRewardVideoAvailable())
            {
                LogMessage(netWork,"GDAdNetwork:");
                netWork.ShowRewardVideo(SuccessCallback,FailedCallback);
                break;
            }else
            {
                LogMessage(netWork,"Not Available GDAdNetwork:");
            }
        }
    }
    public void ShowBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback , AdPosition adPosition)
    {   
        AdMobNetwork.ShowBanner(SuccessCallback,FailedCallback,adPosition);
    }
    
    public void ShowBanner()
    {
        ShowBanner(null,null,AdPosition.Top);
    }
    protected static void LogMessage(IGDAdNetWork netWork,string Message,string Message2 = "")
    {
        Debug.Log(Message+netWork.GetType().ToString()
            +" Priority:"+netWork.Priority+Message2);
    }
    protected static void LogSuccessCallback(bool status)
    {
        Debug.Log("Success call back here");
    }
    protected void LogFaildCallback(bool status)
    {
        Debug.Log("Failed call back here");
    }
}

public interface IGDAdNetWork
{
    int Priority
    {
        get;
    }
    void Inititialize(string NetworkID);
    bool IsRewardVideoAvailable();
    bool IsInterstitialAvailable();
    void ShowInterstitial(Action<bool> SuccessCallback, Action<bool> FailedCallback , string AdID = null);
    void ShowRewardVideo(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdID = null);
    void ShowBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback, AdPosition adPosition, string AdID = null);
    void DestroyBanner(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdID = null);
}
