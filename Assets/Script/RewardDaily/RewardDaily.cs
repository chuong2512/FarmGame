using UnityEngine;
using UnityEngine.UI;

public class RewardDaily : MonoBehaviour
{
    private int IdReward;
    [SerializeField] Text TitleText;
    [SerializeField] Text AmountRewardText;
    [SerializeField] Image IconRewardImage;
    [SerializeField] GameObject Reward;
    [SerializeField] int[] idStype;
    [SerializeField] int[] IdItem;
    [SerializeField] int[] AmountReward;
    [SerializeField] Sprite[] IconRewardSprite;

    private int DayGotDailyLast
    {
        get { if (PlayerPrefs.HasKey("DayGotDailyLast") == false) PlayerPrefs.SetInt("DayGotDailyLast", 0); return PlayerPrefs.GetInt("DayGotDailyLast"); }
        set { PlayerPrefs.SetInt("DayGotDailyLast", value); }
    }

    void Start()
    {
        if (DayGotDailyLast != System.DateTime.Now.Day)
        {
            IdReward = Random.Range(0, idStype.Length);
            Reward.SetActive(true);
            AmountRewardText.text = "" + AmountReward[IdReward];
            IconRewardImage.sprite = IconRewardSprite[IdReward];
            Reward.SetActive(true);
            ManagerAudio.instance.PlayAudio(Audio.NewItem);
        }
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            TitleText.text = "Quà tặng hằng ngày";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            TitleText.text = "Penghargaan Harian";
        }
        else
        {
            TitleText.text = "Daily Gift";
        }
    }

    public void ButtonComfirm()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        if (idStype[IdReward] < 6)
        {
            ManagerMarket.instance.ReciveItem(idStype[IdReward], IdItem[IdReward], AmountReward[IdReward], true);
            ManagerTool.instance.RegisterItemSingle(AmountReward[IdReward], IconRewardSprite[IdReward], Reward.transform.position);
        }
        else if (idStype[IdReward] >= 6)
        {
            Gem.instance.RegisterGemSingle(AmountReward[IdReward], Reward.transform.position);
        }
        Reward.SetActive(false);
        DayGotDailyLast = System.DateTime.Now.Day;
    }

    public void ButtonRewardDaily()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        Reward.SetActive(true);
    }

    public void BackButton()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        Reward.SetActive(false);
    }
}
