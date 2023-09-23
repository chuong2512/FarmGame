using UnityEngine;

public class ToolHarvestCrop : MonoBehaviour
{
    private Vector3 oldPos, camfirstPos;
    private bool isClick, dragging;
    private GameObject obj;

    void Start()
    {
        if (ManagerGuide.instance.GuideClickCutting == 0)
        {
            Vector3 target = new Vector3(transform.position.x - 0.7f, transform.position.y + 1f, 0);
            ManagerGuide.instance.CallArrowDown(target);
        }
    }

    void OnMouseDown()
    {
        isClick = true;
        MainCamera.instance.lockCam();
        oldPos = transform.position;
        transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ManagerGuide.instance.GuideClickCutting == 0)
        {
            ManagerGuide.instance.DoneGuide();
            ManagerGuide.instance.GuideClickCutting = 1;
        }
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

        if (dragging == true)
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            obj.transform.position = target;
        }
    }

    void OnMouseUp()
    {
        MainCamera.instance.unLockCam();
        if (ManagerGuide.instance.GuideClickCutting == 0) ManagerGuide.instance.GuideClickCutting = 1;
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

}
