using UnityEngine;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class ButtonUseGem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            ManagerMission.instance.ButtonUseGem();
        }
    }
}