using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class MobileFullVideo : MonoBehaviour
{
    //full Video Ad
    private static InterstitialAd interstitial;
    public static MobileFullVideo instance;
    string adUnitIdAndroid = "ca-app-pub-5559154090292842/9843614215";
    string adUnitIdIos = "";
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        MobileAds.SetiOSAppPauseOnBackground(true);

        MobileAds.Initialize(initStatus => { });

        RequestInterstitial();
    }

    #region
    public void ShowFullNormal()
    {
        try
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            else
            {
                RequestInterstitial();
            }
        }
        catch
        {

        }
    }

    public void RequestInterstitial()
    {
        try
        {
#if UNITY_ANDROID
            string adUnitId = adUnitIdAndroid;
#elif UNITY_IPHONE
        string adUnitId = adUnitIdIos;
#endif
            if (interstitial != null)
            {
                interstitial.Destroy();
            }
            // Initialize an InterstitialAd.
            interstitial = new InterstitialAd(adUnitId);

            // Called when an ad request has successfully loaded.
            interstitial.OnAdLoaded += HandleOnAdLoaded;
            // Called when an ad request failed to load.
            interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            // Called when an ad is shown.
            interstitial.OnAdOpening += HandleOnAdOpened;
            // Called when the ad is closed.
            interstitial.OnAdClosed += HandleOnAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            interstitial.LoadAd(request);
        }
        catch
        {

        }
    }

    public static void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        RequestInterstitial();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion
}
