using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.IAP
{
    public abstract class BaseProduct : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space] [SerializeField] protected int _amount;
        [SerializeField] protected int _price;
        [SerializeField] protected string _packName;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        protected abstract void OnClickButton();
    }
}