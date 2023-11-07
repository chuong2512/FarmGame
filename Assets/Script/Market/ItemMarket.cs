using UnityEngine;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class ItemMarket : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public int idStype;
        public int idItem;

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            ManagerMarket.instance.ButtonChooseItemSale(idStype, idItem);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        
    }
}