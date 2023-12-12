using System;
using NongTrai;
using UnityEngine;
using UnityEngine.Advertisements;

public class QuangCao : PersistentSingleton<QuangCao>, IUnityAdsLoadListener
{
    public string myGameIdAndroid = "5479929";
    public string myGameIdIOS = "XXXXXXX";
    public string adUnitIdAndroid = "Interstitial_Android";
    public string radUnitIdAndroid = "Rewarded_Android";
    public string adUnitIdIOS = "Interstitial_iOS";
    public string myAdUnitId;
    public bool loadRewardAds;
    public bool loadInterAds;
    private bool testMode = false;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
	Advertisement.Initialize(myGameIdIOS, testMode);
	myAdUnitId = adUnitIdIOS;
#else
        Advertisement.Initialize(myGameIdAndroid, testMode);
        myAdUnitId = adUnitIdAndroid;
#endif

        Advertisement.Show("Banner_Android");

        Advertisement.Load(adUnitIdAndroid, this);
        Advertisement.Load(radUnitIdAndroid, this);
    }

    public void ShowAds()
    {
        Advertisement.Load(adUnitIdAndroid, this);
        Advertisement.Show(adUnitIdAndroid);
        loadInterAds = false;
    }

    public void ShowReAds(Action action = null)
    {
        Advertisement.Load(radUnitIdAndroid, this);
        var optionsadsShowListener = new CallbackRW(action);
        Advertisement.Show(radUnitIdAndroid, null, optionsadsShowListener);
        loadRewardAds = false;
    }

    public bool CanShowAds()
    {
        QuangCao.Instance.LoadAd();
        return loadRewardAds;
    }


    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + radUnitIdAndroid);
        Advertisement.Load(radUnitIdAndroid, this);
        Advertisement.Load(adUnitIdAndroid, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(radUnitIdAndroid))
        {
            loadRewardAds = true;
        }

        if (adUnitId.Equals(adUnitIdAndroid))
        {
            loadInterAds = true;
        }
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(radUnitIdAndroid) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            loadRewardAds = false;
        }

        if (adUnitId.Equals(adUnitIdAndroid) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            loadInterAds = false;
        }

        Advertisement.Load(radUnitIdAndroid, this);
        Advertisement.Load(adUnitIdAndroid, this);
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.


        if (adUnitId.Equals(radUnitIdAndroid))
        {
            loadRewardAds = false;
        }

        if (adUnitId.Equals(adUnitIdAndroid))
        {
            loadInterAds = false;
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
        if (adUnitId.Equals(radUnitIdAndroid))
        {
            loadRewardAds = true;
        }

        if (adUnitId.Equals(adUnitIdAndroid))
        {
            loadInterAds = true;
        }
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
        if (adUnitId.Equals(radUnitIdAndroid))
        {
            loadRewardAds = true;
        }

        if (adUnitId.Equals(adUnitIdAndroid))
        {
            loadInterAds = true;
        }
    }
}

public class CallbackRW : IUnityAdsShowListener
{
    public Action action;

    public CallbackRW(Action action)
    {
        this.action = action;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        action?.Invoke();
    }
}