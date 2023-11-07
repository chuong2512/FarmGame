using UnityEngine;

namespace NongTrai
{
    public class ManagerOrder : MonoBehaviour
    {
        [SerializeField] private OrderPro[] sprPro;

        // Use this for initialization
        void Start()
        {
            var order = transform.position.y * (-100);

            for (int i = 0; i < sprPro.Length; i++)
            {
                foreach (var spr in sprPro[i].SprRenderer)
                {
                    spr.sortingOrder = (int) order + sprPro[i].order;
                }
            }
        }
    }
}