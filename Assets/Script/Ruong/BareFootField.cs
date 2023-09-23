using UnityEngine;

public class BareFootField : MonoBehaviour
{
    [SerializeField] Ruong ruong;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BeraFoot" ) ruong.onTriggerStay2D();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BeraFoot" ) ruong.onTriggerExit2D();
    }
}
