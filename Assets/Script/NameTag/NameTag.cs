using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NongTrai
{
    public class NameTag : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Canvas _canvas;

        private bool _overlap;
        protected bool dragging;
        protected Vector3 oldPos;
        private Vector3 _camfirstPos;
        protected Rigidbody2D rgb2D;
        protected SpriteRenderer _sprRenderer;

        [FormerlySerializedAs("Store")] [SerializeField]
        protected GameObject store;

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
        protected void Start()
        {
            SetName();
            
            _sprRenderer = store.GetComponentInChildren<SpriteRenderer>();
            float order = this.transform.position.y * (-100);
            _sprRenderer.sortingOrder = (int) order;
            _canvas.sortingOrder = (int) (order + 1);
        }

        public void SetName()
        {
            _nameText.text = PlayerPrefs.GetString("NameTag", "NameTag");
        }

        protected void OpenPopup()
        {
            NameTagManager.Instance.OpenPopup();
        }

        public NameTag(bool dragging, Vector3 oldPos, Rigidbody2D rgb2D)
        {
            this.dragging = dragging;
            this.oldPos = oldPos;
            this.rgb2D = rgb2D;
        }

    }
}