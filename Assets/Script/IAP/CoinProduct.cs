using UnityEngine;

namespace Script.IAP
{
    public class CoinProduct : BaseProduct
    {
        protected override void OnClickButton()
        {
            IAPManager.OnPurchaseSuccess += BuyCoin;
            IAPManager.Instance.BuyProductID(_packName);
        }

        private void BuyCoin()
        {
            ManagerCoin.instance.reciveGold(_amount);
        }
    }
}