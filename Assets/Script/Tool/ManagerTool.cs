using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NongTrai;
namespace NongTrai
{
public class ManagerTool : MonoBehaviour
{
    public static ManagerTool instance;

    private Clock clock;
    private Lighting lighting;
    private DetailItemNhaMay detailItemNhaMay;
    private int idTree, idNumberTree;
    private IEnumerator runTimeLive;
    private Vector3 defaultScale;
    private bool isRunIE;
    private bool showClockCrop;
    private bool showClockBreads;
    private bool showClockOldTree;
    private GameObject objTree;
    private Text txtQuantitySeeds;
    private Text txtQuantityFlower;
    public ShowClock showClock;

    [Header("ObjectRecive")]
    [SerializeField] GameObject ItemSingle;
    [SerializeField] GameObject ExperienceCoin;

    [Header("Recive Tool")]
    [SerializeField] GameObject BorderField;

    [Header("Create Method")]
    [SerializeField] GameObject objEat;

    [Header("ObjectCreate")]
    public GameObject objItemNhaMay;
    public GameObject objDetailItemNhaMay;
    public GameObject objSaw;
    public GameObject objBombSmall;
    public GameObject objBombBig;
    public GameObject objShovel;

    public GameObject objHarvestCrop;
    public GameObject objHarvesrBread;
    public GameObject objDecorateTree;
    public GameObject objRo;
    public GameObject objXeng;

    [Header("Tool Alone")]
    [SerializeField] GameObject toolHarvestCrops;
    [SerializeField] GameObject toolCrop;
    [SerializeField] GameObject toolOldTree;
    [SerializeField] GameObject TimeMove;
    [SerializeField] GameObject toolFlower;

    [Header("Clock")]
    [SerializeField] GameObject Clock;
    [SerializeField] GameObject objClock;
    [SerializeField] GameObject objLighting;

    [Header("Tool Array")]
    [SerializeField] GameObject[] toolCage;
    [SerializeField] GameObject[] toolFactory;
    [SerializeField] GameObject[] toolDecorate;

    [HideInInspector] public bool dragging;
    [HideInInspector] public bool checkCollider;

    [HideInInspector] public int ClickUseGemBuySeed;
    [HideInInspector] public int ClickUseGemBuyFlower;

    [HideInInspector] public int idRuong;
    [HideInInspector] public int idRuongHoa;
    [HideInInspector] public int idFactory;
    [HideInInspector] public int idTypeFactory;
    [HideInInspector] public int idItem;
    [HideInInspector] public int idSeeds;
    [HideInInspector] public int idFlower;
    [HideInInspector] public int idToodHarvestBread;
    [HideInInspector] public int idOldTree;
    [HideInInspector] public int idPOL;
    [HideInInspector] public int idDecoratePOL;

    [HideInInspector] public int idDecorate;
    [HideInInspector] public int idToolDecorate;
    [HideInInspector] public int idSerialDecorate;

    [HideInInspector] public GameObject objTimeFactory;

    void Awake()
    {
        instance = this;
        showClock.CheckStype = new bool[5];
    }

    void Start()
    {
        clock = objClock.GetComponent<Clock>();
        lighting = objLighting.GetComponent<Lighting>();
        detailItemNhaMay = objDetailItemNhaMay.GetComponent<DetailItemNhaMay>();
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            lighting.TitleText.text = "Hoàn Thành";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            lighting.TitleText.text = "Selesaikan";
        }
        else lighting.TitleText.text = "Finish Now";
    }

    public void RegisterEatOne(int quantity, Sprite spr, Vector3 target)
    {
        GameObject obj = Instantiate(objEat, target, Quaternion.identity);
        EatOne eo = obj.GetComponent<EatOne>();
        eo.ItemImage.sprite = spr;
        eo.QuantityItemText.text = "-" + quantity;
    }

    public void RegisterItemSingle(int quantity, Sprite spr, Vector3 target)
    {
        GameObject objExp = Instantiate(ItemSingle, target, Quaternion.identity);
        ItemFly itemfly = objExp.GetComponent<ItemFly>();
        itemfly.numberItem = quantity;
        itemfly.ItemImage.sprite = spr;
    }

    public void RegisterExperienceCoin(int valuaCoin, int valueExp, Vector3 target)
    {
        GameObject objExpCoin = Instantiate(ExperienceCoin, target, Quaternion.identity);
        ExperienceCoin experienceCoin = objExpCoin.GetComponent<ExperienceCoin>();
        experienceCoin.numberCoin = valuaCoin;
        experienceCoin.numberExp = valueExp;
    }

    public void RegisterTimeMove(Vector3 target)
    {
        TimeMove.transform.position = target;
        TimeMove.SetActive(true);
    }

