using UnityEngine;
namespace NongTrai
{
public class EmptyCar : MonoBehaviour
{  
    [SerializeField] GameObject body;
    [SerializeField] OrderPro[] oderCar;
    // Use this for initialization
    void Start()
    {
        OrderCar();
    }

    private void OrderCar()
    {
        float order = transform.position.y * (-100);
        for (int i = 0; i < oderCar.Length; i++)
        {
            for (int k = 0; k < oderCar[i].SprRenderer.Length; k++)
            {
                oderCar[i].SprRenderer[k].sortingOrder = (int)order + oderCar[i].order;
            }
        }
    }

    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < oderCar.Length; i++)
        {
            for (int k = 0; k < oderCar[i].SprRenderer.Length; k++)
            {
                oderCar[i].SprRenderer[k].color = new Color(r, g, b, a);
            }
        }
    }

    private void OnMouseDown()
    {
        ColorS(0.3f, 0.3f, 0.3f, 1f);
    }

    private void OnMouseUp()
    {
        ColorS(1f, 1f, 1f, 1f);
    }
}
}
