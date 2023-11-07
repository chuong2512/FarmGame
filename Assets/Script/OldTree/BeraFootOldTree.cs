using UnityEngine;

namespace NongTrai
{
    public class BeraFootOldTree : MonoBehaviour
    {
        [SerializeField] OldTree oldTree;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag != "BeraFoot") return;
            oldTree.onTriggerStay2D();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag != "BeraFoot") return;
            oldTree.onTriggerExit2D();
        }
    }
}