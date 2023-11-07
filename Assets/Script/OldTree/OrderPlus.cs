using UnityEngine;

namespace NongTrai
{
    public class OrderPlus : MonoBehaviour
    {
        [SerializeField] OrderPro[] MyOrder;

        // Use this for initialization
        void Start()
        {
            float order = transform.position.y * (-100);
            for (int i = 0; i < MyOrder.Length; i++)
            {
                for (int k = 0; k < MyOrder[i].SprRenderer.Length; k++)
                {
                    MyOrder[i].SprRenderer[k].sortingOrder = (int) order + MyOrder[i].order;
                }
            }
        }
    }
}