using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Script.IAP
{
    public class BaseShop : MonoBehaviour
    {
        [InlineEditor()]
        [SerializeField] private BaseProduct[] _baseProducts;

        private void OnValidate()
        {
            _baseProducts = GetComponentsInChildren<BaseProduct>();
        }

        private void Start()
        {
            
        }
    }
}