using UnityEngine;

public class ToolDecorates : MonoBehaviour
{
    private bool dragging;
    private Vector3 oldPos;
    private Vector3 oldAmoutPos;
    private Vector3 camfirstPos;
    private GameObject obj;
    private SpriteRenderer sprRendererBgFoot;
    [SerializeField] int idDecorate;
    [SerializeField] GameObject amount;
    [SerializeField] GameObject bgFoot;

    // -----------------------------------------------------

    void Start()
    {
        sprRendererBgFoot = bgFoot.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        oldPos = transform.position;
        oldAmoutPos = amount.transform.position;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
        amount.transform.position = new Vector3(oldPos.x, oldPos.y + 1f, oldPos.z);
        MainCamera.instance.lockCam();
    }

    void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                ManagerTool.instance.dragging = true;
                ManagerTool.instance.idDecorate = idDecorate;
                sprRendererBgFoot.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else if (dragging == true)
        {
            if (ManagerTool.instance.checkCollider == false)
            {
                Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = oldPos.z;
                transform.position = target;
            }
            else if (ManagerTool.instance.checkCollider == true) transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void OnMouseUp()
    {
        if (dragging == false)
        {
            MainCamera.instance.unLockCam();
            transform.position = oldPos;
            amount.transform.position = oldAmoutPos;
            sprRendererBgFoot.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (dragging == true)
        {
            if (ManagerTool.instance.checkCollider == false)
            {
                dragging = false;
                MainCamera.instance.unLockCam();
                transform.position = oldPos;
                amount.transform.position = oldAmoutPos;
                sprRendererBgFoot.color = new Color(1f, 1f, 1f, 1f);
                ManagerTool.instance.dragging = false;
            }
            else if (ManagerTool.instance.checkCollider == true)
            {
                dragging = false;
                MainCamera.instance.unLockCam();
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = oldPos;
                amount.transform.position = oldAmoutPos;
                sprRendererBgFoot.color = new Color(1f, 1f, 1f, 1f);
                ManagerTool.instance.dragging = false;
                sprRendererBgFoot.color = new Color(1f, 1f, 1f, 1f);
                ManagerTool.instance.checkCollider = false;
                ManagerTool.instance.HideToolDecorate(idDecorate);
            }
        }
    }
}
