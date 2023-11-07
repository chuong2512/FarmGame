using UnityEngine;

namespace NongTrai
{
    public class Cargo : MonoBehaviour
    {
        Vector3 _camfirstPos;
        [SerializeField] SpriteRenderer sprRenderer;

        void OnMouseDrag()
        {
            if (!(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)) return;
            if (sprRenderer.color != Color.white) sprRenderer.color = Color.white;
        }

        void OnMouseUp()
        {
            if (!(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            ManagerCargo.Instance.OpenLoadCargo();
        }

        void OnMouseDown()
        {
            if (Camera.main != null) _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }
    }
}