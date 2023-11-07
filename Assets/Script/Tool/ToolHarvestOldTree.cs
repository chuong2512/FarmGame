using UnityEngine;

namespace NongTrai
{
    public class ToolHarvestOldTree : MonoBehaviour
    {
        Vector3 oldPos, oldAmoutPos;
        Vector3 camfirstPos;
        GameObject obj;
        [SerializeField] bool dragging;

        [SerializeField] SpriteRenderer spriteRenderer;

        // Use this for initialization
        void Start()
        {

        }

        void OnMouseDown()
        {
            MainCamera.instance.lockCam();
            oldPos = transform.position;
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
        }

        void OnMouseDrag()
        {
            if (dragging == false)
            {
                if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
                {
                    dragging = true;
                    transform.position = oldPos;
                    Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj = Instantiate(ManagerTool.instance.objRo, target, Quaternion.identity);
                    spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
                    ManagerTool.instance.dragging = true;
                }
            }

            if (dragging != true) return;
            {
                Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                obj.transform.position = target;
            }
        }

        void OnMouseUp()
        {
            MainCamera.instance.unLockCam();

            if (dragging == false)
            {
                transform.position = oldPos;
            }
            else if (dragging == true)
            {
                dragging = false;
                spriteRenderer.color = Color.white;
                ManagerTool.instance.dragging = false;
                Destroy(obj);
            }
        }
    }
}