using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class MobileRewardVideoAd : MonoBehaviour
{
    public static MobileRewardVideoAd instance;
    //ca-app-pub-3940256099942544/5224354917
    //ca-app-pub-5019637857620440/2812154656
    string IdRewardedVideoAndroid = "ca-app-pub-5559154090292842/8038207016";
    string IdRewardedVideoIOs;
    private RewardedAd rewardBasedVideoAd;
    private int idReward;
    private int MaxWatch = 10;
    [SerializeField] Text QuestionTitleText;
    [SerializeField] Text QuestionRewardText;
    [SerializeField] Text QuestionDescribeText;
    [SerializeField] Image QuestionRewardImage;
    [SerializeField] Text CongretulateTitleText;
    [SerializeField] Text CongretulateRewardText;
    [SerializeField] Image CongretulateRewardImage;
    [SerializeField] GameObject Question;
    [SerializeField] GameObject Congretulate;
    [SerializeField] GameObject GiftWordSpace;
    [SerializeField] int[] QuantityReward;
    [SerializeField] Sprite[] IconReward;
    private int totalWatchDay
    {
        get { if (PlayerPrefs.HasKey("totalWatchDay") == false) PlayerPrefs.SetInt("totalWatchDat", 0); return PlayerPrefs.GetInt("totalWatchDay"); }
        set { PlayerPrefs.SetInt("totalWatchDay", value); }
    }
    private int rewardDayNow
    {
        get { if (PlayerPrefs.HasKey("rerwardDayNow") == false) PlayerPrefs.SetInt("rerwardDayNow", 0); return PlayerPrefs.GetInt("rerwardDayNow"); }
        set { PlayerPrefs.SetInt("rerwardDayNow", value); }
    }
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        if (rewardDayNow != System.DateTime.Now.Day)
        {
            totalWatchDay = 10;
            rewardDayNow = System.DateTime.Now.Day;
            RequestRewardedVideo();
        }
        else if (rewardDayNow == System.DateTime.Now.Day)
        {
            if (totalWatchDay > 0) RequestRewardedVideo();
            else GiftWordSpace.SetActive(false);
        }
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            QuestionTitleText.text = "Bạn có đồng ý xem video?";
            QuestionDescribeText.text = "Phần thưởng cho bạn";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            QuestionTitleText.text = "Tonton videonya?";
            QuestionDescribeText.text = "Hadiah untuk Anda";
        }
        else
        {
            QuestionTitleText.text = "Watch the video?";
            QuestionDescribeText.text = "Reward For You";
        }
    }

    void RequestRewardedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = IdRewardedVideoAndroid;
#elif UNITY_IPHONE
        string adUnitId = IdRewardedVideoIOs;
#else
        string adUnitId = "unexpected_platform";
#endif
        rewardBasedVideoAd = new RewardedAd(adUnitId);
        rewardBasedVideoAd.OnAdLoaded += OnAdLoaded;
        rewardBasedVideoAd.OnAdFailedToLoad += OnAdFailedToLoad;
        rewardBasedVideoAd.OnAdLoaded += OnAdLoaded;
        rewardBasedVideoAd.OnAdClosed += OnAdClosed;
        rewardBasedVideoAd.OnUserEarnedReward += OnAdRewarded;
        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideoAd.LoadAd(request);
    }
    void OnAdLoaded(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("[Ad - reward] OnAdLoaded");
    }

    void OnAdFailedToLoad(object sender, System.EventArgs eventArgs)
    {
        //if (onFailed != null)
        //    onFailed.Invoke();
        Debug.Log("[Ad - reward] OnAdFailedToLoad");
    }
    void OnAdStarted(object sender, System.EventArgs eventArgs)
    {

    }

    void OnAdClosed(object sender, System.EventArgs eventArgs)
    {
        rewardBasedVideoAd.OnAdLoaded -= OnAdLoaded;
        rewardBasedVideoAd.OnAdFailedToLoad -= OnAdFailedToLoad;
        rewardBasedVideoAd.OnAdLoaded -= OnAdLoaded;
        rewardBasedVideoAd.OnAdClosed -= OnAdClosed;
        rewardBasedVideoAd.OnUserEarnedReward -= OnAdRewarded;
        totalWatchDay -= 1;
        if (totalWatchDay > 0) RequestRewardedVideo();
        else GiftWordSpace.SetActive(false);
    }

    void OnAdRewarded(object sender, System.EventArgs eventArgs)
    {
        ShowReward();
    }

    public void ButtonGift()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        if (totalWatchDay > 0)
        {
            if (rewardBasedVideoAd.IsLoaded() == true)
            {
                idReward = Random.Range(0, 4);
                QuestionRewardText.text = "" + QuantityReward[idReward];
                QuestionRewardImage.sprite = IconReward[idReward];
                Question.SetActive(true);
            }
            else if (rewardBasedVideoAd.IsLoaded() == false)
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Mời bạn thử lại lần khác, chúc bạn may mắn!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Silakan coba lagi nanti, semoga berhasil!";
                    else str = "Please try again later, good luck!";
                    Notification.instance.dialogBelow(str);
                }
                else if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Mời bạn thử lại lần khác, chúc bạn may mắn!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Silakan coba lagi nanti, semoga berhasil!";
                    else str = "Please try again later, good luck!";
                    Notification.instance.dialogBelow(str);
                }
            }
        }
        else if (totalWatchDay <= 0)
        {
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Bạn đã hết quà tặng trong ngày!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Koneksi internet Anda tidak stabil!";
            else str = "You have missed your gift of the day!";
            Notification.instance.dialogBelow(str);
        }
    }

    public void ButtonYes()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        Question.SetActive(false);
        rewardBasedVideoAd.Show();
    }

    public void ButtonNo()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        Question.SetActive(false);
    }

    private void ShowReward()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            CongretulateTitleText.text = "Chúc mừng";
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
            CongretulateTitleText.text = "Selamat";
        else CongretulateTitleText.text = "Congratulations";
        CongretulateRewardText.text = "" + QuantityReward[idReward];
        CongretulateRewardImage.sprite = IconReward[idReward];
        Congretulate.SetActive(true);
    }

    public void ButtonConfirmReward()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        Congretulate.SetActive(false);
        if (idReward == 0)
        {
            Gem.instance.RegisterGemSingle(QuantityReward[idReward], Congretulate.transform.position);
        }
        else if (idReward > 0)
        {
            ManagerMarket.instance.ReciveItem(5, idReward - 1, QuantityReward[idReward], true);
            ManagerTool.instance.RegisterItemSingle(QuantityReward[idReward], IconReward[idReward], Congretulate.transform.position);
        }
    }
}
