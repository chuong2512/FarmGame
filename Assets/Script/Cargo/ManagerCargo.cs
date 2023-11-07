using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NongTrai;

public class ManagerCargo : MonoBehaviour
{
    public static ManagerCargo instance = null;
    private int status;
    private int idContainer;
    private int idStypeContainer;
    private int NumberBought;
    private int NumberBuy = 2; // tính theo số container
    private bool moveSell = false;
    private bool moveWharf = false;

    private int[] stypeItem = new int[3];
    private int[] idItem = new int[3];
    private int[] amountItemRequire = new int[3];
    private int[] coinReceive = new int[3];
    private int[] expReceive = new int[3];
    private int[] minAmountRequire = new int[4];
    private int[] maxAmountRequire = new int[4];

    private IEnumerator IEWaitSell;
    private IEnumerator IEItemScale;
    private IEnumerator IEShipLeave;
    private IEnumerator IENewShip;

    [SerializeField] float SpeedShip;
    [SerializeField] int TimeNewShip;
    [SerializeField] int TimeLeaveShip;
    [SerializeField] int TimeWaitSell;
    [SerializeField] int LevelUnlock;

    [SerializeField] Text TitleShipText;
    [SerializeField] Text TitleShipLeaveText;
    [SerializeField] Text TimeShipLeaveText;
    [SerializeField] GameObject Ship;
    [SerializeField] GameObject Wave;
    [SerializeField] GameObject CargoLoad;
    [SerializeField] GameObject objCargoGetItem;

    [Header("Possition Ship")]
    [SerializeField] Transform StartPoint;
    [SerializeField] Transform WharfPoint;
    [SerializeField] Transform SellPoint;

    [Header("Item Buy")]
    [SerializeField] StypeContainer[] stypeContainers;

    [Header("Detail Container")]
    [SerializeField] Text CoinText;
    [SerializeField] Text ExpText;
    [SerializeField] Text QuantityItemtext;
    [SerializeField] Image ItemImage;
    [SerializeField] GameObject Item;
    [SerializeField] GameObject DetailItem;
    [SerializeField] Sprite[] ContainerSprite;

    [Header("Wait Sell Item")]
    [SerializeField] Text TitleWaitingText;
    [SerializeField] Text IntroductionText;
    [SerializeField] Text TimeWaitSellText;
    [SerializeField] GameObject WaitSell;

    [Header("Wait New Broad")]
    [SerializeField] Text TitleWaitNewBroadText;
    [SerializeField] Text TimeNewBroadText;
    [SerializeField] Text ButtonCallShipNowText;
    [SerializeField] GameObject ButtonNewShip;
    [SerializeField] GameObject WaitNewBroad;
    [SerializeField] Image[] ItemNewBroadImage;

