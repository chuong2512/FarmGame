namespace NongTrai
{
    using UnityEngine;

    public class BareFootAnimalHome : MonoBehaviour
    {
        [SerializeField] HomeAnimal homeAnimal;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "BeraFoot")
            {
                homeAnimal.onTriggerExit2D();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "BeraFoot")
            {
                homeAnimal.onTriggerStay2D();
            }
        }
    }
}