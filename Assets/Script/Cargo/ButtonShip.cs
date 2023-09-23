using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonShip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        ManagerCargo.instance.ButtonShip();
    }
}
