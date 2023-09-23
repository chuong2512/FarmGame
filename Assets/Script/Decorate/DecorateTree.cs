using UnityEngine;
using System.Collections;

public class DecorateTree : MonoBehaviour
{
    [SerializeField] int idDecorate;
    [SerializeField] int idSerial;
    private int status;
    bool dragging;
    GameObject obj;
    [SerializeField] OrderPro[] OrderTree;
    Animator Ani;
    Vector3 firstCamPos;
    // Use this for initialization
    void Start()
    {
        Ani = this.GetComponent<Animator>();

        if (PlayerPrefs.HasKey("StatusDecorate" + idDecorate + "" + idSerial))
        {
            if (PlayerPrefs.GetInt("StatusDecorate" + idDecorate + "" + idSerial) == 1) Destroy(gameObject);
        }
        else PlayerPrefs.SetInt("StatusDecorate" + idDecorate + "" + idSerial, 0);
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
            if (status == 0)
            {
                ManagerAudio.instance.PlayAudio(Audio.Click);
                MainCamera.instance.DisableAll();
                ColorS(1f, 1f, 1f, 1f);
                ManagerTool.instance.idDecorate = idDecorate;
                ManagerTool.instance.idSerialDecorate = idSerial;
                Vector2 target = new Vector2(transform.position.x + 1f, transform.position.y + 1f);
                ManagerTool.instance.ShowToolDecorate(idDecorate, target);
            }
            else
            {
                ManagerAudio.instance.PlayAudio(Audio.Click);
                MainCamera.instance.DisableAll();
                ColorS(1f, 1f, 1f, 1f);
            }
        }
        else dragging = false;
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
                ManagerUseGem.instance.ShowDialogUseDiamond(idDecorate, StypeUseGem.DecorateTree, Purchase, gameObject);
            }
        }
    }

    public void ConditionEnough()
    {
        status = 1;
        PlayerPrefs.SetInt("StatusDecorate" + idDecorate + "" + idSerial, 1);
        Vector2 target = new Vector2(transform.position.x, transform.position.y + 0.2f);
        obj = Instantiate(ManagerTool.instance.objSaw, target, Quaternion.identity);
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
        Destroy(gameObject);
    }
}
