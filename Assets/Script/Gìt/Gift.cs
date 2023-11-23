namespace NongTrai
{
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class Gift : MonoBehaviour
    {
        private bool _overlap;
        protected bool dragging;
        private Vector3 _camfirstPos;
        protected Rigidbody2D rgb2D;
        protected SpriteRenderer _sprRenderer;

        [FormerlySerializedAs("Store")] [SerializeField]
        protected GameObject store;

        void OnMouseDown()
        {
            _sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            if (Camera.main != null) _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseDrag()
        {
            if (Camera.main != null &&
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)) return;
            if (_sprRenderer.color != Color.white) _sprRenderer.color = Color.white;
        }

        void OnMouseUp()
        {
            if (Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                _sprRenderer.color = Color.white;
                OpenPopup();
            }
        }

        private void Start()
        {
            _sprRenderer = store.GetComponentInChildren<SpriteRenderer>();
            float order = this.transform.position.y * (-100);
            _sprRenderer.sortingOrder = (int) order;
        }


        private void OpenPopup()
        {
            GiftAdsUI.Instance.OpenPopup();
        }
    }
}