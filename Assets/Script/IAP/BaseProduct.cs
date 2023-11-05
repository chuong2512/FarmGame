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
        [SerializeField] protected string _packName;
        [SerializeField] protected float _price;
        [SerializeField] private int _bonus;

        [Space] [SerializeField] protected ProductInfo[] _products;

        [Space] [FoldoutGroup("Visual")] [SerializeField]
        private Button _button;

        [FoldoutGroup("Visual")] [SerializeField]
        private GameObject _bonusObj;

        [FoldoutGroup("Visual")] [SerializeField]
        private Text _bonusText;

        [FoldoutGroup("Visual")] [SerializeField]
        private Text _priceText;

        [FoldoutGroup("Visual")] [SerializeField]
        private Text _amountText;

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
            _priceText.text = $"{_price:F} $";

            _bonusObj.SetActive(_bonus > 0);
            _bonusText.text = $"{_bonus}%";
            _amountText.text = $"{_products[0].amount}";
        }

        protected virtual void OnClickButtonBuy()
        {
            IAPManager.OnPurchaseSuccess = () =>
            {
                IAPShop.Instance.BuyProducts(_products);
                ToastManager.Instance.Show("Buy Successfully!!");
            };
            IAPManager.Instance.BuyProductID(_packName);
        }
    }
}