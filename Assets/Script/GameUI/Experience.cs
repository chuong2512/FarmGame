using UnityEngine;
using UnityEngine.UI;

namespace NongTrai
{
    public class Experience : Singleton<Experience>
    {
        public int level, exp;
        float sliderExp;
        bool isLevelUp;
        [SerializeField] int maxExp, dem;

        [Header("UI")] [SerializeField] Image showExp;
        [SerializeField] Text txtShowLevel;
        [SerializeField] Text txtShowLevelUp;
        [SerializeField] Text txtShowExp;
        [SerializeField] Text txtContinue;
        [SerializeField] Text[] txtNew;

        [Header("GameObject")] [SerializeField]
        GameObject upLevel;

        [SerializeField] GameObject ExpSingle;
        [SerializeField] GameObject ExpAndItem;

        [Header("Transform")] public Transform PointerExp;
        public Transform PoiterDepot;

        [Header("Array")] [SerializeField] Image[] ItemOpen;
        [SerializeField] int[] maxExpLevel;


        void Start()
        {
            if (!PlayerPrefs.HasKey("Level")) PlayerPrefs.SetInt("Level", 0);
            if (!PlayerPrefs.HasKey("Exp")) PlayerPrefs.SetInt("Exp", 0);
            if (!PlayerPrefs.HasKey("MaxExp")) PlayerPrefs.SetInt("MaxExp", 0);
            level = PlayerPrefs.GetInt("Level");
            exp = PlayerPrefs.GetInt("Exp");
            if (level < 30) maxExp = maxExpLevel[level];
            else if (level >= 30) maxExp = PlayerPrefs.GetInt("MaxExp");
            sliderExp = (float) exp / maxExp;
            showExp.fillAmount = sliderExp;
            txtShowLevel.text = "" + (level + 1);
            txtShowExp.text = "" + exp + "/" + maxExp;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                txtContinue.text = "Tiếp tục";
                for (int i = 2; i < txtNew.Length; i++)
                {
                    txtNew[i].text = "Mới";
                }
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                txtContinue.text = "Terus";
                for (int i = 2; i < txtNew.Length; i++)
                {
                    txtNew[i].text = "Baru";
                }
            }
            else
            {
                txtContinue.text = "Continue";
                for (int i = 2; i < txtNew.Length; i++)
                {
                    txtNew[i].text = "New";
                }
            }
            //MobileFullVideo.instance.ShowFullNormal();
        }

        public void reciveExp(int expGet)
        {
            exp = exp + expGet;
            PlayerPrefs.SetInt("Exp", exp);
            sliderExp = (float) exp / maxExp;
            showExp.fillAmount = sliderExp;
            txtShowExp.text = "" + exp + "/" + maxExp;
            if (exp >= maxExp)
            {
                level = level + 1;
                PlayerPrefs.SetInt("Level", level);
                txtShowLevel.text = "" + (level + 1);
                exp -= maxExp;
                sliderExp = (float) exp / maxExp;
                showExp.fillAmount = sliderExp;
                if (level < 30) maxExp = maxExpLevel[level];
                else if (level >= 30)
                {
                    maxExp = (int) (1.2 * maxExp);
                    PlayerPrefs.SetInt("MaxExp", maxExp);
                }

                txtShowExp.text = "" + exp + "/" + maxExp;
                ManagerShop.instance.checkupdate(level);
                ManagerMaps.ins.RegisterUpdate(level);
                ManagerMainHouse.instance.CheckUpdate(level);
                ManagerMission.instance.CheckAddOrder(level);
                ManagerFountain.Instance.RegisterBuild(level);
                ManagerCargo.Instance.UnlockDockAndBoat(level);
                isLevelUp = true;
            }
        }

        void Update()
        {
            if (isLevelUp == true && ManagerTool.instance.dragging == false)
            {
                isLevelUp = false;
                LeveUp();
            }
        }

        public void registerExp(Sprite spr, int expShow, int amountShow, Vector3 position)
        {
            GameObject objExp = Instantiate(ExpAndItem, position, Quaternion.identity);
            ExprerienceItem exprer = objExp.GetComponent<ExprerienceItem>();
            exprer.iconItem.sprite = spr;
            exprer.numberExp = expShow;
            exprer.numberItem = amountShow;
        }

        public void registerExpSingle(int expShow, Vector3 position)
        {
            GameObject objExp = Instantiate(ExpSingle, position, Quaternion.identity);
            ExperienceFly exprer = objExp.GetComponent<ExperienceFly>();
            exprer.numberExp = expShow;
        }

        public void registerItemOpen(Sprite spr)
        {
            if (dem < ItemOpen.Length)
            {
                ItemOpen[dem].sprite = spr;
                ItemOpen[dem].gameObject.SetActive(true);
                dem += 1;
            }
        }

        void LeveUp()
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                txtShowLevelUp.text = "LÊN CẤP " + (level + 1);
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                txtShowLevelUp.text = "NAIK TINGKAT " + (level + 1);
            else txtShowLevelUp.text = "LEVEL UP " + (level + 1);
            upLevel.SetActive(true);
            ManagerAudio.Instance.PlayAudio(Audio.Uplevel);
            MainCamera.instance.LockCamLevelUp();
            
            QuangCaoGoogle.Instance.ShowInterAds();
        }

        public void closeUpLevel()
        {
            for (int i = 2; i < dem; i++)
            {
                ItemOpen[i].gameObject.SetActive(false);
            }

            dem = 2;
            upLevel.SetActive(false);
            ManagerGem.Instance.ReciveGem(2);
            ManagerMarket.instance.ReciveItem(5, 0, 2, false);
            MainCamera.instance.UnlockCamLevelUp();
            ManagerAudio.Instance.PlayAudio(Audio.Click);
        }
    }
}