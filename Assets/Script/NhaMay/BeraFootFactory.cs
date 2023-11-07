using UnityEngine;

namespace NongTrai
{
    public class BeraFootFactory : MonoBehaviour
    {
        [SerializeField] NhaMay nhaMay;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != "BeraFoot") return;
            nhaMay.onTriggerStay2D();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag != "BeraFoot") return;
            nhaMay.onTriggerExit2D();
        }
    }
}