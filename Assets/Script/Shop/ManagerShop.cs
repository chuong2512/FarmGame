using NongTrai;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace NongTrai
{
    public class ManagerShop : MonoBehaviour
    {
        public static ManagerShop instance;

        private Animator Ani;
        private bool showShop;
        private bool buying;
        private bool blNewItem;
        private int idShopSelect;

        [SerializeField] Animator AniButtonShop;

        [Header("Object")] public GameObject obj;
        public GameObject objPet;

        [Header("Name Shop")] [SerializeField] Text txtNameShopHome;
        [SerializeField] Text txtNameShopPet;
        [SerializeField] Text txtNameShopFactory;
        [SerializeField] Text txtNameShopOldTree;
        [SerializeField] Text txtNameDecorate;
        [SerializeField] Text[] NewItemText;

        [Header("Image Shop")] [SerializeField]
        Image[] StypeShop;

        [Header("Image Button Shop")] [SerializeField]
        Sprite[] ShopLock;

        [SerializeField] Sprite[] ShopSelect;

        [Header("SrollSnap")] public ScrollRect scrollRectCage;
        public ScrollRect scrollRectPet;
        public ScrollRect scrollRectFactory;
        public ScrollRect scrollRectOldTree;
        public ScrollRect scrollRectDecorate;

        [Header("Information")] public Information infoField;
        public Information infoCage;
        public Information infoPet;
        public Information infoFactory;
        public Information inforTree;
        public Information inforDecorate;
        public InformationItemFactory infoItemFactory;
        public InformationItemSeeds infoSeeds;
        public InformationItemFlowers inforFlowers;

        [Header("Object Create")] public GameObject[] Field;
        public GameObject[] Cage;
        public GameObject[] Pet;
        public GameObject[] Factory;
        public GameObject[] Tree;
        public GameObject[] Decorate;
        public GameObject[] Shop;

        [Header("Parent")] public Transform[] parentField;
        public Transform[] parentCage;
        public Transform[] parentFactory;
        public Transform[] parentTree;
        public Transform[] parentDCR;

        [Header("Buy Pets")] [HideInInspector] public int idPet;
        [HideInInspector] public int idHomeAnimal;
        [HideInInspector] public bool buyingPet;

        [SerializeField] InforShop[] inforShops;

        // Use this for initialization
        void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);
        }

        void Start()
        {
            LoadData();
            Ani = GetComponent<Animator>();
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                txtNameShopHome.text = "CHUỒNG TRẠI";
                txtNameShopPet.text = "VẬT NUÔI";
                txtNameShopFactory.text = "NHÀ MÁY";
                txtNameShopOldTree.text = "CÂY LÂU NĂM";
                txtNameDecorate.text = "TRANG TRÍ";
                for (int i = 0; i < NewItemText.Length; i++) NewItemText[i].text = "Mới";
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                txtNameShopHome.text = "RUMAH HEWAN";
                txtNameShopPet.text = "HEWAN";
                txtNameShopFactory.text = "BANGUNAN PRODUKSI";
                txtNameShopOldTree.text = "POHON & SEMAK";
                txtNameDecorate.text = "MENGHIAS";
                for (int i = 0; i < NewItemText.Length; i++) NewItemText[i].text = "Baru";
            }
            else
            {
                txtNameShopHome.text = "ANIMAL HOME";
                txtNameShopPet.text = "PETS";
                txtNameShopFactory.text = "FACTORY";
                txtNameShopOldTree.text = "OLD TREE";
                txtNameDecorate.text = "DECORATE";
                for (int i = 0; i < NewItemText.Length; i++) NewItemText[i].text = "New";
            }
        }

        public void ButtonShop()
        {
            if (showShop == false)
            {
                MainCamera.instance.DisableAll();
                MainCamera.instance.lockCam();
                ManagerAudio.Instance.PlayAudio(Audio.ClickOpen);
                ShowShop();
            }
            else if (showShop == true)
            {
                MainCamera.instance.DisableAll();
                MainCamera.instance.unLockCam();
                ManagerAudio.Instance.PlayAudio(Audio.ClickExit);
                HideShop();
            }

            if (blNewItem == true)
            {
                blNewItem = false;
                PlayerPrefs.SetInt("StatusButtonShop", 0);
                AniButtonShop.SetBool("Run", blNewItem);
            }
        }

        public void ShowShop()
        {
            showShop = true;
            Ani.SetBool("isShowShop", showShop);
            idShopSelect = 0;
            Shop[idShopSelect].SetActive(true);
            StypeShop[idShopSelect].sprite = ShopSelect[idShopSelect];
            if (inforShops[idShopSelect].status == 1)
            {
                inforShops[idShopSelect].status = 0;
                PlayerPrefs.SetInt("StatusShop" + idShopSelect, inforShops[idShopSelect].status);
                inforShops[idShopSelect].NewItem.SetActive(false);
            }

            if (ManagerGuide.Instance.GuideClickShopPetsBuyChicken == 0) ManagerGuide.Instance.CallArrowShopPet();
        }

        public void HideShop()
        {
            Shop[idShopSelect].SetActive(false);
            StypeShop[idShopSelect].sprite = ShopLock[idShopSelect];
            showShop = false;
            Ani.SetBool("isShowShop", showShop);
            MainCamera.instance.unLockCam();
        }

        public void isBuying()
        {
            if (buying == false)
            {
                buying = true;
                Ani.SetBool("isBuying", true);
            }
            else if (buying == true)
            {
                buying = false;
                Ani.SetBool("isBuying", false);
            }
        }

        public void ChangeShop(int id)
        {
            ManagerAudio.Instance.PlayAudio(Audio.Click);
            if (id != idShopSelect)
            {
                Shop[idShopSelect].SetActive(false);
                StypeShop[idShopSelect].sprite = ShopLock[idShopSelect];
                idShopSelect = id;
                Shop[idShopSelect].SetActive(true);
                StypeShop[idShopSelect].sprite = ShopSelect[idShopSelect];
                if (inforShops[idShopSelect].status == 1)
                {
                    inforShops[idShopSelect].status = 0;
                    PlayerPrefs.SetInt("StatusShop" + idShopSelect, inforShops[idShopSelect].status);
                    inforShops[idShopSelect].NewItem.SetActive(false);
                }

                if (idShopSelect == 1 && ManagerGuide.Instance.GuideClickShopPetsBuyChicken == 0)
                {
                    ManagerGuide.Instance.GuideClickShopPetsBuyChicken = 1;
                    ManagerGuide.Instance.DoneArrowShopPet();
                }
            }
        }

        public void ChangeValueScrollCage(float value)
        {
            scrollRectCage.horizontalNormalizedPosition += -value;
        }

        public void ChangeValueScrollPet(UnityEngine.EventSystems.PointerEventData eventData)
        {
            scrollRectPet.OnDrag(eventData);
        }

        public void ChangeValueScrollFactory(float value)
        {
            scrollRectFactory.horizontalNormalizedPosition += -value;
        }

        public void ChangeValueScrollOldTree(float value)
        {
            scrollRectOldTree.horizontalNormalizedPosition += -value;
        }

        public void buyField(int id)
        {
            ManagerCoin.Instance.MunisGold(infoField.info[id].goldPrice);
            infoField.info[id].amount = infoField.info[id].amount + 1;
            PlayerPrefs.SetInt("amountField" + id, infoField.info[id].amount);
            infoField.info[id].txtAmount.text = "" + infoField.info[id].amount + "/" + infoField.info[id].total;
            if (infoField.info[id].amount == infoField.info[id].total)
            {
                infoField.info[id].status = 0;
                PlayerPrefs.SetInt("statusFieldStore" + id, infoField.info[id].status);
                infoField.info[id].txtGoldPrice.gameObject.SetActive(false);
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoField.info[id].txtInfo.text = "Mở khóa cấp " + (infoField.info[id].levelOpen + 1);
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoField.info[id].txtInfo.text = "Terbuka di level " + (infoField.info[id].levelOpen + 1);
                else infoField.info[id].txtInfo.text = "Unlock Level " + (infoField.info[id].levelOpen + 1);
            }
        }

        void updateField(int id)
        {
            infoField.info[id].status = 1;
            PlayerPrefs.SetInt("statusFieldStore" + id, infoField.info[id].status);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                infoField.info[id].txtInfo.text = "Đất trồng cây";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                infoField.info[id].txtInfo.text = "Menumbuhkan Tanaman";
            else infoField.info[id].txtInfo.text = "Grow Crops";

            infoField.info[id].goldPrice = infoField.info[id].goldPrice * ManagerData.instance.lands.Land[id].mutiGold;
            PlayerPrefs.SetInt("goldPriceField" + id, infoField.info[id].goldPrice);
            infoField.info[id].txtGoldPrice.text = "" + infoField.info[id].goldPrice;
            infoField.info[id].txtGoldPrice.gameObject.SetActive(true);
            infoField.info[id].levelOpen =
                infoField.info[id].levelOpen + ManagerData.instance.lands.Land[id].distanceLvOpen;
            PlayerPrefs.SetInt("levelOpenField" + id, infoField.info[id].levelOpen);
            infoField.info[id].total = infoField.info[id].total + ManagerData.instance.lands.Land[id].amountOpen;
            PlayerPrefs.SetInt("totalField" + id, infoField.info[id].total);
            infoField.info[id].txtAmount.text = "" + infoField.info[id].amount + "/" + infoField.info[id].total;
            if (PlayerPrefs.GetInt("iconField" + id) == 0)
            {
                infoField.info[id].icon.sprite = ManagerData.instance.lands.Land[id].iconStore;
                PlayerPrefs.SetInt("iconField" + id, 1);
            }

            Experience.Instance.registerItemOpen(ManagerData.instance.lands.Land[id].iconStore);
        }

        public void buyCage(int id)
        {
            ManagerCoin.Instance.MunisGold(infoCage.info[id].goldPrice);
            infoCage.info[id].amount = infoCage.info[id].amount + 1;
            PlayerPrefs.SetInt("amountCage" + id, infoCage.info[id].amount);
            infoCage.info[id].txtAmount.text = "" + infoCage.info[id].amount + "/" + infoCage.info[id].total;
            if (infoCage.info[id].amount == infoCage.info[id].total)
            {
                infoCage.info[id].status = 0;
                PlayerPrefs.SetInt("statusCageStore" + id, infoCage.info[id].status);
                infoCage.info[id].txtGoldPrice.gameObject.SetActive(false);
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoCage.info[id].txtInfo.text = "Mở khóa cấp " + (infoCage.info[id].levelOpen + 1);
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoCage.info[id].txtInfo.text = "Terbuka di level " + (infoCage.info[id].levelOpen + 1);
                else infoCage.info[id].txtInfo.text = "Unlock Level " + (infoCage.info[id].levelOpen + 1);
            }
        }

        void updateCage(int id)
        {
            infoCage.info[id].status = 1;
            PlayerPrefs.SetInt("statusCageStore" + id, infoCage.info[id].status);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                infoCage.info[id].txtInfo.text = "Sức chứa " + ManagerData.instance.cages.Cage[id].amountPet + " " +
                                                 ManagerData.instance.petCollection.Pet[id].detailPet.name;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                infoCage.info[id].txtInfo.text = "Menampung " + ManagerData.instance.cages.Cage[id].amountPet + " " +
                                                 ManagerData.instance.petCollection.Pet[id].detailPet.nameINS;
            else
                infoCage.info[id].txtInfo.text = "Capacity " + ManagerData.instance.cages.Cage[id].amountPet + " " +
                                                 ManagerData.instance.petCollection.Pet[id].detailPet.engName;
            infoCage.info[id].goldPrice = infoCage.info[id].goldPrice * ManagerData.instance.cages.Cage[id].mutiGold;
            PlayerPrefs.SetInt("goldPriceCage" + id, infoCage.info[id].goldPrice);
            infoCage.info[id].txtGoldPrice.text = "" + infoCage.info[id].goldPrice;
            infoCage.info[id].txtGoldPrice.gameObject.SetActive(true);
            infoCage.info[id].levelOpen =
                infoCage.info[id].levelOpen + ManagerData.instance.cages.Cage[id].distanceLvOpen;
            PlayerPrefs.SetInt("levelOpenCage" + id, infoCage.info[id].levelOpen);
            infoCage.info[id].total = infoCage.info[id].total + ManagerData.instance.cages.Cage[id].amountOpen;
            PlayerPrefs.SetInt("totalCage" + id, infoCage.info[id].total);
            infoCage.info[id].txtAmount.text = "" + infoCage.info[id].amount + "/" + infoCage.info[id].total;
            if (PlayerPrefs.GetInt("iconCage" + id) == 0)
            {
                infoCage.info[id].icon.sprite = ManagerData.instance.cages.Cage[id].iconStore;
                PlayerPrefs.SetInt("iconCage" + id, 1);
            }

            Experience.Instance.registerItemOpen(ManagerData.instance.cages.Cage[id].iconStore);
        }

        public void BuildCage(int idCage, int idProduct, Vector3 target)
        {
            ManagerBreads.Instance.UpdateNumberCage(idCage);
            GameObject cageBuilding = Instantiate(Cage[idCage], target, Quaternion.identity, parentCage[idCage]);
            HomeAnimal ha = cageBuilding.transform.GetChild(0).GetComponent<HomeAnimal>();
            ha.idAmountHome = idProduct;
            PlayerPrefs.SetFloat("PosCageX" + idCage + "" + idProduct, ha.transform.position.x);
            PlayerPrefs.SetFloat("PosCageY" + idCage + "" + idProduct, ha.transform.position.y);
            Experience.Instance.registerExpSingle(ManagerData.instance.cages.Cage[idCage].Exp, target);
        }

        public void buyPet(int id)
        {
            ManagerCoin.Instance.MunisGold(infoPet.info[id].goldPrice);
            infoPet.info[id].amount = infoPet.info[id].amount + 1;
            PlayerPrefs.SetInt("amountPet" + id, infoPet.info[id].amount);
            infoPet.info[id].txtAmount.text = "" + infoPet.info[id].amount + "/" + infoPet.info[id].total;
            if (infoPet.info[id].amount == infoPet.info[id].total)
            {
                infoPet.info[id].status = 0;
                PlayerPrefs.SetInt("statusPetStore" + id, infoPet.info[id].status);
                infoPet.info[id].txtGoldPrice.gameObject.SetActive(false);
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoPet.info[id].txtInfo.text = "Mở khóa cấp " + (infoCage.info[id].levelOpen + 1);
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoPet.info[id].txtInfo.text = "Terbuka di level " + (infoCage.info[id].levelOpen + 1);
                else infoPet.info[id].txtInfo.text = "Unlock Level " + (infoCage.info[id].levelOpen + 1);
            }
        }

        void updatePet(int id)
        {
            if (infoPet.info[id].total == 0) ManagerItem.Instance.UpdateItem(1, id);
            infoPet.info[id].status = 1;
            PlayerPrefs.SetInt("statusPetStore" + id, infoPet.info[id].status);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                infoPet.info[id].txtInfo.text = "Thời gian thu hoạch " +
                                                ManagerGame.Instance.TimeText(ManagerData.instance.petCollection.Pet[id]
                                                    .detailPet.time);
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                infoPet.info[id].txtInfo.text = "Waktu panen" +
                                                ManagerGame.Instance.TimeText(ManagerData.instance.petCollection.Pet[id]
                                                    .detailPet.time);
            else
                infoPet.info[id].txtInfo.text = "Harvest Time " +
                                                ManagerGame.Instance.TimeText(ManagerData.instance.petCollection.Pet[id]
                                                    .detailPet.time);
            infoPet.info[id].goldPrice =
                infoPet.info[id].goldPrice * ManagerData.instance.petCollection.Pet[id].detailPet.mutiGold;
            PlayerPrefs.SetInt("goldPricePet" + id, infoPet.info[id].goldPrice);
            infoPet.info[id].txtGoldPrice.text = "" + infoPet.info[id].goldPrice;
            infoPet.info[id].txtGoldPrice.gameObject.SetActive(true);
            infoPet.info[id].levelOpen = infoPet.info[id].levelOpen +
                                         ManagerData.instance.petCollection.Pet[id].detailPet.distanceLvOpen;
            PlayerPrefs.SetInt("levelOpenPet" + id, infoPet.info[id].levelOpen);
            infoPet.info[id].total =
                infoPet.info[id].total + ManagerData.instance.petCollection.Pet[id].detailPet.quantityOpen;
            PlayerPrefs.SetInt("totalPet" + id, infoPet.info[id].total);
            infoPet.info[id].txtAmount.text = "" + infoPet.info[id].amount + "/" + infoPet.info[id].total;
            if (PlayerPrefs.GetInt("iconPet" + id) == 0)
            {
                infoPet.info[id].icon.sprite = ManagerData.instance.petCollection.Pet[id].detailPet.iconStore;
                PlayerPrefs.SetInt("iconPet" + id, 1);
            }

            Experience.Instance.registerItemOpen(ManagerData.instance.petCollection.Pet[id].detailPet.iconStore);
        }

        public void buyFactory(int id)
        {
            ManagerCoin.Instance.MunisGold(infoFactory.info[id].goldPrice);
            infoFactory.info[id].amount = infoFactory.info[id].amount + 1;
            PlayerPrefs.SetInt("amountFactory" + id, infoFactory.info[id].amount);
            infoFactory.info[id].txtAmount.text = "" + infoFactory.info[id].amount + "/" + infoFactory.info[id].total;
            if (infoFactory.info[id].amount >= infoFactory.info[id].total)
            {
                infoFactory.info[id].status = 0;
                PlayerPrefs.SetInt("statusFactoryStore" + id, infoFactory.info[id].status);
                infoFactory.info[id].txtGoldPrice.gameObject.SetActive(false);
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoFactory.info[id].txtInfo.text = "Mở khóa cấp " + (infoFactory.info[id].levelOpen + 1);
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoFactory.info[id].txtInfo.text = "Terbuka di level " + (infoFactory.info[id].levelOpen + 1);
                else infoFactory.info[id].txtInfo.text = "Unlock Level " + (infoFactory.info[id].levelOpen + 1);
            }
        }

        public void BuildFactory(int idFactory, int idProduct, Vector3 target)
        {
            ManagerFactory.instance.UpdateNumberFactory(idFactory);
            GameObject factoryBuilding =
                Instantiate(Factory[idFactory], target, Quaternion.identity, parentFactory[idFactory]);
            NhaMay nm = factoryBuilding.GetComponent<NhaMay>();
            nm.idSoNhaMay = idProduct;
            PlayerPrefs.SetFloat("PosFactoryX" + idFactory + "" + idProduct, nm.transform.position.x);
            PlayerPrefs.SetFloat("PosFactoryY" + idFactory + "" + idProduct, nm.transform.position.y);
            Experience.Instance.registerExpSingle(ManagerData.instance.facetorys.Facetory[idFactory].Exp, target);
        }

        void updateFactory(int id)
        {
            infoFactory.info[id].status = 1;
            PlayerPrefs.SetInt("statusFactoryStore" + id, infoFactory.info[id].status);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                infoFactory.info[id].txtInfo.text = ManagerData.instance.facetorys.Facetory[id].name;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                infoFactory.info[id].txtInfo.text = ManagerData.instance.facetorys.Facetory[id].nameINS;
            else infoFactory.info[id].txtInfo.text = ManagerData.instance.facetorys.Facetory[id].engName;
            infoFactory.info[id].goldPrice =
                infoFactory.info[id].goldPrice * ManagerData.instance.facetorys.Facetory[id].mutiGold;
            PlayerPrefs.SetInt("goldPriceFactory" + id, infoFactory.info[id].goldPrice);
            infoFactory.info[id].txtGoldPrice.text = "" + infoFactory.info[id].goldPrice;
            infoFactory.info[id].txtGoldPrice.gameObject.SetActive(true);
            infoFactory.info[id].levelOpen =
                infoFactory.info[id].levelOpen + ManagerData.instance.facetorys.Facetory[id].distanceLvOpen;
            PlayerPrefs.SetInt("levelOpenFactory" + id, infoFactory.info[id].levelOpen);
            infoFactory.info[id].total =
                infoFactory.info[id].total + ManagerData.instance.facetorys.Facetory[id].amountOpen;
            PlayerPrefs.SetInt("totalFactory" + id, infoFactory.info[id].total);
            infoFactory.info[id].txtAmount.text = "" + infoFactory.info[id].amount + "/" + infoFactory.info[id].total;
            if (PlayerPrefs.GetInt("iconFactory" + id) == 0)
            {
                infoFactory.info[id].icon.sprite = ManagerData.instance.facetorys.Facetory[id].iconStore;
                PlayerPrefs.SetInt("iconFactory" + id, 1);
            }

            Experience.Instance.registerItemOpen(ManagerData.instance.facetorys.Facetory[id].iconStore);
        }

        public void buyTree(int id)
        {
            ManagerCoin.Instance.MunisGold(inforTree.info[id].goldPrice);
            inforTree.info[id].amount = inforTree.info[id].amount + 1;
            PlayerPrefs.SetInt("amountTree" + id, inforTree.info[id].amount);
            inforTree.info[id].txtAmount.text = "" + inforTree.info[id].amount + "/" + inforTree.info[id].total;
            if (inforTree.info[id].amount == inforTree.info[id].total)
            {
                inforTree.info[id].status = 0;
                PlayerPrefs.SetInt("statusTreeStore" + id, inforTree.info[id].status);
                inforTree.info[id].txtGoldPrice.gameObject.SetActive(false);
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    inforTree.info[id].txtInfo.text = "Mở khóa cấp " + (inforTree.info[id].levelOpen + 1);
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    inforTree.info[id].txtInfo.text = "Terbuka di level " + (inforTree.info[id].levelOpen + 1);
                else inforTree.info[id].txtInfo.text = "Unlock Level " + (inforTree.info[id].levelOpen + 1);
            }
        }

        void updateTree(int id)
        {
            if (inforTree.info[id].total == 0) ManagerItem.Instance.UpdateItem(3, id);
            inforTree.info[id].status = 1;
            PlayerPrefs.SetInt("statusTreeStore" + id, inforTree.info[id].status);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                inforTree.info[id].txtInfo.text = ManagerData.instance.trees.data[id].Tree.name;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                inforTree.info[id].txtInfo.text = ManagerData.instance.trees.data[id].Tree.nameINS;
            else inforTree.info[id].txtInfo.text = ManagerData.instance.trees.data[id].Tree.engName;
            inforTree.info[id].goldPrice =
                (int) inforTree.info[id].goldPrice * ManagerData.instance.trees.data[id].Tree.mutiGold;
            PlayerPrefs.SetInt("goldPriceTree" + id, inforTree.info[id].goldPrice);
            inforTree.info[id].txtGoldPrice.text = "" + inforTree.info[id].goldPrice;
            inforTree.info[id].txtGoldPrice.gameObject.SetActive(true);
            inforTree.info[id].levelOpen =
                inforTree.info[id].levelOpen + ManagerData.instance.trees.data[id].Tree.distanceLvOpen;
            PlayerPrefs.SetInt("levelOpenTree" + id, inforTree.info[id].levelOpen);
            inforTree.info[id].total = inforTree.info[id].total + ManagerData.instance.trees.data[id].Tree.quantityOpen;
            PlayerPrefs.SetInt("totalTree" + id, inforTree.info[id].total);
            inforTree.info[id].txtAmount.text = "" + inforTree.info[id].amount + "/" + inforTree.info[id].total;
            if (PlayerPrefs.GetInt("iconTree" + id) == 0)
            {
                inforTree.info[id].icon.sprite = ManagerData.instance.trees.data[id].Tree.iconStore;
                PlayerPrefs.SetInt("iconTree" + id, 1);
            }

            Experience.Instance.registerItemOpen(ManagerData.instance.trees.data[id].Tree.iconStore);
        }

        void updateItemFactory(int id)
        {
            infoItemFactory.info[id].status = 1;
            PlayerPrefs.SetInt("updateItemFactory" + id, 1);
            infoItemFactory.info[id].sprRenderer.sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].item;
            ManagerItem.Instance.UpdateItem(2, id);
            ManagerMarket.instance.ReciveItem(2, id, ManagerData.instance.facetoryItems.FacetoryItemDatas[id].donate,
                false);
        }

        void updateSeeds(int id)
        {
            infoSeeds.info[id].status = 1;
            PlayerPrefs.SetInt("updateSeeds" + id, 1);
            infoSeeds.info[id].quantity.SetActive(true);
            infoSeeds.info[id].sprRenderer.sprite = ManagerData.instance.seeds.SeedDatas[id].iconStore;
            ManagerMarket.instance.ReciveItem(0, id, ManagerData.instance.seeds.SeedDatas[id].quantityOpen, false);
            ManagerItem.Instance.UpdateItem(0, id);
            Experience.Instance.registerItemOpen(ManagerData.instance.seeds.SeedDatas[id].iconStore);
        }

        [Button]
        public void AddSeeds(int id)
        {
            infoSeeds.info[id].status = 1;
            PlayerPrefs.SetInt("updateSeeds" + id, 1);
            infoSeeds.info[id].quantity.SetActive(true);
            infoSeeds.info[id].sprRenderer.sprite = ManagerData.instance.seeds.SeedDatas[id].iconStore;
            ManagerMarket.instance.ReciveItem(0, id, ManagerData.instance.seeds.SeedDatas[id].quantityOpen, false);
            ManagerItem.Instance.UpdateItem(0, id);
            Experience.Instance.registerItemOpen(ManagerData.instance.seeds.SeedDatas[id].iconStore);
        }

        void updateFlowers(int id)
        {
            inforFlowers.info[id].status = 1;
            PlayerPrefs.SetInt("updateFlowers" + id, 1);
            inforFlowers.info[id].quantity.SetActive(true);
            inforFlowers.info[id].sprRenderer.sprite = ManagerData.instance.flowers.Data[id].detailFlower.iconStore;
            ManagerMarket.instance.ReciveItem(6, id, ManagerData.instance.flowers.Data[id].detailFlower.donate, false);
            Experience.Instance.registerItemOpen(ManagerData.instance.flowers.Data[id].detailFlower.iconStore);
        }

        void updateDecorate(int id)
        {
            inforDecorate.info[id].status = 1;
            PlayerPrefs.SetInt("statusDCRStore" + id, inforDecorate.info[id].status);
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                inforDecorate.info[id].txtInfo.text = ManagerData.instance.decorate.Datas[id].NameVNS;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                inforDecorate.info[id].txtInfo.text = ManagerData.instance.decorate.Datas[id].NameINS;
            else inforDecorate.info[id].txtInfo.text = ManagerData.instance.decorate.Datas[id].NameENG;
            inforDecorate.info[id].goldPrice = ManagerData.instance.decorate.Datas[id].Purchase;
            inforDecorate.info[id].txtGoldPrice.text = inforDecorate.info[id].goldPrice.ToString();
            inforDecorate.info[id].txtGoldPrice.gameObject.SetActive(true);
            inforDecorate.info[id].icon.sprite = ManagerData.instance.decorate.Datas[id].IconStore;
            Experience.Instance.registerItemOpen(inforDecorate.info[id].icon.sprite);
        }

        [Button]
        public void AddToolDecorate(int id, int amount)
        {
            ManagerMarket.instance.ReciveItem(5, id, amount, true);
        }
        
        [Button]
        public void AddSeeds(int id, int amount)
        {
            ManagerMarket.instance.ReciveItem(0, id, amount, true);
        }

        public void buyDecorate(int id)
        {
            ManagerCoin.Instance.MunisGold(inforDecorate.info[id].goldPrice);
            inforDecorate.info[id].amount += 1;
            PlayerPrefs.SetInt("amountDCRStore" + id, inforDecorate.info[id].amount);
        }

        public void checkupdate(int level)
        {
            for (int i = 0; i < infoField.info.Length; i++)
            {
                if (infoField.info[i].levelOpen <= level)
                {
                    updateField(i);
                    HaveItem(0);
                }
            }

            for (int i = 0; i < infoCage.info.Length; i++)
            {
                if (infoCage.info[i].levelOpen <= level)
                {
                    updateCage(i);
                    HaveItem(0);
                }
            }

            for (int i = 0; i < infoPet.info.Length; i++)
            {
                if (infoPet.info[i].levelOpen <= level)
                {
                    updatePet(i);
                    HaveItem(1);
                }
            }

            for (int i = 0; i < infoFactory.info.Length; i++)
            {
                if (infoFactory.info[i].levelOpen <= level)
                {
                    updateFactory(i);
                    HaveItem(2);
                }
            }

            for (int i = 0; i < inforTree.info.Length; i++)
            {
                if (inforTree.info[i].levelOpen == level)
                {
                    updateTree(i);
                    HaveItem(3);
                }
            }

            for (int i = 0; i < inforDecorate.info.Length; i++)
            {
                if (inforDecorate.info[i].status == 0)
                    if (ManagerData.instance.decorate.Datas[i].LevelUnlock <= level)
                    {
                        updateDecorate(i);
                        HaveItem(4);
                    }
            }

            for (int i = 0; i < ManagerData.instance.facetoryItems.FacetoryItemDatas.Length; i++)
            {
                if (infoItemFactory.info[i].status == 0)
                    if (ManagerData.instance.facetoryItems.FacetoryItemDatas[i].levelOpen <= level)
                        updateItemFactory(i);
            }

            for (int i = 0; i < ManagerData.instance.seeds.SeedDatas.Length; i++)
            {
                if (infoSeeds.info[i].status == 0)
                    if (ManagerData.instance.seeds.SeedDatas[i].levelOpen <= level)
                        updateSeeds(i);
            }

            for (int i = 0; i < ManagerData.instance.flowers.Data.Length; i++)
            {
                if (inforFlowers.info[i].status == 0)
                    if (ManagerData.instance.flowers.Data[i].detailFlower.levelOpen <= level)
                        updateFlowers(i);
            }
        }

        private void HaveItem(int Shop)
        {
            if (inforShops[Shop].status == 0)
            {
                inforShops[Shop].status = 1;
                PlayerPrefs.SetInt("StatusShop" + Shop, inforShops[Shop].status);
                inforShops[Shop].NewItem.SetActive(true);
            }

            if (blNewItem == false)
            {
                blNewItem = true;
                PlayerPrefs.SetInt("StatusButtonShop", 1);
                AniButtonShop.SetBool("Run", blNewItem);
            }
        }

        private void LoadData()
        {
            for (int i = 0; i < infoField.info.Length; i++)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoField.info[i].txtName.text = ManagerData.instance.lands.Land[i].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoField.info[i].txtName.text = ManagerData.instance.lands.Land[i].nameINS;
                else infoField.info[i].txtName.text = ManagerData.instance.lands.Land[i].engName;

                if (PlayerPrefs.HasKey("statusFieldStore" + i) == true)
                {
                    if (PlayerPrefs.GetInt("iconField" + i) == 1)
                        infoField.info[i].icon.sprite = ManagerData.instance.lands.Land[i].iconStore;
                    infoField.info[i].status = PlayerPrefs.GetInt("statusFieldStore" + i);
                    infoField.info[i].levelOpen = PlayerPrefs.GetInt("levelOpenField" + i);
                    infoField.info[i].goldPrice = PlayerPrefs.GetInt("goldPriceField" + i);
                    infoField.info[i].amount = PlayerPrefs.GetInt("amountField" + i);
                    infoField.info[i].total = PlayerPrefs.GetInt("totalField" + i);
                    infoField.info[i].txtAmount.text = infoField.info[i].amount + "/" + infoField.info[i].total;
                    if (infoField.info[i].status == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoField.info[i].txtInfo.text = "Mở khóa cấp " + (infoField.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoField.info[i].txtInfo.text = "Terbuka di level " + (infoField.info[i].levelOpen + 1);
                        else infoField.info[i].txtInfo.text = "Unlock Level " + (infoField.info[i].levelOpen + 1);
                        infoField.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }
                    else if (infoField.info[i].status == 1)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoField.info[i].txtInfo.text = "Đất trồng cây";
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoField.info[i].txtInfo.text = "Menumbuhkan Tanaman";
                        else infoField.info[i].txtInfo.text = "Grow Crops";
                        infoField.info[i].txtGoldPrice.text = "" + infoField.info[i].goldPrice;
                    }
                }
                else if (PlayerPrefs.HasKey("statusFieldStore" + i) == false)
                {
                    infoField.info[i].levelOpen = ManagerData.instance.lands.Land[i].levelOpen;

                    if (infoField.info[i].levelOpen == Experience.Instance.level)
                    {
                        infoField.info[i].status = 1;
                        infoField.info[i].levelOpen =
                            infoField.info[i].levelOpen + ManagerData.instance.lands.Land[i].distanceLvOpen;
                        infoField.info[i].total =
                            infoField.info[i].total + ManagerData.instance.lands.Land[i].amountOpen;
                        infoField.info[i].goldPrice =
                            infoField.info[i].goldPrice * ManagerData.instance.lands.Land[i].purchase;
                        infoField.info[i].txtGoldPrice.text = "" + infoField.info[i].goldPrice;
                        infoField.info[i].icon.sprite = ManagerData.instance.lands.Land[i].iconStore;
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoField.info[i].txtInfo.text = "Đất trồng cây";
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoField.info[i].txtInfo.text = "Menumbuhkan Tanaman";
                        else infoField.info[i].txtInfo.text = "Grow Crops";
                        PlayerPrefs.SetInt("iconField" + i, 1);
                    }
                    else if (infoField.info[i].levelOpen != Experience.Instance.level)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoField.info[i].txtInfo.text = "Mở khóa cấp " + (infoField.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoField.info[i].txtInfo.text = "Terbuka di level " + (infoField.info[i].levelOpen + 1);
                        else infoField.info[i].txtInfo.text = "Unlock Level " + (infoField.info[i].levelOpen + 1);
                        PlayerPrefs.SetInt("iconField" + i, 0);
                        infoField.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }

                    infoField.info[i].txtAmount.text = infoField.info[i].amount + "/" + infoField.info[i].total;
                    PlayerPrefs.SetInt("statusFieldStore" + i, infoField.info[i].status);
                    PlayerPrefs.SetInt("levelOpenField" + i, infoField.info[i].levelOpen);
                    PlayerPrefs.SetInt("goldPriceField" + i, infoField.info[i].goldPrice);
                    PlayerPrefs.SetInt("amountField" + i, infoField.info[i].amount);
                    PlayerPrefs.SetInt("totalField" + i, infoField.info[i].total);
                    PlayerPrefs.SetInt("purchaseField" + i, infoField.info[i].goldPrice);
                }
            }

            for (int i = 0; i < infoCage.info.Length; i++)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoCage.info[i].txtName.text = ManagerData.instance.cages.Cage[i].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoCage.info[i].txtName.text = ManagerData.instance.cages.Cage[i].nameINS;
                else infoCage.info[i].txtName.text = ManagerData.instance.cages.Cage[i].engName;

                if (PlayerPrefs.HasKey("statusCageStore" + i))
                {
                    if (PlayerPrefs.GetInt("iconCage" + i) == 1)
                        infoCage.info[i].icon.sprite = ManagerData.instance.cages.Cage[i].iconStore;
                    infoCage.info[i].status = PlayerPrefs.GetInt("statusCageStore" + i);
                    infoCage.info[i].levelOpen = PlayerPrefs.GetInt("levelOpenCage" + i);
                    infoCage.info[i].goldPrice = PlayerPrefs.GetInt("goldPriceCage" + i);
                    infoCage.info[i].amount = PlayerPrefs.GetInt("amountCage" + i);
                    infoCage.info[i].total = PlayerPrefs.GetInt("totalCage" + i);
                    infoCage.info[i].txtAmount.text = infoCage.info[i].amount + "/" + infoCage.info[i].total;
                    if (infoCage.info[i].status == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoCage.info[i].txtInfo.text = "Mở khóa cấp " + (infoCage.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoCage.info[i].txtInfo.text = "Terbuka di level " + (infoCage.info[i].levelOpen + 1);
                        else infoCage.info[i].txtInfo.text = "Unlock Level " + (infoCage.info[i].levelOpen + 1);
                        infoCage.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }
                    else if (infoCage.info[i].status == 1)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoCage.info[i].txtInfo.text = "Sức chứa " + ManagerData.instance.cages.Cage[i].amountPet +
                                                            " " + ManagerData.instance.petCollection.Pet[i].detailPet
                                                                .name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoCage.info[i].txtInfo.text =
                                "Menampung " + ManagerData.instance.cages.Cage[i].amountPet +
                                " " + ManagerData.instance.petCollection.Pet[i].detailPet
                                    .nameINS;
                        else
                            infoCage.info[i].txtInfo.text = "Capacity " + ManagerData.instance.cages.Cage[i].amountPet +
                                                            " " + ManagerData.instance.petCollection.Pet[i].detailPet
                                                                .engName;
                        infoCage.info[i].txtGoldPrice.text = "" + infoCage.info[i].goldPrice;
                    }
                }
                else
                {
                    infoCage.info[i].levelOpen = ManagerData.instance.cages.Cage[i].levelOpen;
                    if (infoCage.info[i].levelOpen == Experience.Instance.level)
                    {
                        infoCage.info[i].status = 1;
                        infoCage.info[i].levelOpen =
                            infoCage.info[i].levelOpen + ManagerData.instance.cages.Cage[i].distanceLvOpen;
                        infoCage.info[i].total = infoCage.info[i].total + ManagerData.instance.cages.Cage[i].amountOpen;
                        infoCage.info[i].goldPrice =
                            infoCage.info[i].goldPrice * ManagerData.instance.cages.Cage[i].purchase;
                        infoCage.info[i].txtGoldPrice.text = "" + infoCage.info[i].goldPrice;
                        infoCage.info[i].icon.sprite = ManagerData.instance.cages.Cage[i].iconStore;
                        PlayerPrefs.SetInt("iconCage" + i, 1);
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoCage.info[i].txtInfo.text = "Sức chứa " + ManagerData.instance.cages.Cage[i].amountPet +
                                                            " " + ManagerData.instance.petCollection.Pet[i].detailPet
                                                                .name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoCage.info[i].txtInfo.text =
                                "Menampung " + ManagerData.instance.cages.Cage[i].amountPet +
                                " " + ManagerData.instance.petCollection.Pet[i].detailPet
                                    .nameINS;
                        else
                            infoCage.info[i].txtInfo.text = "Capacity " + ManagerData.instance.cages.Cage[i].amountPet +
                                                            " " + ManagerData.instance.petCollection.Pet[i].detailPet
                                                                .engName;
                    }
                    else if (infoCage.info[i].levelOpen != Experience.Instance.level)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoCage.info[i].txtInfo.text = "Mở khóa cấp " + (infoCage.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoCage.info[i].txtInfo.text = "Terbuka di level " + (infoCage.info[i].levelOpen + 1);
                        else infoCage.info[i].txtInfo.text = "Unlock Level " + (infoCage.info[i].levelOpen + 1);
                        infoCage.info[i].status = 0;
                        PlayerPrefs.SetInt("iconCage" + i, 0);
                        infoCage.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }

                    PlayerPrefs.SetInt("statusCageStore" + i, infoCage.info[i].status);
                    PlayerPrefs.SetInt("levelOpenCage" + i, infoCage.info[i].levelOpen);
                    PlayerPrefs.SetInt("goldPriceCage" + i, infoCage.info[i].goldPrice);
                    PlayerPrefs.SetInt("amountCage" + i, infoCage.info[i].amount);
                    PlayerPrefs.SetInt("totalCage" + i, infoCage.info[i].total);
                    PlayerPrefs.SetInt("purchaseCage" + i, infoCage.info[i].goldPrice);

                    infoCage.info[i].txtAmount.text = infoCage.info[i].amount + "/" + infoCage.info[i].total;
                }
            }

            for (int i = 0; i < infoPet.info.Length; i++)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoPet.info[i].txtName.text = ManagerData.instance.petCollection.Pet[i].detailPet.name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoPet.info[i].txtName.text = ManagerData.instance.petCollection.Pet[i].detailPet.nameINS;
                else infoPet.info[i].txtName.text = ManagerData.instance.petCollection.Pet[i].detailPet.engName;
                if (PlayerPrefs.HasKey("statusPetStore" + i) == true)
                {
                    if (PlayerPrefs.GetInt("iconPet" + i) == 1)
                        infoPet.info[i].icon.sprite = ManagerData.instance.petCollection.Pet[i].detailPet.iconStore;
                    infoPet.info[i].status = PlayerPrefs.GetInt("statusPetStore" + i);
                    infoPet.info[i].levelOpen = PlayerPrefs.GetInt("levelOpenPet" + i);
                    infoPet.info[i].goldPrice = PlayerPrefs.GetInt("goldPricePet" + i);
                    infoPet.info[i].amount = PlayerPrefs.GetInt("amountPet" + i);
                    infoPet.info[i].total = PlayerPrefs.GetInt("totalPet" + i);
                    infoPet.info[i].txtAmount.text = infoPet.info[i].amount + "/" + infoPet.info[i].total;
                    if (infoPet.info[i].status == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoPet.info[i].txtInfo.text = "Mở khóa cấp " + (infoCage.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoPet.info[i].txtInfo.text = "Terbuka di level " + (infoCage.info[i].levelOpen + 1);
                        else infoPet.info[i].txtInfo.text = "Unlock Level " + (infoCage.info[i].levelOpen + 1);
                        infoPet.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }
                    else if (infoPet.info[i].status == 1)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoPet.info[i].txtInfo.text = "Thời gian thu hoạch " +
                                                           ManagerGame.Instance.TimeText(ManagerData.instance
                                                               .petCollection
                                                               .Pet[i].detailPet.time);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoPet.info[i].txtInfo.text = "Waktu panen" +
                                                           ManagerGame.Instance.TimeText(ManagerData.instance
                                                               .petCollection
                                                               .Pet[i].detailPet.time);
                        else
                            infoPet.info[i].txtInfo.text = "Harvest Time " +
                                                           ManagerGame.Instance.TimeText(ManagerData.instance
                                                               .petCollection
                                                               .Pet[i].detailPet.time);
                        infoPet.info[i].txtGoldPrice.text = "" + infoPet.info[i].goldPrice;
                    }
                }
                else if (PlayerPrefs.HasKey("statusPetStore" + i) == false)
                {
                    infoPet.info[i].levelOpen = ManagerData.instance.petCollection.Pet[i].detailPet.levelOpen;
                    PlayerPrefs.SetInt("purchasePet" + i, infoPet.info[i].goldPrice);
                    if (infoPet.info[i].levelOpen == 0)
                    {
                        if (infoPet.info[i].total == 0) ManagerItem.Instance.UpdateItem(1, i);
                        infoPet.info[i].status = 1;
                        infoPet.info[i].levelOpen = infoPet.info[i].levelOpen +
                                                    ManagerData.instance.petCollection.Pet[i].detailPet.distanceLvOpen;
                        infoPet.info[i].total = infoPet.info[i].total +
                                                ManagerData.instance.petCollection.Pet[i].detailPet.quantityOpen;
                        infoPet.info[i].goldPrice = infoPet.info[i].goldPrice *
                                                    ManagerData.instance.petCollection.Pet[i].detailPet.purchase;
                        infoPet.info[i].txtGoldPrice.text = "" + infoPet.info[i].goldPrice;
                        infoPet.info[i].icon.sprite = ManagerData.instance.petCollection.Pet[i].detailPet.iconStore;
                        PlayerPrefs.SetInt("iconPet" + i, 1);
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoPet.info[i].txtInfo.text = "Thời gian thu hoạch " +
                                                           ManagerGame.Instance.TimeText(ManagerData.instance
                                                               .petCollection
                                                               .Pet[i].detailPet.time);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoPet.info[i].txtInfo.text = "Waktu panen" +
                                                           ManagerGame.Instance.TimeText(ManagerData.instance
                                                               .petCollection
                                                               .Pet[i].detailPet.time);
                        else
                            infoPet.info[i].txtInfo.text = "Harvest Time " +
                                                           ManagerGame.Instance.TimeText(ManagerData.instance
                                                               .petCollection
                                                               .Pet[i].detailPet.time);
                    }
                    else if (infoPet.info[i].levelOpen != Experience.Instance.level)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoPet.info[i].txtInfo.text = "Mở khóa cấp " + (infoCage.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoPet.info[i].txtInfo.text = "Terbuka di level " + (infoCage.info[i].levelOpen + 1);
                        else infoPet.info[i].txtInfo.text = "Unlock Level " + (infoCage.info[i].levelOpen + 1);
                        PlayerPrefs.SetInt("iconPet" + i, 0);
                        infoPet.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }

                    infoPet.info[i].txtAmount.text = infoPet.info[i].amount + "/" + infoPet.info[i].total;
                    PlayerPrefs.SetInt("statusPetStore" + i, infoPet.info[i].status);
                    PlayerPrefs.SetInt("levelOpenPet" + i, infoPet.info[i].levelOpen);
                    PlayerPrefs.SetInt("goldPricePet" + i, infoPet.info[i].goldPrice);
                    PlayerPrefs.SetInt("amountPet" + i, infoPet.info[i].amount);
                    PlayerPrefs.SetInt("totalPet" + i, infoPet.info[i].total);
                }
            }

            for (int i = 0; i < infoFactory.info.Length; i++)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    infoFactory.info[i].txtName.text = ManagerData.instance.facetorys.Facetory[i].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    infoFactory.info[i].txtName.text = ManagerData.instance.facetorys.Facetory[i].nameINS;
                else infoFactory.info[i].txtName.text = ManagerData.instance.facetorys.Facetory[i].engName;

                if (PlayerPrefs.HasKey("statusFactoryStore" + i))
                {
                    if (PlayerPrefs.GetInt("iconFactory" + i) == 1)
                        infoFactory.info[i].icon.sprite = ManagerData.instance.facetorys.Facetory[i].iconStore;
                    infoFactory.info[i].status = PlayerPrefs.GetInt("statusFactoryStore" + i);
                    infoFactory.info[i].levelOpen = PlayerPrefs.GetInt("levelOpenFactory" + i);
                    infoFactory.info[i].goldPrice = PlayerPrefs.GetInt("goldPriceFactory" + i);
                    infoFactory.info[i].amount = PlayerPrefs.GetInt("amountFactory" + i);
                    infoFactory.info[i].total = PlayerPrefs.GetInt("totalFactory" + i);
                    infoFactory.info[i].txtAmount.text = infoFactory.info[i].amount + "/" + infoFactory.info[i].total;
                    if (infoFactory.info[i].status == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoFactory.info[i].txtInfo.text = "Mở khóa cấp " + (infoFactory.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoFactory.info[i].txtInfo.text =
                                "Terbuka di level " + (infoFactory.info[i].levelOpen + 1);
                        else infoFactory.info[i].txtInfo.text = "Unlock Level " + (infoFactory.info[i].levelOpen + 1);
                        infoFactory.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }
                    else if (infoFactory.info[i].status == 1)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoFactory.info[i].txtInfo.text = ManagerData.instance.facetorys.Facetory[i].name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoFactory.info[i].txtInfo.text = ManagerData.instance.facetorys.Facetory[i].nameINS;
                        else infoFactory.info[i].txtInfo.text = ManagerData.instance.facetorys.Facetory[i].engName;
                        infoFactory.info[i].txtGoldPrice.text = "" + infoFactory.info[i].goldPrice;
                    }
                }
                else
                {
                    infoFactory.info[i].levelOpen = ManagerData.instance.facetorys.Facetory[i].levelOpen;
                    if (infoFactory.info[i].levelOpen == Experience.Instance.level)
                    {
                        infoFactory.info[i].status = 1;
                        infoFactory.info[i].levelOpen = infoFactory.info[i].levelOpen +
                                                        ManagerData.instance.facetorys.Facetory[i].distanceLvOpen;
                        infoFactory.info[i].total = infoFactory.info[i].total +
                                                    ManagerData.instance.facetorys.Facetory[i].amountOpen;
                        infoFactory.info[i].goldPrice = infoFactory.info[i].goldPrice *
                                                        ManagerData.instance.facetorys.Facetory[i].purchase;
                        infoFactory.info[i].txtGoldPrice.text = "" + infoFactory.info[i].goldPrice;
                        infoFactory.info[i].icon.sprite = ManagerData.instance.facetorys.Facetory[i].iconStore;
                        PlayerPrefs.SetInt("iconFactory" + i, 1);
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoFactory.info[i].txtInfo.text = ManagerData.instance.facetorys.Facetory[i].name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoFactory.info[i].txtInfo.text = ManagerData.instance.facetorys.Facetory[i].nameINS;
                        else infoFactory.info[i].txtInfo.text = ManagerData.instance.facetorys.Facetory[i].engName;
                    }
                    else if (infoFactory.info[i].levelOpen != Experience.Instance.level)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            infoFactory.info[i].txtInfo.text = "Mở khóa cấp " + (infoFactory.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            infoFactory.info[i].txtInfo.text =
                                "Terbuka di level " + (infoFactory.info[i].levelOpen + 1);
                        else infoFactory.info[i].txtInfo.text = "Unlock Level " + (infoFactory.info[i].levelOpen + 1);
                        PlayerPrefs.SetInt("iconFactory" + i, 0);
                        infoFactory.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }

                    infoFactory.info[i].txtAmount.text = infoFactory.info[i].amount + "/" + infoFactory.info[i].total;
                    PlayerPrefs.SetInt("statusFactoryStore" + i, infoFactory.info[i].status);
                    PlayerPrefs.SetInt("levelOpenFactory" + i, infoFactory.info[i].levelOpen);
                    PlayerPrefs.SetInt("goldPriceFactory" + i, infoFactory.info[i].goldPrice);
                    PlayerPrefs.SetInt("amountFactory" + i, infoFactory.info[i].amount);
                    PlayerPrefs.SetInt("totalFactory" + i, infoFactory.info[i].total);
                }
            }

            // Tree--------------------------------------------
            for (int i = 0; i < inforTree.info.Length; i++)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    inforTree.info[i].txtName.text = ManagerData.instance.trees.data[i].Tree.name;
                else if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    inforTree.info[i].txtName.text = ManagerData.instance.trees.data[i].Tree.nameINS;
                else inforTree.info[i].txtName.text = ManagerData.instance.trees.data[i].Tree.engName;
                if (PlayerPrefs.HasKey("statusTreeStore" + i) == true)
                {
                    if (PlayerPrefs.GetInt("iconTree" + i) == 1)
                        inforTree.info[i].icon.sprite = ManagerData.instance.trees.data[i].Tree.iconStore;
                    inforTree.info[i].status = PlayerPrefs.GetInt("statusTreeStore" + i);
                    inforTree.info[i].levelOpen = PlayerPrefs.GetInt("levelOpenTree" + i);
                    inforTree.info[i].goldPrice = PlayerPrefs.GetInt("goldPriceTree" + i);
                    inforTree.info[i].amount = PlayerPrefs.GetInt("amountTree" + i);
                    inforTree.info[i].total = PlayerPrefs.GetInt("totalTree" + i);
                    inforTree.info[i].txtAmount.text = inforTree.info[i].amount + "/" + inforTree.info[i].total;
                    if (inforTree.info[i].status == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforTree.info[i].txtInfo.text = "Mở khóa cấp " + (inforTree.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforTree.info[i].txtInfo.text = "Terbuka di level " + (inforTree.info[i].levelOpen + 1);
                        else inforTree.info[i].txtInfo.text = "Unlock Level " + (inforTree.info[i].levelOpen + 1);
                        inforTree.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }
                    else if (inforTree.info[i].status == 1)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforTree.info[i].txtInfo.text = ManagerData.instance.trees.data[i].Tree.name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforTree.info[i].txtInfo.text = ManagerData.instance.trees.data[i].Tree.nameINS;
                        else inforTree.info[i].txtInfo.text = ManagerData.instance.trees.data[i].Tree.engName;
                        inforTree.info[i].txtGoldPrice.text = "" + inforTree.info[i].goldPrice;
                    }
                }
                else if (PlayerPrefs.HasKey("statusTreeStore" + i) == false)
                {
                    inforTree.info[i].levelOpen = ManagerData.instance.trees.data[i].Tree.levelOpen;
                    if (inforTree.info[i].levelOpen == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforTree.info[i].txtInfo.text = ManagerData.instance.trees.data[i].Tree.name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforTree.info[i].txtInfo.text = ManagerData.instance.trees.data[i].Tree.nameINS;
                        else inforTree.info[i].txtInfo.text = ManagerData.instance.trees.data[i].Tree.engName;
                        inforTree.info[i].status = 1;
                        inforTree.info[i].levelOpen = inforTree.info[i].levelOpen +
                                                      ManagerData.instance.trees.data[i].Tree.distanceLvOpen;
                        inforTree.info[i].total =
                            inforTree.info[i].total + ManagerData.instance.trees.data[i].Tree.quantityOpen;
                        inforTree.info[i].goldPrice =
                            inforTree.info[i].goldPrice * ManagerData.instance.trees.data[i].Tree.purchase;
                        inforTree.info[i].txtGoldPrice.text = "" + inforTree.info[i].goldPrice;
                        inforTree.info[i].icon.sprite = ManagerData.instance.trees.data[i].Tree.iconStore;
                        PlayerPrefs.SetInt("iconTree" + i, 1);
                        ManagerItem.Instance.UpdateItem(3, i);
                    }
                    else if (inforTree.info[i].levelOpen != 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforTree.info[i].txtInfo.text = "Mở khóa cấp " + (inforTree.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforTree.info[i].txtInfo.text = "Terbuka di level " + (inforTree.info[i].levelOpen + 1);
                        else inforTree.info[i].txtInfo.text = "Unlock Level " + (inforTree.info[i].levelOpen + 1);
                        PlayerPrefs.SetInt("iconTree" + i, 0);
                        inforTree.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }

                    inforTree.info[i].txtAmount.text = inforTree.info[i].amount + "/" + inforTree.info[i].total;
                    PlayerPrefs.SetInt("statusTreeStore" + i, inforTree.info[i].status);
                    PlayerPrefs.SetInt("levelOpenTree" + i, inforTree.info[i].levelOpen);
                    PlayerPrefs.SetInt("goldPriceTree" + i, inforTree.info[i].goldPrice);
                    PlayerPrefs.SetInt("amountTree" + i, inforTree.info[i].amount);
                    PlayerPrefs.SetInt("totalTree" + i, inforTree.info[i].total);
                    PlayerPrefs.SetInt("purchaseTree" + i, inforTree.info[i].goldPrice);
                }
            }

            // Decorate-------------------------------------------
            for (int i = 0; i < inforDecorate.info.Length; i++)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    inforDecorate.info[i].txtName.text = ManagerData.instance.decorate.Datas[i].NameVNS;
                else if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    inforDecorate.info[i].txtName.text = ManagerData.instance.decorate.Datas[i].NameINS;
                else inforDecorate.info[i].txtName.text = ManagerData.instance.decorate.Datas[i].NameENG;
                inforDecorate.info[i].txtAmount.text = "";
                inforDecorate.info[i].levelOpen = ManagerData.instance.decorate.Datas[i].LevelUnlock;
                if (PlayerPrefs.HasKey("statusDCRStore" + i) == false)
                {
                    if (inforDecorate.info[i].levelOpen == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforDecorate.info[i].txtInfo.text = ManagerData.instance.decorate.Datas[i].NameVNS;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforDecorate.info[i].txtInfo.text = ManagerData.instance.decorate.Datas[i].NameINS;
                        else inforDecorate.info[i].txtInfo.text = ManagerData.instance.decorate.Datas[i].NameENG;
                        inforDecorate.info[i].status = 1;
                        inforDecorate.info[i].goldPrice = ManagerData.instance.decorate.Datas[i].Purchase;
                        inforDecorate.info[i].txtGoldPrice.text = inforDecorate.info[i].goldPrice.ToString();
                        inforDecorate.info[i].icon.sprite = ManagerData.instance.decorate.Datas[i].IconStore;
                    }
                    else if (inforDecorate.info[i].levelOpen != 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforDecorate.info[i].txtInfo.text = "Mở khóa cấp " + (inforDecorate.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforDecorate.info[i].txtInfo.text =
                                "Terbuka di level " + (inforDecorate.info[i].levelOpen + 1);
                        else
                            inforDecorate.info[i].txtInfo.text =
                                "Unlock Level " + (inforDecorate.info[i].levelOpen + 1);
                        inforDecorate.info[i].status = 0;
                        inforDecorate.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }

                    PlayerPrefs.SetInt("statusDCRStore" + i, inforDecorate.info[i].status);
                    PlayerPrefs.SetInt("amountDCRStore" + i, inforDecorate.info[i].amount);
                }
                else if (PlayerPrefs.HasKey("statusDCRStore" + i) == true)
                {
                    inforDecorate.info[i].status = PlayerPrefs.GetInt("statusDCRStore" + i);
                    inforDecorate.info[i].amount = PlayerPrefs.GetInt("amountDCRStore" + i);
                    if (inforDecorate.info[i].status == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforDecorate.info[i].txtInfo.text = "Mở khóa cấp " + (inforDecorate.info[i].levelOpen + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforDecorate.info[i].txtInfo.text =
                                "Terbuka di level " + (inforDecorate.info[i].levelOpen + 1);
                        else
                            inforDecorate.info[i].txtInfo.text =
                                "Unlock Level " + (inforDecorate.info[i].levelOpen + 1);
                        inforDecorate.info[i].txtGoldPrice.gameObject.SetActive(false);
                    }
                    else if (inforDecorate.info[i].status == 1)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            inforDecorate.info[i].txtInfo.text = ManagerData.instance.decorate.Datas[i].NameVNS;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            inforDecorate.info[i].txtInfo.text = ManagerData.instance.decorate.Datas[i].NameINS;
                        else inforDecorate.info[i].txtInfo.text = ManagerData.instance.decorate.Datas[i].NameENG;
                        inforDecorate.info[i].goldPrice = ManagerData.instance.decorate.Datas[i].Purchase;
                        inforDecorate.info[i].txtGoldPrice.text = inforDecorate.info[i].goldPrice.ToString();
                        inforDecorate.info[i].icon.sprite = ManagerData.instance.decorate.Datas[i].IconStore;
                    }
                }
            }

            // Seed------------------------------------------------
            for (int i = 0; i < infoSeeds.info.Length; i++)
            {
                if (PlayerPrefs.HasKey("updateSeeds" + i))
                {
                    if (PlayerPrefs.GetInt("updateSeeds" + i) == 1)
                    {
                        infoSeeds.info[i].status = 1;
                        infoSeeds.info[i].sprRenderer.sprite = ManagerData.instance.seeds.SeedDatas[i].iconStore;
                        infoSeeds.info[i].quantity.SetActive(true);
                    }
                }
                else if (!PlayerPrefs.HasKey("updateSeeds" + i))
                {
                    if (ManagerData.instance.seeds.SeedDatas[i].levelOpen == Experience.Instance.level)
                    {
                        infoSeeds.info[i].status = 1;
                        PlayerPrefs.SetInt("updateSeeds" + i, infoSeeds.info[i].status);
                        infoSeeds.info[i].sprRenderer.sprite = ManagerData.instance.seeds.SeedDatas[i].iconStore;
                        infoSeeds.info[i].quantity.SetActive(true);
                        ManagerItem.Instance.UpdateItem(0, i);
                    }
                    else if (infoSeeds.info[i].status == 0) PlayerPrefs.SetInt("updateSeeds" + i, 0);
                }
            }

            for (int i = 0; i < inforFlowers.info.Length; i++)
            {
                if (PlayerPrefs.HasKey("updateFlowers" + i) == true)
                {
                    if (PlayerPrefs.GetInt("updateFlowers" + i) == 1)
                    {
                        inforFlowers.info[i].status = 1;
                        inforFlowers.info[i].sprRenderer.sprite =
                            ManagerData.instance.flowers.Data[i].detailFlower.iconStore;
                        inforFlowers.info[i].quantity.SetActive(true);
                    }
                }
                else if (PlayerPrefs.HasKey("updateFlowers" + i) == false)
                {
                    if (ManagerData.instance.flowers.Data[i].detailFlower.levelOpen == 0)
                    {
                        inforFlowers.info[i].status = 1;
                        PlayerPrefs.SetInt("updateFlowers" + i, inforFlowers.info[i].status);
                        inforFlowers.info[i].sprRenderer.sprite =
                            ManagerData.instance.flowers.Data[i].detailFlower.iconStore;
                        inforFlowers.info[i].quantity.SetActive(true);
                    }
                    else if (inforFlowers.info[i].status == 0) PlayerPrefs.SetInt("updateFlowers" + i, 0);
                }
            }

            for (int i = 0; i < infoItemFactory.info.Length; i++)
            {
                if (PlayerPrefs.HasKey("updateItemFactory" + i))
                {
                    if (PlayerPrefs.GetInt("updateItemFactory" + i) == 1)
                    {
                        infoItemFactory.info[i].status = PlayerPrefs.GetInt("updateItemFactory" + i);
                        infoItemFactory.info[i].sprRenderer.sprite =
                            ManagerData.instance.facetoryItems.FacetoryItemDatas[i].item;
                    }
                }
                else if (!PlayerPrefs.HasKey("updateItemFactory" + i))
                {
                    if (ManagerData.instance.facetoryItems.FacetoryItemDatas[i].levelOpen == Experience.Instance.level)
                    {
                        infoItemFactory.info[i].status = 1;
                        PlayerPrefs.SetInt("updateItemFactory" + i, infoItemFactory.info[i].status);
                        infoItemFactory.info[i].sprRenderer.sprite =
                            ManagerData.instance.facetoryItems.FacetoryItemDatas[i].item;
                        ManagerItem.Instance.UpdateItem(2, i);
                    }
                    else if (ManagerData.instance.facetoryItems.FacetoryItemDatas[i].levelOpen !=
                             Experience.Instance.level)
                    {
                        PlayerPrefs.SetInt("updateItemFactory" + i, 0);
                    }
                }
            }

            for (int i = 0; i < inforShops.Length; i++)
            {
                if (PlayerPrefs.HasKey("StatusShop" + i) == false)
                {
                    PlayerPrefs.SetInt("StatusShop" + i, inforShops[i].status);
                }
                else if (PlayerPrefs.HasKey("StatusShop" + i) == true)
                {
                    inforShops[i].status = PlayerPrefs.GetInt("StatusShop" + i);
                    if (inforShops[i].status == 1) inforShops[i].NewItem.SetActive(true);
                }
            }

            if (PlayerPrefs.HasKey("StatusButtonShop") == false)
            {
                PlayerPrefs.SetInt("StatusButtonShop", 0);
            }
            else if (PlayerPrefs.HasKey("StatusButtonShop") == true)
            {
                if (PlayerPrefs.GetInt("StatusButtonShop") == 1)
                {
                    blNewItem = true;
                    AniButtonShop.SetBool("Run", blNewItem);
                }
            }
        }

        [System.Serializable]
        private struct InforShop
        {
            public int status;
            public GameObject NewItem;
        }
    }
}