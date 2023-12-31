﻿using UnityEngine;

public class ToolFlower : MonoBehaviour
{
    private bool dragging;
    private bool unlock;
    private Vector3 oldPos;
    private Vector3 oldAmoutPos;
    private Vector3 camfirstPos;
    private GameObject obj;
    private SpriteRenderer sprRenderer;
    [SerializeField] int idFlower;
    [SerializeField] GameObject Quantity;
    // Use this for initialization
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        oldPos = transform.position;
    }
    void OnMouseDown()
    {
        if (ManagerShop.instance.inforFlowers.info[idFlower].status == 1)
        {
            unlock = true;
            MainCamera.instance.lockCam();
            oldPos = transform.position;
            oldAmoutPos = Quantity.transform.position;
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ManagerTool.instance.idFlower = idFlower;
            transform.position = new Vector3(oldPos.x, oldPos.y + 0.1f, oldPos.z);
            Quantity.transform.position = new Vector2(oldPos.x, oldPos.y + 0.7f);
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
                    ManagerTool.instance.ObjFlowerCrop(obj.transform.GetChild(1).gameObject);
                    transform.position = oldPos;
                    Quantity.transform.position = oldAmoutPos;
                    ManagerTool.instance.HideToolFlower();
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
                    ManagerTool.instance.HideToolFlower();
                    ManagerTool.instance.dragging = false;
                    ManagerTool.instance.CloseToolFlower();
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
            }
            else if (dragging == true)
            {
                if (ManagerTool.instance.checkCollider == false)
                {
                    Destroy(obj);
                    dragging = false;
                    ManagerTool.instance.ShowToolFlower();
                    ManagerTool.instance.dragging = false;
                    ManagerTool.instance.ShowToolCrop();
                }
                else if (ManagerTool.instance.checkCollider == true)
                {
                    Destroy(obj);
                    dragging = false;
                    ManagerTool.instance.ShowToolFlower();
                    ManagerTool.instance.checkCollider = false;
                    ManagerTool.instance.dragging = false;
                    ManagerTool.instance.CloseToolCrop();
                }
            }
        }
    }
}
