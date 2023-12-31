﻿using UnityEngine;
using System.Collections;

public class DecoratePond : MonoBehaviour
{
    [SerializeField] int idDecorate;
    [SerializeField] int idSerial;
    [SerializeField] int status;
    private bool dragging;
    private GameObject obj;
    private SpriteRenderer sprRenderer;
    private Vector3 firstCamPos;

    void Start()
    {
        sprRenderer = this.GetComponent<SpriteRenderer>();
        float order = transform.position.y * (-100);
        sprRenderer.sortingOrder = (int)order;

        if (PlayerPrefs.HasKey("StatusDecorate" + idDecorate + "" + idSerial))
        {
            if (PlayerPrefs.GetInt("StatusDecorate" + idDecorate + "" + idSerial) == 1) Destroy(gameObject);
        }
        else PlayerPrefs.SetInt("StatusDecorate" + idDecorate + "" + idSerial, 0);
    }

    void OnMouseDown()
    {
        firstCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
    }

    void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector3.Distance(firstCamPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                sprRenderer.color = Color.white;
            }
        }

    }

    void OnMouseUp()
    {
        if (dragging == false)
        {
            if (status == 0)
            {
                ManagerAudio.instance.PlayAudio(Audio.Click);
                MainCamera.instance.DisableAll();
                sprRenderer.color = Color.white;
                ManagerTool.instance.idDecorate = idDecorate;
                ManagerTool.instance.idSerialDecorate = idSerial;
                Vector2 target = new Vector2(transform.position.x - 0.7f, transform.position.y + 0.5f);
                ManagerTool.instance.ShowToolDecorate(idDecorate, target);
            }
            else
            {
                ManagerAudio.instance.PlayAudio(Audio.Click);
                MainCamera.instance.DisableAll();
                sprRenderer.color = Color.white;
            }
        }
        else
            dragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ToolDecorate" && ManagerTool.instance.dragging == true
            && idDecorate == ManagerTool.instance.idDecorate
            && idSerial == ManagerTool.instance.idSerialDecorate && status == 0)
        {
            if (ManagerMarket.instance.QuantityToolDecorate[idDecorate] > 0)
            {
                ManagerTool.instance.checkCollider = true;
                ManagerMarket.instance.MinusItem(5, idDecorate, 1);
                ConditionEnough();
            }
            else if (ManagerMarket.instance.QuantityToolDecorate[idDecorate] == 0)
            {
                ManagerTool.instance.checkCollider = true;
                int Purchase = ManagerData.instance.toolDecorate.Data[idDecorate].Purchare;
                ManagerUseGem.instance.ShowDialogUseDiamond(idDecorate, StypeUseGem.DecoratePond, Purchase, gameObject);
            }
        }
    }

    public void ConditionEnough()
    {
        status = 1;
        PlayerPrefs.SetInt("StatusDecorate" + idDecorate + "" + idSerial, 1);
        Vector2 target = new Vector2(transform.position.x, transform.position.y - 0.1f);
        obj = Instantiate(ManagerTool.instance.objShovel, target, Quaternion.identity);
        StartCoroutine(DestroyDecorate());
    }

    IEnumerator DestroyDecorate()
    {
        yield return new WaitForSeconds(3f);
        Destroy(obj);
        if (Experience.instance.level < 7) Experience.instance.registerExpSingle(1, transform.position);
        else Experience.instance.registerExpSingle(5, transform.position);
        Destroy(gameObject);
    }
}
