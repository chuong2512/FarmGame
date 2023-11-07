using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class ShopButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] Image imageButton;
        
        public void OnPointerUp(PointerEventData eventData)
        {
            imageButton.color = new Color(1f, 1f, 1f, 1f);
            ManagerShop.instance.ButtonShop();
            if (ManagerGuide.Instance.GuideClickShopBuyChicken == 0)
            {
                ManagerGuide.Instance.GuideClickShopBuyChicken = 1;
                ManagerGuide.Instance.DoneArrowShop();
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            imageButton.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        }

    }
}