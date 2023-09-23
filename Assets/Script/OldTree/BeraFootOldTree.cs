using UnityEngine;

public class BeraFootOldTree : MonoBehaviour
{
    [SerializeField] OldTree oldTree;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BeraFoot") oldTree.onTriggerStay2D();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BeraFoot") oldTree.onTriggerExit2D();
    }
}
