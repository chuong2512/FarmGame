using UnityEngine;

public class ToolHarvesrBread : MonoBehaviour
{
    Animator Ani;
    Vector3 oldPos, oldAmoutPos;
    Vector3 camfirstPos;
    [SerializeField] int idToodHarvestBread;
    bool dragging;
    SpriteRenderer sprRenderer;
    GameObject obj;
    // Use this for initialization
    void Awake()
    {
        sprRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Ani = this.GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        MainCamera.instance.lockCam();
        oldPos = transform.position;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
        ManagerTool.instance.idToodHarvestBread = idToodHarvestBread;
    }

    void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                transform.position = oldPos;
                ManagerTool.instance.dragging = true;
                sprRenderer.color = new Color(1f, 1f, 1f, 0f);
                Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                obj = Instantiate(ManagerTool.instance.objHarvesrBread, target, Quaternion.identity);
                obj.GetComponent<SpriteRenderer>().sprite = sprRenderer.sprite;
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

        if (dragging == false)
        {
            transform.position = oldPos;
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        else
            if (dragging == true)
        {
            dragging = false;
            ManagerTool.instance.dragging = false;
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            Destroy(obj);
        }
    }
}
