using UnityEngine;

namespace Script.GameUI
{
    public abstract class BaseBuild : MonoBehaviour
    {
        Vector3 camfirstPos;
        Animator Ani;
        protected float order;

        [SerializeField] SpriteRenderer sprRenderer;

        // Use this for initialization
        protected virtual void Start()
        {
            Ani = this.GetComponent<Animator>();
            order = this.transform.position.y * (-100);
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
                OpenPopup();
            }
        }

        protected abstract void OpenPopup();
    }
}