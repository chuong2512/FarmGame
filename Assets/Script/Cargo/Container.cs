using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Container : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] int idStype;
    [SerializeField] int idContainer;
    private IEnumerator IEScale;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IEScale != null) StopCoroutine(IEScale);
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        IEScale = ClickUp();
        StartCoroutine(IEScale);
        ManagerCargo.instance.ClickContainer(idStype, idContainer);
    }

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
        IEScale = null;
    }
}
