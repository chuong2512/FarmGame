using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NongTrai;
using UnityEngine.Serialization;

namespace NongTrai
{
    public class ManagerMaps : MonoBehaviour
    {
        public static ManagerMaps ins = null;
        private int NumberPOL = 18;
        private int idPOLNow;
        private GameObject[] Purchase;
        [FormerlySerializedAs("TitleExplaneText")] [SerializeField] Text titleExplaneText;
        [FormerlySerializedAs("DescritionText")] [SerializeField] Text descritionText;
        [FormerlySerializedAs("ContentBuy")] [SerializeField] Transform contentBuy;
        [FormerlySerializedAs("Unlock")] [SerializeField] GameObject unlock;
        [FormerlySerializedAs("PurchaseCoin")] [SerializeField] GameObject purchaseCoin;
        [FormerlySerializedAs("PurchaseGem")] [SerializeField] GameObject purchaseGem;
        [FormerlySerializedAs("ConfirmBuy")] [SerializeField] GameObject confirmBuy;
        [FormerlySerializedAs("POLs")] [SerializeField] FieldPOL[] poLs;

        private void Awake()
        {
            if (ins == null) ins = this;
            else if (ins != this) Destroy(gameObject);
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
                    Purchase[i] = Instantiate(purchaseCoin, contentBuy);
                    Text NumberCoin = Purchase[i].transform.GetChild(0).GetComponent<Text>();
                    NumberCoin.text = ManagerCoin.Instance.Coin + "/" +
                                      ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase;
                    if (ManagerCoin.Instance.Coin <
                        ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                        NumberCoin.color = new Color(1f, 127f / 255, 127f / 255, 1f);
                }
                else if (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem == true)
                {
                    Purchase[i] = Instantiate(purchaseGem, contentBuy);
                    Text NumberGem = Purchase[i].transform.GetChild(0).GetComponent<Text>();
                    NumberGem.text = ManagerGem.Instance.GemLive + "/" +
                                     ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase;
                    if (ManagerCoin.Instance.Coin <
                        ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                        NumberGem.color = new Color(1f, 127f / 255, 127f / 255, 1f);
                }
            }

            confirmBuy.SetActive(true);
        }

        
        public int GetStatusPol(int idPOL)
        {
            return poLs[idPOL].StatusPOL;
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
            else if (CheckCondition())
            {
                poLs[idPOLNow].StatusPOL = 2;
                PlayerPrefs.SetInt("StatusPOL" + idPOLNow, poLs[idPOLNow].StatusPOL);
                confirmBuy.SetActive(false);
                for (int i = 0; i < Purchase.Length; i++) Destroy(Purchase[i]);
                Purchase = new GameObject[0];
                poLs[idPOLNow].sprPOL.color = new Color(1f, 1f, 1f, 0f);
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
            for (int i = 0; i < poLs[idPOLNow].field.Length; i++)
            {
                Vector3 target = poLs[idPOLNow].field[i].transform.position;
                Instantiate(unlock, target, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void ButtonExit()
        {
            confirmBuy.SetActive(false);
            foreach (var pur in Purchase)
                Destroy(pur);

            Purchase = new GameObject[0];
        }

        public void RegisterDestroyDecorate(int POL)
        {
            poLs[POL].NumberDCRDestroy += 1;
            PlayerPrefs.SetInt("NumberDCRDestroyPOL" + POL, poLs[POL].NumberDCRDestroy);
            if (poLs[POL].NumberDCRDestroy == poLs[POL].NumberDecoratePOL)
            {
                poLs[POL].StatusPOL = 3;
                PlayerPrefs.SetInt("StatusPOL" + POL, poLs[POL].StatusPOL);
            }
        }

        public void DestroyDone(int idPOL)
        {
            if (poLs[idPOL].StatusPOL == 3) StartCoroutine(CreateField(idPOL));
        }

        private IEnumerator CreateField(int idPOL)
        {
            foreach (var fi in poLs[idPOL].field)
            {
                fi.SetActive(true);
                Vector3 target = fi.transform.position;
                Instantiate(unlock, target, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }

        public void RegisterUpdate(int level)
        {
            for (int i = 0; i < poLs.Length; i++)
            {
                if (poLs[i].StatusPOL != 0) continue;
                if (ManagerData.instance.plotOfLands.Datas[i].LevelUnlock != level) continue;
                poLs[i].StatusPOL = 1;
                PlayerPrefs.SetInt("StatusPOL" + i, poLs[i].StatusPOL);
                PlayerPrefs.SetInt("NumberDCRDestroyPOL" + i, poLs[i].NumberDCRDestroy);
            }
        }

        private bool CheckCondition()
        {
            bool isCheck = true;
            for (int i = 0; i < ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy.Length; i++)
            {
                switch (ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].isGem)
                {
                    case false:
                    {
                        if (ManagerCoin.Instance.Coin <
                            ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                            isCheck = false;
                        break;
                    }
                    case true:
                    {
                        if (ManagerGem.Instance.GemLive <
                            ManagerData.instance.plotOfLands.Datas[idPOLNow].InforBuy[i].Purchase)
                            isCheck = false;
                        break;
                    }
                }
            }

            return isCheck;
        }

        private void InitData()
        {
            for (int i = 0; i < poLs.Length; i++)
            {
                if (PlayerPrefs.HasKey("StatusPOL" + i) == false)
                {
                    if (ManagerData.instance.plotOfLands.Datas[i].LevelUnlock == 0)
                    {
                        poLs[i].StatusPOL = 1;
                        PlayerPrefs.SetInt("StatusPOL" + i, poLs[i].StatusPOL);
                        PlayerPrefs.SetInt("NumberDCRDestroyPOL" + i, poLs[i].NumberDCRDestroy);
                    }
                    else if (ManagerData.instance.plotOfLands.Datas[i].LevelUnlock != 0)
                    {
                        PlayerPrefs.SetInt("StatusPOL" + i, poLs[i].StatusPOL);
                    }
                }
                else if (PlayerPrefs.HasKey("StatusPOL" + i) == true)
                {
                    poLs[i].StatusPOL = PlayerPrefs.GetInt("StatusPOL" + i);
                    switch (poLs[i].StatusPOL)
                    {
                        case 2:
                            poLs[i].sprPOL.color = new Color(1f, 1f, 1f, 0f);
                            poLs[i].NumberDCRDestroy = PlayerPrefs.GetInt("NumberDCRDestroyPOL" + i);
                            break;
                        case 3:
                        {
                            Destroy(poLs[i].POL);
                            for (int k = 0; k < poLs[i].field.Length; k++)
                                poLs[i].field[k].SetActive(true);
                            break;
                        }
                    }
                }
            }
        }
        
        private void Start()
        {
            InitData();
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                titleExplaneText.text = "MỞ RỘNG TRANG TRẠI CỦA BẠN!";
                descritionText.text = "Thêm diện tích đất trồng hoa!";
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                titleExplaneText.text = "PERLUAS PERTANIANMU!";
                descritionText.text = "Lebih banyak area untuk bangunanmu!";
            }
            else
            {
                titleExplaneText.text = "EXPLAND YOUR FARM!";
                descritionText.text = "More area for your building!";
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