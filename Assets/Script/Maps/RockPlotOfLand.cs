using UnityEngine;
using System.Collections;

public class RockPlotOfLand : MonoBehaviour
{
    [SerializeField] int idPOL;
    [SerializeField] int idSeri;
    [SerializeField] int idDecorate;
    private int status;
    private bool dragging;
    private GameObject obj;
    private SpriteRenderer sprRenderer;
    private Vector3 firstCamPos;

    void Start()
    {
        sprRenderer = this.GetComponent<SpriteRenderer>();
        float order = transform.position.y * (-100);
        sprRenderer.sortingOrder = (int)order;
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
            sprRenderer.color = Color.white;
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
                    Vector2 target = new Vector2(transform.position.x - 0.7f, transform.position.y + 0.5f);
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
                ManagerUseGem.instance.ShowDialogUseDiamond(idDecorate, StypeUseGem.DecorateRockBig, Purchase, gameObject);
            }
        }
    }
    public void ConditionEnough()
    {
        status = 1;
        PlayerPrefs.SetInt("StatusRockBigPOL" + idPOL + "" + idSeri, status);
        Vector2 target = new Vector2(transform.position.x, transform.position.y + 0.3f);
        obj = Instantiate(ManagerTool.instance.objBombSmall, target, Quaternion.identity);
        ManagerMaps.ins.RegisterDestroyDecorate(idPOL);
        StartCoroutine(DestroyDecorate());
    }
    IEnumerator DestroyDecorate()
    {
        yield return new WaitForSeconds(1.6f);
        Destroy(obj);
        if (Experience.instance.level < 7) Experience.instance.registerExpSingle(1, transform.position);
        else Experience.instance.registerExpSingle(5, transform.position);
        ManagerMaps.ins.DestroyDone(idPOL);
        Destroy(gameObject);
    }
    private void InitData()
    {
        if (PlayerPrefs.HasKey("StatusRockBigPOL" + idPOL + "" + idSeri) == false)
        {
            status = 0;
            PlayerPrefs.SetInt("StatusRockBigPOL" + idPOL + "" + idSeri, status);
        }
        else if (PlayerPrefs.HasKey("StatusRockBigPOL" + idPOL + "" + idSeri) == true)
        {
            status = PlayerPrefs.GetInt("StatusRockBigPOL" + idPOL + "" + idSeri);
            if (status == 1) Destroy(gameObject);
        }
    }
}