    public void CloseTimeMove()
    {
        TimeMove.SetActive(false);
    }

    public void ShowToolDecorate(int idTool, Vector2 target)
    {
        HideTool();
        toolDecorate[idTool].transform.position = target;
        toolDecorate[idTool].SetActive(true);
    }

    public void HideToolDecorate(int idTool)
    {
        toolDecorate[idTool].SetActive(false);
    }
    public void ShowToolPlotOfLand(int Tool, int POL, int Seri, Vector2 target)
    {
        HideTool();
        idPOL = POL;
        idDecoratePOL = Seri;
        toolDecorate[Tool].transform.position = target;
        toolDecorate[Tool].SetActive(true);
    }
    public void ShowedItemFactory(GameObject obj)
    {
        objTimeFactory = obj;
    }

    public void OpenToolCrops(Vector3 destination)
    {
        Vector3 target = new Vector3(destination.x - 1f, destination.y + 1.5f, destination.z);
        if (toolCrop.activeSelf == false)
        {
            HideTool();
            BorderField.transform.position = destination;
            toolCrop.transform.position = target;
            BorderField.SetActive(true);
            toolCrop.SetActive(true);
        }
        else if (toolCrop.activeSelf == true)
        {
            if (toolCrop.transform.position != target)
            {
                BorderField.transform.position = destination;
                toolCrop.transform.position = target;
            }
        }
    }

    public void OpenToolFlowers(Vector3 destination)
    {
        Vector3 target = new Vector3(destination.x - 1f, destination.y + 1.5f, destination.z);
        if (toolCrop.activeSelf == false)
        {
            HideTool();
            BorderField.transform.position = destination;
            toolFlower.transform.position = target;
            BorderField.SetActive(true);
            toolFlower.SetActive(true);
        }
        else if (toolCrop.activeSelf == true)
        {
            if (toolCrop.transform.position != target)
            {
                BorderField.transform.position = destination;
                toolFlower.transform.position = target;
            }
        }
    }

    public void ObjSeedsCrop(GameObject objSeedsUsing)
    {
        txtQuantitySeeds = objSeedsUsing.GetComponent<Text>();
    }

    public void ObjFlowerCrop(GameObject objFlowersUsing)
    {
        txtQuantityFlower = objFlowersUsing.GetComponent<Text>();
    }

    public void UpdateQuantitySeeds()
    {
        txtQuantitySeeds.text = "" + ManagerMarket.instance.QuantityItemSeeds[idSeeds];
    }

    public void UpdateQuantityFlower()
    {
        txtQuantityFlower.text = "" + ManagerMarket.instance.QuantityItemFlower[idFlower];
    }

