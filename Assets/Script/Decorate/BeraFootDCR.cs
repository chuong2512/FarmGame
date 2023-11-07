namespace NongTrai
{
    using UnityEngine;

    public class BeraFootDCR : MonoBehaviour
    {
        [SerializeField] Decorate decorate;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "BeraFoot")
            {
                decorate.Overlap();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "BeraFoot")
            {
                decorate.UnOverlap();
            }
        }
    }
}