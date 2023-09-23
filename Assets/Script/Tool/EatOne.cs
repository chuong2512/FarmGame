using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EatOne : MonoBehaviour
{
    [SerializeField] float timeDestroy;
    public Text QuantityItemText;
    public Image ItemImage;
    [SerializeField] Canvas canvas;
    void Start()
    {
        int order = (int)(transform.position.y * (-100));
        canvas.sortingOrder = order + 1;
        StartCoroutine(DestroyMySeft());
    }

    IEnumerator DestroyMySeft()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
