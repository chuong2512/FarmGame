using UnityEngine;

public class BeraFootFactory : MonoBehaviour
{
    [SerializeField] NhaMay nhaMay;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BeraFoot") nhaMay.onTriggerStay2D();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BeraFoot") nhaMay.onTriggerExit2D();
    }
}
