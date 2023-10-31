using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Script.IAP
{
    public abstract class BaseProduct : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space] [SerializeField] protected int _amount;
        [SerializeField] protected float _price;
        [SerializeField] protected string _packName;

        [SerializeField] private Text _priceText;
        [SerializeField] private Text _amountText;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClickButtonBuy);
            SetInfo();
        }

        [Button]
        private void SetInfo()
        {
            _priceText.text = $"{_price:F1} $";
            _amountText.text = $"{_amount} <sprite=0>";
        }

        protected abstract void OnClickButtonBuy();
    }
}