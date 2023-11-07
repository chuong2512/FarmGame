using UnityEngine;

namespace NongTrai
{
    public class ToolHarvestCrop : MonoBehaviour
    {
        private Vector3 oldPos, camfirstPos;
        private bool isClick, dragging;
        private GameObject obj;

        void Start()
        {
            if (ManagerGuide.Instance.GuideClickCutting != 0) return;
            Vector3 target = new Vector3(transform.position.x - 0.7f, transform.position.y + 1f, 0);
            ManagerGuide.Instance.CallArrowDown(target);
        }

        
        void OnMouseDrag()
        {
            if (dragging == false)
            {
                if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
                {
                    dragging = true;
                    ManagerTool.instance.dragging = true;
                    Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj = Instantiate(ManagerTool.instance.objHarvestCrop, target, Quaternion.identity);
                    ManagerTool.instance.ScaleHarvesrCropMin();
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
            if (ManagerGuide.Instance.GuideClickCutting == 0) ManagerGuide.Instance.GuideClickCutting = 1;
            if (dragging == false)
            {
                transform.position = oldPos;
            }
            else if (dragging == true)
            {
                dragging = false;
                ManagerTool.instance.ScaleHarvesrCropMax();
                ManagerTool.instance.dragging = false;
                transform.position = oldPos;
                Destroy(obj);
                if (ManagerTool.instance.checkCollider == true)
                {
                    ManagerTool.instance.checkCollider = false;
                    ManagerTool.instance.CloseHarvesrCrop();
                }
            }
        }
        void OnMouseDown()
        {
            isClick = true;
            MainCamera.instance.lockCam();
            oldPos = transform.position;
            transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (ManagerGuide.Instance.GuideClickCutting == 0)
            {
                ManagerGuide.Instance.DoneGuide();
                ManagerGuide.Instance.GuideClickCutting = 1;
            }
        }

    }
}