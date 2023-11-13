using System;
using UnityEngine;

namespace Script.IAP
{
    public class EventProduct : BaseProduct
    {
        [SerializeField] private int amountBlockDays;
        private const string TimeEventKey = "TimeEventKey";
        private readonly DateTime _dateTime = new DateTime(2020, 11, 11, 11, 11, 11);
        private int _amountBlockMinutes;

        private void Awake()
        {
            _amountBlockMinutes = amountBlockDays * 24 * 60;
        }

        protected override void OnClickButtonBuy()
        {
            var timePass = GetTimePass();
            var timeRemain = _amountBlockMinutes - timePass;
            var time = TimeSpan.FromMinutes(timeRemain);

            int days = time.Days;
            int hours = time.Hours;
            int minutes = time.Minutes;

            if (IsAvailable(timePass))
            {
                IAPManager.OnPurchaseSuccess = () =>
                {
                    IAPShop.Instance.BuyProducts(_products);
                    ToastManager.Instance.Show("Buy Package Successfully!!");
                    PlayerPrefs.SetInt(TimeEventKey + _packName, GetTime());
                };

                IAPManager.Instance.BuyProductID(_packName);
            }
            else
            {
                ToastManager.Instance.Show(
                    $"Time remain to buy this package again: {days:D2}D:{hours:D2}H:{minutes:D2}M");
            }

            Debug.Log($"Remain: {days:D2}D:{hours:D2}H:{minutes:D2}M");
        }

        private bool IsAvailable(int timePass)
        {
            return timePass >= _amountBlockMinutes;
        }

        private int GetTime()
        {
            var timeSpan = DateTime.Now.Subtract(_dateTime);
            return timeSpan.Days * 24 * 60 + timeSpan.Hours * 60 + timeSpan.Minutes;
        }

        private int GetTimePass()
        {
            var cacheTime = PlayerPrefs.GetInt(TimeEventKey + _packName, 0);
            return GetTime() - cacheTime;
        }
    }
}