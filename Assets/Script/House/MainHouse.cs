using UnityEngine;
using System.Collections;

namespace NongTrai
{
    public class MainHouse : MonoBehaviour
    {
        [SerializeField] GameObject house, shadow;
        SpriteRenderer _sprRenderer;

        Vector3 _camfirstPos;

        // Use this for initialization
        void Start()
        {
            _sprRenderer = house.GetComponent<SpriteRenderer>();
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
            if (!(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)) return;
            if (_sprRenderer.color != Color.white) _sprRenderer.color = Color.white;
        }

        void OnMouseUp()
        {
            if (!(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;
            _sprRenderer.color = Color.white;
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Sắp có!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Segera akan datang!";
            else str = "Coming soon!";
            Notification.Instance.dialogBelow(str);
        }
    }
}