using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace NongTrai
{
    public class IPointerUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private bool _dragging;
        private Vector3 _camFirstPos;

        [FormerlySerializedAs("Event")] [SerializeField]
        UnityEvent @event;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Camera.main != null) _camFirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragging != false) return;
            if (Camera.main == null ||
                !(Vector3.Distance(_camFirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >=
                  0.2f)) return;
            _dragging = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            switch (_dragging)
            {
                case false:
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    @event.Invoke();
                    break;
                case true:
                    _dragging = false;
                    break;
            }
        }
    }
}