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

        void Start()
        {
        }

        void OnMouseDown()
        {
            if (ManagerShop.instance.infoItemFactory.info[idItem].status == 1)
            {
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
        }

        void OnMouseDrag()
        {
            if (unlocked == true)
            {
                if (dragging == false)
                {
                    if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
                    {
                        dragging = true;
                        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        obj = Instantiate(ManagerTool.instance.objItemNhaMay, target, Quaternion.identity);
                        obj.GetComponent<SpriteRenderer>().sprite = sprRenerer.sprite;
                        sprRenerer.color = new Color(1f, 1f, 1f, 0);
                        transform.position = oldPos;
                        ManagerTool.instance.dragging = true;
                    }
                }
                else if (dragging == true)
                {
                    Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj.transform.position = target;
                    Vector2 targetCam = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - 1f,
                        Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 1f);
                    ManagerTool.instance.moveDetailItemNhaMay(targetCam);
                }
            }
        }

        void OnMouseUp()
        {
            if (unlocked == true)
            {
                unlocked = false;
                MainCamera.instance.unLockCam();

                if (dragging == false)
                {
                    transform.position = oldPos;
                    ManagerTool.instance.HideDetailItemNhaMay();
                }
                else if (dragging == true)
                {
                    dragging = false;
                    ManagerTool.instance.dragging = false;
                    sprRenerer.color = new Color(1f, 1f, 1f, 1f);
                    ManagerTool.instance.HideDetailItemNhaMay();
                    Destroy(obj);
                }
            }
        }
    }
}