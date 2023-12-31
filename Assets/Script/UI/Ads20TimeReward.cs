using UnityEngine;
using UnityEngine.UI;

public class Ads20TimeReward : MonoBehaviour
{
    public RewardBtn[] RewardBtns;

    public GameObject popup;
    public Image slider;
    public Button watchAdsBtn;
    public float[] fillAmounts;

    private void Start()
    {
        watchAdsBtn.onClick.AddListener(OnClickBtnAds);

        ReloadData();
    }

    private void OnClickBtnAds()
    {
        /*if (QuangCao.Instance.CanShowAds() && Application.internetReachability != NetworkReachability.NotReachable)
        {
            Debug.Log("Show Ads");
                
            QuangCao.Instance.ShowReAds(() =>
            {
                var time = PlayerPrefs.GetInt($"WatchAdsTimes", 0);
                PlayerPrefs.SetInt($"WatchAdsTimes", time + 1);
                ReloadData();
                RefreshButtons();
            });
        }*/
    }

    private void ReloadData()
    {
        var time = PlayerPrefs.GetInt($"WatchAdsTimes", 0);

        var index = Mathf.Min(time, fillAmounts.Length - 1);

        slider.fillAmount = fillAmounts[index];
    }

    public void SetActive(bool b)
    {
        popup.SetActive(b);

        if (b)
        {
            RefreshButtons();
        }
    }

    public void RefreshButtons()
    {
        for (int i = 0; i < RewardBtns.Length; i++)
        {
            RewardBtns[i].SetInfo(i, PlayerPrefs.GetInt($"ReceiveAds{i}", 0) == 1);
        }
    }
}