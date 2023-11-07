using UnityEngine;

namespace NongTrai
{
    public class ItemNhaMay : MonoBehaviour
    {
        [SerializeField] int idNhaMay, idItem;
        private SpriteRenderer sprRenerer;
        Vector3 oldPos, camfirstPos;
        bool dragging, unlocked;

        GameObject obj;

        // Use this for initialization
        void Awake()
        {
            sprRenerer = this.GetComponent<SpriteRenderer>();
        }
        void OnMouseDown()
        {
            if (ManagerShop.instance.infoItemFactory.info[idItem].status != 1) return;
            unlocked = true;
            MainCamera.instance.lockCam();
            oldPos = transform.position;
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ManagerTool.instance.idItem = idItem;
            ManagerTool.instance.idTypeFactory = idNhaMay;
            transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f);
            ManagerTool.instance.ShowDetailItemNhaMay(idItem);
            Vector3 target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - 1.4f,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            ManagerTool.instance.moveDetailItemNhaMay(target);
        }
        
        void OnMouseUp()
        {
            if (unlocked == true)
            {
                unlocked = false;
                MainCamera.instance.unLockCam();

                switch (dragging)
                {
                    case false:
                        transform.position = oldPos;
                        ManagerTool.instance.HideDetailItemNhaMay();
                        break;
                    case true:
                        dragging = false;
                        ManagerTool.instance.dragging = false;
                        sprRenerer.color = new Color(1f, 1f, 1f, 1f);
                        ManagerTool.instance.HideDetailItemNhaMay();
                        Destroy(obj);
                        break;
                }
            }
        }

        void OnMouseDrag()
        {
            if (unlocked != true) return;
            switch (dragging)
            {
                case false when !(Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >
                                  0.1f):
                    return;
                case false:
                {
                    dragging = true;
                    Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj = Instantiate(ManagerTool.instance.objItemNhaMay, target, Quaternion.identity);
                    obj.GetComponent<SpriteRenderer>().sprite = sprRenerer.sprite;
                    sprRenerer.color = new Color(1f, 1f, 1f, 0);
                    transform.position = oldPos;
                    ManagerTool.instance.dragging = true;
                    break;
                }
                case true:
                {
                    Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj.transform.position = target;
                    Vector2 targetCam = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - 1f,
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 1f);
                    ManagerTool.instance.moveDetailItemNhaMay(targetCam);
                    break;
                }
            }
        }

        
    }
}