    public void CloseBorderField()
    {
        if (BorderField.activeSelf == true) BorderField.SetActive(false);
    }
    public void HideToolCrop()
    {
        defaultScale = toolCrop.transform.localScale;
        toolCrop.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
    public void ShowToolCrop()
    {
        toolCrop.transform.localScale = defaultScale;
    }
    public void HideToolFlower()
    {
        defaultScale = toolFlower.transform.localScale;
        toolFlower.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
    public void ShowToolFlower()
    {
        toolFlower.transform.localScale = defaultScale;
    }
    public void ScaleCropMin()
    {
        defaultScale = toolCrop.transform.localScale;
        toolCrop.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void CloseToolCrop()
    {
        toolCrop.SetActive(false);
    }

    public void CloseToolFlower()
    {
        toolFlower.SetActive(false);
    }

    public void showToolHarvestCrops(Vector3 destination)
    {
        if (toolHarvestCrops.activeSelf == false)
        {
            HideTool();
            toolHarvestCrops.transform.position = destination;
            toolHarvestCrops.SetActive(true);
        }
        else if (toolHarvestCrops.activeSelf == true)
        {
            if (toolHarvestCrops.transform.position != destination)
            {
                toolHarvestCrops.transform.position = destination;
            }
        }
    }

    public void ScaleHarvesrCropMin()
    {
        defaultScale = toolHarvestCrops.transform.localScale;
        toolHarvestCrops.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void ScaleHarvesrCropMax()
    {
        toolHarvestCrops.transform.localScale = defaultScale;
    }

    public void CloseHarvesrCrop()
    {
        toolHarvestCrops.SetActive(false);
    }

    public void ShowToolOldTree(Vector3 destination)
    {
        if (toolOldTree.activeSelf == false)
        {
            HideTool();
            Vector2 target = destination;
            toolOldTree.transform.position = target;
            toolOldTree.SetActive(true);
        }
        else if (toolOldTree.activeSelf == true)
        {
            Vector2 target = destination;
            if (toolOldTree.transform.position != destination)
                toolOldTree.transform.position = target;
        }
    }

    public void showToolCage(int id, Vector3 destination)
    {
        if (toolCage[id].activeSelf == false)
        {
            Vector3 target = new Vector3(destination.x - 1.5f, destination.y + 0.8f, 0);
            toolCage[id].transform.position = target;
            toolCage[id].SetActive(true);
        }
        else if (toolCage[id].activeSelf == true)
        {
            Vector3 target = new Vector3(destination.x - 1.5f, destination.y + 0.8f, 0);
            if (toolCage[id].transform.position != target)
            {
                toolCage[id].transform.position = target;
            }
        }
    }

    public void ShowClockBread(int status, int timeLive, int totalTime)
    {
        if (status == 0)
        {
            clock.fillAmount.fillAmount = 0;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                clock.timeLive.text = "Đói";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                clock.timeLive.text = "Lapar";
            else clock.timeLive.text = "Hungry";
            if (objLighting.activeSelf == true) objLighting.SetActive(false);
        }
        else if (status == 1)
        {
            clock.fillAmount.fillAmount = (float)(totalTime - timeLive) / totalTime;
            if (objLighting.activeSelf == false) objLighting.SetActive(true);
            clock.timeLive.text = ManagerGame.Instance.TimeText(timeLive);
            lighting.quantityGem = ManagerGame.Instance.CalcalutorGem(timeLive);
            lighting.GemText.text = "" + lighting.quantityGem;
        }
        else if (status == 2)
        {
            clock.fillAmount.fillAmount = 1;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                clock.timeLive.text = "Thu hoạch";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                clock.timeLive.text = "Panen";
            else clock.timeLive.text = "Harvest";
            if (objLighting.activeSelf == true) objLighting.SetActive(false);
        }
    }

    public void ShowClockCrop(int timeLive, int totalTime)
    {
        clock.fillAmount.fillAmount = (float)(totalTime - timeLive) / totalTime;
        if (timeLive > 0)
        {
            if (objLighting.activeSelf == false) objLighting.SetActive(true);
            clock.timeLive.text = ManagerGame.Instance.TimeText(timeLive);
            lighting.quantityGem = ManagerGame.Instance.CalcalutorGem(timeLive);
            lighting.GemText.text = "" + lighting.quantityGem;
        }
        else if (timeLive <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                clock.timeLive.text = "Thu hoạch";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                clock.timeLive.text = "Panen";
            else clock.timeLive.text = "Harvest";
            if (objLighting.activeSelf == true) objLighting.SetActive(false);
        }
    }
    public void ShowClockCropFlower(int timeLive, int totalTime)
    {
        clock.fillAmount.fillAmount = (float)(totalTime - timeLive) / totalTime;
        if (timeLive > 0)
        {
            if (objLighting.activeSelf == false) objLighting.SetActive(true);
            clock.timeLive.text = ManagerGame.Instance.TimeText(timeLive);
            lighting.quantityGem = ManagerGame.Instance.CalcalutorGem(timeLive);
            lighting.GemText.text = "" + lighting.quantityGem;
        }
        else if (timeLive <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                clock.timeLive.text = "Thu hoạch";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                clock.timeLive.text = "Panen";
            else clock.timeLive.text = "Harvest";
            if (objLighting.activeSelf == true) objLighting.SetActive(false);
        }
    }
    public void ShowClockOldTree(int timeLive, int totalTime)
    {
        clock.fillAmount.fillAmount = (float)(totalTime - timeLive) / totalTime;
        if (timeLive > 0)
        {
            clock.timeLive.text = ManagerGame.Instance.TimeText(timeLive);
            if (objLighting.activeSelf == false) objLighting.SetActive(true);
            lighting.quantityGem = ManagerGame.Instance.CalcalutorGem(timeLive);
            lighting.GemText.text = "" + lighting.quantityGem;
        }
        else if (timeLive <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                clock.timeLive.text = "Thu hoạch";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                clock.timeLive.text = "Panen";
            else clock.timeLive.text = "Harvest";
            if (objLighting.activeSelf == true) objLighting.SetActive(false);
        }
    }

    public void ShowToolFactory(int id, Vector3 target)
    {
        if (toolFactory[id].activeSelf == false)
        {
            HideTool();
            Vector3 targetX = new Vector3(target.x - 2f, target.y + 1f, 0);
            toolFactory[id].transform.position = targetX;
            toolFactory[id].SetActive(true);
        }
        else if (toolFactory[id].activeSelf == true)
        {
            Vector3 targetX = new Vector3(target.x - 1f, target.y + 1.3f, 0);
            if (toolFactory[id].transform.position != targetX) toolFactory[id].transform.position = targetX;
        }
    }

    public void ShowDetailItemNhaMay(int id)
    {
        detailItemNhaMay.quantityItem.text = ManagerMarket.instance.QuantityItemFactory[id].ToString();
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            detailItemNhaMay.nameItem.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].name;
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
            detailItemNhaMay.nameItem.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].nameINS;
        else detailItemNhaMay.nameItem.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].engName;

        for (int i = 0; i < ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial.Length; i++)
        {
            int idStype = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].stypeIDYC;
            int idYc = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].IdYc;
            detailItemNhaMay.iconItem[i].sprite = ManagerMarket.instance.IconItem(idStype, idYc);
            detailItemNhaMay.detail[i].text = ManagerMarket.instance.AmountItem(idStype, idYc) + "/" + ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].Amount;
            detailItemNhaMay.iconItem[i].gameObject.SetActive(true);
        }
        int time = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].time;
        detailItemNhaMay.time.text = ManagerGame.Instance.TimeText(time);
        objDetailItemNhaMay.SetActive(true);
    }

    public void moveDetailItemNhaMay(Vector3 target)
    {
        objDetailItemNhaMay.transform.position = target;
    }

    public void HideDetailItemNhaMay()
    {
        for (int i = 0; i < detailItemNhaMay.iconItem.Length; i++)
        {
            detailItemNhaMay.iconItem[i].gameObject.SetActive(false);
        }
        objDetailItemNhaMay.SetActive(false);
    }

    public void HideClock()
    {
        if (showClock.CheckShow == true)
        {
            showClock.CheckShow = false;
            showClock.CheckStype[showClock.Stype] = false;
            Clock.SetActive(false);
        }
    }

    public void HideTool()
    {
        HideClock();
        if (toolHarvestCrops.activeSelf == true) toolHarvestCrops.SetActive(false);
        if (toolCrop.activeSelf == true) toolCrop.SetActive(false);
        if (toolFlower.activeSelf == true) toolFlower.SetActive(false);
        if (BorderField.activeSelf == true) BorderField.SetActive(false);
        if (toolOldTree.activeSelf == true) toolOldTree.SetActive(false);
        if (objTimeFactory != null) { objTimeFactory.SetActive(false); objTimeFactory = null; }
        for (int i = 0; i < toolFactory.Length; i++) { if (toolFactory[i].activeSelf == true) toolFactory[i].SetActive(false); }
        for (int i = 0; i < toolDecorate.Length; i++) { if (toolDecorate[i].activeSelf == true) toolDecorate[i].SetActive(false); }
        for (int i = 0; i < toolCage.Length; i++) { if (toolCage[i].activeSelf == true) toolCage[i].SetActive(false); }
    }

    public void ShowClockBuilding(int timeLive, int totalTime)
    {
        if (timeLive > 0)
        {
            clock.fillAmount.fillAmount = (float)(totalTime - timeLive) / totalTime;
            clock.timeLive.text = ManagerGame.Instance.TimeText(timeLive);
            lighting.quantityGem = ManagerGame.Instance.CalcalutorGem(timeLive);
            lighting.GemText.text = "" + lighting.quantityGem;
        }
        else if (timeLive <= 0)
        {
            clock.fillAmount.fillAmount = (float)(totalTime - timeLive) / totalTime;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                clock.timeLive.text = "Hoàn Thiện";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                clock.timeLive.text = "Selesaikan";
            else clock.timeLive.text = "Finish";
            if (objLighting.activeSelf == true) objLighting.SetActive(false);
        }
    }

    public void RegisterShowClock(int stype, int product, int idproduct, int idshow, string name, Vector3 target, GameObject obj)
    {
        if (showClock.CheckShow == false)
        {
            showClock.Stype = stype;
            showClock.Product = product;
            showClock.IdProduct = idproduct;
            showClock.IdShow = idshow;
            showClock.CheckShow = true;
            showClock.CheckStype[stype] = true;
            clock.name.text = name;
            lighting.idStype = stype;
            lighting.status = 0;
            lighting.objUseGem = obj;
            Clock.transform.position = target;
            if (objLighting.activeSelf == false) objLighting.SetActive(true);
            Clock.SetActive(true);
        }
        else if (showClock.CheckShow == true)
        {
            showClock.CheckStype[showClock.Stype] = false;
            showClock.Stype = stype;
            showClock.Product = product;
            showClock.IdProduct = idproduct;
            showClock.IdShow = idshow;
            showClock.CheckStype[stype] = true;
            clock.name.text = name;
            lighting.idStype = stype;
            lighting.status = 0;
            lighting.objUseGem = obj;
            Clock.transform.position = target;
            if (objLighting.activeSelf == false) objLighting.SetActive(true);
            Clock.SetActive(true);
        }
    }
}

public struct ShowClock
{
    public int Stype;
    public int Product;
    public int IdProduct;
    public int IdShow;
    public bool CheckShow;
    public bool[] CheckStype;
}
}
