using UnityEngine;

public class ManagerOrder : MonoBehaviour
{
    [SerializeField] OrderPro[] sprPro;
    // Use this for initialization
    void Start()
    {
        float order = transform.position.y * (-100);
        for (int i = 0; i < sprPro.Length; i++)
        {
            for (int k = 0; k < sprPro[i].SprRenderer.Length; k++)
            {
                sprPro[i].SprRenderer[k].sortingOrder = (int)order + sprPro[i].order;
            }
        }
    }
}
