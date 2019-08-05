using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class AdNetworkSettings : ScriptableObject
{
    
    
    public string AdMobNetworkID;
    public string AdMobBannerID;
    public string AdMobInterstitialID;
    public string AdMobRewardVideoID;
    public int AdmobPeriority;
    public string UnityAdNerworkId;
    public int UnityAdPeriority;

    public string AppLovinAdNetworkId;
    public int ApplovinAdPeriority;


    const string GDANSettingsAssetName = "GDAdNetworkSettings";
    const string GDANSettingsPath = "GameDistrict/Resources";
    const string GDANSettingsAssetExtension = ".asset";
    private static AdNetworkSettings instance
    {
        get;
        set;
    }
    public static AdNetworkSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load(GDANSettingsAssetName) as AdNetworkSettings;
                if (instance == null)
                {
                    // If not found, autocreate the asset object.
                    instance = CreateInstance<AdNetworkSettings>();
#if UNITY_EDITOR
                    string properPath = Path.Combine(Application.dataPath, GDANSettingsPath);
                    if (!Directory.Exists(properPath))
                    {
                        AssetDatabase.CreateFolder("Assets/GameDistrict", "Resources");
                    }

                    string fullPath = Path.Combine(Path.Combine("Assets", GDANSettingsPath),
                            GDANSettingsAssetName + GDANSettingsAssetExtension
                        );
                    AssetDatabase.CreateAsset(instance, fullPath);
#endif
                }
            }
            return instance;
        }
    }
#if UNITY_EDITOR
    [MenuItem("GameDistrict/Edit Settings")]
    public static void Edit()
    {
        Selection.activeObject = Instance;
    }
#endif
    public void SetAdmobNetworkId(string id)
    {
        AdMobNetworkID = id;
        DirtyEditor();
    }
    public void SetAdmobBannerId(string id)
    {
        AdMobBannerID = id;
        DirtyEditor();
    }
    public void SetAdmobInterstitialId(string id)
    {
        AdMobInterstitialID = id;
        DirtyEditor();
    }
    public void SetAdmobRewardVideoId(string id)
    {
        AdMobRewardVideoID = id;
        DirtyEditor();
    }
    public void SetAdmobPeriority(string periority)
    {
        AdmobPeriority = int.Parse(periority);
        DirtyEditor();
    }
    public void SetUnityAppId(string id)
    {
        UnityAdNerworkId = id;
        DirtyEditor();
    }
    public void SetUnityAdPeriority(string periority)
    {
        UnityAdPeriority = int.Parse(periority);;
        DirtyEditor();
    }
    public void SetAppLovinId(string id)
    {
        AppLovinAdNetworkId = id;
        DirtyEditor();
    }
    public void SetAppLovinAdPeriority(string periority)
    {
        ApplovinAdPeriority = int.Parse(periority);
        DirtyEditor();
    }
    private static void DirtyEditor()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(Instance);
#endif
    }
}
