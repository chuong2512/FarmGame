using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NongTrai;

namespace NongTrai
{
    public class ManagerMaps : MonoBehaviour
    {
        public static ManagerMaps ins = null;
        private int NumberPOL = 18;
        private int idPOLNow;
        private GameObject[] Purchase;
        [SerializeField] Text TitleExplaneText;
        [SerializeField] Text DescritionText;
        [SerializeField] Transform ContentBuy;
        [SerializeField] GameObject Unlock;
        [SerializeField] GameObject PurchaseCoin;
        [SerializeField] GameObject PurchaseGem;
        [SerializeField] GameObject ConfirmBuy;
        [SerializeField] FieldPOL[] POLs;

        private void Awake()
        {
            if (ins == null) ins = this;
            else if (ins != this) Destroy(gameObject);
        }

        private void Start()
        {
            InitData();
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                TitleExplaneText.text = "MỞ RỘNG TRANG TRẠI CỦA BẠN!";
                DescritionText.text = "Thêm diện tích đất trồng hoa!";
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                TitleExplaneText.text = "PERLUAS PERTANIANMU!";
                DescritionText.text = "Lebih banyak area untuk bangunanmu!";
            }
            else
            {
                TitleExplaneText.text = "EXPLAND YOUR FARM!";
                DescritionText.text = "More area for your building!";
            }
        }

        public int GetStatusPOL(int idPOL)
        {
            return POLs[idPOL].StatusPOL;
        }

        public void RegisterExpland(int idPOL)
        {
            idPOLNow = idPOL;
            int numberRequest = ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy.Length;
            Purchase = new GameObject[numberRequest];
            for (int i = 0; i < numberRequest; i++)
            {
                if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == false)
                {
                    Purchase[i] = Instantiate(PurchaseCoin, ContentBuy);
                    Text NumberCoin = Purchase[i].transform.GetChild(0).GetComponent<Text>();
                    NumberCoin.text = ManagerCoin.Instance.Coin + "/" +
                                      ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase;
                    if (ManagerCoin.Instance.Coin <
                        ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                        NumberCoin.color = new Color(1f, 127f / 255, 127f / 255, 1f);
                }
                else if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == true)
                {
                    Purchase[i] = Instantiate(PurchaseGem, ContentBuy);
                    Text NumberGem = Purchase[i].transform.GetChild(0).GetComponent<Text>();
                    NumberGem.text = ManagerGem.Instance.GemLive + "/" +
                                     ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase;
                    if (ManagerCoin.Instance.Coin <
                        ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                        NumberGem.color = new Color(1f, 127f / 255, 127f / 255, 1f);
                }
            }

            ConfirmBuy.SetActive(true);
        }

