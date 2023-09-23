using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManagerMarket : MonoBehaviour
{
    public static ManagerMarket instance;

    private int couterItemSell;

    private int idDepot;
    private int idShelves;

    private int idItemSale;
    private int idTypeItemSale;
    private int QuantitySaleItem;
    private int GoldGotSaleItem;

    private bool clickItem;

    [HideInInspector] public int idAnimalFood;
    [HideInInspector] public int idItemFactoryAnimalUse;
    [HideInInspector] public int QuantityItemTower;
    [HideInInspector] public int QuantityTotalItemTower;
    [HideInInspector] public int QuantityItemDepot;
    [HideInInspector] public int QuantityTotalItemDepot;

    [Header("Warning")]
    [SerializeField] Text TitleWarningText;
    [SerializeField] Text NotificationWaringText;
    [SerializeField] Image NotificationWarningImage;
    [SerializeField] GameObject Warning;

    [Header("Market")]
    [SerializeField] Sprite DepotNormal;
    [SerializeField] Sprite DepotChoose;
    [SerializeField] Text NameMarketText;
    [SerializeField] Text NameDepotText;
    [SerializeField] Text SaleButtonText;
    [SerializeField] GameObject Market;
    [SerializeField] GameObject Store;
    [SerializeField] GameObject Depot;
    [SerializeField] GameObject HaveItem;
    [SerializeField] Image[] DepotImage;
    [SerializeField] GameObject[] TypeDepot;

    [Header("Sale")]
    [SerializeField] Sprite CoinSprite;
    [SerializeField] Image ItemSaleImage;
    [SerializeField] Text NameItemSaleText;
    [SerializeField] Text QuantityItemSaleText;
    [SerializeField] Text QuantityGoldGotSaleItemText;

    [Header("Shalves")]
    private int[] StatusShalves = new int[6];
    private int[] CoinShalves = new int[6];
    private int[] IdStypeShalves = new int[6];
    private int[] idItemShaleves = new int[6];
    private int[] QuantityItemShalves = new int[6];
    private int[] TimeSellShalves = new int[6];
    private IEnumerator[] IETimeShalves = new IEnumerator[6];
    [SerializeField] Text[] QuantityItemShalvesText;
    [SerializeField] Text[] PriceItemShalvesText;
    [SerializeField] Image[] ItemShalvesImage;
    [SerializeField] Image[] CoinImage;

    [Header("Seeds")]
    [SerializeField] int[] CropSeeds = new int[16];
    [SerializeField] GameObject[] SeedsCrop;
    [SerializeField] GameObject[] SeedsBuy;

    [Header("Flowers")]
    [SerializeField] int[] CropFlowers = new int[10];
    [SerializeField] GameObject[] FlowersCrop;
    [SerializeField] GameObject[] FlowersBuy;

    [Header("ObjItem")]
    [SerializeField] GameObject[] itemPet;
    [SerializeField] GameObject[] itemSeeds;
    [SerializeField] GameObject[] itemFactory;
    [SerializeField] GameObject[] itemOldTree;
    [SerializeField] GameObject[] itemItemBuilding;
    [SerializeField] GameObject[] itemToolDecorate;
    [SerializeField] GameObject[] itemFlower;

    [Header("TextItem")]
    [SerializeField] Text[] txtQuantityItemSeeds;
    [SerializeField] Text[] txtQuantitySeedsCrop;
    [SerializeField] Text[] txtQuantityItemPet;
    [SerializeField] Text[] txtQuantityItemFactory;
    [SerializeField] Text[] txtQuantityItemOldTree;
    [SerializeField] Text[] txtQuantityFoodAnimal;
    [SerializeField] Text[] txtQuantityItemBuilding;
    [SerializeField] Text[] txtQuantityToolDecorate;
    [SerializeField] Text[] txtQuantityToolDecorateWS;
    [SerializeField] Text[] txtQuantityFlowerCrop;
    [SerializeField] Text[] txtQuantityItemFlower;

    [Header("Quantity")]
    public int[] QuantityItemSeeds;
    public int[] QuantityItemFlower;
    public int[] QuantityItemPets;
    public int[] QuantityItemFactory;
    public int[] QuantityItemOldTree;
    public int[] QuantityItemBuilding;
    public int[] QuantityToolDecorate;
    //------------------------------------------
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        InitData();
        InitShelves();
        QuantityTotalItemTower = ManagerTower.instance.capacity;
        QuantityTotalItemDepot = ManagerDepot.instance.capacity;
        ManagerTower.instance.ShowNameTower();
        ManagerDepot.instance.ShowNameDepot();
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            NameMarketText.text = "Chợ";
            TitleWarningText.text = "Chú Ý!";
            SaleButtonText.text = "Đưa ra bán";
            NotificationWaringText.text = "Bạn sắp sử dụng hạt giống cuối cùng. Bạn có muốn tiếp tục?";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            NameMarketText.text = "Pasar";
            TitleWarningText.text = "Peringatan!";
            SaleButtonText.text = "Jual Obral";
            NotificationWaringText.text = "Kamu akan memakai hasil panen terakhir. Lanjutkan?";
        }
        else
        {
            NameMarketText.text = "Market";
            TitleWarningText.text = "Warning!";
            SaleButtonText.text = "Put on sale";
            NotificationWaringText.text = "Are you about to use your last crops. Do you want proceed?";
        }
    }

    public void UpgradeQuantityItemDepot()
    {
        QuantityTotalItemDepot = ManagerDepot.instance.capacity;
    }

    public void UpgradeQuantityItemTower()
    {
        QuantityTotalItemTower = ManagerTower.instance.capacity;
    }

    public void OpenMarket()
    {
        MainCamera.instance.DisableAll();
        MainCamera.instance.lockCam();
        if (idDepot == 0)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                NameDepotText.text = "Tháp nông sản " + QuantityItemTower + "/" + QuantityTotalItemTower;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                NameDepotText.text = "Penyimpanan Silo " + QuantityItemTower + "/" + QuantityTotalItemTower;
            else
                NameDepotText.text = "Tower Agricultural " + QuantityItemTower + "/" + QuantityTotalItemTower;
        }
        else if (idDepot == 1)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                NameDepotText.text = "Kho Lưu Trữ " + QuantityItemDepot + "/" + QuantityTotalItemDepot;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                NameDepotText.text = "Penyimpanan Lumbung " + QuantityItemDepot + "/" + QuantityTotalItemDepot;
            else
                NameDepotText.text = "Barn Storage " + QuantityItemDepot + "/" + QuantityTotalItemDepot;
        }
        Market.SetActive(true);
    }

    private void CloseMarket()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        MainCamera.instance.unLockCam();
        Market.SetActive(false);
    }

    private void CloseDepot()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        if (clickItem == true) clickItem = false;
        NameItemSaleText.text = "";
        QuantityGoldGotSaleItemText.text = "0";
        QuantityItemSaleText.text = "";
        if (ItemSaleImage.sprite != null) ItemSaleImage.sprite = null;
        if (ItemSaleImage.color == Color.white) ItemSaleImage.color = new Color(1f, 1f, 1f, 0f);
        Depot.SetActive(false);
        Store.SetActive(true);
    }

    public void ButtonCloseMarket()
    {
        ManagerAudio.instance.PlayAudio(Audio.ClickExit);
        CloseMarket();
    }

    public void ButtonShelves(int id)
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        switch (StatusShalves[id])
        {
            case 0:
                idShelves = id;
                Store.SetActive(false);
                Depot.SetActive(true);
                break;
            case 1:

                break;
            case 2:
                StatusShalves[id] = 0;
                PlayerPrefs.SetInt("StatusShalves" + id, StatusShalves[id]);
                GameObject obj = Instantiate(ManagerCoin.instance.goldFly, CoinImage[id].transform.position, Quaternion.identity);
                obj.GetComponent<GoldFly>().numberGold = CoinShalves[id];
                CoinImage[id].color = new Color(1f, 1f, 1f, 0f);
                PriceItemShalvesText[id].text = "";
                couterItemSell -= 1;
                if (couterItemSell == 0) HaveItem.SetActive(false);
                break;
        }

    }

    public void ButtonCloseDepot()
    {
        ManagerAudio.instance.PlayAudio(Audio.ClickExit);
        CloseDepot();
    }

    public void ButtonTypeDepot(int id)
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        if (idDepot != id)
        {
            clickItem = false;
            DepotImage[idDepot].sprite = DepotNormal;
            TypeDepot[idDepot].SetActive(false);
            idDepot = id;
            DepotImage[idDepot].sprite = DepotChoose;
            TypeDepot[idDepot].SetActive(true);
            NameItemSaleText.text = "";
            QuantityItemSaleText.text = "";
            QuantityGoldGotSaleItemText.text = "0";
            if (idDepot == 0)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameDepotText.text = "Tháp nông sản " + QuantityItemTower + "/" + QuantityTotalItemTower;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameDepotText.text = "Penyimpanan Silo " + QuantityItemTower + "/" + QuantityTotalItemTower;
                else NameDepotText.text = "Tower Agricultural " + QuantityItemTower + "/" + QuantityTotalItemTower;
            }
            else if (idDepot == 1)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameDepotText.text = "Kho Lưu Trữ " + QuantityItemDepot + "/" + QuantityTotalItemDepot;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameDepotText.text = "Penyimpanan Lumbung " + QuantityItemDepot + "/" + QuantityTotalItemDepot;
                else NameDepotText.text = "Barn Storage " + QuantityItemDepot + "/" + QuantityTotalItemDepot;
            }
            if (ItemSaleImage.color == Color.white) ItemSaleImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void ButtonChooseItemSale(int idStypeItem, int idItem)
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        idItemSale = idItem;
        idTypeItemSale = idStypeItem;
        if (clickItem == false) clickItem = true;
        if (ItemSaleImage.color != Color.white) ItemSaleImage.color = Color.white;
        switch (idTypeItemSale)
        {
            case 0:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.seeds.Seed[idItemSale].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.seeds.Seed[idItemSale].nameINS;
                else NameItemSaleText.text = ManagerData.instance.seeds.Seed[idItemSale].engName;
                ItemSaleImage.sprite = ManagerData.instance.seeds.Seed[idItemSale].item;
                if (QuantityItemSeeds[idItemSale] > 1) QuantitySaleItem = QuantityItemSeeds[idItemSale] / 2;
                else QuantitySaleItem = QuantityItemSeeds[idItemSale];
                ShowGoldGotSale();
                break;
            case 1:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.pets.Pet[idItemSale].itemPet.name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.pets.Pet[idItemSale].itemPet.nameINS;
                else NameItemSaleText.text = ManagerData.instance.pets.Pet[idItemSale].itemPet.engName;
                ItemSaleImage.sprite = ManagerData.instance.pets.Pet[idItemSale].itemPet.item;
                if (QuantityItemPets[idItemSale] > 1) QuantitySaleItem = QuantityItemPets[idItemSale] / 2;
                else QuantitySaleItem = QuantityItemPets[idItemSale];
                ShowGoldGotSale();
                break;
            case 2:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.facetoryItems.FacetoryItem[idItemSale].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.facetoryItems.FacetoryItem[idItemSale].nameINS;
                else NameItemSaleText.text = ManagerData.instance.facetoryItems.FacetoryItem[idItemSale].engName;
                ItemSaleImage.sprite = ManagerData.instance.facetoryItems.FacetoryItem[idItemSale].item;
                if (QuantityItemFactory[idItemSale] > 1) QuantitySaleItem = QuantityItemFactory[idItemSale] / 2;
                else QuantitySaleItem = QuantityItemFactory[idItemSale];
                ShowGoldGotSale();
                break;
            case 3:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.trees.data[idItemSale].ItemTree.name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.trees.data[idItemSale].ItemTree.nameINS;
                else NameItemSaleText.text = ManagerData.instance.trees.data[idItemSale].ItemTree.engName;
                ItemSaleImage.sprite = ManagerData.instance.trees.data[idItemSale].ItemTree.item;
                if (QuantityItemOldTree[idItemSale] > 1) QuantitySaleItem = QuantityItemOldTree[idItemSale] / 2;
                else QuantitySaleItem = QuantityItemOldTree[idItemSale];
                ShowGoldGotSale();
                break;
            case 4:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.itemBuilding.Data[idItemSale].NameItem;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.itemBuilding.Data[idItemSale].nameINS;
                else NameItemSaleText.text = ManagerData.instance.itemBuilding.Data[idItemSale].EngName;
                ItemSaleImage.sprite = ManagerData.instance.itemBuilding.Data[idItemSale].Icon;
                if (QuantityItemBuilding[idItemSale] > 1) QuantitySaleItem = QuantityItemBuilding[idItemSale] / 2;
                else QuantitySaleItem = QuantityItemBuilding[idItemSale];
                ShowGoldGotSale();
                break;
            case 5:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.toolDecorate.Data[idItemSale].NameItem;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.toolDecorate.Data[idItemSale].nameINS;
                else NameItemSaleText.text = ManagerData.instance.toolDecorate.Data[idItemSale].EngName;
                ItemSaleImage.sprite = ManagerData.instance.toolDecorate.Data[idItemSale].Icon;
                if (QuantityToolDecorate[idItemSale] > 1) QuantitySaleItem = QuantityToolDecorate[idItemSale] / 2;
                else QuantitySaleItem = QuantityToolDecorate[idItemSale];
                ShowGoldGotSale();
                break;
            case 6:
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    NameItemSaleText.text = ManagerData.instance.flowers.Data[idItemSale].DetailItemFlower.name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    NameItemSaleText.text = ManagerData.instance.flowers.Data[idItemSale].DetailItemFlower.nameINS;
                else NameItemSaleText.text = ManagerData.instance.flowers.Data[idItemSale].DetailItemFlower.engName;
                ItemSaleImage.sprite = ManagerData.instance.flowers.Data[idItemSale].DetailItemFlower.item;
                if (QuantityItemFlower[idItemSale] > 1) QuantitySaleItem = QuantityItemFlower[idItemSale] / 2;
                else QuantitySaleItem = QuantityItemFlower[idItemSale];
                ShowGoldGotSale();
                break;
        }
    }

    private void ShowGoldGotSale()
    {
        switch (idTypeItemSale)
        {
            case 0: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.seeds.Seed[idItemSale].sell; break;
            case 1: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.pets.Pet[idItemSale].itemPet.sell; break;
            case 2: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.facetoryItems.FacetoryItem[idItemSale].sell; break;
            case 3: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.trees.data[idItemSale].ItemTree.sell; break;
            case 4: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.itemBuilding.Data[idItemSale].Sell; break;
            case 5: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.toolDecorate.Data[idItemSale].Sell; break;
            case 6: GoldGotSaleItem = QuantitySaleItem * ManagerData.instance.flowers.Data[idItemSale].DetailItemFlower.Sell; break;
        }
        QuantityGoldGotSaleItemText.text = "" + GoldGotSaleItem;
        QuantityItemSaleText.text = "x" + QuantitySaleItem;
    }

    public void ButtonSale()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        if (clickItem == false)
        {
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Bạn chưa chọn vật phẩm!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Anda belum memilih item!";
            else str = "You aren't selected an item yet!";
            Notification.instance.dialogBelow(str);
        }
        else if (clickItem == true)
        {
            if (QuantitySaleItem > 0)
            {
                if (idTypeItemSale == 0)
                {
                    if (QuantitySaleItem == QuantityItemSeeds[idItemSale] && CropSeeds[idItemSale] == 0)
                        RegisterWaring();
                    else FinishSale();
                }
                else FinishSale();
            }
            else if (QuantitySaleItem <= 0)
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Số lượng vật phẩm phải lớn hơn 0 !";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Kuantitas barang harus lebih besar dari 0 !";
                else str = "Product number must be greater than 0 !";
                Notification.instance.dialogBelow(str);
            }
        }
    }

    public void RecieveCropSeeds(int id)
    {
        CropSeeds[id] += 1;
        PlayerPrefs.SetInt("CropSeeds" + id, CropSeeds[id]);
    }

    public void RecieveCropFlower(int id)
    {
        CropFlowers[id] += 1;
        PlayerPrefs.SetInt("CropFlowers" + id, CropFlowers[id]);
    }

    public void MinusCropsSeeds(int id)
    {
        CropSeeds[id] -= 1;
        PlayerPrefs.SetInt("CropSeeds" + id, CropSeeds[id]);
    }

    public void MinusCropsFlower(int id)
    {
        CropFlowers[id] -= 1;
        PlayerPrefs.SetInt("CropFlowers" + id, CropFlowers[id]);
    }

    public void BuySeeds(int id, Vector3 target)
    {
        ReciveItem(0, id, 1, true);
        ManagerTool.instance.RegisterItemSingle(1, ManagerData.instance.seeds.Seed[id].item, target);
        SeedsBuy[id].SetActive(false);
        SeedsCrop[id].SetActive(true);
        PlayerPrefs.SetInt("StatusBuySeeds" + id, 0);
    }

    private void FinishSale()
    {
        StatusShalves[idShelves] = 1;
        PlayerPrefs.SetInt("StatusShalves" + idShelves, 1);
        IdStypeShalves[idShelves] = idTypeItemSale;
        PlayerPrefs.SetInt("IdStypeShalves" + idShelves, IdStypeShalves[idShelves]);
        idItemShaleves[idShelves] = idItemSale;
        PlayerPrefs.SetInt("idItemShaleves" + idShelves, idItemShaleves[idShelves]);
        QuantityItemShalves[idShelves] = QuantitySaleItem;
        PlayerPrefs.SetInt("QuantityItemShalves" + idShelves, QuantityItemShalves[idShelves]);
        CoinShalves[idShelves] = GoldGotSaleItem;
        PlayerPrefs.SetInt("CoinShalves" + idShelves, CoinShalves[idShelves]);
        switch (idTypeItemSale)
        {
            case 0: ItemShalvesImage[idShelves].sprite = ManagerData.instance.seeds.Seed[idItemSale].item; break;
            case 1: ItemShalvesImage[idShelves].sprite = ManagerData.instance.pets.Pet[idItemSale].itemPet.item; break;
            case 2: ItemShalvesImage[idShelves].sprite = ManagerData.instance.facetoryItems.FacetoryItem[idItemSale].item; break;
            case 3: ItemShalvesImage[idShelves].sprite = ManagerData.instance.trees.data[idItemSale].ItemTree.item; break;
            case 4: ItemShalvesImage[idShelves].sprite = ManagerData.instance.itemBuilding.Data[idItemSale].Icon; break;
            case 5: ItemShalvesImage[idShelves].sprite = ManagerData.instance.toolDecorate.Data[idItemSale].Icon; break;
            case 6: ItemShalvesImage[idShelves].sprite = ManagerData.instance.flowers.Data[idItemSale].DetailItemFlower.item; break;
        }
        ItemShalvesImage[idShelves].color = Color.white;
        QuantityItemShalvesText[idShelves].text = "x" + QuantityItemShalves[idShelves];
        PriceItemShalvesText[idShelves].text = CoinShalves[idShelves].ToString();
        MinusItem(IdStypeShalves[idShelves], idItemShaleves[idShelves], QuantityItemShalves[idShelves]);
        int randomTime = Random.Range(60, 300);
        TimeSellShalves[idShelves] = randomTime;
        PlayerPrefs.SetInt("TimeSellShalves" + idShelves, TimeSellShalves[idShelves]);
        PlayerPrefs.SetInt("TimeLastShelves" + idShelves, ManagerGame.instance.RealTime());
        IETimeShalves[idShelves] = WaitSaleItem(idShelves);
        StartCoroutine(IETimeShalves[idShelves]);
        CloseDepot();
    }

    IEnumerator WaitSaleItem(int id)
    {
        yield return new WaitForSeconds(1f);
        TimeSellShalves[id] -= 1;
        PlayerPrefs.SetInt("TimeSellShalves" + id, TimeSellShalves[id]);
        PlayerPrefs.SetInt("TimeLastShelves" + id, ManagerGame.instance.RealTime());
        if (TimeSellShalves[id] > 0)
        {
            IETimeShalves[id] = WaitSaleItem(id);
            StartCoroutine(IETimeShalves[id]);
        }
        else if (TimeSellShalves[id] <= 0) SoldItem(id);
    }

    private void SoldItem(int id)
    {
        StatusShalves[id] = 2;
        ItemShalvesImage[id].sprite = null;
        ItemShalvesImage[id].color = new Color(1f, 1f, 1f, 0f);
        QuantityItemShalvesText[id].text = "";
        CoinImage[id].color = Color.white;
        couterItemSell += 1;
        if (HaveItem.activeSelf == false) HaveItem.SetActive(true);
    }

    public void ButtonPlus()
    {
        if (clickItem == true)
        {
            switch (idTypeItemSale)
            {
                case 0:
                    if (QuantitySaleItem < QuantityItemSeeds[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
                case 1:
                    if (QuantitySaleItem < QuantityItemPets[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
                case 2:
                    if (QuantitySaleItem < QuantityItemFactory[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
                case 3:
                    if (QuantitySaleItem < QuantityItemOldTree[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
                case 4:
                    if (QuantitySaleItem < QuantityItemBuilding[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
                case 5:
                    if (QuantitySaleItem < QuantityToolDecorate[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
                case 6:
                    if (QuantitySaleItem < QuantityItemFlower[idItemSale])
                    {
                        QuantitySaleItem += 1;
                        ShowGoldGotSale();
                    }
                    break;
            }
        }
    }

    public void ButtonMunis()
    {
        if (clickItem == true)
        {
            if (QuantitySaleItem > 0)
            {
                QuantitySaleItem -= 1;
                ShowGoldGotSale();
            }
        }
    }

    public void ReciveItem(int idstype, int iditem, int amount, bool show)
    {
        switch (idstype)
        {
            case 0:
                QuantityItemSeeds[iditem] += amount;
                PlayerPrefs.SetInt("QuantityItemSeeds" + iditem, QuantityItemSeeds[iditem]);
                QuantityItemTower += amount;
                ManagerTower.instance.ShowNameTower();
                if (show == true) Notification.instance.dialogTower();
                txtQuantityItemSeeds[iditem].text = "" + QuantityItemSeeds[iditem];
                txtQuantitySeedsCrop[iditem].text = "" + QuantityItemSeeds[iditem];
                ManagerTower.instance.ShowQuantity(idstype, iditem, QuantityItemSeeds[iditem]);
                if (QuantityItemSeeds[iditem] > 0 && itemSeeds[iditem].activeSelf == false) itemSeeds[iditem].SetActive(true);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 1:
                QuantityItemPets[iditem] += amount;
                PlayerPrefs.SetInt("QuantityItemPets" + iditem, QuantityItemPets[iditem]);
                QuantityItemDepot += amount;
                ManagerDepot.instance.ShowNameDepot();
                if (show == true) Notification.instance.dialogDepot();
                txtQuantityItemPet[iditem].text = "" + QuantityItemPets[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityItemPets[iditem]);
                if (QuantityItemPets[iditem] > 0 && itemPet[iditem].activeSelf == false) itemPet[iditem].SetActive(true);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 2:
                QuantityItemFactory[iditem] += amount;
                PlayerPrefs.SetInt("QuantityItemFactory" + iditem, QuantityItemFactory[iditem]);
                QuantityItemDepot += amount;
                ManagerDepot.instance.ShowNameDepot();
                if (show == true) Notification.instance.dialogDepot();
                txtQuantityItemFactory[iditem].text = "" + QuantityItemFactory[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityItemFactory[iditem]);
                if (QuantityItemFactory[iditem] > 0 && itemFactory[iditem].activeSelf == false) itemFactory[iditem].SetActive(true);
                CheckItemFeed(iditem);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 3:
                QuantityItemOldTree[iditem] += amount;
                PlayerPrefs.SetInt("QuantityItemOldTree" + iditem, QuantityItemOldTree[iditem]);
                QuantityItemTower += amount;
                ManagerTower.instance.ShowNameTower();
                if (show == true) Notification.instance.dialogTower();
                txtQuantityItemOldTree[iditem].text = "" + QuantityItemOldTree[iditem];
                ManagerTower.instance.ShowQuantity(idstype, iditem, QuantityItemOldTree[iditem]);
                if (QuantityItemOldTree[iditem] > 0 && itemOldTree[iditem].activeSelf == false) itemOldTree[iditem].SetActive(true);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 4:
                QuantityItemBuilding[iditem] += amount;
                PlayerPrefs.SetInt("QuantityItemBuilding" + iditem, QuantityItemBuilding[iditem]);
                QuantityItemDepot += amount;
                ManagerDepot.instance.ShowNameDepot();
                if (show == true) Notification.instance.dialogDepot();
                txtQuantityItemBuilding[iditem].text = "" + QuantityItemBuilding[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityItemBuilding[iditem]);
                if (QuantityItemBuilding[iditem] > 0 && itemItemBuilding[iditem].activeSelf == false) itemItemBuilding[iditem].SetActive(true);
                break;
            case 5:
                QuantityToolDecorate[iditem] += amount;
                PlayerPrefs.SetInt("QuantityToolDecorate" + iditem, QuantityToolDecorate[iditem]);
                QuantityItemDepot += amount;
                ManagerDepot.instance.ShowNameDepot();
                if (show == true) Notification.instance.dialogDepot();
                txtQuantityToolDecorate[iditem].text = "" + QuantityToolDecorate[iditem];
                txtQuantityToolDecorateWS[iditem].text = "" + QuantityToolDecorate[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityToolDecorate[iditem]);
                if (QuantityToolDecorate[iditem] > 0 && itemToolDecorate[iditem].activeSelf == false) itemToolDecorate[iditem].SetActive(true);
                break;
            case 6:
                QuantityItemFlower[iditem] += amount;
                PlayerPrefs.SetInt("QuantityItemFlowers" + iditem, QuantityItemFlower[iditem]);
                QuantityItemTower += amount;
                ManagerTower.instance.ShowNameTower();
                if (show == true) Notification.instance.dialogDepot();
                txtQuantityItemFlower[iditem].text = "" + QuantityItemFlower[iditem];
                txtQuantityFlowerCrop[iditem].text = "" + QuantityItemFlower[iditem];
                ManagerTower.instance.ShowQuantity(idstype, iditem, QuantityItemFlower[iditem]);
                if (QuantityItemFlower[iditem] > 0 && itemFlower[iditem].activeSelf == false) itemFlower[iditem].SetActive(true);
                break;
        }
    }

    public void MinusItem(int idstype, int iditem, int amount)
    {
        switch (idstype)
        {
            case 0:
                QuantityItemSeeds[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityItemSeeds" + iditem, QuantityItemSeeds[iditem]);
                QuantityItemTower -= amount;
                ManagerTower.instance.ShowNameTower();
                txtQuantityItemSeeds[iditem].text = "" + QuantityItemSeeds[iditem];
                txtQuantitySeedsCrop[iditem].text = "" + QuantityItemSeeds[iditem];
                ManagerTower.instance.ShowQuantity(idstype, iditem, QuantityItemSeeds[iditem]);
                if (QuantityItemSeeds[iditem] == 0 && itemSeeds[iditem].activeSelf == true) itemSeeds[iditem].SetActive(false);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 1:
                QuantityItemPets[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityItemPets" + iditem, QuantityItemPets[iditem]);
                QuantityItemDepot -= amount;
                ManagerDepot.instance.ShowNameDepot();
                txtQuantityItemPet[iditem].text = "" + QuantityItemPets[iditem];
                txtQuantitySeedsCrop[iditem].text = "" + QuantityItemSeeds[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityItemPets[iditem]);
                if (QuantityItemPets[iditem] == 0 && itemPet[iditem].activeSelf == true) itemPet[iditem].SetActive(false);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 2:
                QuantityItemFactory[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityItemFactory" + iditem, QuantityItemFactory[iditem]);
                QuantityItemDepot -= amount;
                ManagerDepot.instance.ShowNameDepot();
                txtQuantityItemFactory[iditem].text = "" + QuantityItemFactory[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityItemFactory[iditem]);
                if (QuantityItemFactory[iditem] == 0 && itemFactory[iditem].activeSelf == true) itemFactory[iditem].SetActive(false);
                CheckItemFeed(iditem);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 3:
                QuantityItemOldTree[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityItemOldTree" + iditem, QuantityItemOldTree[iditem]);
                QuantityItemTower -= amount;
                ManagerTower.instance.ShowNameTower();
                txtQuantityItemOldTree[iditem].text = "" + QuantityItemOldTree[iditem];
                ManagerTower.instance.ShowQuantity(idstype, iditem, QuantityItemOldTree[iditem]);
                if (QuantityItemOldTree[iditem] == 0 && itemOldTree[iditem].activeSelf == true) itemOldTree[iditem].SetActive(false);
                ManagerMission.instance.CheckDoneOrder();
                break;
            case 4:
                QuantityItemBuilding[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityItemBuilding" + iditem, QuantityItemBuilding[iditem]);
                QuantityItemDepot -= amount;
                ManagerDepot.instance.ShowNameDepot();
                txtQuantityItemBuilding[iditem].text = "" + QuantityItemBuilding[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityItemBuilding[iditem]);
                if (QuantityItemBuilding[iditem] == 0 && itemItemBuilding[iditem].activeSelf == true) itemItemBuilding[iditem].SetActive(false);
                break;
            case 5:
                QuantityToolDecorate[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityToolDecorate" + iditem, QuantityToolDecorate[iditem]);
                QuantityItemDepot -= amount;
                ManagerDepot.instance.ShowNameDepot();
                Notification.instance.dialogDepot();
                txtQuantityToolDecorate[iditem].text = "" + QuantityToolDecorate[iditem];
                txtQuantityToolDecorateWS[iditem].text = "" + QuantityToolDecorate[iditem];
                ManagerDepot.instance.ShowQuantity(idstype, iditem, QuantityToolDecorate[iditem]);
                if (QuantityToolDecorate[iditem] == 0 && itemToolDecorate[iditem].activeSelf == true) itemToolDecorate[iditem].SetActive(false);
                break;
            case 6:
                QuantityItemFlower[iditem] -= amount;
                PlayerPrefs.SetInt("QuantityItemFlowers" + iditem, QuantityItemFlower[iditem]);
                QuantityItemTower -= amount;
                ManagerTower.instance.ShowNameTower();
                Notification.instance.dialogDepot();
                txtQuantityItemFlower[iditem].text = "" + QuantityItemFlower[iditem];
                txtQuantityFlowerCrop[iditem].text = "" + QuantityItemFlower[iditem];
                ManagerTower.instance.ShowQuantity(idstype, iditem, QuantityItemFlower[iditem]);
                if (QuantityItemFlower[iditem] == 0 && itemFlower[iditem].activeSelf == true) itemFlower[iditem].SetActive(false);
                break;
        }
    }

    private void CheckItemFeed(int iditem)
    {
        switch (iditem)
        {
            case 3: txtQuantityFoodAnimal[0].text = QuantityItemFactory[iditem].ToString(); break;
            case 4: txtQuantityFoodAnimal[0].text = QuantityItemFactory[iditem].ToString(); break;
            case 5: txtQuantityFoodAnimal[1].text = QuantityItemFactory[iditem].ToString(); break;
            case 6: txtQuantityFoodAnimal[2].text = QuantityItemFactory[iditem].ToString(); break;
            case 53: txtQuantityFoodAnimal[3].text = QuantityItemFactory[iditem].ToString(); break;
            case 54: txtQuantityFoodAnimal[4].text = QuantityItemFactory[iditem].ToString(); break;
            case 55: txtQuantityFoodAnimal[5].text = QuantityItemFactory[iditem].ToString(); break;
        }
    }

    private void RegisterWaring()
    {
        NotificationWarningImage.sprite = IconItem(idTypeItemSale, idItemSale);
        Depot.SetActive(false);
        Warning.SetActive(true);
    }

    public void ButtonYesWarning()
    {
        Warning.SetActive(false);
        FinishSale();
        SeedsCrop[idItemSale].SetActive(false);
        SeedsBuy[idItemSale].SetActive(true);
        PlayerPrefs.SetInt("StatusBuySeeds" + idItemSale, 1);
    }

    public void ButtonNoWarning()
    {
        if (clickItem == true) clickItem = false;
        NameItemSaleText.text = "";
        QuantityItemSaleText.text = "";
        QuantityGoldGotSaleItemText.text = "0";
        if (ItemSaleImage.color == Color.white) ItemSaleImage.color = new Color(1f, 1f, 1f, 0f);
        Warning.SetActive(false);
        Depot.SetActive(true);
    }

    public int AmountItem(int idStype, int idItem)
    {
        int amount = 0;
        if (idStype == 0) amount = QuantityItemSeeds[idItem];
        else if (idStype == 1) amount = QuantityItemPets[idItem];
        else if (idStype == 2) amount = QuantityItemFactory[idItem];
        else if (idStype == 3) amount = QuantityItemOldTree[idItem];
        else if (idStype == 4) amount = QuantityItemBuilding[idItem];
        else if (idStype == 5) amount = QuantityToolDecorate[idItem];
        else if (idStype == 6) amount = QuantityItemFlower[idItem];
        return amount;
    }

    public Sprite IconItem(int stype, int id)
    {
        Sprite sprItem = null;
        switch (stype)
        {
            case 0: sprItem = ManagerData.instance.seeds.Seed[id].item; break;
            case 1: sprItem = ManagerData.instance.pets.Pet[id].itemPet.item; break;
            case 2: sprItem = ManagerData.instance.facetoryItems.FacetoryItem[id].item; break;
            case 3: sprItem = ManagerData.instance.trees.data[id].ItemTree.item; break;
            case 4: sprItem = ManagerData.instance.itemBuilding.Data[id].Icon; break;
            case 5: sprItem = ManagerData.instance.toolDecorate.Data[id].Icon; break;
            case 6: sprItem = ManagerData.instance.flowers.Data[id].DetailItemFlower.item; break;
        }
        return sprItem;
    }

    private void InitData()
    {
        for (int i = 0; i < QuantityItemSeeds.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityItemSeeds" + i) == true)
            {
                QuantityItemSeeds[i] = PlayerPrefs.GetInt("QuantityItemSeeds" + i);
                QuantityItemTower += QuantityItemSeeds[i];
                txtQuantityItemSeeds[i].text = "" + QuantityItemSeeds[i];
                txtQuantitySeedsCrop[i].text = "" + QuantityItemSeeds[i];
                ManagerTower.instance.ShowQuantity(0, i, QuantityItemSeeds[i]);
                if (QuantityItemSeeds[i] > 0) itemSeeds[i].SetActive(true);
                CropSeeds[i] = PlayerPrefs.GetInt("CropSeeds" + i);
                if (PlayerPrefs.GetInt("StatusBuySeeds" + i) == 1) { SeedsCrop[i].SetActive(false); SeedsBuy[i].SetActive(true); }
            }
            else if (PlayerPrefs.HasKey("QuantityItemSeeds" + i) == false)
            {
                QuantityItemSeeds[i] = ManagerData.instance.seeds.Seed[i].ValueStart;
                QuantityItemTower += QuantityItemSeeds[i];
                txtQuantityItemSeeds[i].text = "" + QuantityItemSeeds[i];
                txtQuantitySeedsCrop[i].text = "" + QuantityItemSeeds[i];
                ManagerTower.instance.ShowQuantity(0, i, QuantityItemSeeds[i]);
                if (QuantityItemSeeds[i] > 0) itemSeeds[i].SetActive(true);
                PlayerPrefs.SetInt("QuantityItemSeeds" + i, QuantityItemSeeds[i]);
                PlayerPrefs.SetInt("StatusBuySeeds" + i, 0);
                PlayerPrefs.SetInt("CropSeeds" + i, CropSeeds[i]);
            }
        }

        for (int i = 0; i < QuantityItemPets.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityItemPets" + i))
            {
                QuantityItemPets[i] = PlayerPrefs.GetInt("QuantityItemPets" + i);
                QuantityItemDepot += QuantityItemPets[i];
                txtQuantityItemPet[i].text = "" + QuantityItemPets[i];
                ManagerDepot.instance.ShowQuantity(1, i, QuantityItemPets[i]);
                if (QuantityItemPets[i] > 0) itemPet[i].SetActive(true);
            }
            else
            {
                QuantityItemDepot += QuantityItemPets[i];
                txtQuantityItemPet[i].text = "" + QuantityItemPets[i];
                ManagerDepot.instance.ShowQuantity(1, i, QuantityItemPets[i]);
                PlayerPrefs.SetInt("QuantityItemPets" + i, QuantityItemPets[i]);
            }
        }

        for (int i = 0; i < QuantityItemFactory.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityItemFactory" + i))
            {
                QuantityItemFactory[i] = PlayerPrefs.GetInt("QuantityItemFactory" + i);
                QuantityItemDepot += QuantityItemFactory[i];
                txtQuantityItemFactory[i].text = "" + QuantityItemFactory[i];
                ManagerDepot.instance.ShowQuantity(2, i, QuantityItemFactory[i]);
                if (QuantityItemFactory[i] > 0) itemFactory[i].SetActive(true);
                CheckItemFeed(i);
            }
            else
            {
                QuantityItemFactory[i] = ManagerData.instance.facetoryItems.FacetoryItem[i].valueStart;
                PlayerPrefs.SetInt("QuantityItemFactory" + i, QuantityItemFactory[i]);
                QuantityItemDepot += QuantityItemFactory[i];
                txtQuantityItemFactory[i].text = "" + QuantityItemFactory[i];
                ManagerDepot.instance.ShowQuantity(2, i, QuantityItemFactory[i]);
                if (QuantityItemFactory[i] > 0) itemFactory[i].SetActive(true);
                CheckItemFeed(i);
            }
        }

        for (int i = 0; i < QuantityItemOldTree.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityItemOldTree" + i) == true)
            {
                QuantityItemOldTree[i] = PlayerPrefs.GetInt("QuantityItemOldTree" + i);
                QuantityItemTower += QuantityItemOldTree[i];
                txtQuantityItemOldTree[i].text = "" + QuantityItemOldTree[i];
                ManagerTower.instance.ShowQuantity(3, i, QuantityItemOldTree[i]);
                if (QuantityItemOldTree[i] > 0) itemOldTree[i].SetActive(true);
            }
            else if (PlayerPrefs.HasKey("QuantityItemOldTree" + i) == false)
            {
                QuantityItemTower += QuantityItemOldTree[i];
                txtQuantityItemOldTree[i].text = "" + QuantityItemOldTree[i];
                ManagerTower.instance.ShowQuantity(3, i, QuantityItemOldTree[i]);
                PlayerPrefs.SetInt("QuantityItemOldTree" + i, QuantityItemOldTree[i]);
            }
        }

        for (int i = 0; i < QuantityItemBuilding.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityItemBuilding" + i))
            {
                QuantityItemBuilding[i] = PlayerPrefs.GetInt("QuantityItemBuilding" + i);
                QuantityItemDepot += QuantityItemBuilding[i];
                txtQuantityItemBuilding[i].text = "" + QuantityItemBuilding[i];
                ManagerDepot.instance.ShowQuantity(4, i, QuantityItemBuilding[i]);
                if (QuantityItemBuilding[i] > 0) itemItemBuilding[i].SetActive(true);
            }
            else
            {
                QuantityItemBuilding[i] = ManagerData.instance.itemBuilding.Data[i].ValueStart;
                QuantityItemDepot += QuantityItemBuilding[i];
                txtQuantityItemBuilding[i].text = "" + QuantityItemBuilding[i];
                ManagerDepot.instance.ShowQuantity(4, i, QuantityItemBuilding[i]);
                PlayerPrefs.SetInt("QuantityItemBuilding" + i, QuantityItemBuilding[i]);
            }
        }

        for (int i = 0; i < QuantityToolDecorate.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityToolDecorate" + i) == true)
            {
                QuantityToolDecorate[i] = PlayerPrefs.GetInt("QuantityToolDecorate" + i);
                QuantityItemDepot += QuantityToolDecorate[i];
                txtQuantityToolDecorate[i].text = "" + QuantityToolDecorate[i];
                txtQuantityToolDecorateWS[i].text = "" + QuantityToolDecorate[i];
                ManagerDepot.instance.ShowQuantity(5, i, QuantityToolDecorate[i]);
                if (QuantityToolDecorate[i] > 0) itemToolDecorate[i].SetActive(true);

            }
            else if (PlayerPrefs.HasKey("QuantityToolDecorate" + i) == false)
            {
                QuantityToolDecorate[i] = ManagerData.instance.toolDecorate.Data[i].ValueStart;
                QuantityItemDepot += QuantityToolDecorate[i];
                txtQuantityToolDecorate[i].text = "" + QuantityToolDecorate[i];
                txtQuantityToolDecorateWS[i].text = "" + QuantityToolDecorate[i];
                ManagerDepot.instance.ShowQuantity(5, i, QuantityToolDecorate[i]);
                PlayerPrefs.SetInt("QuantityToolDecorate" + i, QuantityToolDecorate[i]);
            }


        }
        for (int i = 0; i < QuantityItemFlower.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuantityItemFlowers" + i) == true)
            {
                QuantityItemFlower[i] = PlayerPrefs.GetInt("QuantityItemFlowers" + i);
                QuantityItemTower += QuantityItemFlower[i];
                txtQuantityItemFlower[i].text = "" + QuantityItemFlower[i];
                txtQuantityFlowerCrop[i].text = "" + QuantityItemFlower[i];
                ManagerTower.instance.ShowQuantity(6, i, QuantityItemFlower[i]);
                if (QuantityItemFlower[i] > 0) itemFlower[i].SetActive(true);

            }
            else if (PlayerPrefs.HasKey("QuantityItemFlowers" + i) == false)
            {
                QuantityItemFlower[i] = ManagerData.instance.flowers.Data[i].detailFlower.ValueStart;
                PlayerPrefs.SetInt("QuantityItemFlowers" + i, QuantityItemFlower[i]);
                QuantityItemTower += QuantityItemFlower[i];
                txtQuantityItemFlower[i].text = "" + QuantityItemFlower[i];
                txtQuantityFlowerCrop[i].text = "" + QuantityItemFlower[i];
                ManagerTower.instance.ShowQuantity(6, i, QuantityItemFlower[i]);
                if (QuantityItemFlower[i] > 0) itemFlower[i].SetActive(true);
            }
        }
    }

    private void InitShelves()
    {
        for (int i = 0; i < StatusShalves.Length; i++)
        {
            if (PlayerPrefs.HasKey("StatusShalves" + i) == false)
            {
                PlayerPrefs.SetInt("StatusShalves" + i, StatusShalves[i]);
                PlayerPrefs.SetInt("IdStypeShalves" + i, IdStypeShalves[i]);
                PlayerPrefs.SetInt("idItemShaleves" + i, idItemShaleves[i]);
                PlayerPrefs.SetInt("QuantityItemShalves" + i, QuantityItemShalves[i]);
            }
            else if (PlayerPrefs.HasKey("StatusShalves" + i) == true)
            {
                StatusShalves[i] = PlayerPrefs.GetInt("StatusShalves" + i);
                IdStypeShalves[i] = PlayerPrefs.GetInt("IdStypeShalves" + i);
                idItemShaleves[i] = PlayerPrefs.GetInt("idItemShaleves" + i);
                QuantityItemShalves[i] = PlayerPrefs.GetInt("QuantityItemShalves" + i);
                CoinShalves[i] = PlayerPrefs.GetInt("CoinShalves" + i);
                if (StatusShalves[i] == 1)
                {
                    ItemShalvesImage[i].sprite = IconItem(IdStypeShalves[i], idItemShaleves[i]);
                    ItemShalvesImage[i].color = Color.white;
                    QuantityItemShalvesText[i].text = "x" + QuantityItemShalves[i];
                    PriceItemShalvesText[i].text = "" + CoinShalves[i];

                    int timeNow = ManagerGame.instance.RealTime();
                    int time = timeNow - PlayerPrefs.GetInt("TimeLastShelves" + i);
                    TimeSellShalves[i] = PlayerPrefs.GetInt("TimeSellShalves" + i) - time;
                    IETimeShalves[i] = WaitSaleItem(i);
                    StartCoroutine(IETimeShalves[i]);
                }
                else if (StatusShalves[i] == 2)
                {
                    ItemShalvesImage[i].sprite = null;
                    ItemShalvesImage[i].color = new Color(1f, 1f, 1f, 0f);
                    QuantityItemShalvesText[i].text = "";
                    CoinImage[i].color = Color.white;
                    couterItemSell += 1;
                    if (HaveItem.activeSelf == false) HaveItem.SetActive(true);
                }
            }
        }
    }
}
