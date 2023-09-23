using UnityEngine;

public class BareFootAnimalHome : MonoBehaviour
{

    [SerializeField] HomeAnimal homeAnimal;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BeraFoot") homeAnimal.onTriggerStay2D();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BeraFoot")   homeAnimal.onTriggerExit2D();
    }
}
