using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class Container : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] int idStype;
        [SerializeField] int idContainer;
        
        private IEnumerator _ieScale;

        private IEnumerator ClickUp()
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            yield return new WaitForSeconds(0.1f);
            transform.localScale = new Vector3(1f, 1f, 1f);
            _ieScale = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_ieScale != null) StopCoroutine(_ieScale);
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            _ieScale = ClickUp();
            StartCoroutine(_ieScale);
            ManagerCargo.Instance.ClickContainer(idStype, idContainer);
        }
    }
}