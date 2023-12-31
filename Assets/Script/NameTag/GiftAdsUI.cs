using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using NongTrai;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GiftAdsUI : Singleton<GiftAdsUI>
{
    [FormerlySerializedAs("_open")] [SerializeField] private GameObject open;
    [FormerlySerializedAs("_open")] [SerializeField] private GameObject fail;
   
    [SerializeField] private Button adsButton;

    
    private void Start()
    {
        adsButton.onClick.AddListener(ShowAds);
    }

    [Button]
    public void OpenPopup()
    {
        open.SetActive(true);
        fail.SetActive(false);
    }
    
    private void ShowAds()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Debug.Log("Show Ads");
                
            QuangCaoGoogle.Instance.ShowRewardedAds(() =>
            {
                Debug.Log("Get Gem");
                ManagerGem.Instance.ReciveGem(2);
                open.SetActive(false);
            });
        }
        else
        {
            fail.SetActive(true);
        }
    }
    
}