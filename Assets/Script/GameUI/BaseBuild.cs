using UnityEngine;
using UnityEngine.Serialization;

namespace NongTrai
{
    public abstract class BaseBuild : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprRenderer;

        private bool _overlap;
        protected bool dragging;
        private Vector3 _camfirstPos;
        protected Rigidbody2D rgb2D;
        protected SpriteRenderer _sprRenderer;

        [FormerlySerializedAs("Store")] [SerializeField]
        protected GameObject store;

        public BaseBuild(bool isRunIE, bool dragging, Rigidbody2D rgb2D)
        {
            this.dragging = dragging;
            this.rgb2D = rgb2D;
        }

        protected virtual void Start()
        {
            _sprRenderer = store.GetComponent<SpriteRenderer>();
            float order = this.transform.position.y * (-100);
            _sprRenderer.sortingOrder = (int) order;
        }

        void OnMouseDown()
        {
            _sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
            _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseDrag()
        {
            if (Camera.main != null &&
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)) return;
            if (_sprRenderer.color != Color.white) _sprRenderer.color = Color.white;
        }

        void OnMouseUp()
        {
            Debug.LogError("Choose ho cai");
            
            if (Camera.main == null ||
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;

            _sprRenderer.color = Color.white;
            OpenPopup();
        }

        public void onTriggerStay2D()
        {
            if (dragging)
            {
                _overlap = true;
                _sprRenderer.color = new Color(1f, 0f, 0f, 1f);
            }
        }

        public void onTriggerExit2D()
        {
            if (dragging == true)
            {
                _overlap = false;
                _sprRenderer.color = Color.white;
            }
        }

        protected abstract void OpenPopup();
    }
}