    private void Awake()
    {
        minAmountRequire[0] = 10; maxAmountRequire[0] = 20;
        minAmountRequire[1] = 10; maxAmountRequire[1] = 20;
        minAmountRequire[2] = 5; maxAmountRequire[2] = 10;
        minAmountRequire[3] = 5; maxAmountRequire[3] = 10;
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        InitData();
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            TitleShipText.text = "Tàu chở hàng";
            TitleShipLeaveText.text = "Tàu rời trong: ";
            TitleWaitingText.text = "Vui lòng chờ...!";
            TitleWaitNewBroadText.text = "Tàu đến trong:";
            IntroductionText.text = "Bạn có thể tạo thuyển mới trong:";
            ButtonCallShipNowText.text = "Nhận Tàu Ngay";
        }
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
        {
            TitleShipText.text = "Angkut Muatan";
            TitleShipLeaveText.text = "Perahu berangkat dalam: ";
            TitleWaitingText.text = "Waitting...!";
            TitleWaitNewBroadText.text = "Boat arriving in:";
            IntroductionText.text = "Perharu tiba dalam:";
            ButtonCallShipNowText.text = "Dapatkan perharu sekarang";
        }
        else
        {
            TitleShipText.text = "Cargo";
            TitleShipLeaveText.text = "Ship leave in: ";
            TitleWaitingText.text = "Waitting...!";
            TitleWaitNewBroadText.text = "Boat arriving in:";
            IntroductionText.text = "You can get new ship in:";
            ButtonCallShipNowText.text = "Call Ship Now";
        }
    }

    public void OpenLoadCargo()
    {
        switch (status)
        {
            case 0:
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Bến và tàu được mở ở cấp " + (LevelUnlock + 1);
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Dok dan papan skor perahu terbuka di level " + (LevelUnlock + 1);
                else str = "Dock and Ship open at level open " + (LevelUnlock + 1);
                Notification.instance.dialogBelow(str);
                break;
            case 1:
                MainCamera.instance.DisableAll();
                MainCamera.instance.lockCam();
                CargoLoad.SetActive(true);
                break;
            case 2:
                MainCamera.instance.DisableAll();
                MainCamera.instance.lockCam();
                WaitSell.SetActive(true);
                break;
            case 3:
                MainCamera.instance.DisableAll();
                MainCamera.instance.lockCam();
                WaitNewBroad.SetActive(true);
                break;
        }
    }

    public void CloseCargo()
    {
        MainCamera.instance.unLockCam();
        CargoLoad.SetActive(false);
    }

    public void ButtonShip()
    {
        if (NumberBought == NumberBuy * 3)
        {
            status = 2;
            PlayerPrefs.SetInt("StatusShip", status);
            Wave.SetActive(true);
            moveSell = true;
            ResetContainer();
            CargoLoad.SetActive(false);
            StopCoroutine(IEShipLeave);
            IEWaitSell = IETimeLiveWaitSellItem(TimeWaitSell);
            StartCoroutine(IEWaitSell);
        }
        else if (NumberBought < NumberBuy * 3)
        {
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Tàu chưa thể khởi hành vì chưa đủ số lượng vật phẩm";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Kapal tidak dapat berangkat karena kuantitas produk tidak sesuai dengan persyaratan";
            else str = "The Ship can't depart due to the quantity of products does't match the requirements";
            Notification.instance.dialogBelow(str);
        }
    }

    private IEnumerator IETimeLiveShipLeave(int time)
    {
        yield return new WaitForSeconds(1f);
        time -= 1;
        PlayerPrefs.SetInt("TimeLiveShipLeave", time);
        PlayerPrefs.SetInt("TimeLastShipLeave", ManagerGame.instance.RealTime());
        TimeShipLeaveText.text = ManagerGame.instance.TimeText(time);
        if (time > 0)
        {
            IEShipLeave = IETimeLiveShipLeave(time);
            StartCoroutine(IEShipLeave);
        }
        else if (time <= 0)
        {
            status = 2;
            PlayerPrefs.SetInt("StatusShip", status);
            Wave.SetActive(true);
            moveSell = true;
            ResetContainer();
            CargoLoad.SetActive(false);
            TimeWaitSellText.text = ManagerGame.instance.TimeText(TimeWaitSell);
            IEWaitSell = IETimeLiveWaitSellItem(TimeWaitSell);
            StartCoroutine(IEWaitSell);
            if (CargoLoad.activeSelf == true)
            {
                CargoLoad.SetActive(false);
                WaitSell.SetActive(true);
            }
        }
    }

    private IEnumerator IETimeLiveWaitSellItem(int time)
    {
        yield return new WaitForSeconds(1);
        time -= 1;
        PlayerPrefs.SetInt("TimeLiveWaitSellItem", time);
        PlayerPrefs.SetInt("TimelastWaitSellItem", ManagerGame.instance.RealTime());
        TimeWaitSellText.text = ManagerGame.instance.TimeText(time);
        if (time > 0)
        {
            IEWaitSell = IETimeLiveWaitSellItem(time);
            StartCoroutine(IEWaitSell);
        }
        else if (time <= 0)
        {
            status = 3;
            PlayerPrefs.SetInt("StatusShip", status);
            ItemBuy();
            ItemNewBroad();
            ShowContainer();
            TimeNewBroadText.text = ManagerGame.instance.TimeText(TimeNewShip);
            IENewShip = IETimeLiveNewShip(TimeNewShip);
            StartCoroutine(IENewShip);
            if (ButtonNewShip.activeSelf == false) ButtonNewShip.SetActive(true);
            if (WaitSell.activeSelf == true)
            {
                WaitSell.SetActive(false);
                WaitNewBroad.SetActive(true);
            }
        }
    }

    public void ButtonConfirmWaitSell()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        WaitSell.SetActive(false);
    }

    private void ItemNewBroad()
    {
        for (int i = 0; i < ItemNewBroadImage.Length; i++)
        {
            ItemNewBroadImage[i].sprite = SpriteItem(stypeItem[i], idItem[i]);
        }
    }

    private IEnumerator IETimeLiveNewShip(int time)
    {
        yield return new WaitForSeconds(1f);
        time -= 1;
        PlayerPrefs.SetInt("TimeLiveNewShip", time);
        PlayerPrefs.SetInt("TimeLastNewShip", ManagerGame.instance.RealTime());
        TimeNewBroadText.text = ManagerGame.instance.TimeText(time);
        if (time > 0)
        {
            IENewShip = IETimeLiveNewShip(time);
            StartCoroutine(IENewShip);
        }
        else if (time <= 0)
        {
            moveWharf = true;
            Wave.SetActive(true);
            ButtonNewShip.SetActive(false);
            TimeNewBroadText.text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Tàu đang đến!" : "Ship is coming!";
        }
    }

    public void ButtonCallShipNow()
    {
        moveWharf = true;
        Wave.SetActive(true);
        StopCoroutine(IENewShip);
        PlayerPrefs.SetInt("TimeLiveNewShip", 0);
        ManagerGem.instance.MunisGem(5);
        ButtonNewShip.SetActive(false);
        TimeNewBroadText.text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Tàu đang đến!" : "Ship is coming!";
        WaitNewBroad.SetActive(false);
    }

    public void ButtonCloseWaitNewShip()
    {
        ManagerAudio.instance.PlayAudio(Audio.Click);
        WaitNewBroad.SetActive(false);
    }

    public void ButtonBuyItem()
    {
        if (stypeContainers[idStypeContainer].detailContainers[idContainer].status == 0)
        {
            if (AmountMarket(stypeItem[idStypeContainer], idItem[idStypeContainer]) >= amountItemRequire[idStypeContainer])
            {
                NumberBought += 1;
                PlayerPrefs.SetInt("NumberBoughtShip", NumberBought);
                stypeContainers[idStypeContainer].detailContainers[idContainer].status = 1;
                PlayerPrefs.SetInt("StatusContainer" + idStypeContainer + "" + idContainer, stypeContainers[idStypeContainer].detailContainers[idContainer].status);
                ManagerMarket.instance.MinusItem(stypeItem[idStypeContainer], idItem[idStypeContainer], amountItemRequire[idStypeContainer]);
                Sprite spr = SpriteItem(stypeItem[idStypeContainer], idItem[idStypeContainer]);
                Vector3 target = stypeContainers[idStypeContainer].detailContainers[idContainer].Container.transform.position;
                target.z = 0;
                stypeContainers[idStypeContainer].detailContainers[idContainer].Item.SetActive(false);
                stypeContainers[idStypeContainer].detailContainers[idContainer].Lid.SetActive(true);
                DetailItem.SetActive(false);
                StartCoroutine(Crating(idStypeContainer, idContainer, amountItemRequire[idStypeContainer], spr, target, coinReceive[idStypeContainer], expReceive[idStypeContainer]));
            }
            else if (AmountMarket(stypeItem[idStypeContainer], idItem[idStypeContainer]) < amountItemRequire[idStypeContainer])
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Bạn không đủ vật phẩm!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Anda tidak cukup barang!";
                else str = "You aren't enough item!";
                Notification.instance.dialogBelow(str);
            }
        }
    }

    private IEnumerator Crating(int idstype, int idcontainer, int quantityItem, Sprite spr, Vector3 target, int coin, int exp)
    {
        yield return new WaitForSeconds(1f);
        stypeContainers[idstype].detailContainers[idcontainer].containerImage.sprite = ContainerSprite[1];
        stypeContainers[idstype].detailContainers[idcontainer].Lid.SetActive(false);
        RegisterCargoGetItem(quantityItem, spr, target);
        yield return new WaitForSeconds(1f);
        ManagerTool.instance.RegisterExperienceCoin(coin, exp, target);
    }

    public void ClickContainer(int idstype, int idcontainer)
    {
        idStypeContainer = idstype;
        idContainer = idcontainer;
        if (stypeContainers[idStypeContainer].detailContainers[idContainer].status == 0)
        {
            ShowDetailItem();
        }
        else if (stypeContainers[idStypeContainer].detailContainers[idContainer].status == 1)
        {
            DetailItem.SetActive(false);
        }
    }

    private void ItemBuy()
    {
        for (int i = 0; i < stypeItem.Length; i++)
        {
            int randomStype = Random.Range(0, ManagerItem.instance.idItemUnlock.Length);
            int randomIdItem = Random.Range(0, ManagerItem.instance.totalItem[randomStype]);
            while (CheckSame(i, randomStype, randomIdItem) == true)
            {
                randomStype = Random.Range(0, ManagerItem.instance.idItemUnlock.Length);
                randomIdItem = Random.Range(0, ManagerItem.instance.totalItem[randomStype]);
            }
            stypeItem[i] = randomStype;
            PlayerPrefs.SetInt("StypeItemShip" + i, stypeItem[i]);
            idItem[i] = ManagerItem.instance.idItemUnlock[stypeItem[i]].IdItem[randomIdItem];
            PlayerPrefs.SetInt("IdItemShip" + i, idItem[i]);
            amountItemRequire[i] = Random.Range(minAmountRequire[i], maxAmountRequire[i]);
            PlayerPrefs.SetInt("AmountItemRequire" + i, amountItemRequire[i]);
            coinReceive[i] = CalculationCoin(stypeItem[i], idItem[i], amountItemRequire[i]);
            expReceive[i] = CalculationExp(stypeItem[i], idItem[i], amountItemRequire[i]);
        }
    }

    private bool CheckSame(int target, int stype, int id)
    {
        bool condition = false;
        for (int i = 0; i < target; i++)
        {
            if (stypeItem[i] == stype && idItem[i] == id) condition = true;
        }
        return condition;
    }

    private void ShowContainer()
    {
        for (int i = 0; i < stypeContainers.Length; i++)
        {
            for (int j = 0; j < stypeContainers[i].detailContainers.Length; j++)
            {
                if (j < NumberBuy)
                {
                    stypeContainers[i].detailContainers[j].amountText.text = "" + amountItemRequire[i];
                    stypeContainers[i].detailContainers[j].itemImage.sprite = SpriteItem(stypeItem[i], idItem[i]);
                    stypeContainers[i].detailContainers[j].containerImage.sprite = ContainerSprite[0];
                    if (stypeContainers[i].detailContainers[j].Container.activeSelf == false) stypeContainers[i].detailContainers[j].Container.SetActive(true);
                }
                else if (j >= NumberBuy)
                {
                    stypeContainers[i].detailContainers[j].amountText.text = "";
                    stypeContainers[i].detailContainers[j].itemImage.sprite = null;
                    stypeContainers[i].detailContainers[j].containerImage.sprite = ContainerSprite[0];
                    if (stypeContainers[i].detailContainers[j].Container.activeSelf == true) stypeContainers[i].detailContainers[j].Container.SetActive(false);
                }
            }
        }
    }

    private void ResetContainer()
    {
        for (int i = 0; i < stypeContainers.Length; i++)
        {
            for (int j = 0; j < stypeContainers[i].detailContainers.Length; j++)
            {
                if (stypeContainers[i].detailContainers[j].status == 1)
                {
                    stypeContainers[i].detailContainers[j].status = 0;
                    stypeContainers[i].detailContainers[j].amountText.text = "";
                    if (stypeContainers[i].detailContainers[j].Item.activeSelf == false) stypeContainers[i].detailContainers[j].Item.SetActive(true);
                    stypeContainers[i].detailContainers[j].itemImage.sprite = null;
                    if (stypeContainers[i].detailContainers[j].Container.activeSelf == true) stypeContainers[i].detailContainers[j].Container.SetActive(false);
                }
            }
        }
    }

    private void ShowDetailItem()
    {
        ItemImage.sprite = SpriteItem(stypeItem[idStypeContainer], idItem[idStypeContainer]);
        QuantityItemtext.text = AmountMarket(stypeItem[idStypeContainer], idItem[idStypeContainer]) + "/" + amountItemRequire[idStypeContainer];
        CoinText.text = "" + coinReceive[idStypeContainer];
        ExpText.text = "" + expReceive[idStypeContainer];
        if (DetailItem.activeSelf == false) DetailItem.SetActive(true);
        if (IEItemScale == null)
        {
            IEItemScale = VibrateItem();
            StartCoroutine(IEItemScale);
        }
    }

    private void RegisterCargoGetItem(int quantity, Sprite spr, Vector3 target)
    {
        GameObject objItem = Instantiate(objCargoGetItem, target, Quaternion.identity);
        CargoGetItem cgi = objItem.GetComponent<CargoGetItem>();
        cgi.ItemImage.sprite = spr;
        cgi.QuantityItemText.text = "-" + quantity;
    }

    private Sprite SpriteItem(int idstype, int idYC)
    {
        Sprite spr = null;
        switch (idstype)
        {
            case 0: spr = ManagerData.instance.seeds.SeedDatas[idYC].item; break;
            case 1: spr = ManagerData.instance.petCollection.Pet[idYC].itemPet.item; break;
            case 2: spr = ManagerData.instance.facetoryItems.FacetoryItemDatas[idYC].item; break;
            case 3: spr = ManagerData.instance.trees.data[idYC].ItemTree.item; break;
        }
        return spr;
    }

    private int AmountMarket(int idstype, int idYC)
    {
        int amount = 0;
        switch (idstype)
        {
            case 0: amount = ManagerMarket.instance.QuantityItemSeeds[idYC]; break;
            case 1: amount = ManagerMarket.instance.QuantityItemPets[idYC]; break;
            case 2: amount = ManagerMarket.instance.QuantityItemFactory[idYC]; break;
            case 3: amount = ManagerMarket.instance.QuantityItemOldTree[idYC]; break;
        }
        return amount;
    }

    private int CalculationCoin(int stype, int iditem, int amount)
    {
        int coin = 0;
        switch (stype)
        {
            case 0: coin += (int)(1.5 * amount * ManagerData.instance.seeds.SeedDatas[iditem].sell); break;
            case 1: coin += (int)(1.5 * amount * ManagerData.instance.petCollection.Pet[iditem].itemPet.sell); break;
            case 2: coin += (int)(1.5 * amount * ManagerData.instance.facetoryItems.FacetoryItemDatas[iditem].sell); break;
            case 3: coin += (int)(1.5 * amount * ManagerData.instance.trees.data[iditem].ItemTree.sell); break;
        }
        return coin;
    }

    private int CalculationExp(int stype, int iditem, int amount)
    {
        int exp = 0;
        switch (stype)
        {
            case 0: exp += 2 * amount * ManagerData.instance.seeds.SeedDatas[iditem].exp; break;
            case 1: exp += 2 * amount * ManagerData.instance.petCollection.Pet[iditem].detailPet.exp; break;
            case 2: exp += 2 * amount * ManagerData.instance.facetoryItems.FacetoryItemDatas[iditem].exp; break;
            case 3: exp += 2 * amount * ManagerData.instance.trees.data[iditem].ItemTree.exp; break;
        }
        return exp;
    }

    private IEnumerator VibrateItem()
    {
        yield return new WaitForSeconds(0.1f);
        Item.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        yield return new WaitForSeconds(0.1f);
        Item.transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(0.1f);
        Item.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        yield return new WaitForSeconds(0.1f);
        Item.transform.localScale = new Vector3(1f, 1f, 1f);
        IEItemScale = null;
    }

    public void UnlockDockAndBoat(int level)
    {
        if (level == LevelUnlock)
        {
            ItemBuy();
            ShowContainer();
            Wave.SetActive(true);
            moveWharf = true;
            MainCamera.instance.MoveCameraTarget(WharfPoint.position);
        }
    }

    private void Update()
    {
        if (moveWharf == true)
        {
            Ship.transform.position = Vector3.MoveTowards(Ship.transform.position, WharfPoint.position, SpeedShip);
            if (Ship.transform.position == WharfPoint.position)
            {
                status = 1;
                PlayerPrefs.SetInt("StatusShip", status);
                moveWharf = false;
                Wave.SetActive(false);
                TimeShipLeaveText.text = ManagerGame.instance.TimeText(TimeLeaveShip);
                IEShipLeave = IETimeLiveShipLeave(TimeLeaveShip);
                StartCoroutine(IEShipLeave);
            }
        }

        if (moveSell == true)
        {
            Ship.transform.position = Vector3.MoveTowards(Ship.transform.position, SellPoint.position, SpeedShip);
            if (Ship.transform.position == SellPoint.position)
            {
                moveSell = false;
                Wave.SetActive(false);
                Ship.transform.position = StartPoint.position;
            }
        }
    }

    private void InitData()
    {
        if (PlayerPrefs.HasKey("StatusShip") == false)
        {
            PlayerPrefs.SetInt("StatusShip", status);
            PlayerPrefs.SetInt("NumberBoughtShip", NumberBought);
            for (int i = 0; i < stypeItem.Length; i++)
            {
                PlayerPrefs.SetInt("StypeItemShip" + i, stypeItem[i]);
                PlayerPrefs.SetInt("IdItemShip" + i, idItem[i]);
                PlayerPrefs.SetInt("AmountItemRequire" + i, amountItemRequire[i]);
            }
            for (int i = 0; i < stypeContainers.Length; i++)
            {
                for (int j = 0; j < stypeContainers[i].detailContainers.Length; j++)
                {
                    PlayerPrefs.SetInt("StatusContainer" + i + "" + j, stypeContainers[i].detailContainers[j].status);
                }
            }
            PlayerPrefs.SetInt("TimeLiveShipLeave", 0);
            PlayerPrefs.SetInt("TimeLastShipLeave", ManagerGame.instance.RealTime());
            PlayerPrefs.SetInt("TimeLiveWaitSellItem", 0);
            PlayerPrefs.SetInt("TimeLastWaitSellItem", ManagerGame.instance.RealTime());
            PlayerPrefs.SetInt("TimeLiveNewShip", 0);
            PlayerPrefs.SetInt("TimeLastNewShip", ManagerGame.instance.RealTime());
        }
        else if (PlayerPrefs.HasKey("StatusShip") == true)
        {
            status = PlayerPrefs.GetInt("StatusShip");
            NumberBought = PlayerPrefs.GetInt("NumberBoughtShip");
            switch (status)
            {
                case 0:
                    if (Experience.instance.level >= 15)
                    {
                        ItemBuy();
                        ShowContainer();
                        Wave.SetActive(true);
                        moveWharf = true;
                    }
                    break;
                case 1:
                    int timeNowShipLeave = ManagerGame.instance.RealTime();
                    int timeShipLeaveDie = timeNowShipLeave - PlayerPrefs.GetInt("TimeLastShipLeave");
                    int TimeLiveShipLeave = PlayerPrefs.GetInt("TimeLiveShipLeave") - timeShipLeaveDie;
                    if (TimeLiveShipLeave > 0)
                    {
                        for (int i = 0; i < stypeItem.Length; i++)
                        {
                            stypeItem[i] = PlayerPrefs.GetInt("StypeItemShip" + i);
                            idItem[i] = PlayerPrefs.GetInt("IdItemShip" + i);
                            amountItemRequire[i] = PlayerPrefs.GetInt("AmountItemRequire" + i);
                            coinReceive[i] = CalculationCoin(stypeItem[i], idItem[i], amountItemRequire[i]);
                            expReceive[i] = CalculationExp(stypeItem[i], idItem[i], amountItemRequire[i]);
                        }
                        for (int i = 0; i < stypeContainers.Length; i++)
                        {
                            for (int j = 0; j < stypeContainers[i].detailContainers.Length; j++)
                            {
                                stypeContainers[i].detailContainers[j].status = PlayerPrefs.GetInt("StatusContainer" + i + "" + j);
                                if (j < NumberBuy)
                                {
                                    if (stypeContainers[i].detailContainers[j].status == 0)
                                    {
                                        stypeContainers[i].detailContainers[j].amountText.text = "" + amountItemRequire[i];
                                        stypeContainers[i].detailContainers[j].itemImage.sprite = SpriteItem(stypeItem[i], idItem[i]);
                                        stypeContainers[i].detailContainers[j].containerImage.sprite = ContainerSprite[0];
                                        if (stypeContainers[i].detailContainers[j].Container.activeSelf == false) stypeContainers[i].detailContainers[j].Container.SetActive(true);
                                    }
                                    else if (stypeContainers[i].detailContainers[j].status == 1)
                                    {
                                        stypeContainers[i].detailContainers[j].Item.SetActive(false);
                                        stypeContainers[i].detailContainers[j].containerImage.sprite = ContainerSprite[1];
                                        stypeContainers[i].detailContainers[i].Lid.SetActive(false);
                                        if (stypeContainers[i].detailContainers[j].Container.activeSelf == false) stypeContainers[i].detailContainers[j].Container.SetActive(true);
                                    }
                                }
                                else if (j >= NumberBuy)
                                {
                                    stypeContainers[i].detailContainers[j].amountText.text = "";
                                    stypeContainers[i].detailContainers[j].itemImage.sprite = null;
                                    stypeContainers[i].detailContainers[j].containerImage.sprite = ContainerSprite[0];
                                    if (stypeContainers[i].detailContainers[j].Container.activeSelf == true) stypeContainers[i].detailContainers[j].Container.SetActive(false);
                                }
                            }
                        }
                        IEShipLeave = IETimeLiveShipLeave(TimeLiveShipLeave);
                        StartCoroutine(IEShipLeave);
                        Ship.transform.position = WharfPoint.position;
                    }
                    else if (TimeLiveShipLeave <= 0)
                    {
                        status = 3;
                        PlayerPrefs.SetInt("StatusShip", 3);
                        ItemBuy();
                        ItemNewBroad();
                        ShowContainer();
                        int timeNewShip = TimeNewShip + TimeLiveShipLeave;
                        IENewShip = IETimeLiveNewShip(timeNewShip);
                        StartCoroutine(IENewShip);
                    }
                    break;
                case 2:
                    int timeNowWaitSell = ManagerGame.instance.RealTime();
                    int timeWaitSellDie = timeNowWaitSell - PlayerPrefs.GetInt("TimeLastWaitSellItem");
                    int TimeLiveWaitSell = PlayerPrefs.GetInt("TimeLiveWaitSellItem") - timeWaitSellDie;

                    if (TimeLiveWaitSell > 0)
                    {
                        IEWaitSell = IETimeLiveWaitSellItem(TimeLiveWaitSell);
                        StartCoroutine(IEWaitSell);
                    }
                    else if (TimeLiveWaitSell <= 0)
                    {
                        status = 3;
                        ItemBuy();
                        ItemNewBroad();
                        ShowContainer();
                        int TimeLiveNewShipWaitSell = TimeNewShip + TimeLiveWaitSell;
                        IENewShip = IETimeLiveNewShip(TimeLiveNewShipWaitSell);
                        StartCoroutine(IENewShip);
                    }
                    break;
                case 3:
                    int timeNowNewShip = ManagerGame.instance.RealTime();
                    int timeNewShipDie = timeNowNewShip - PlayerPrefs.GetInt("TimeLastNewShip");
                    int TimeLiveNewShip = PlayerPrefs.GetInt("TimeLiveNewShip") - timeNewShipDie;
                    for (int i = 0; i < stypeItem.Length; i++)
                    {
                        stypeItem[i] = PlayerPrefs.GetInt("StypeItemShip" + i);
                        idItem[i] = PlayerPrefs.GetInt("IdItemShip" + i);
                        amountItemRequire[i] = PlayerPrefs.GetInt("AmountItemRequire" + i);
                    }
                    ItemNewBroad();
                    ShowContainer();
                    for (int i = 0; i < stypeContainers.Length; i++)
                    {
                        for (int j = 0; j < stypeContainers[i].detailContainers.Length; j++)
                        {
                            stypeContainers[i].detailContainers[j].status = PlayerPrefs.GetInt("StatusContainer" + i + "" + j);
                            if (stypeContainers[i].detailContainers[j].status == 1)
                            {
                                stypeContainers[i].detailContainers[j].Item.SetActive(false);
                                stypeContainers[i].detailContainers[j].containerImage.sprite = ContainerSprite[1];
                            }
                        }
                    }
                    IENewShip = IETimeLiveNewShip(TimeLiveNewShip);
                    StartCoroutine(IENewShip);
                    break;
            }
        }
    }
}

[System.Serializable]
public struct StypeContainer
{
    public DetailContainer[] detailContainers;
}

[System.Serializable]
public struct DetailContainer
{
    public int status;
    public Text amountText;
    public Image itemImage;
    public Image containerImage;
    public GameObject Lid;
    public GameObject Item;
    public GameObject Container;
}
