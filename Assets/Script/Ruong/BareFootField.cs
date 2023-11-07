using UnityEngine;

namespace NongTrai
{
    public class BareFootField : MonoBehaviour
    {
        [SerializeField] Ruong ruong;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "BeraFoot") return;
            ruong.onTriggerStay2D();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag != "BeraFoot") return;
            ruong.onTriggerExit2D();
        }
    }
}