using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NongTrai
{
    public class ComingSoonHouse : MonoBehaviour
    {
        private bool isRunIE;
        private bool _overlap;
        private bool dragging;
        private Vector3 oldPos;
        private Vector3 _camfirstPos;
        private Animator _ani;
        private Rigidbody2D rgb2D;
        private SpriteRenderer _sprRenderer;
        private SpriteRenderer _sprShadow;

        [FormerlySerializedAs("Store")] [SerializeField]
        GameObject store;

        [FormerlySerializedAs("Shadow")] [SerializeField]
        GameObject shadow;

        private static readonly int IsClick = Animator.StringToHash("isClick");

        public ComingSoonHouse(bool isRunIE, bool dragging, Vector3 oldPos, Rigidbody2D rgb2D)
        {
            this.isRunIE = isRunIE;
            this.dragging = dragging;
            this.oldPos = oldPos;
            this.rgb2D = rgb2D;
        }

        // Use this for initialization
        void Start()
        {
            _ani = GetComponent<Animator>();
            _sprRenderer = store.GetComponent<SpriteRenderer>();
            _sprShadow = shadow.GetComponent<SpriteRenderer>();
            float order = this.transform.position.y * (-100);
            _sprRenderer.sortingOrder = (int) order;
            _sprShadow.sortingOrder = (int) order - 1;
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
            if (Camera.main == null ||
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;

            _ani.SetTrigger(IsClick);
            _sprRenderer.color = Color.white;
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Sắp có!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Segera akan datang!";
            else str = "Coming soon!";
            Notification.Instance.dialogBelow(str);
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
    }
}