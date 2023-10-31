using UnityEngine;

namespace Script.IAP
{
    public class GemProduct : BaseProduct
    {
        protected override void OnClickButtonBuy()
        {
            IAPManager.OnPurchaseSuccess += BuyGem;
            IAPManager.Instance.BuyProductID(_packName);
        }

        private void BuyGem()
        {
            ManagerGem.instance.ReciveGem(_amount);
        }
    }
}