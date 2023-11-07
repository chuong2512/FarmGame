using UnityEngine;
using System.Collections;

namespace NongTrai
{
    public class Market : MonoBehaviour
    {
        private bool dragging;
        private Vector3 camfirstPos;
        private IEnumerator stopDrag;

        [SerializeField] GameObject shadow;

        [SerializeField] OrderPro[] orderMarket;

        // Use this for initialization
        void Start()
        {
            OrderMarket();
        }

        private void OrderMarket()
        {
            float order = transform.position.y * (-100);
            for (int i = 0; i < orderMarket.Length; i++)
            {
                for (int k = 0; k < orderMarket[i].SprRenderer.Length; k++)
                {
                    orderMarket[i].SprRenderer[k].sortingOrder = (int) order + orderMarket[i].order;
                }
            }
        }

        private void ColorS(float r, float g, float b, float a)
        {
            for (int i = 0; i < orderMarket.Length; i++)
            {
                for (int k = 0; k < orderMarket[i].SprRenderer.Length; k++)
                {
                    orderMarket[i].SprRenderer[k].color = new Color(r, g, b, a);
                }
            }
        }

        void OnMouseDown()
        {
            ColorS(0.3f, 0.3f, 0.3f, 1f);
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseDrag()
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
            {
                if (orderMarket[0].SprRenderer[0].color != Color.white) ColorS(1f, 1f, 1f, 1f);
            }
        }

        void OnMouseUp()
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                ManagerAudio.Instance.PlayAudio(Audio.ClickOpen);
                ColorS(1f, 1f, 1f, 1f);
                ManagerMarket.instance.OpenMarket();
            }
        }
    }
}