using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdNetworkSettings))]
public class AdNetworkSettingsEditor : Editor
{
    const string AdMobDocLink = "https://developers.google.com/admob/android/test-ads";
    const string UnityDocLink = "https://unityads.unity3d.com/help/unity/integration-guide-unity#implementing-rewarded-ads";
    const string Applovein = "https://developers.ironsrc.com/ironsource-mobile/unity/applovin-mediation-integration-guide-unity/";
    GUIContent AdmobLabel = new GUIContent("AdMob");
    GUIContent UnityLabel = new GUIContent("Unity Ads");
    GUIContent AppLovinLabel = new GUIContent("App Lovin");
    GUIContent AdmobBannerLabel = new GUIContent("Banner ID [?]:", "Admob Banner ID Can Found On Admob Dashboard" +AdMobDocLink);
    GUIContent AdmobInterstitialLabel = new GUIContent("Interstitial Id [?]:", "Admob Interstitial ID Can Found On Admob Dashboard "+AdMobDocLink );
    GUIContent AdmobRewardVideoLabel = new GUIContent("Reward Video Id [?]:", "Admob Reward Video ID Can Found On Admob Dashboard "+ AdMobDocLink );
    GUIContent AdmobPeriorityLabel = new GUIContent("Periority [?]:", "Admob Periority Means Which Network Get Chance First Call Note: Low Value Is Heightest " );
    
    GUIContent AdmobAppIdLabel = new GUIContent("Admob AppId [?]:", "Admob App Id Can Found On Admob Dashboard "+AdMobDocLink);
    GUIContent UnityAppIdLabel = new GUIContent("Unity Id [?]:", "Unity App Ids can be found at " +UnityDocLink);
    GUIContent UnityPeriorityLabel = new GUIContent("Periority [?]:", "Unity Periority Means Which Network Get Chance First Call Note: Low Value Is Heightest " );

    GUIContent ApplovinIdLabel = new GUIContent("Applovin Id:", "Applovein App Ids can be found at " +Applovein);
    GUIContent AppLovinPeriorityLabel = new GUIContent("Periority [?]:", "AppLovin Periority Means Which Network Get Chance First Call Note: Low Value Is Heightest " );

   private AdNetworkSettings instance;

    public override void OnInspectorGUI() 
    {
        instance = (AdNetworkSettings)target;

        SetupUI();
    }
    private void SetupUI() {
        // Admob
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(AdmobLabel);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(AdmobAppIdLabel);
        instance.SetAdmobNetworkId(EditorGUILayout.TextField(instance.AdMobNetworkID));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(AdmobBannerLabel);
        EditorGUILayout.LabelField(AdmobInterstitialLabel);
        EditorGUILayout.EndHorizontal();
        

        EditorGUILayout.BeginHorizontal();
        instance.SetAdmobBannerId(EditorGUILayout.TextField(instance.AdMobBannerID));
        instance.SetAdmobInterstitialId(EditorGUILayout.TextField(instance.AdMobInterstitialID));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(AdmobRewardVideoLabel);
        EditorGUILayout.LabelField(AdmobPeriorityLabel);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        instance.SetAdmobRewardVideoId(EditorGUILayout.TextField(instance.AdMobRewardVideoID));
        instance.SetAdmobPeriority(EditorGUILayout.TextField(instance.AdmobPeriority.ToString()));
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        // UNITY APP ID
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(UnityLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(UnityAppIdLabel);
        EditorGUILayout.LabelField(UnityPeriorityLabel);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        instance.SetUnityAppId(EditorGUILayout.TextField(instance.UnityAdNerworkId));
        instance.SetUnityAdPeriority(EditorGUILayout.TextField(instance.UnityAdPeriority.ToString()));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        // APPLOVIN APP ID

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(AppLovinLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(ApplovinIdLabel);
        EditorGUILayout.LabelField(AppLovinPeriorityLabel);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        instance.SetAppLovinId(EditorGUILayout.TextField(instance.AppLovinAdNetworkId));
        instance.SetAppLovinAdPeriority(EditorGUILayout.TextField(instance.ApplovinAdPeriority.ToString()));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Setup GAME DISTRICT SDK"))
        {
            DoSetup();
        }
        EditorGUILayout.EndHorizontal();
	}
    private void DoSetup() 
    {
        // // check that Android SDK is there
        // if (!HasAndroidSdk()) {
        //     Debug.LogError("Android SDK not found.");
        //     EditorUtility.DisplayDialog(sSdkNotFound,
        //         sSdkNotFoundBlurb, sOk);
        //     return;
        // }

        // // create needed directories
        // EnsureDirExists("Assets/Plugins");
        // EnsureDirExists("Assets/Plugins/Android");

        // // refresh assets, and we're done
        // AssetDatabase.Refresh();
        // EditorUtility.DisplayDialog(sSuccess,
        //     sSetupComplete, sOk);
    }
}
