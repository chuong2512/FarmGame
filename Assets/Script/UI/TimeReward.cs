using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NongTrai;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimeReward : MonoBehaviour
{
    public GameObject reward;
    public GameObject popup;
    public GameObject[] rewards;
    public Button receiveBtn;
    public GameObject openBtn;

    public TextMeshProUGUI timeTMP;

    private bool _isReceived = true;

    public float _timeCount;
    private static float TimeToReceive = 30 * 60;

    private void Start()
    {
        ResetData();

        receiveBtn.onClick.AddListener(OnClickReceive);

        receiveBtn.gameObject.SetActive(false);
    }

    private void OnClickReceive()
    {
        if (!_isReceived)
        {
            PlayerPrefs.SetFloat("RewardTimeCount", TimeToReceive);
            PlayerPrefs.SetFloat("LastTimeReward", DateTime.Now.DayOfYear);

            var random = Random.Range(0, 4);

            for (int i = 0; i < rewards.Length; i++)
            {
                rewards[i].SetActive(i == random);
            }

            switch (random)
            {
                case 0:
                    BuyCoin(100);
                    break;
                case 1:
                    BuyGem(20);
                    break;
                case 2:
                    BuyCua(5);
                    break;
                case 3:
                    BuyMin(5);
                    break;
            }

            _isReceived = true;

            reward.SetActive(false);
        }
    }

    private void ResetData()
    {
        _timeCount = PlayerPrefs.GetFloat("RewardTimeCount", TimeToReceive);

        var lastTime = PlayerPrefs.GetFloat("LastTimeReward", 0);
        var currentTime = DateTime.Now.DayOfYear;

        if (currentTime > lastTime)
        {
            _isReceived = false;
            PlayerPrefs.SetFloat("RewardTimeCount", TimeToReceive);
        }
    }

    public void SetActive(bool b)
    {
        popup.SetActive(b);
    }

    void Update()
    {
        SetTime();
    }

    private void SetTime()
    {
        openBtn.SetActive(!_isReceived);

        receiveBtn.gameObject.SetActive(_timeCount <= 0);

        if (_timeCount > 0)
        {
            _timeCount -= Time.deltaTime;

            var timeSpan = TimeSpan.FromSeconds(_timeCount);

            timeTMP.SetText(timeSpan.ToString(@"mm\:ss"));
        }
        else
        {
            timeTMP.SetText("--:--");
        }
    }

    private void OnDisable()
    {
        if (_isReceived)
        {
            PlayerPrefs.SetFloat("RewardTimeCount", TimeToReceive);
        }
        else
        {
            PlayerPrefs.SetFloat("RewardTimeCount", _timeCount);
        }
    }

    private void BuyCoin(int amount)
    {
        ManagerCoin.Instance.ReciveGold(amount);
    }

    private void BuyGem(int amount)
    {
        ManagerGem.Instance.ReciveGem(amount);
    }

    private void BuyCua(int amount)
    {
        ManagerShop.instance.AddToolDecorate(0, amount);
    }

    private void BuyMin(int amount)
    {
        ManagerShop.instance.AddToolDecorate(1, amount);
    }
}