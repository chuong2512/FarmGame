using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image imageButton;
    public void OnPointerDown(PointerEventData eventData)
    {
        imageButton.color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        imageButton.color = new Color(1f, 1f, 1f, 1f);
        ManagerShop.instance.ButtonShop();
        if (ManagerGuide.instance.GuideClickShopBuyChicken == 0)
        {
            ManagerGuide.instance.GuideClickShopBuyChicken = 1;
            ManagerGuide.instance.DoneArrowShop();
        }
    }
}
