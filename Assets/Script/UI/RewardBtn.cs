using System;
using System.Collections;
using System.Collections.Generic;
using NongTrai;
using UnityEngine;
using UnityEngine.UI;

public class RewardBtn : MonoBehaviour
{
    public int index;

    private bool _isReceive;

    public Button receiveBtn;
    public GameObject tickObj;

    private void Start()
    {
        receiveBtn.onClick.AddListener(OnClickReceiveBtn);
    }

    private void OnClickReceiveBtn()
    {
        switch (index)
        {
            case 0:
                BuyLua(20);
                break;
            case 1:
                BuyMin(2);
                break;
            case 2:
                BuyNgo(5);
                break;
            case 3:
                BuyCua(5);
                BuyMin(5);
                break;
            case 4:
                BuyLua(10);
                BuyNgo(10);
                BuyCaRot(10);
                break;
        }

        PlayerPrefs.SetInt($"ReceiveAds{index}", 1);

        receiveBtn.gameObject.SetActive(false);
        tickObj.SetActive(true);
    }

    public void SetInfo(int id, bool isReceive)
    {
        _isReceive = isReceive;
        this.index = id;

        bool active = false;
        var time = PlayerPrefs.GetInt($"WatchAdsTimes", 0);
        switch (index)
        {
            case 0:
                active = time >= 1;
                break;
            case 1:
                active = time >= 2;
                break;
            case 2:
                active = time >= 5;
                break;
            case 3:
                active = time >= 10;
                break;
            case 4:
                active = time >= 20;
                break;
        }

        receiveBtn.gameObject.SetActive(active && !isReceive);

        tickObj.SetActive(isReceive);
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

    private void BuyLua(int amount)
    {
        ManagerShop.instance.AddSeeds(0, amount);
    }

    private void BuyNgo(int amount)
    {
        ManagerShop.instance.AddSeeds(1, amount);
    }

    private void BuyCaRot(int amount)
    {
        ManagerShop.instance.AddSeeds(4, amount);
    }
}