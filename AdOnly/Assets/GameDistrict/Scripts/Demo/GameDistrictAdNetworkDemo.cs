using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDistrictAdNetworkDemo : MonoBehaviour
{
    public TextMeshProUGUI LogTextBox;
    public Button BtnBanner;
    public Button BtnInrterstitial;
    public Button BtnRewardVideo;


    void Start()
    {
        BtnBanner.onClick.AddListener(()=>
        {
            GameDistrictAdNetWorkManager.Instance.ShowBanner
            (
                (staus)=>
                {
                    string Message = "\nSuccess call back here";
                    Debug.Log(Message);
                    LogTextBox.text += Message;
                },
                (staus)=>
                {
                    string Message = "\nFailed call back here";
                    Debug.Log(Message);
                    LogTextBox.text += Message;
                }, 
                GoogleMobileAds.Api.AdPosition.Top
            );
        });
        BtnInrterstitial.onClick.AddListener(()=>
        {
            GameDistrictAdNetWorkManager.Instance.ShowInterstitial(
                (staus)=>
                {
                    string Message = "\nSuccess call back here";
                    Debug.Log(Message);
                    LogTextBox.text += Message;
                },
                (staus)=>
                {
                    string Message = "\nFailed call back here";
                    Debug.Log(Message);
                    LogTextBox.text += Message;
                }
            );

        });
        BtnRewardVideo.onClick.AddListener(()=>
        {
            GameDistrictAdNetWorkManager.Instance.ShowRewardVideo(
                (staus)=>
                {
                    string Message = "\nSuccess call back here";
                    Debug.Log(Message);
                    LogTextBox.text += Message;
                },
                (staus)=>
                {
                    string Message = "\nFailed call back here";
                    Debug.Log(Message);
                    LogTextBox.text += Message;
                }
            );

        });
    }   
}
