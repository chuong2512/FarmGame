using UnityEngine;

public class ToolFoodAnimal : MonoBehaviour
{
    private Animator Ani;
    private Vector3 oldPos;
    private Vector3 oldAmoutPos;
    private Vector3 camfirstPos;
    private bool dragging;
    [SerializeField] int idAnimalFeed;
    [SerializeField] int idItemFactory;
    [SerializeField] GameObject Quantity;
    // Use this for initialization
    void Start()
    {
        Ani = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        MainCamera.instance.lockCam();
        oldPos = transform.position;
        oldAmoutPos = Quantity.transform.position;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ManagerMarket.instance.idAnimalFood = idAnimalFeed;
        ManagerMarket.instance.idItemFactoryAnimalUse = idItemFactory;
        transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
        Quantity.transform.position = new Vector2(oldPos.x, oldPos.y + 0.7f);
        if (idAnimalFeed == 0 && ManagerGuide.instance.GuideClickFoodChicken == 0) ManagerGuide.instance.DoneGuide();
    }

    void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                ManagerTool.instance.dragging = true;
            }
        }
        if (dragging == true)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = oldPos.z;
            transform.position = target;
        }
    }

    void OnMouseUp()
    {
        MainCamera.instance.unLockCam();
        if (dragging == false)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.position = oldPos;
            Quantity.transform.position = oldAmoutPos;
        }
        else if (dragging == true)
        {
            dragging = false;
            ManagerTool.instance.dragging = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.position = oldPos;
            Quantity.transform.position = oldAmoutPos;
        }
        if (idAnimalFeed == 0 && ManagerGuide.instance.GuideClickFoodChicken == 0) ManagerGuide.instance.CallArrowFoodsChicken();
    }
}
