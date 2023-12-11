using System;
using System.Collections;
using System.Collections.Generic;
using NongTrai;
using UnityEngine;
using UnityEngine.UI;

public class LoginReward : MonoBehaviour
{
    private readonly int max_day = 7;

    private int _currentDay;
    private bool _isReceived = true;

    [SerializeField] private Image[] dayImages;
    [SerializeField] private GameObject[] _ticks;
    [SerializeField] private Sprite _chooseImg, _unChooseImg;
    [SerializeField] private Button _collectBtn, _openBtn;
    [SerializeField] private GameObject _loginObj;

    // Start is called before the first frame update
    void Start()
    {
        ResetData();
        SetImages();
        SetBtn();

        SetActive(false);

        _collectBtn.onClick.AddListener(ClickCollect);
    }

    private void ResetData()
    {
        _currentDay = PlayerPrefs.GetInt("LoginDay", 0);
        var lastTime = PlayerPrefs.GetFloat("TimeLogin", 0);

        var currentTime = DateTime.Now.DayOfYear;

        if (currentTime > lastTime)
        {
            _isReceived = false;
            _currentDay++;
            
        }
    }

    private void ClickCollect()
    {
        if (!_isReceived)
        {
            PlayerPrefs.SetInt("LoginDay", _currentDay);
            PlayerPrefs.SetFloat("TimeLogin", DateTime.Now.DayOfYear);
            
            ToastManager.Instance.Show("Receive Successfully!");

            switch (_currentDay)
            {
                case 1:
                    BuyCoin(20);
                    break;
                case 2:
                    BuyGem(5);
                    break;
                case 3:
                    BuyCua(2);
                    break;
                case 4:
                    BuyMin(2);
                    break;
                case 5:
                    BuyCoin(50);
                    break;
                case 6:
                    BuyGem(10);
                    break;
                case 7:
                    BuyCoin(50);
                    BuyCua(2);
                    BuyMin(2);
                    break;
            }

            _isReceived = true;
        }
        else
        {
            ToastManager.Instance.Show(_currentDay != 7 ? "Received!! Comeback tomorrow" : "Received all reward");
        }

        SetActive(false);
    }

    public void SetActive(bool b)
    {
        _loginObj.SetActive(b);

        if (b)
        {
            SetImages();
            SetBtn();
        }
    }

    private void SetBtn()
    {
        _openBtn.gameObject.SetActive(_currentDay <= max_day);
    }

    private void SetImages()
    {
        for (int i = 0; i < max_day; i++)
        {
            dayImages[i].sprite = (i == _currentDay - 1) ? _chooseImg : _unChooseImg;

            _ticks[i].SetActive(i < _currentDay - 1 || ((i == _currentDay - 1) && _isReceived));
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