using System;
using UnityEngine;

namespace Script.IAP
{
    public class EventProduct : BaseProduct
    {
        [SerializeField] private int amountBlockDays;
        private const string TIME_EVENT_KEY = "TimeEventKey";
        private readonly DateTime _dateTime = new DateTime(2020, 11, 11, 11, 11, 11);
        private int _amountBlockMinutes;

        private void Awake()
        {
            _amountBlockMinutes = amountBlockDays * 24 * 60;
        }

        protected override void OnClickButtonBuy()
        {
            var timePass = GetTimePass();
            if (IsAvailable(timePass))
            {
                //todo on purchase success 
                BuyCoin();
                return;
            }
            ShowTimeRemain(_amountBlockMinutes - timePass);
        }

        private bool IsAvailable(int timePass)
        {
            return timePass >= _amountBlockMinutes;
        }
        
        private int GetTime()
        {
            var timeSpan =  DateTime.Now.Subtract(_dateTime);
            return timeSpan.Days * 24 * 60 + timeSpan.Hours * 60 + timeSpan.Minutes;
        }

        private int GetTimePass()
        {
            var cacheTime = PlayerPrefs.GetInt(TIME_EVENT_KEY, 0);
            return GetTime() - cacheTime;
        }
        
        private void BuyCoin()
        {
            //todo buy coin
            Debug.Log("buy");
            PlayerPrefs.SetInt(TIME_EVENT_KEY, GetTime());
        }

        private void ShowTimeRemain(int timeRemain)
        {
            int days = timeRemain / (24 * 60);
            int hours = (timeRemain % (24 * 60)) / 60;
            int minutes = timeRemain % 60;
            Debug.Log($"Remain: {days:D2}D:{hours:D2}H:{minutes:D2}M");
        }
    }
}