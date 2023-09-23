using System.Collections;
using UnityEngine;

public class Gift : MonoBehaviour
{
    private Vector3 camfirstPos;
    [SerializeField] OrderPro[] orderGift;
    // Use this for initialization
    void Start()
    {
        Order();
    }

    public void Order()
    {
        float order = transform.position.y * (-100);
        for (int i = 0; i < orderGift.Length; i++)
        {
            for (int k = 0; k < orderGift[i].SprRenderer.Length; k++)
            {
                orderGift[i].SprRenderer[k].sortingOrder = (int)order + orderGift[i].order;
            }
        }
    }

    void OnMouseDown()
    {
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        orderGift[0].SprRenderer[0].color = new Color(0.3f, 0.3f, 0.3f, 1f);
    }

    void OnMouseDrag()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
        {
            if (orderGift[0].SprRenderer[0].color != Color.white ) orderGift[0].SprRenderer[0].color = Color.white;
        }
    }

    void OnMouseUp()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
        {
            orderGift[0].SprRenderer[0].color = Color.white;
            MobileRewardVideoAd.instance.ButtonGift();
        }
    }
}
