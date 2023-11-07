namespace NongTrai
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ButtonSaleItemLoadCargo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            ManagerCargo.Instance.ButtonBuyItem();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }
}