        public void ButtonConfirm()
        {
            if (CheckCondition() == false)
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Bạn chưa đủ điều kiện để nâng cấp!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Anda tidak memenuhi syarat untuk meningkatkan!";
                else str = "You are not eligible to upgrade!";
                Notification.Instance.dialogBelow(str);
            }
            else if (CheckCondition() == true)
            {
                POLs[idPOLNow].StatusPOL = 2;
                PlayerPrefs.SetInt("StatusPOL" + idPOLNow, POLs[idPOLNow].StatusPOL);
                ConfirmBuy.SetActive(false);
                for (int i = 0; i < Purchase.Length; i++) Destroy(Purchase[i]);
                Purchase = new GameObject[0];
                POLs[idPOLNow].sprPOL.color = new Color(1f, 1f, 1f, 0f);
                int numberRequest = ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy.Length;
                for (int i = 0; i < numberRequest; i++)
                {
                    if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == false)
                    {
                        int numberCoinPurchase = ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase;
                        ManagerCoin.Instance.MunisGold(numberCoinPurchase);
                    }
                    else if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == true)
                    {
                        int numberGemPurchase = ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase;
                        ManagerGem.Instance.MunisGem(numberGemPurchase);
                    }
                }

                StartCoroutine(CreateEffectUnlock());
            }
        }

        private IEnumerator CreateEffectUnlock()
        {
            for (int i = 0; i < POLs[idPOLNow].field.Length; i++)
            {
                Vector3 target = POLs[idPOLNow].field[i].transform.position;
                Instantiate(Unlock, target, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void ButtonExit()
        {
            ConfirmBuy.SetActive(false);
            for (int i = 0; i < Purchase.Length; i++) Destroy(Purchase[i]);
            Purchase = new GameObject[0];
        }

        public void RegisterDestroyDecorate(int POL)
        {
            POLs[POL].NumberDCRDestroy += 1;
            PlayerPrefs.SetInt("NumberDCRDestroyPOL" + POL, POLs[POL].NumberDCRDestroy);
            if (POLs[POL].NumberDCRDestroy == POLs[POL].NumberDecoratePOL)
            {
                POLs[POL].StatusPOL = 3;
                PlayerPrefs.SetInt("StatusPOL" + POL, POLs[POL].StatusPOL);
            }
        }

        public void DestroyDone(int idPOL)
        {
            if (POLs[idPOL].StatusPOL == 3) StartCoroutine(CreateField(idPOL));
        }

        private IEnumerator CreateField(int idPOL)
        {
            for (int i = 0; i < POLs[idPOL].field.Length; i++)
            {
                POLs[idPOL].field[i].SetActive(true);
                Vector3 target = POLs[idPOL].field[i].transform.position;
                Instantiate(Unlock, target, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void RegisterUpdate(int level)
        {
            for (int i = 0; i < POLs.Length; i++)
            {
                if (POLs[i].StatusPOL == 0)
                {
                    if (ManagerData.instance.plotOfLands.Datas[i].LevelUnlock == level)
                    {
                        POLs[i].StatusPOL = 1;
                        PlayerPrefs.SetInt("StatusPOL" + i, POLs[i].StatusPOL);
                        PlayerPrefs.SetInt("NumberDCRDestroyPOL" + i, POLs[i].NumberDCRDestroy);
                    }
                }
            }
        }

        private bool CheckCondition()
        {
            bool isCheck = true;
            for (int i = 0; i < ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy.Length; i++)
            {
                if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == false)
                {
                    if (ManagerCoin.Instance.Coin <
                        ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                        isCheck = false;
                }
                else if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == true)
                {
                    if (ManagerGem.Instance.GemLive <
                        ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                        isCheck = false;
                }
            }

            return isCheck;
        }

        private void InitData()
        {
            for (int i = 0; i < POLs.Length; i++)
            {
                if (PlayerPrefs.HasKey("StatusPOL" + i) == false)
                {
                    if (ManagerData.instance.plotOfLands.Datas[i].LevelUnlock == 0)
                    {
                        POLs[i].StatusPOL = 1;
                        PlayerPrefs.SetInt("StatusPOL" + i, POLs[i].StatusPOL);
                        PlayerPrefs.SetInt("NumberDCRDestroyPOL" + i, POLs[i].NumberDCRDestroy);
                    }
                    else if (ManagerData.instance.plotOfLands.Datas[i].LevelUnlock != 0)
                    {
                        PlayerPrefs.SetInt("StatusPOL" + i, POLs[i].StatusPOL);
                    }
                }
                else if (PlayerPrefs.HasKey("StatusPOL" + i) == true)
                {
                    POLs[i].StatusPOL = PlayerPrefs.GetInt("StatusPOL" + i);
                    if (POLs[i].StatusPOL == 2)
                    {
                        POLs[i].sprPOL.color = new Color(1f, 1f, 1f, 0f);
                        POLs[i].NumberDCRDestroy = PlayerPrefs.GetInt("NumberDCRDestroyPOL" + i);
                    }
                    else if (POLs[i].StatusPOL == 3)
                    {
                        Destroy(POLs[i].POL);
                        for (int k = 0; k < POLs[i].field.Length; k++)
                            POLs[i].field[k].SetActive(true);
                    }
                }
            }
        }

        [System.Serializable]
        private struct FieldPOL
        {
            public int StatusPOL;
            public int NumberDCRDestroy;
            public int NumberDecoratePOL;
            public SpriteRenderer sprPOL;
            public GameObject POL;
            public GameObject[] field;
        }
    }
}