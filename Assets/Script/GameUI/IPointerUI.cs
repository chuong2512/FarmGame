using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IPointerUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool dragging;
    private Vector3 camFirstPos;
    [SerializeField] UnityEvent Event;
    public void OnPointerDown(PointerEventData eventData)
    {
        camFirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (dragging == false)
        {
            if (Vector3.Distance(camFirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
            {
                dragging = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragging == false)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Event.Invoke();
        }
        else if (dragging == true) dragging = false;
    }
}
