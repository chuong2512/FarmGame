﻿using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsAppOpen : MonoBehaviour
{
#if UNITY_ANDROID
    //id test
    //private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/3419835294";
    private const string AD_UNIT_ID = "ca-app-pub-5559154090292842/7534455992";
#elif UNITY_IOS
    private const string AD_UNIT_ID = "ca-app-pub-3321735106491906/1464416834";
#else
    private const string AD_UNIT_ID = "unexpected_platform";
#endif

    private static AdsAppOpen instance;

    private AppOpenAd ad;

    private bool isShowingAd = false;

    public static AdsAppOpen Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AdsAppOpen();
            }

            return instance;
        }
    }

    private bool IsAdAvailable
    {
        get
        {
            return ad != null;
        }
    }
    private void Start()
    {
        LoadAd();
    }
    public void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(AD_UNIT_ID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
        }));
    }
    public void ShowAdIfAvailable()
    {
        Debug.Log("IsAdAvailable: " + IsAdAvailable);
        Debug.Log("isShowingAd: " + isShowingAd);
        if (!IsAdAvailable || isShowingAd)
        {
            return;
        }
        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();
        Debug.Log("show app openn");
    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
                args.AdValue.CurrencyCode, args.AdValue.Value);
    }
   
}
