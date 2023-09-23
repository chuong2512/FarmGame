using UnityEngine;
using UnityEngine.UI;

public class ToolCrop : MonoBehaviour
{
    
    private bool dragging;
    private bool unlock;
    private SpriteRenderer sprRenderer;
    private GameObject obj;
    private Vector3 oldPos;
    private Vector3 oldAmoutPos;
    private Vector3 camfirstPos;
    [SerializeField] int idSeed;
    [SerializeField] GameObject Quantity;
    // Use this for initialization
    void Start()
    {
        sprRenderer = this.GetComponent<SpriteRenderer>();
        oldPos = transform.position;
        
    }

    void OnMouseDown()
    {
        if (ManagerShop.instance.infoSeeds.info[idSeed].status == 1)
        {
            unlock = true;
            MainCamera.instance.lockCam();
            oldPos = transform.position;
            oldAmoutPos = Quantity.transform.position;
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ManagerTool.instance.idSeeds = idSeed;
            transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
            Quantity.transform.position = new Vector2(oldPos.x, oldPos.y + 0.7f);
            if (idSeed == 0 && ManagerGuide.instance.GuideClickSeeds == 0) ManagerGuide.instance.CallArrowFieldEat();
        }
    }

    void OnMouseDrag()
    {
        if (unlock == true)
        {
            if (dragging == false)
            {
                if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
                {
                    dragging = true;
                    obj = Instantiate(gameObject, transform.position, Quaternion.identity);
                    Rigidbody2D rgb2D = obj.AddComponent<Rigidbody2D>();
                    rgb2D.bodyType = RigidbodyType2D.Kinematic;
                    ManagerTool.instance.ObjSeedsCrop(obj.transform.GetChild(1).gameObject);
                    transform.position = oldPos;
                    Quantity.transform.position = oldAmoutPos;
                    ManagerTool.instance.HideToolCrop();
                    ManagerTool.instance.dragging = true;
                }
            }
            else if (dragging == true)
            {
                if (ManagerUseGem.instance.CheckUseGem == false)
                {
                    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = oldPos.z;
                    obj.transform.position = target;
                }
                else if (ManagerUseGem.instance.CheckUseGem == true)
                {
                    Destroy(obj);
                    dragging = false;
                    ManagerTool.instance.ShowToolCrop();
                    ManagerTool.instance.dragging = false;
                    ManagerTool.instance.CloseToolCrop();
                }
            }
        }

    }

    void OnMouseUp()
    {
        if (unlock == true)
        {
            unlock = false;
            MainCamera.instance.unLockCam();
            if (dragging == false)
            {
                transform.position = oldPos;
                Quantity.transform.position = oldAmoutPos;
                if (ManagerGuide.instance.GuideClickSeeds == 0) ManagerGuide.instance.CallArowSeedsRice();
            }
            else if (dragging == true)
            {
                if (ManagerTool.instance.checkCollider == false)
                {
                    Destroy(obj);
                    dragging = false;
                    ManagerTool.instance.ShowToolCrop();
                    ManagerTool.instance.dragging = false;
                    ManagerTool.instance.ShowToolCrop();
                    if (ManagerGuide.instance.GuideClickSeeds == 0) ManagerGuide.instance.CallArowSeedsRice();
                }
                else if (ManagerTool.instance.checkCollider == true)
                {
                    Destroy(obj);
                    dragging = false;
                    ManagerTool.instance.ShowToolCrop();
                    ManagerTool.instance.checkCollider = false;
                    ManagerTool.instance.dragging = false;
                    ManagerTool.instance.CloseToolCrop();
                    if (ManagerGuide.instance.MoveCameraCageChicken == 0 && ManagerGuide.instance.GuideClickSeeds == 1) ManagerGuide.instance.CallMoveCameraCageChicken();
                }
            }
        }
    }
}

