using UnityEngine;

namespace NongTrai
{
    public class Order : MonoBehaviour
    {
        Vector3 camfirstPos;
        Animator Ani;

        [SerializeField] SpriteRenderer sprRenderer;

        // Use this for initialization
        void Start()
        {
            Ani = this.GetComponent<Animator>();
            float order = this.transform.position.y * (-100);
            sprRenderer.sortingOrder = (int) order;
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
                sprRenderer.color = Color.white;
            }
        }

        void OnMouseUp()
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                sprRenderer.color = Color.white;
                ManagerMission.instance.OpenOrder();
            }
        }
    }
}