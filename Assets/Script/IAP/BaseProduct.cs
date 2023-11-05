using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Script.IAP
{
    public enum IAPType
    {
        Coin,
        Gem,
        Cua,
        Min
    }

    [Serializable]
    public struct ProductInfo
    {
        public IAPType type;
        public int amount;
    }

    public abstract class BaseProduct : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space] [SerializeField] protected ProductInfo[] _products;
        [SerializeField] protected float _price;
        [SerializeField] protected string _packName;

        [SerializeField] private int _bonus;
        [SerializeField] private GameObject _bonusObj;
        [SerializeField] private Text _bonusText;

        [SerializeField] private Text _priceText;

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

            _bonusObj.SetActive(_bonus > 0);
            _bonusText.text = $"{_bonus}%";
        }

        protected virtual void OnClickButtonBuy()
        {
            IAPShop.Instance.BuyProducts(_products);
        }
    }
}