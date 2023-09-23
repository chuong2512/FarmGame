using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] int idFruit;
    [SerializeField] OldTree oldTree;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HarvestOldTree" && ManagerTool.instance.dragging == true)
        {
            oldTree.HarvestFruit(idFruit);
        }
    }
}
