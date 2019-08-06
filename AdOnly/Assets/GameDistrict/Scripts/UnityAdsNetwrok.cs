using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsNetwrok : MonoBehaviour,IGDAdNetWork,IUnityAdsListener
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
                Advertisement.IsReady("video");
        }
    }
    protected bool IsRewardVideoAvilable
    {
        get
        {
            return 
                Advertisement.IsReady("rewardedVideo");
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
            return AdNetworkSettings.Instance.UnityAdPeriority;
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
        AppLovin.SetUnityAdListener(gameObject.name);
        Advertisement.Initialize(NetWorkID,true);
    }
    public void ShowInterstitial(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdId = null)
    {
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
        if(IsInterstitialAvailable())
        {
            Advertisement.Show("video",new ShowOptions
                {
                    resultCallback = RewardVideoStatus
                });
        }else
        {
            FailedCallback(true);
            DestroyCallBacks();

        }
    }

    public void ShowRewardVideo(Action<bool> SuccessCallback, Action<bool> FailedCallback, string AdId = null)
    {
        this.SuccessCallback = SuccessCallback;
        this.FailedCallback = FailedCallback;
        if(IsRewardVideoAvilable)
        {
            Advertisement.Show("rewardedVideo",new ShowOptions
                {
                    resultCallback = RewardVideoStatus
                }
            );
        }
    }

    void RewardVideoStatus(ShowResult showResult)
    {
        Debug.Log("ShowResult:"+showResult.ToString());
        ShowResultStatus = showResult;
        switch(showResult)
        {
            case ShowResult.Finished:
                if(SuccessCallback != null)
                {
                    SuccessCallback(true);
                }
                DestroyCallBacks();

            break;
            case ShowResult.Skipped:
                //SuccessCallback(true);
            break;
            case ShowResult.Failed:
                if(FailedCallback!=null)
                {
                    FailedCallback(true);
                }
                DestroyCallBacks();
            break;
            
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

    public void OnUnityAdsReady(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new NotImplementedException();
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        throw new NotImplementedException();
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("ShowResult:"+showResult.ToString());
        switch(showResult)
        {
            case ShowResult.Finished:
                if(SuccessCallback != null)
                {
                    SuccessCallback(true);
                }
                DestroyCallBacks();
            break;
            case ShowResult.Skipped:
                //SuccessCallback(true);
            break;
            case ShowResult.Failed:
                if(FailedCallback!=null)
                {
                    FailedCallback(true);
                }
                DestroyCallBacks();
            break;
            
        }
    }
}
public interface IUnityAdsListener {
    void OnUnityAdsReady (string placementId);
    void OnUnityAdsDidError (string message);
    void OnUnityAdsDidStart (string placementId);
    void OnUnityAdsDidFinish (string placementId, ShowResult showResult);
}