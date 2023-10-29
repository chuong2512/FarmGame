using UnityEngine;
using UnityEngine.UI;

public enum StypeUseGem
{
    DecorateTree, DecorateRockSmall, DecorateRockBig,
    DecoratePond, TreePOL, RockSmallPOL, RockBigPOL
}
public enum StypeUseGemBuySeed { SortDay, Flower}

public class ManagerUseGem : MonoBehaviour
{
    public static ManagerUseGem instance;
    private int amountDiamond;
    private StypeUseGem stypeUseGem;
    private StypeUseGemBuySeed stypeUseGemBuy;
    private GameObject objUseGem;
    public bool CheckUseGem;
    [Header("Gem Tool Decorate")]
    [SerializeField] Text TitleUseGemToDecorateText;
    [SerializeField] Text txtAmountDiamond;
    [SerializeField] Text txtQuestion;
    [SerializeField] Image imgTool;
    [SerializeField] GameObject dialogUseDiamond;

    [Header("Gem Buy Seeds")]
    [SerializeField] Text TitleGemUseBuySeedsText;
    [SerializeField] Text QuestionGemUseBuySeedsText;
    [SerializeField] Text NumberGemText;
    [SerializeField] Image IconSeedsImage;
    [SerializeField] GameObject UseGemBuySeeds;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            TitleUseGemToDecorateText.text = "Không đủ dụng cụ";
            txtQuestion.text = "Bạn có muốn sử dụng gem không?";
            TitleGemUseBuySeedsText.text =  "Không đủ hạt giống";
            QuestionGemUseBuySeedsText.text =  "Bạn có muốn mua hạt giống ngay không?";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            TitleUseGemToDecorateText.text = "Bahan baku tidak cukup";
            txtQuestion.text = "Apakah Anda ingin menggunakan permata?";
            TitleGemUseBuySeedsText.text = "Bahan baku tidak cukup";
            QuestionGemUseBuySeedsText.text = "Apakah Anda ingin membeli bibit?";
        }
        else
        {
            TitleUseGemToDecorateText.text = "Not Enough Resource";
            txtQuestion.text =  "Do you want to use gem?";
            TitleGemUseBuySeedsText.text = "Not Enough Resource";
            QuestionGemUseBuySeedsText.text = "Do you want buy them now with gem?";
        }
        
    }
    public void ShowDialogUseDiamond(int tool, StypeUseGem stype, int value, GameObject obj)
    {
        stypeUseGem = stype;
        amountDiamond = value;
        objUseGem = obj;
        imgTool.sprite = ManagerData.instance.toolDecorate.Data[tool].Icon;
        txtAmountDiamond.text = "" + amountDiamond;
        dialogUseDiamond.SetActive(true);
    }
    public void BtnAgree()
    {
        if (ManagerGem.instance.GemLive >= amountDiamond)
        {
            if (stypeUseGem == StypeUseGem.DecorateTree) objUseGem.GetComponent<DecorateTree>().ConditionEnough();
            else if (stypeUseGem == StypeUseGem.DecorateRockSmall) objUseGem.GetComponent<DecorateRockSmall>().ConditionEnough();
            else if (stypeUseGem == StypeUseGem.DecorateRockBig) objUseGem.GetComponent<DecorateRockBig>().ConditionEnough();
            else if (stypeUseGem == StypeUseGem.DecoratePond) objUseGem.GetComponent<DecorateRockBig>().ConditionEnough();
            else if (stypeUseGem == StypeUseGem.TreePOL) objUseGem.GetComponent<TreePlotOfLand>().ConditionEnough();
            else if (stypeUseGem == StypeUseGem.RockBigPOL) objUseGem.GetComponent<RockPlotOfLand>().ConditionEnough();
            ManagerGem.instance.MunisGem(amountDiamond);
            btnDisAgree();
        }
        else if (ManagerGem.instance.GemLive < amountDiamond)
        {
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Bạn không đủ gem!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Anda tidak memiliki cukup permata!";
            else str = "You haven't enough gem!";
            Notification.instance.dialogBelow(str);
        }
    }
    public void btnDisAgree()
    {
        MainCamera.instance.unLockCam();
        dialogUseDiamond.SetActive(false);
    }
    public void RegisterUseGemBuySeeds(StypeUseGemBuySeed type, int idseed, GameObject obj)
    {
        CheckUseGem = true;
        objUseGem = obj;
        stypeUseGemBuy = type;
        if (type == StypeUseGemBuySeed.SortDay)
        {
            NumberGemText.text = "2";
            IconSeedsImage.sprite = ManagerData.instance.seeds.Seed[idseed].iconStore;
        }
        else if (type == StypeUseGemBuySeed.Flower)
        {
            NumberGemText.text = "3";
            IconSeedsImage.sprite = ManagerData.instance.flowers.Data[idseed].detailFlower.iconStore;
        } 
        UseGemBuySeeds.SetActive(true);
    }
    public void ButtonYesUseGemBuySeeds()
    {
        CheckUseGem = false;
        if (stypeUseGemBuy == StypeUseGemBuySeed.SortDay) objUseGem.GetComponent<Ruong>().UseGemBuySeeds();
        else if (stypeUseGemBuy == StypeUseGemBuySeed.Flower) objUseGem.GetComponent<RuongHoa>().UseGemBuySeeds();
        UseGemBuySeeds.SetActive(false);
        MainCamera.instance.unLockCam();
    }
    public void ButtonNoUseGemBuySeeds()
    {
        CheckUseGem = false;
        if (stypeUseGemBuy == StypeUseGemBuySeed.SortDay) objUseGem.GetComponent<Ruong>().DontUseGemBuySeeds();
        else if (stypeUseGemBuy == StypeUseGemBuySeed.Flower) objUseGem.GetComponent<RuongHoa>().DontUseGemBuySeeds();
        UseGemBuySeeds.SetActive(false);
        MainCamera.instance.unLockCam();
    }
}
