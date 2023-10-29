using UnityEngine;

namespace Script.IAP
{
    public class GemProduct : BaseProduct
    {
        protected override void OnClickButton()
        {
            IAPManager.OnPurchaseSuccess += BuyGem;
            IAPManager.Instance.BuyProductID(_packName);
        }

        private void BuyGem()
        {
            
        }
    }
}