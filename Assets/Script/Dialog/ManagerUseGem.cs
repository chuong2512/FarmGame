using NongTrai;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NongTrai
{
    public enum StypeUseGem
    {
        DecorateTree,
        DecorateRockSmall,
        DecorateRockBig,
        DecoratePond,
        TreePOL,
        RockSmallPOL,
        RockBigPOL
    }

    public enum StypeUseGemBuySeed
    {
        SortDay,
        Flower
    }

    public class ManagerUseGem : Singleton<ManagerUseGem>
    {
        private int _amountDiamond;
        private StypeUseGem _stypeUseGem;
        private StypeUseGemBuySeed _stypeUseGemBuy;
        private GameObject _objUseGem;
        [FormerlySerializedAs("CheckUseGem")] public bool checkUseGem;

        [FormerlySerializedAs("TitleUseGemToDecorateText")] [Header("Gem Tool Decorate")] [SerializeField]
        Text titleUseGemToDecorateText;

        [SerializeField] Text txtAmountDiamond;
        [SerializeField] Text txtQuestion;
        [SerializeField] Image imgTool;
        [SerializeField] GameObject dialogUseDiamond;

        [FormerlySerializedAs("TitleGemUseBuySeedsText")] [Header("Gem Buy Seeds")] [SerializeField]
        Text titleGemUseBuySeedsText;

        [FormerlySerializedAs("QuestionGemUseBuySeedsText")] [SerializeField]
        Text questionGemUseBuySeedsText;

        [FormerlySerializedAs("NumberGemText")] [SerializeField]
        Text numberGemText;

        [FormerlySerializedAs("IconSeedsImage")] [SerializeField]
        Image iconSeedsImage;

        [FormerlySerializedAs("UseGemBuySeeds")] [SerializeField]
        GameObject useGemBuySeeds;

        void Start()
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                titleUseGemToDecorateText.text = "Không đủ dụng cụ";
                txtQuestion.text = "Bạn có muốn sử dụng gem không?";
                titleGemUseBuySeedsText.text = "Không đủ hạt giống";
                questionGemUseBuySeedsText.text = "Bạn có muốn mua hạt giống ngay không?";
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                titleUseGemToDecorateText.text = "Bahan baku tidak cukup";
                txtQuestion.text = "Apakah Anda ingin menggunakan permata?";
                titleGemUseBuySeedsText.text = "Bahan baku tidak cukup";
                questionGemUseBuySeedsText.text = "Apakah Anda ingin membeli bibit?";
            }
            else
            {
                titleUseGemToDecorateText.text = "Not Enough Resource";
                txtQuestion.text = "Do you want to use gem?";
                titleGemUseBuySeedsText.text = "Not Enough Resource";
                questionGemUseBuySeedsText.text = "Do you want buy them now with gem?";
            }
        }

        public void ShowDialogUseDiamond(int tool, StypeUseGem stype, int value, GameObject obj)
        {
            _stypeUseGem = stype;
            _amountDiamond = value;
            _objUseGem = obj;
            imgTool.sprite = ManagerData.instance.toolDecorate.Datas[tool].Icon;
            txtAmountDiamond.text = "" + _amountDiamond;
            dialogUseDiamond.SetActive(true);
        }

        public void BtnAgree()
        {
            if (ManagerGem.Instance.GemLive >= _amountDiamond)
            {
                if (_stypeUseGem == StypeUseGem.DecorateTree) _objUseGem.GetComponent<DecorateTree>().ConditionEnough();
                else if (_stypeUseGem == StypeUseGem.DecorateRockSmall)
                    _objUseGem.GetComponent<DecorateRockSmall>().ConditionEnough();
                else if (_stypeUseGem == StypeUseGem.DecorateRockBig)
                    _objUseGem.GetComponent<DecorateRockBig>().ConditionEnough();
                else if (_stypeUseGem == StypeUseGem.DecoratePond)
                    _objUseGem.GetComponent<DecorateRockBig>().ConditionEnough();
                else if (_stypeUseGem == StypeUseGem.TreePOL)
                    _objUseGem.GetComponent<TreePlotOfLand>().ConditionEnough();
                else if (_stypeUseGem == StypeUseGem.RockBigPOL)
                    _objUseGem.GetComponent<RockPlotOfLand>().ConditionEnough();
                ManagerGem.Instance.MunisGem(_amountDiamond);
                btnDisAgree();
            }
            else if (ManagerGem.Instance.GemLive < _amountDiamond)
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Bạn không đủ gem!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Anda tidak memiliki cukup permata!";
                else str = "You haven't enough gem!";
                Notification.Instance.dialogBelow(str);
            }
        }

        public void btnDisAgree()
        {
            MainCamera.instance.unLockCam();
            dialogUseDiamond.SetActive(false);
        }

        public void ButtonYesUseGemBuySeeds()
        {
            checkUseGem = false;
            if (_stypeUseGemBuy == StypeUseGemBuySeed.SortDay) _objUseGem.GetComponent<Ruong>().UseGemBuySeeds();
            else if (_stypeUseGemBuy == StypeUseGemBuySeed.Flower) _objUseGem.GetComponent<RuongHoa>().UseGemBuySeeds();
            useGemBuySeeds.SetActive(false);
            MainCamera.instance.unLockCam();
        }

        public void ButtonNoUseGemBuySeeds()
        {
            checkUseGem = false;
            if (_stypeUseGemBuy == StypeUseGemBuySeed.SortDay) _objUseGem.GetComponent<Ruong>().DontUseGemBuySeeds();
            else if (_stypeUseGemBuy == StypeUseGemBuySeed.Flower)
                _objUseGem.GetComponent<RuongHoa>().DontUseGemBuySeeds();
            useGemBuySeeds.SetActive(false);
            MainCamera.instance.unLockCam();
        }

        public void RegisterUseGemBuySeeds(StypeUseGemBuySeed type, int idseed, GameObject obj)
        {
            checkUseGem = true;
            _objUseGem = obj;
            _stypeUseGemBuy = type;
            if (type == StypeUseGemBuySeed.SortDay)
            {
                numberGemText.text = "2";
                iconSeedsImage.sprite = ManagerData.instance.seeds.SeedDatas[idseed].iconStore;
            }
            else if (type == StypeUseGemBuySeed.Flower)
            {
                numberGemText.text = "3";
                iconSeedsImage.sprite = ManagerData.instance.flowers.Data[idseed].detailFlower.iconStore;
            }

            useGemBuySeeds.SetActive(true);
        }
    }
}