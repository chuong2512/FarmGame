using System;
using UnityEngine.Serialization;

namespace NongTrai
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using NongTrai;

    public class ManagerCargo : Singleton<ManagerCargo>
    {
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

        [Header("Possition Ship")] [SerializeField]
        Transform StartPoint;

        [SerializeField] Transform WharfPoint;
        [SerializeField] Transform SellPoint;

        [Header("Item Buy")] [SerializeField] StypeContainer[] stypeContainers;

        [Header("Detail Container")] [SerializeField]
        Text CoinText;

        [FormerlySerializedAs("ExpText")] [SerializeField]
        Text expText;

        [FormerlySerializedAs("QuantityItemtext")] [SerializeField]
        Text quantityItemtext;

        [FormerlySerializedAs("ItemImage")] [SerializeField]
        Image itemImage;

        [FormerlySerializedAs("Item")] [SerializeField]
        GameObject item;

        [FormerlySerializedAs("DetailItem")] [SerializeField]
        GameObject detailItem;

        [FormerlySerializedAs("ContainerSprite")] [SerializeField]
        Sprite[] containerSprite;

        [Header("Wait Sell Item")] [SerializeField]
        Text TitleWaitingText;

        [FormerlySerializedAs("IntroductionText")] [SerializeField]
        Text introductionText;

        [FormerlySerializedAs("TimeWaitSellText")] [SerializeField]
        Text timeWaitSellText;

        [FormerlySerializedAs("WaitSell")] [SerializeField]
        GameObject waitSell;

        [Header("Wait New Broad")] [SerializeField]
        Text TitleWaitNewBroadText;

        [FormerlySerializedAs("TimeNewBroadText")] [SerializeField]
        Text timeNewBroadText;

        [FormerlySerializedAs("ButtonCallShipNowText")] [SerializeField]
        Text buttonCallShipNowText;

        [FormerlySerializedAs("ButtonNewShip")] [SerializeField]
        GameObject buttonNewShip;

        [FormerlySerializedAs("WaitNewBroad")] [SerializeField]
        GameObject waitNewBroad;

        [FormerlySerializedAs("ItemNewBroadImage")] [SerializeField]
        Image[] itemNewBroadImage;

        protected override void Awake()
        {
            base.Awake();

            minAmountRequire[0] = 10;
            maxAmountRequire[0] = 20;

            minAmountRequire[1] = 10;
            maxAmountRequire[1] = 20;

            minAmountRequire[2] = 5;
            maxAmountRequire[2] = 10;

            minAmountRequire[3] = 5;
            maxAmountRequire[3] = 10;
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
                introductionText.text = "Bạn có thể tạo thuyển mới trong:";
                buttonCallShipNowText.text = "Nhận Tàu Ngay";
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                TitleShipText.text = "Angkut Muatan";
                TitleShipLeaveText.text = "Perahu berangkat dalam: ";
                TitleWaitingText.text = "Waitting...!";
                TitleWaitNewBroadText.text = "Boat arriving in:";
                introductionText.text = "Perharu tiba dalam:";
                buttonCallShipNowText.text = "Dapatkan perharu sekarang";
            }
            else
            {
                TitleShipText.text = "Cargo";
                TitleShipLeaveText.text = "Ship leave in: ";
                TitleWaitingText.text = "Waitting...!";
                TitleWaitNewBroadText.text = "Boat arriving in:";
                introductionText.text = "You can get new ship in:";
                buttonCallShipNowText.text = "Call Ship Now";
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
                    Notification.Instance.dialogBelow(str);
                    break;
                case 1:
                    MainCamera.instance.DisableAll();
                    MainCamera.instance.lockCam();
                    CargoLoad.SetActive(true);
                    break;
                case 2:
                    MainCamera.instance.DisableAll();
                    MainCamera.instance.lockCam();
                    waitSell.SetActive(true);
                    break;
                case 3:
                    MainCamera.instance.DisableAll();
                    MainCamera.instance.lockCam();
                    waitNewBroad.SetActive(true);
                    break;
            }
        }

        public void CloseCargo()
        {
            MainCamera.instance.unLockCam();
            CargoLoad.SetActive(false);
        }

        public void OnClickButtonShip()
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
                string str = Application.systemLanguage switch
                {
                    SystemLanguage.Vietnamese => "Tàu chưa thể khởi hành vì chưa đủ số lượng vật phẩm",
                    SystemLanguage.Indonesian =>
                        "Kapal tidak dapat berangkat karena kuantitas produk tidak sesuai dengan persyaratan",
                    _ => "The Ship can't depart due to the quantity of products does't match the requirements"
                };
                Notification.Instance.dialogBelow(str);
            }
        }

        private IEnumerator IETimeLiveWaitSellItem(int time)
        {
            yield return new WaitForSeconds(1);
            time -= 1;
            PlayerPrefs.SetInt("TimeLiveWaitSellItem", time);
            PlayerPrefs.SetInt("TimelastWaitSellItem", ManagerGame.Instance.RealTime());
            timeWaitSellText.text = ManagerGame.Instance.TimeText(time);
            if (time > 0)
            {
                IEWaitSell = IETimeLiveWaitSellItem(time);
                StartCoroutine(IEWaitSell);
            }
            else
            {
                status = 3;
                PlayerPrefs.SetInt("StatusShip", status);
                ItemBuy();
                ItemNewBroad();
                ShowContainer();
                timeNewBroadText.text = ManagerGame.Instance.TimeText(TimeNewShip);
                IENewShip = IETimeLiveNewShip(TimeNewShip);
                StartCoroutine(IENewShip);
                if (buttonNewShip.activeSelf == false) buttonNewShip.SetActive(true);
                if (waitSell.activeSelf == true)
                {
                    waitSell.SetActive(false);
                    waitNewBroad.SetActive(true);
                }
            }
        }


        private IEnumerator IETimeLiveNewShip(int time)
        {
            yield return new WaitForSeconds(1f);
            time -= 1;
            PlayerPrefs.SetInt("TimeLiveNewShip", time);
            PlayerPrefs.SetInt("TimeLastNewShip", ManagerGame.Instance.RealTime());
            timeNewBroadText.text = ManagerGame.Instance.TimeText(time);
            if (time > 0)
            {
                IENewShip = IETimeLiveNewShip(time);
                StartCoroutine(IENewShip);
            }
            else
            {
                moveWharf = true;
                Wave.SetActive(true);
                buttonNewShip.SetActive(false);
                timeNewBroadText.text = Application.systemLanguage == SystemLanguage.Vietnamese
                    ? "Tàu đang đến!"
                    : "Ship is coming!";
            }
        }

        public void ButtonConfirmWaitSell()
        {
            ManagerAudio.Instance.PlayAudio(Audio.Click);
            waitSell.SetActive(false);
        }

        private IEnumerator IETimeLiveShipLeave(int time)
        {
            yield return new WaitForSeconds(1f);
            time -= 1;
            PlayerPrefs.SetInt("TimeLiveShipLeave", time);
            PlayerPrefs.SetInt("TimeLastShipLeave", ManagerGame.Instance.RealTime());
            TimeShipLeaveText.text = ManagerGame.Instance.TimeText(time);
            if (time > 0)
            {
                IEShipLeave = IETimeLiveShipLeave(time);
                StartCoroutine(IEShipLeave);
            }
            else
            {
                status = 2;
                PlayerPrefs.SetInt("StatusShip", status);
                Wave.SetActive(true);
                moveSell = true;
                ResetContainer();
                CargoLoad.SetActive(false);
                timeWaitSellText.text = ManagerGame.Instance.TimeText(TimeWaitSell);
                IEWaitSell = IETimeLiveWaitSellItem(TimeWaitSell);
                StartCoroutine(IEWaitSell);
                if (CargoLoad.activeSelf != true) yield break;
                CargoLoad.SetActive(false);
                waitSell.SetActive(true);
            }
        }


        private void ItemNewBroad()
        {
            for (var i = 0; i < itemNewBroadImage.Length; i++)
            {
                itemNewBroadImage[i].sprite = SpriteItem(stypeItem[i], idItem[i]);
            }
        }

        public void ButtonCloseWaitNewShip()
        {
            ManagerAudio.Instance.PlayAudio(Audio.Click);
            waitNewBroad.SetActive(false);
        }

        public void ButtonCallShipNow()
        {
            moveWharf = true;
            Wave.SetActive(true);
            StopCoroutine(IENewShip);
            PlayerPrefs.SetInt("TimeLiveNewShip", 0);
            ManagerGem.Instance.MunisGem(5);
            buttonNewShip.SetActive(false);
            timeNewBroadText.text = Application.systemLanguage == SystemLanguage.Vietnamese
                ? "Tàu đang đến!"
                : "Ship is coming!";
            waitNewBroad.SetActive(false);
        }


        public void ButtonBuyItem()
        {
            if (stypeContainers[idStypeContainer].detailContainers[idContainer].status == 0)
            {
                if (AmountMarket(stypeItem[idStypeContainer], idItem[idStypeContainer]) >=
                    amountItemRequire[idStypeContainer])
                {
                    NumberBought += 1;
                    PlayerPrefs.SetInt("NumberBoughtShip", NumberBought);
                    stypeContainers[idStypeContainer].detailContainers[idContainer].status = 1;
                    PlayerPrefs.SetInt("StatusContainer" + idStypeContainer + "" + idContainer,
                        stypeContainers[idStypeContainer].detailContainers[idContainer].status);
                    ManagerMarket.instance.MinusItem(stypeItem[idStypeContainer], idItem[idStypeContainer],
                        amountItemRequire[idStypeContainer]);
                    Sprite spr = SpriteItem(stypeItem[idStypeContainer], idItem[idStypeContainer]);
                    Vector3 target = stypeContainers[idStypeContainer].detailContainers[idContainer].container.transform
                        .position;
                    target.z = 0;
                    stypeContainers[idStypeContainer].detailContainers[idContainer].item.SetActive(false);
                    stypeContainers[idStypeContainer].detailContainers[idContainer].lid.SetActive(true);
                    detailItem.SetActive(false);
                    StartCoroutine(Crating(idStypeContainer, idContainer, amountItemRequire[idStypeContainer], spr,
                        target, coinReceive[idStypeContainer], expReceive[idStypeContainer]));
                }
                else if (AmountMarket(stypeItem[idStypeContainer], idItem[idStypeContainer]) <
                         amountItemRequire[idStypeContainer])
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Bạn không đủ vật phẩm!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Anda tidak cukup barang!";
                    else str = "You aren't enough item!";
                    Notification.Instance.dialogBelow(str);
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Crating(int idstype, int idcontainer, int quantityItem, Sprite spr, Vector3 target,
            int coin, int exp)
        {
            yield return new WaitForSeconds(1f);
            stypeContainers[idstype].detailContainers[idcontainer].containerImage.sprite = containerSprite[1];
            stypeContainers[idstype].detailContainers[idcontainer].lid.SetActive(false);
            RegisterCargoGetItem(quantityItem, spr, target);
            yield return new WaitForSeconds(1f);
            ManagerTool.instance.RegisterExperienceCoin(coin, exp, target);
        }

        public void ClickContainer(int idstype, int idcontainer)
        {
            idStypeContainer = idstype;
            idContainer = idcontainer;
            switch (stypeContainers[idStypeContainer].detailContainers[idContainer].status)
            {
                case 0:
                    ShowDetailItem();
                    break;
                case 1:
                    detailItem.SetActive(false);
                    break;
            }
        }

        private void ItemBuy()
        {
            for (var i = 0; i < stypeItem.Length; i++)
            {
                var randomStype = Random.Range(0, ManagerItem.instance.idItemUnlock.Length);
                var randomIdItem = Random.Range(0, ManagerItem.instance.totalItem[randomStype]);

                while (CheckSame(i, randomStype, randomIdItem))
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
                        stypeContainers[i].detailContainers[j].containerImage.sprite = containerSprite[0];
                        if (stypeContainers[i].detailContainers[j].container.activeSelf == false)
                            stypeContainers[i].detailContainers[j].container.SetActive(true);
                    }
                    else if (j >= NumberBuy)
                    {
                        stypeContainers[i].detailContainers[j].amountText.text = "";
                        stypeContainers[i].detailContainers[j].itemImage.sprite = null;
                        stypeContainers[i].detailContainers[j].containerImage.sprite = containerSprite[0];
                        if (stypeContainers[i].detailContainers[j].container.activeSelf == true)
                            stypeContainers[i].detailContainers[j].container.SetActive(false);
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
                        if (stypeContainers[i].detailContainers[j].item.activeSelf == false)
                            stypeContainers[i].detailContainers[j].item.SetActive(true);
                        stypeContainers[i].detailContainers[j].itemImage.sprite = null;
                        if (stypeContainers[i].detailContainers[j].container.activeSelf == true)
                            stypeContainers[i].detailContainers[j].container.SetActive(false);
                    }
                }
            }
        }

        private void ShowDetailItem()
        {
            itemImage.sprite = SpriteItem(stypeItem[idStypeContainer], idItem[idStypeContainer]);
            quantityItemtext.text = AmountMarket(stypeItem[idStypeContainer], idItem[idStypeContainer]) + "/" +
                                    amountItemRequire[idStypeContainer];
            CoinText.text = "" + coinReceive[idStypeContainer];
            expText.text = "" + expReceive[idStypeContainer];
            if (detailItem.activeSelf == false) detailItem.SetActive(true);
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
            cgi.itemImage.sprite = spr;
            cgi.quantityItemText.text = "-" + quantity;
        }

        private Sprite SpriteItem(int idstype, int idYC)
        {
            Sprite spr = null;
            switch (idstype)
            {
                case 0:
                    spr = ManagerData.instance.seeds.SeedDatas[idYC].item;
                    break;
                case 1:
                    spr = ManagerData.instance.petCollection.Pet[idYC].itemPet.item;
                    break;
                case 2:
                    spr = ManagerData.instance.facetoryItems.FacetoryItemDatas[idYC].item;
                    break;
                case 3:
                    spr = ManagerData.instance.trees.data[idYC].ItemTree.item;
                    break;
            }

            return spr;
        }

        private int AmountMarket(int idstype, int idYC)
        {
            int amount = idstype switch
            {
                0 => ManagerMarket.instance.QuantityItemSeeds[idYC],
                1 => ManagerMarket.instance.QuantityItemPets[idYC],
                2 => ManagerMarket.instance.QuantityItemFactory[idYC],
                3 => ManagerMarket.instance.QuantityItemOldTree[idYC],
                _ => 0
            };

            return amount;
        }

        private int CalculationCoin(int stype, int iditem, int amount)
        {
            int coin = 0;
            if (stype == 0)
                coin += (int) (1.5 * amount * ManagerData.instance.seeds.SeedDatas[iditem].sell);
            else if (stype == 1)
                coin += (int) (1.5 * amount * ManagerData.instance.petCollection.Pet[iditem].itemPet.sell);
            else if (stype == 2)
                coin += (int) (1.5 * amount * ManagerData.instance.facetoryItems.FacetoryItemDatas[iditem].sell);
            else if (stype == 3) coin += (int) (1.5 * amount * ManagerData.instance.trees.data[iditem].ItemTree.sell);

            return coin;
        }

        private int CalculationExp(int stype, int iditem, int amount)
        {
            int exp = 0;
            switch (stype)
            {
                case 0:
                    exp += 2 * amount * ManagerData.instance.seeds.SeedDatas[iditem].exp;
                    break;
                case 1:
                    exp += 2 * amount * ManagerData.instance.petCollection.Pet[iditem].detailPet.exp;
                    break;
                case 2:
                    exp += 2 * amount * ManagerData.instance.facetoryItems.FacetoryItemDatas[iditem].exp;
                    break;
                case 3:
                    exp += 2 * amount * ManagerData.instance.trees.data[iditem].ItemTree.exp;
                    break;
            }

            return exp;
        }

        private IEnumerator VibrateItem()
        {
            yield return new WaitForSeconds(0.1f);
            item.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            yield return new WaitForSeconds(0.1f);
            item.transform.localScale = new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
            item.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
            yield return new WaitForSeconds(0.1f);
            item.transform.localScale = new Vector3(1f, 1f, 1f);
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
                    TimeShipLeaveText.text = ManagerGame.Instance.TimeText(TimeLeaveShip);
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
                for (var i = 0; i < stypeItem.Length; i++)
                {
                    PlayerPrefs.SetInt("StypeItemShip" + i, stypeItem[i]);
                    PlayerPrefs.SetInt("IdItemShip" + i, idItem[i]);
                    PlayerPrefs.SetInt("AmountItemRequire" + i, amountItemRequire[i]);
                }

                for (var i = 0; i < stypeContainers.Length; i++)
                {
                    for (var j = 0; j < stypeContainers[i].detailContainers.Length; j++)
                    {
                        PlayerPrefs.SetInt("StatusContainer" + i + "" + j,
                            stypeContainers[i].detailContainers[j].status);
                    }
                }

                PlayerPrefs.SetInt("TimeLiveShipLeave", 0);
                PlayerPrefs.SetInt("TimeLastShipLeave", ManagerGame.Instance.RealTime());
                PlayerPrefs.SetInt("TimeLiveWaitSellItem", 0);
                PlayerPrefs.SetInt("TimeLastWaitSellItem", ManagerGame.Instance.RealTime());
                PlayerPrefs.SetInt("TimeLiveNewShip", 0);
                PlayerPrefs.SetInt("TimeLastNewShip", ManagerGame.Instance.RealTime());
            }

            else
            {
                status = PlayerPrefs.GetInt("StatusShip");
                NumberBought = PlayerPrefs.GetInt("NumberBoughtShip");
                switch (status)
                {
                    case 0:
                        if (Experience.Instance.level >= 15)
                        {
                            ItemBuy();
                            ShowContainer();
                            Wave.SetActive(true);
                            moveWharf = true;
                        }

                        break;
                    case 1:
                        int timeNowShipLeave = ManagerGame.Instance.RealTime();
                        int timeShipLeaveDie = timeNowShipLeave - PlayerPrefs.GetInt("TimeLastShipLeave");
                        int timeLiveShipLeave = PlayerPrefs.GetInt("TimeLiveShipLeave") - timeShipLeaveDie;
                        if (timeLiveShipLeave > 0)
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
                                    stypeContainers[i].detailContainers[j].status =
                                        PlayerPrefs.GetInt("StatusContainer" + i + "" + j);
                                    if (j < NumberBuy)
                                    {
                                        if (stypeContainers[i].detailContainers[j].status == 0)
                                        {
                                            stypeContainers[i].detailContainers[j].amountText.text =
                                                "" + amountItemRequire[i];
                                            stypeContainers[i].detailContainers[j].itemImage.sprite =
                                                SpriteItem(stypeItem[i], idItem[i]);
                                            stypeContainers[i].detailContainers[j].containerImage.sprite =
                                                containerSprite[0];
                                            if (stypeContainers[i].detailContainers[j].container.activeSelf == false)
                                                stypeContainers[i].detailContainers[j].container.SetActive(true);
                                        }
                                        else if (stypeContainers[i].detailContainers[j].status == 1)
                                        {
                                            stypeContainers[i].detailContainers[j].item.SetActive(false);
                                            stypeContainers[i].detailContainers[j].containerImage.sprite =
                                                containerSprite[1];
                                            stypeContainers[i].detailContainers[i].lid.SetActive(false);
                                            if (stypeContainers[i].detailContainers[j].container.activeSelf == false)
                                                stypeContainers[i].detailContainers[j].container.SetActive(true);
                                        }
                                    }
                                    else if (j >= NumberBuy)
                                    {
                                        stypeContainers[i].detailContainers[j].amountText.text = "";
                                        stypeContainers[i].detailContainers[j].itemImage.sprite = null;
                                        stypeContainers[i].detailContainers[j].containerImage.sprite =
                                            containerSprite[0];
                                        if (stypeContainers[i].detailContainers[j].container.activeSelf == true)
                                            stypeContainers[i].detailContainers[j].container.SetActive(false);
                                    }
                                }
                            }

                            IEShipLeave = IETimeLiveShipLeave(timeLiveShipLeave);
                            StartCoroutine(IEShipLeave);
                            Ship.transform.position = WharfPoint.position;
                        }
                        else if (timeLiveShipLeave <= 0)
                        {
                            status = 3;
                            PlayerPrefs.SetInt("StatusShip", 3);
                            ItemBuy();
                            ItemNewBroad();
                            ShowContainer();
                            int timeNewShip = TimeNewShip + timeLiveShipLeave;
                            IENewShip = IETimeLiveNewShip(timeNewShip);
                            StartCoroutine(IENewShip);
                        }

                        break;
                    case 2:
                        int timeNowWaitSell = ManagerGame.Instance.RealTime();
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
                        int timeNowNewShip = ManagerGame.Instance.RealTime();
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
                                stypeContainers[i].detailContainers[j].status =
                                    PlayerPrefs.GetInt("StatusContainer" + i + "" + j);
                                if (stypeContainers[i].detailContainers[j].status == 1)
                                {
                                    stypeContainers[i].detailContainers[j].item.SetActive(false);
                                    stypeContainers[i].detailContainers[j].containerImage.sprite = containerSprite[1];
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

    [Serializable]
    public struct StypeContainer
    {
        public DetailContainer[] detailContainers;
    }

    [Serializable]
    public struct DetailContainer
    {
        public int status;
        public Text amountText;
        public Image itemImage;
        public Image containerImage;

        [FormerlySerializedAs("Lid")] public GameObject lid;
        [FormerlySerializedAs("Item")] public GameObject item;
        [FormerlySerializedAs("Container")] public GameObject container;
    }
}