using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class GameDistrictFBAnalyticsManager : MonoBehaviour
{
    protected static GameDistrictFBAnalyticsManager mInstance
    {
        get;
        set;
    }
    public static GameDistrictFBAnalyticsManager Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<GameDistrictFBAnalyticsManager>();
                if(mInstance == null)
                {
                    mInstance = new GameObject().AddComponent<GameDistrictFBAnalyticsManager>();
                }
                DontDestroyOnLoad(mInstance.gameObject);
                
            }
            return mInstance;
        }
    }
    protected bool IsFBInitialized
    {
        get
        {
            bool result = false;
            if(FB.IsInitialized) 
            {
                result =  true;
            }
            FB.Init(() => 
            {
                FB.ActivateApp();
                result =  true;
            });
            return result;
        }
    }
    public void FBAppActivated()
    {
        if(IsFBInitialized)
        {
            FB.LogAppEvent(AppEventName.ActivatedApp,null,new Dictionary<string, object>()
            {
                [Application.productName] = Application.productName +" activated..."
            });
        }
    }
}
