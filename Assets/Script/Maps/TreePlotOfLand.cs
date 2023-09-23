using UnityEngine;
using System.Collections;

public class TreePlotOfLand : MonoBehaviour
{
    [SerializeField] int idPOL;
    [SerializeField] int idSeri;
    [SerializeField] int idDecorate;
    private int status;
    bool dragging;
    GameObject obj;
    [SerializeField] OrderPro[] OrderTree;
    Animator Ani;
    Vector3 firstCamPos;
    // Use this for initialization
    void Start()
    {
        InitData();
        Ani = GetComponent<Animator>();
        Order();
    }

    private void Order()
    {
        float order = transform.position.y * (-100);
        for (int i = 0; i < OrderTree.Length; i++)
        {
            for (int k = 0; k < OrderTree[i].SprRenderer.Length; k++)
            {
                OrderTree[i].SprRenderer[k].sortingOrder = (int)order + OrderTree[i].order;
            }
        }
    }
    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < OrderTree.Length; i++)
        {
            for (int k = 0; k < OrderTree[i].SprRenderer.Length; k++)
            {
                OrderTree[i].SprRenderer[k].color = new Color(r, g, b, a);
            }
        }
    }
    void OnMouseDown()
    {
        firstCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ColorS(0.3f, 0.3f, 0.3f, 1f);
    }
    void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector3.Distance(firstCamPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                ColorS(1f, 1f, 1f, 1f);
            }
        }
    }
    void OnMouseUp()
    {
        if (dragging == false)
        {
            ColorS(1f, 1f, 1f, 1f);
            switch (ManagerMaps.ins.GetStatusPOL(idPOL))
            {
                case 0:
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Ô đất được mở khóa khi bạn đạt cấp độ " + (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1);
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Tanah terbuka di level " + (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1);
                    else str = "Land is unlocked when you reach the level " + (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1);
                    Notification.instance.dialogBelow(str);
                    break;
                case 1:
                    ManagerMaps.ins.RegisterExpland(idPOL);
                    break;
                case 2:
                    Vector2 target = new Vector2(transform.position.x + 1f, transform.position.y + 1f);
                    ManagerTool.instance.ShowToolPlotOfLand(idDecorate, idPOL, idSeri, target);
                    break;
            }
        }
        else dragging = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ToolDecorate" && ManagerTool.instance.dragging == true && status == 0
            && idPOL == ManagerTool.instance.idPOL
            && idSeri == ManagerTool.instance.idDecoratePOL
            && idDecorate == ManagerTool.instance.idDecorate)
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
                ManagerUseGem.instance.ShowDialogUseDiamond(idDecorate, StypeUseGem.TreePOL, Purchase, gameObject);
            }
        }
    }
    public void ConditionEnough()
    {
        status = 1;
        PlayerPrefs.SetInt("StatusTreePOL" + idPOL + "" + idSeri, status);
        Vector2 target = new Vector2(transform.position.x, transform.position.y + 0.2f);
        obj = Instantiate(ManagerTool.instance.objSaw, target, Quaternion.identity);
        ManagerMaps.ins.RegisterDestroyDecorate(idPOL);
        StartCoroutine(DestroyDecorate());
    }
    IEnumerator DestroyDecorate()
    {
        yield return new WaitForSeconds(2f);
        Destroy(obj);
        Ani.SetTrigger("isCut");
        yield return new WaitForSeconds(2f);
        if (Experience.instance.level < 7) Experience.instance.registerExpSingle(1, transform.position);
        else Experience.instance.registerExpSingle(5, transform.position);
        ManagerMaps.ins.DestroyDone(idPOL);
        Destroy(gameObject);
    }
    private void InitData()
    {
        if (PlayerPrefs.HasKey("StatusTreePOL" + idPOL + "" + idSeri) == false)
        {
            status = 0;
            PlayerPrefs.SetInt("StatusTreePOL" + idPOL + "" + idSeri, status);
        }
        else if (PlayerPrefs.HasKey("StatusTreePOL" + idPOL + "" + idSeri) == true)
        {
            status = PlayerPrefs.GetInt("StatusTreePOL" + idPOL + "" + idSeri);
            if (status == 1) Destroy(gameObject);
        }
    }
}
