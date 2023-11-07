using UnityEngine;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class SelectMission : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] int idOrder;
        [SerializeField] Animator Ani;
        string[] shake = new string[] {"ShakeLeft", "SkakeRight"};

        
        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            int random = Random.Range(0, shake.Length);
            Ani.SetTrigger(shake[random]);
            ManagerMission.instance.ClickOrder(idOrder);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }

        
    }
}