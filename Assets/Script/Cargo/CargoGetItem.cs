using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CargoGetItem : MonoBehaviour
{
    [SerializeField] float timeDestroy;
    public Image ItemImage;
    public Text QuantityItemText;
    void Start()
    {
        StartCoroutine(DestroyMySeft());
    }

    IEnumerator DestroyMySeft()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
