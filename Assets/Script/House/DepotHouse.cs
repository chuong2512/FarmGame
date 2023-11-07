using UnityEngine;
using System.Collections;

namespace NongTrai
{
    public class DepotHouse : MonoBehaviour
    {
        private bool isRunIE;
        private bool overlap;
        private bool dragging;

        private Vector3 oldPos;
        private Vector3 camfirstPos;
        private Animator Ani;
        private Rigidbody2D rgb2D;
        private SpriteRenderer sprRenderer;
        private SpriteRenderer sprShadow;
        [SerializeField] GameObject Store;

        [SerializeField] GameObject Shadow;

        // Use this for initialization
        void Start()
        {
            Ani = GetComponent<Animator>();
            sprRenderer = Store.GetComponent<SpriteRenderer>();
            sprShadow = Shadow.GetComponent<SpriteRenderer>();
            float order = this.transform.position.y * (-100);
            sprRenderer.sortingOrder = (int) order;
            sprShadow.sortingOrder = (int) order - 1;
        }

        void OnMouseDown()
        {
            sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseDrag()
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
            {
                if (sprRenderer.color != Color.white) sprRenderer.color = Color.white;
            }
        }

        void OnMouseUp()
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                Ani.SetTrigger("isClick");
                sprRenderer.color = Color.white;
                ManagerDepot.instance.OpenDepot();
            }
        }

        public void onTriggerStay2D()
        {
            if (dragging == true)
            {
                overlap = true;
                sprRenderer.color = new Color(1f, 0f, 0f, 1f);
            }
        }

        public void onTriggerExit2D()
        {
            if (dragging == true)
            {
                overlap = false;
                sprRenderer.color = Color.white;
            }
        }
    }
}