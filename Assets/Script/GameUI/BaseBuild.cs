using UnityEngine;

namespace NongTrai
{
    public abstract class BaseBuild : MonoBehaviour
    {
        Vector3 _camfirstPos;
        Animator _ani;
        protected float Order;

        [SerializeField] SpriteRenderer sprRenderer;

        // Use this for initialization
        protected virtual void Start()
        {
            _ani = this.GetComponent<Animator>();
            Order = this.transform.position.y * (-100);
            sprRenderer.sortingOrder = (int) Order;
        }

        void OnMouseDown()
        {
            sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            if (Camera.main != null) _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseDrag()
        {
            if (Camera.main == null ||
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)) return;
            sprRenderer.color = Color.white;
        }

        void OnMouseUp()
        {
            if (Camera.main == null ||
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;
            sprRenderer.color = Color.white;
            OpenPopup();
        }

        protected abstract void OpenPopup();
    }
}