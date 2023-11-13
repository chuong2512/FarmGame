using System;
using NongTrai;
using Script.IAP;
using Sirenix.OdinInspector;
using UnityEngine;

public class IAPShop : Singleton<IAPShop>
{
    [SerializeField] private GameObject _blur;

    [InlineEditor()] [SerializeField] private BaseShop _coinShop, _gemShop;

    private void Start()
    {
        Close();
    }

    public void OpenGemShop()
    {
        _blur.gameObject.SetActive(true);
        _gemShop.gameObject.SetActive(true);
        _coinShop.gameObject.SetActive(false);
    }

    public void OpenCoinShop()
    {
        _blur.gameObject.SetActive(true);
        _coinShop.gameObject.SetActive(true);
        _gemShop.gameObject.SetActive(false);
    }

    public void Close()
    {
        _blur.gameObject.SetActive(false);
        _gemShop?.gameObject.SetActive(false);
        _coinShop.gameObject.SetActive(false);
    }

    public void BuyProducts(ProductInfo[] productInfos)
    {
        for (int i = 0; i < productInfos.Length; i++)
        {
            BuyProduct(productInfos[i]);
        }
    }

    public void BuyProduct(ProductInfo productInfo)
    {
        switch (productInfo.type)
        {
            case IAPType.Coin:
                BuyCoin(productInfo.amount);
                break;
            case IAPType.Cua:
                BuyCua(productInfo.amount);
                break;
            case IAPType.Gem:
                BuyGem(productInfo.amount);
                break;
            case IAPType.Min:
                BuyMin(productInfo.amount);
                break;
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