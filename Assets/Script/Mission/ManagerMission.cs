using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NongTrai;

namespace NongTrai
{
    public class ManagerMission : MonoBehaviour
    {
        public static ManagerMission instance = null;

        private int Status;
        private int StypeItem;
        private int CouterClickUseGem;
        private bool CheckRun;
        private bool LoadData;
        private bool CheckShowTime;
        private int totalMissions = 20;
        private int timeWait = 300;

        // Đếm
        private int[] itemUse = new int[4];
        private int[] NumberCombination = new int[4];

        private int MissionFinal;
        private int OrderSelected;
        private int OrderPerForm;
        private int NumberOrder;
        private int NumberFinal = 1;
        private bool CheckPerForm;

        private int[] idItemNow;
        private int[] idStypeNow;

        private int[] statusOrder = new int[12];
        private int[] timeNewOrder = new int[12];
        private int[] stypeMisson = new int[12];
        private int[] idMissionOrder = new int[12];
        private bool[] OrderDone = new bool[12];
        private IEnumerator[] IETimeNewOrder = new IEnumerator[12];
        private Mission[] mission = new Mission[12];
        [SerializeField] Text NameOrder;
        [SerializeField] GameObject Mission;
        [SerializeField] Missions missions;

        [Header("Order")] [SerializeField] Sprite[] StatusSprite;
        [SerializeField] Text[] ExpOrderText;
        [SerializeField] Text[] CoinOrderText;
        [SerializeField] Image[] OrderImage;
        [SerializeField] GameObject[] OrderTick;
        [SerializeField] GameObject[] Order;
        [SerializeField] GameObject[] CoinOrder;
        [SerializeField] GameObject[] ExpOrder;
        [SerializeField] GameObject[] TickWordSpace;
        [SerializeField] GameObject[] OrderWordSpace;
        [SerializeField] int[] LevelAddOrder;

        [Header("Mission")] [SerializeField] Text AddressText;
        [SerializeField] Text CoinMissionText;
        [SerializeField] Text ExpMissionText;
        [SerializeField] Text SuggestText;
        [SerializeField] Text TimeLiveText;
        [SerializeField] GameObject SuggestGO;
        [SerializeField] GameObject[] StatusMission;
        [SerializeField] Text[] MissionText;
        [SerializeField] Image[] MissionImage;
        [SerializeField] GameObject[] MissionGO;
        [SerializeField] GameObject[] MissionTick;

        [Header("InforOrder")] [SerializeField]
        DeliveryAddress[] Address;

        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);
        }

        void Start()
        {
            NameOrder.text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Đơn Hàng" : "Order";
            SuggestText.text = Application.systemLanguage == SystemLanguage.Vietnamese
                ? "Chọn một vào một đơn hàng bên trái"
                : "Select an order from the left";
            idItemNow = new int[ManagerItem.Instance.totalItem.Length - 1];
            idStypeNow = new int[ManagerItem.Instance.totalItem.Length - 1];
            Invoke("InitData", 3f);
        }

        public void OpenOrder()
        {
            if (LoadData == true)
            {
                if (Experience.Instance.level < LevelAddOrder[0])
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Tính năng đơn hàng được mở khi đạt level " + (LevelAddOrder[0] + 1);
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Perluasa terbuka di level " + (LevelAddOrder[0] + 1);
                    else str = "Taking order feature will be unlocked at level " + (LevelAddOrder[0] + 1);
                    Notification.Instance.dialogBelow(str);
                }
                else if (Experience.Instance.level >= LevelAddOrder[0])
                {
                    MainCamera.instance.DisableAll();
                    MainCamera.instance.lockCam();
                    if (CheckPerForm == true)
                    {
                        CheckPerForm = false;
                        PlayerPrefs.SetInt("CheckPerForm", 0);
                        Mission.SetActive(true);
                        MissionOrder(OrderSelected);
                        DetailMission(OrderSelected);
                        StartCoroutine(NewOrder(OrderSelected));
                    }
                    else if (CheckPerForm == false) Mission.SetActive(true);
                }
            }
            else if (LoadData == false)
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Đang lấy dữ liệu thông tin đơn hàng!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Tunggu urutan tanggal pemuatan!";
                else str = "Wait for load date order!";
                Notification.Instance.dialogBelow(str);
            }
        }

        IEnumerator NewOrder(int idOrder)
        {
            yield return new WaitForSeconds(1f);
            Order[idOrder].SetActive(true);
        }

        public void CloseOrder()
        {
            MainCamera.instance.unLockCam();
            Mission.SetActive(false);
        }

        public void AddOrder()
        {
            if (NumberOrder < 12)
            {
                if (MissionFinal < totalMissions)
                {
                    NumberOrder += 1;
                    PlayerPrefs.SetInt("NumberOrder", NumberOrder);
                    Order[NumberOrder - 1].SetActive(true);
                    OrderWordSpace[NumberOrder - 1].SetActive(true);
                    MissionOrder(NumberOrder - 1);
                }
                else if (MissionFinal >= totalMissions)
                {
                    NumberOrder += 1;
                    PlayerPrefs.SetInt("NumberOrder", NumberOrder);
                    Order[NumberOrder - 1].SetActive(true);
                    OrderWordSpace[NumberOrder - 1].SetActive(true);
                    MissionOrder(NumberOrder - 1);
                }
            }
        }

        public void ClickOrder(int id)
        {
            OrderSelected = id;
            PlayerPrefs.SetInt("OrderSelected", OrderSelected);
            DetailMission(id);
            CouterClickUseGem = 0;
            for (int i = 0; i < StatusMission.Length; i++)
            {
                if (i == statusOrder[id]) StatusMission[i].SetActive(true);
                else if (i != statusOrder[id]) StatusMission[i].SetActive(false);
            }
        }

        public void ButtonPerform()
        {
            switch (Status)
            {
                case 0:
                    if (OrderDone[OrderSelected] == true)
                    {
                        Status = 1;
                        PlayerPrefs.SetInt("StatusPerForm", Status);
                        OrderPerForm = OrderSelected;
                        PlayerPrefs.SetInt("OrderPerForm", OrderPerForm);
                        CheckPerForm = true;
                        PlayerPrefs.SetInt("CheckPerForm", 1);
                        Order[OrderSelected].SetActive(false);
                        CloseOrder();
                        HideDetailOrder();
                        if (OrderTick[OrderSelected].activeSelf == true) OrderTick[OrderSelected].SetActive(false);
                        if (TickWordSpace[OrderSelected].activeSelf == true)
                            TickWordSpace[OrderSelected].SetActive(false);
                        if (stypeMisson[OrderSelected] == 0)
                        {
                            int coinMission = missions.mission[idMissionOrder[OrderSelected]].coin;
                            int expMission = missions.mission[idMissionOrder[OrderSelected]].exp;
                            ManagerCar.instance.startCargo(coinMission, expMission);
                            for (int i = 0; i < missions.mission[idMissionOrder[OrderSelected]].metarial.Length; i++)
                            {
                                int stypeIDYC = missions.mission[idMissionOrder[OrderSelected]].metarial[i].stypeIDYC;
                                int IDYC = missions.mission[idMissionOrder[OrderSelected]].metarial[i].IdYc;
                                int quantityIDYC = missions.mission[idMissionOrder[OrderSelected]].metarial[i].Amount;
                                ManagerMarket.instance.MinusItem(stypeIDYC, IDYC, quantityIDYC);
                            }
                        }
                        else if (stypeMisson[OrderSelected] == 1)
                        {
                            int coinMission = mission[OrderSelected].coin;
                            int expMission = mission[OrderSelected].exp;
                            ManagerCar.instance.startCargo(coinMission, expMission);
                            for (int i = 0; i < mission[OrderSelected].metarial.Length; i++)
                            {
                                int stypeIDYC = mission[OrderSelected].metarial[i].stypeIDYC;
                                int IDYC = mission[OrderSelected].metarial[i].IdYc;
                                int quantityIDYC = mission[OrderSelected].metarial[i].Amount;
                                ManagerMarket.instance.MinusItem(stypeIDYC, IDYC, quantityIDYC);
                            }
                        }
                    }

                    break;
                case 1:
                    string str1;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str1 = "Xe của bạn đã có vật phẩm vui lòng chờ xe của bạn về!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str1 = "Truk Anda penuh dengan barang, harap tunggu!";
                    else str1 = "Your truck is full of items, please wait!";
                    ;
                    Notification.Instance.dialogBelow(str1);
                    break;
                case 2:
                    string str2;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str2 = "Bạn hãy nhận vật phẩm trên xe trước!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str2 = "Koin yang tersisa di truk, Silahkan kosongkan truk dulu!";
                    else str2 = "Coins are left in the truck, Please emty the truck first!";
                    Notification.Instance.dialogBelow(str2);
                    break;
            }
        }

        public void ButtonDelete()
        {
            statusOrder[OrderSelected] = 1;
            PlayerPrefs.SetInt("StatusOrder" + OrderSelected, statusOrder[OrderSelected]);
            DeleteOrder(OrderSelected);
            for (int i = 0; i < StatusMission.Length; i++)
            {
                if (i == statusOrder[OrderSelected]) StatusMission[i].SetActive(true);
                else if (i != statusOrder[OrderSelected]) StatusMission[i].SetActive(false);
            }

            for (int i = 0; i < MissionGO.Length; i++)
            {
                if (MissionGO[i].activeSelf == true) MissionGO[i].SetActive(false);
            }

            if (Application.systemLanguage == SystemLanguage.Vietnamese)
            {
                AddressText.text = "Vui lòng chờ...";
                SuggestText.text = "Đơn hàng không khả dụng \n Hãy chờ hoặc tăng tốc!";
            }
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
            {
                AddressText.text = "Tunggu sebentar...";
                SuggestText.text = "Ini adalah pesanan tidak tersedia \n Tunggu atau percepat!";
            }
            else
            {
                AddressText.text = "Please wait...";
                SuggestText.text = "This is order isn't available \n Wait or speed up!";
            }

            if (SuggestGO.activeSelf == false) SuggestGO.SetActive(true);
            CheckShowTime = true;
            timeNewOrder[OrderSelected] = timeWait;
            PlayerPrefs.SetInt("TimeLiveOrder" + OrderSelected, timeNewOrder[OrderSelected]);
            PlayerPrefs.SetInt("TimeLastOrder" + OrderSelected, ManagerGame.Instance.RealTime());
            TimeLiveText.text = ManagerGame.Instance.TimeText(timeNewOrder[OrderSelected]);
            IETimeNewOrder[OrderSelected] = CouterTimeNewOrder(OrderSelected);
            StartCoroutine(IETimeNewOrder[OrderSelected]);
        }

        private void DeleteOrder(int id)
        {
            OrderImage[id].sprite = StatusSprite[statusOrder[id]];
            if (CoinOrder[id].activeSelf == true) CoinOrder[id].SetActive(false);
            if (ExpOrder[id].activeSelf == true) ExpOrder[id].SetActive(false);
            if (OrderTick[id].activeSelf == true) OrderTick[id].SetActive(false);
        }

        IEnumerator CouterTimeNewOrder(int id)
        {
            yield return new WaitForSeconds(1f);
            timeNewOrder[id] -= 1;
            PlayerPrefs.SetInt("TimeLiveOrder" + id, timeNewOrder[id]);
            PlayerPrefs.SetInt("TimeLastOrder" + id, ManagerGame.Instance.RealTime());
            if (timeNewOrder[id] > 0)
            {
                IETimeNewOrder[id] = CouterTimeNewOrder(id);
                StartCoroutine(IETimeNewOrder[id]);
                if (CheckShowTime == true && id == OrderSelected)
                    TimeLiveText.text = ManagerGame.Instance.TimeText(timeNewOrder[id]);
            }

            if (timeNewOrder[id] <= 0)
            {
                statusOrder[id] = 0;
                PlayerPrefs.SetInt("StatusOrder" + id, statusOrder[id]);
                MissionOrder(id);
                FinishWaitTime(id);
                if (OrderSelected == id)
                {
                    CheckShowTime = false;
                    DetailMission(id);
                    for (int i = 0; i < StatusMission.Length; i++)
                    {
                        if (i == statusOrder[OrderSelected]) StatusMission[i].SetActive(true);
                        else if (i != statusOrder[OrderSelected]) StatusMission[i].SetActive(false);
                    }
                }
            }
        }

        private void FinishWaitTime(int id)
        {
            OrderImage[id].sprite = StatusSprite[statusOrder[id]];
            if (CoinOrder[id].activeSelf == false) CoinOrder[id].SetActive(true);
            if (ExpOrder[id].activeSelf == false) ExpOrder[id].SetActive(true);
        }

        public void DetailMission(int id)
        {
            if (statusOrder[id] == 0)
            {
                if (SuggestGO.activeSelf == true) SuggestGO.SetActive(false);
                if (stypeMisson[id] == 0)
                {
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        AddressText.text = missions.mission[idMissionOrder[id]].name;
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        AddressText.text = missions.mission[idMissionOrder[id]].nameINS;
                    else AddressText.text = missions.mission[idMissionOrder[id]].engName;
                    CoinMissionText.text = "" + missions.mission[idMissionOrder[id]].coin;
                    ExpMissionText.text = "" + missions.mission[idMissionOrder[id]].exp;
                    for (int i = 0; i < MissionGO.Length; i++)
                    {
                        if (i < missions.mission[idMissionOrder[id]].metarial.Length)
                        {
                            if (MissionGO[i].activeSelf == false) MissionGO[i].SetActive(true);
                            int amout = AmountMarket(missions.mission[idMissionOrder[id]].metarial[i].stypeIDYC,
                                missions.mission[idMissionOrder[id]].metarial[i].IdYc);
                            MissionText[i].text = amout + "/" + missions.mission[idMissionOrder[id]].metarial[i].Amount;
                            MissionImage[i].sprite = SpriteItem(
                                missions.mission[idMissionOrder[id]].metarial[i].stypeIDYC,
                                missions.mission[idMissionOrder[id]].metarial[i].IdYc);
                            if (amout >= missions.mission[idMissionOrder[id]].metarial[i].Amount)
                                MissionTick[i].SetActive(true);
                            else MissionTick[i].SetActive(false);
                        }
                        else
                        {
                            if (MissionTick[i].activeSelf == true) MissionTick[i].SetActive(false);
                            if (MissionGO[i].activeSelf == true) MissionGO[i].SetActive(false);
                        }
                    }
                }
                else if (stypeMisson[id] == 1)
                {
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        AddressText.text = mission[id].name;
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        AddressText.text = mission[id].nameINS;
                    else AddressText.text = mission[id].engName;
                    CoinMissionText.text = "" + mission[id].coin;
                    ExpMissionText.text = "" + mission[id].exp;
                    for (int i = 0; i < MissionGO.Length; i++)
                    {
                        if (i < mission[id].metarial.Length)
                        {
                            if (MissionGO[i].activeSelf == false) MissionGO[i].SetActive(true);
                            int amout = AmountMarket(mission[id].metarial[i].stypeIDYC, mission[id].metarial[i].IdYc);
                            MissionText[i].text = amout + "/" + mission[id].metarial[i].Amount;
                            Debug.Log(id + ":" + mission[id].metarial[i].stypeIDYC + "/" +
                                      mission[id].metarial[i].IdYc);
                            MissionImage[i].sprite =
                                SpriteItem(mission[id].metarial[i].stypeIDYC, mission[id].metarial[i].IdYc);
                            if (amout >= mission[id].metarial[i].Amount) MissionTick[i].SetActive(true);
                            else MissionTick[i].SetActive(false);
                        }
                        else
                        {
                            if (MissionTick[i].activeSelf == true) MissionTick[i].SetActive(false);
                            if (MissionGO[i].activeSelf == true) MissionGO[i].SetActive(false);
                        }
                    }
                }
            }
            else if (statusOrder[id] == 1)
            {
                for (int i = 0; i < StatusMission.Length; i++)
                {
                    if (i == statusOrder[id]) StatusMission[i].SetActive(true);
                    else if (i != statusOrder[id]) StatusMission[i].SetActive(false);
                }

                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                {
                    AddressText.text = "Vui lòng chờ...";
                    SuggestText.text = "Đơn hàng không khả dụng \n Hãy chờ hoặc tăng tốc!";
                }
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                {
                    AddressText.text = "Tunggu sebentar...";
                    SuggestText.text = "Ini adalah pesanan tidak tersedia \n Tunggu atau percepat!";
                }
                else
                {
                    AddressText.text = "Please wait...";
                    SuggestText.text = "This is order isn't available \n Wait or speed up!";
                }

                if (SuggestGO.activeSelf == false) SuggestGO.SetActive(true);
                CheckShowTime = true;
                TimeLiveText.text = ManagerGame.Instance.TimeText(timeNewOrder[id]);
            }
        }

        private void MissionOrder(int idOrder)
        {
            if (MissionFinal < totalMissions)
            {
                stypeMisson[idOrder] = 0;
                PlayerPrefs.SetInt("StypeMission" + idOrder, stypeMisson[idOrder]);
                CoinOrderText[idOrder].text = "" + missions.mission[MissionFinal].coin;
                ExpOrderText[idOrder].text = "" + missions.mission[MissionFinal].exp;
                idMissionOrder[idOrder] = MissionFinal;
                PlayerPrefs.SetInt("IdMissionOrder" + idOrder, idMissionOrder[idOrder]);
                MissionFinal += 1;
                PlayerPrefs.SetInt("MissionFinal", MissionFinal);
                CheckDoneOrder();
            }
            else if (MissionFinal >= totalMissions)
            {
                stypeMisson[idOrder] = 1;
                PlayerPrefs.SetInt("StypeMission" + idOrder, stypeMisson[idOrder]);
                int randomAddress = Random.Range(0, Address.Length);
                PlayerPrefs.SetInt("AdreesOrder" + Order, randomAddress);
                mission[idOrder].name = Address[randomAddress].Name;
                mission[idOrder].engName = Address[randomAddress].EngName;
                mission[idOrder].nameINS = Address[randomAddress].InsName;
                if (CheckRun == false)
                {
                    bool condition = false;
                    for (int i = 0; i < itemUse.Length; i++)
                    {
                        if (itemUse[i] < ManagerItem.Instance.totalItem[i])
                        {
                            condition = true;
                            StypeItem = i;
                            PlayerPrefs.SetInt("StypeItem", i);
                            CheckRun = true;
                            PlayerPrefs.SetInt("CheckRun", 1);
                            break;
                        }
                    }

                    if (condition == false)
                    {
                        Debug.Log("Condition :" + condition);
                        int dem = 0;
                        for (int i = 0; i < ManagerItem.Instance.totalItem.Length; i++)
                        {
                            if (ManagerItem.Instance.totalItem[i] > 0) dem += 1;
                        }

                        mission[idOrder].metarial = new Metarial[dem];
                        PlayerPrefs.SetInt("NumberItem" + idOrder, mission[idOrder].metarial.Length);
                        int couter = 0;
                        for (int i = 0; i < ManagerItem.Instance.totalItem.Length; i++)
                        {
                            if (ManagerItem.Instance.totalItem[i] > 0)
                            {
                                mission[idOrder].metarial[couter].stypeIDYC = i;
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + couter,
                                    mission[idOrder].metarial[couter].stypeIDYC);
                                mission[idOrder].metarial[couter].IdYc =
                                    ManagerItem.Instance.idItemUnlock[i].IdItem[idItemNow[i]];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + couter,
                                    mission[idOrder].metarial[couter].IdYc);
                                mission[idOrder].metarial[couter].Amount = 3 + 2 * NumberFinal;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + couter,
                                    mission[idOrder].metarial[couter].Amount);
                                couter += 1;
                                NumberFinal += 1;
                                PlayerPrefs.SetInt("NumderFinal", NumberFinal);
                                CalculationCoin(idOrder);
                                CalculationExp(idOrder);
                            }
                        }

                        CalculationCoin(idOrder);
                    }
                    else if (condition == true)
                    {
                        Debug.Log("Condition :" + condition);
                        int dem = 0;
                        for (int i = 0; i < ManagerItem.Instance.totalItem.Length; i++)
                        {
                            if (i != StypeItem)
                            {
                                idStypeNow[dem] = i;
                                PlayerPrefs.SetInt("IdStypeNow" + dem, idStypeNow[dem]);
                                idItemNow[dem] = ManagerItem.Instance.idItemUnlock[i].IdItem[itemUse[i]];
                                PlayerPrefs.SetInt("IdItemNow" + dem, idItemNow[dem]);
                                dem += 1;
                            }
                        }

                        Combination(idOrder);
                    }
                }
                else if (CheckRun == true) Combination(idOrder);
            }
        }

        private void Combination(int idOrder)
        {
            for (int i = 0; i < NumberCombination.Length; i++)
            {
                if (i == 0 && NumberCombination[i] < 1)
                {
                    Debug.Log("i: " + i);
                    mission[idOrder].metarial = new Metarial[1];
                    PlayerPrefs.SetInt("NumberItem" + idOrder, mission[idOrder].metarial.Length);
                    mission[idOrder].metarial[0].stypeIDYC = StypeItem;
                    PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 0, mission[idOrder].metarial[0].stypeIDYC);
                    mission[idOrder].metarial[0].IdYc = itemUse[StypeItem];
                    PlayerPrefs.SetInt("IdItem" + idOrder + "" + 0, mission[idOrder].metarial[0].IdYc);
                    mission[idOrder].metarial[0].Amount = 3;
                    PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 0, mission[idOrder].metarial[0].Amount);
                    NumberCombination[i] += 1;
                    PlayerPrefs.SetInt("NumberCombination" + i, NumberCombination[i]);
                    CalculationCoin(idOrder);
                    CalculationExp(idOrder);
                    break;
                }

                if (i == 1 && NumberCombination[i] < 3)
                {
                    Debug.Log("i: " + i);
                    bool CheckCondiotion = false;
                    for (int j = NumberCombination[i]; j < idItemNow.Length; j++)
                    {
                        if (ManagerItem.Instance.totalItem[idStypeNow[j]] <= 0)
                        {
                            NumberCombination[i] += 1;
                        }
                        else if (ManagerItem.Instance.totalItem[idStypeNow[j]] > 0)
                        {
                            CheckCondiotion = true;
                            mission[idOrder].metarial = new Metarial[2];
                            PlayerPrefs.SetInt("NumberItem" + idOrder, mission[idOrder].metarial.Length);
                            mission[idOrder].metarial[0].stypeIDYC = StypeItem;
                            PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 0,
                                mission[idOrder].metarial[0].stypeIDYC);
                            mission[idOrder].metarial[0].IdYc =
                                ManagerItem.Instance.idItemUnlock[StypeItem].IdItem[itemUse[StypeItem]];
                            PlayerPrefs.SetInt("IdItem" + idOrder + "" + 0, mission[idOrder].metarial[0].IdYc);
                            mission[idOrder].metarial[0].Amount = 3;
                            PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 1, mission[idOrder].metarial[0].Amount);
                            mission[idOrder].metarial[1].stypeIDYC = idStypeNow[j];
                            PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 1,
                                mission[idOrder].metarial[1].stypeIDYC);
                            mission[idOrder].metarial[1].IdYc = idItemNow[j];
                            PlayerPrefs.SetInt("IdItem" + idOrder + "" + 1, mission[idOrder].metarial[1].IdYc);
                            mission[idOrder].metarial[1].Amount = 3;
                            PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 1, mission[idOrder].metarial[1].Amount);
                            NumberCombination[i] += 1;
                            PlayerPrefs.SetInt("NumberCombination" + i, NumberCombination[i]);
                            CalculationCoin(idOrder);
                            CalculationExp(idOrder);
                            break;
                        }
                    }

                    if (CheckCondiotion == true) break;
                }

                if (i == 2 && NumberCombination[i] < 3)
                {
                    Debug.Log("i: " + i);
                    bool CheckCondition = false;
                    for (int j = NumberCombination[i]; j < idItemNow.Length; j++)
                    {
                        if (j + 1 < idItemNow.Length)
                        {
                            if (ManagerItem.Instance.totalItem[j] <= 0 || ManagerItem.Instance.totalItem[j + 1] <= 0)
                            {
                                NumberCombination[i] += 1;
                            }
                            else if (ManagerItem.Instance.totalItem[idStypeNow[j]] > 0 &&
                                     ManagerItem.Instance.totalItem[idStypeNow[j + 1]] > 0)
                            {
                                CheckCondition = true;
                                mission[idOrder].metarial = new Metarial[3];
                                PlayerPrefs.SetInt("NumberItem" + idOrder, mission[idOrder].metarial.Length);
                                mission[idOrder].metarial[0].stypeIDYC = StypeItem;
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 0,
                                    mission[idOrder].metarial[0].stypeIDYC);
                                mission[idOrder].metarial[0].IdYc = ManagerItem.Instance.idItemUnlock[StypeItem]
                                    .IdItem[itemUse[StypeItem]];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + 0, mission[idOrder].metarial[0].IdYc);
                                mission[idOrder].metarial[0].Amount = 3;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 1,
                                    mission[idOrder].metarial[1].Amount);
                                mission[idOrder].metarial[1].stypeIDYC = idStypeNow[j];
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 1,
                                    mission[idOrder].metarial[1].stypeIDYC);
                                mission[idOrder].metarial[1].IdYc = idItemNow[j];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + 1, mission[idOrder].metarial[1].IdYc);
                                mission[idOrder].metarial[1].Amount = 3;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 1,
                                    mission[idOrder].metarial[1].Amount);
                                mission[idOrder].metarial[2].stypeIDYC = idStypeNow[j + 1];
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 2,
                                    mission[idOrder].metarial[2].stypeIDYC);
                                mission[idOrder].metarial[2].IdYc = idItemNow[j + 1];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + 2, mission[idOrder].metarial[2].IdYc);
                                mission[idOrder].metarial[2].Amount = 3;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 2,
                                    mission[idOrder].metarial[2].Amount);
                                NumberCombination[i] += 1;
                                PlayerPrefs.SetInt("NumberCombination" + i, NumberCombination[i]);
                                CalculationCoin(idOrder);
                                CalculationExp(idOrder);
                                break;
                            }
                        }
                        else if (i + 1 >= idItemNow.Length)
                        {
                            if (ManagerItem.Instance.totalItem[idStypeNow[j]] <= 0 ||
                                ManagerItem.Instance.totalItem[idStypeNow[j + 1 - 3]] <= 0)
                            {
                                NumberCombination[i] += 1;
                            }
                            else if (ManagerItem.Instance.totalItem[j] > 0 && ManagerItem.Instance.totalItem[j + 1] > 0)
                            {
                                CheckCondition = true;
                                mission[idOrder].metarial = new Metarial[3];
                                PlayerPrefs.SetInt("NumberItem" + idOrder, mission[idOrder].metarial.Length);
                                mission[idOrder].metarial[0].stypeIDYC = StypeItem;
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 0,
                                    mission[idOrder].metarial[0].stypeIDYC);
                                mission[idOrder].metarial[0].IdYc = ManagerItem.Instance.idItemUnlock[StypeItem]
                                    .IdItem[itemUse[StypeItem]];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + 0, mission[idOrder].metarial[0].IdYc);
                                mission[idOrder].metarial[0].Amount = 3;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 0,
                                    mission[idOrder].metarial[0].Amount);
                                mission[idOrder].metarial[1].stypeIDYC = idStypeNow[j];
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 1,
                                    mission[idOrder].metarial[1].stypeIDYC);
                                mission[idOrder].metarial[1].IdYc = idItemNow[j];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + 1, mission[idOrder].metarial[1].IdYc);
                                mission[idOrder].metarial[1].Amount = 3;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 1,
                                    mission[idOrder].metarial[1].Amount);
                                mission[idOrder].metarial[2].stypeIDYC = idStypeNow[j + 1 - 3];
                                PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 2,
                                    mission[idOrder].metarial[2].stypeIDYC);
                                mission[idOrder].metarial[2].IdYc = idItemNow[j + 1 - 3];
                                PlayerPrefs.SetInt("IdItem" + idOrder + "" + 2, mission[idOrder].metarial[2].IdYc);
                                mission[idOrder].metarial[2].Amount = 3;
                                PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 2,
                                    mission[idOrder].metarial[2].Amount);
                                NumberCombination[i] += 1;
                                PlayerPrefs.SetInt("NumberCombination" + i, NumberCombination[i]);
                                CalculationCoin(idOrder);
                                CalculationExp(idOrder);
                                break;
                            }
                        }
                    }

                    if (CheckCondition == true) break;
                }

                if (i == 3 && NumberCombination[i] < 1)
                {
                    Debug.Log("i: " + i);
                    bool kt = true;
                    for (int j = 0; j < idStypeNow.Length; j++)
                    {
                        if (ManagerItem.Instance.totalItem[idStypeNow[j]] <= 0)
                        {
                            kt = false;
                            break;
                        }
                    }

                    Debug.Log("kt: " + kt);
                    if (kt == false)
                    {
                        CheckRun = false;
                        PlayerPrefs.SetInt("CheckRun", 0);
                        itemUse[StypeItem] += 1;
                        PlayerPrefs.SetInt("ItemUse" + StypeItem, itemUse[StypeItem]);
                        for (int j = 0; j < NumberCombination.Length; j++)
                        {
                            NumberCombination[j] = 0;
                        }

                        MissionOrder(idOrder);
                    }
                    else if (kt == true)
                    {
                        mission[idOrder].metarial = new Metarial[4];
                        PlayerPrefs.SetInt("NumberItem" + idOrder, mission[idOrder].metarial.Length);
                        mission[idOrder].metarial[0].stypeIDYC = StypeItem;
                        PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + 0, mission[idOrder].metarial[0].stypeIDYC);
                        mission[idOrder].metarial[0].IdYc =
                            ManagerItem.Instance.idItemUnlock[StypeItem].IdItem[itemUse[StypeItem]];
                        PlayerPrefs.SetInt("IdItem" + idOrder + "" + 0, mission[idOrder].metarial[0].IdYc);
                        mission[idOrder].metarial[0].Amount = 3;
                        PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + 0, mission[idOrder].metarial[0].Amount);
                        for (int j = 0; j < idItemNow.Length; j++)
                        {
                            mission[idOrder].metarial[j + 1].stypeIDYC = idStypeNow[j];
                            PlayerPrefs.SetInt("IdStypeItem" + idOrder + "" + (j + 1),
                                mission[idOrder].metarial[j + 1].stypeIDYC);
                            mission[idOrder].metarial[j + 1].IdYc = idItemNow[j];
                            PlayerPrefs.SetInt("IdItem" + idOrder + "" + (j + 1),
                                mission[idOrder].metarial[j + 1].IdYc);
                            mission[idOrder].metarial[j + 1].Amount = 3;
                            PlayerPrefs.SetInt("QuantityItem" + idOrder + "" + (j + 1),
                                mission[idOrder].metarial[j + 1].Amount);
                        }

                        CheckRun = false;
                        itemUse[StypeItem] += 1;
                        PlayerPrefs.SetFloat("ItemUse" + StypeItem, itemUse[StypeItem]);
                        CalculationCoin(idOrder);
                        CalculationExp(idOrder);
                        for (int j = 0; j < NumberCombination.Length; j++)
                        {
                            NumberCombination[j] = 0;
                        }
                    }
                }
            }

            CheckDoneOrder();
        }

        public void CheckDoneOrder()
        {
            for (int i = 0; i < NumberOrder; i++)
            {
                if (statusOrder[i] == 0)
                {
                    if (CheckDone(i) == false)
                    {
                        OrderDone[i] = false;
                        if (OrderTick[i].activeSelf == true) OrderTick[i].SetActive(false);
                        if (TickWordSpace[i].activeSelf == true) TickWordSpace[i].SetActive(false);
                    }
                    else if (CheckDone(i) == true)
                    {
                        OrderDone[i] = true;
                        if (OrderTick[i].activeSelf == false) OrderTick[i].SetActive(true);
                        if (TickWordSpace[i].activeSelf == false) TickWordSpace[i].SetActive(true);
                    }
                }
            }
        }

        private bool CheckDone(int idOrder)
        {
            bool DoneOrder = true;
            if (stypeMisson[idOrder] == 0)
            {
                for (int i = 0; i < missions.mission[idMissionOrder[idOrder]].metarial.Length; i++)
                {
                    int idstype = missions.mission[idMissionOrder[idOrder]].metarial[i].stypeIDYC;
                    int idYC = missions.mission[idMissionOrder[idOrder]].metarial[i].IdYc;
                    int Amount = missions.mission[idMissionOrder[idOrder]].metarial[i].Amount;
                    if (AmountMarket(idstype, idYC) < Amount)
                    {
                        DoneOrder = false;
                        break;
                    }
                }
            }
            else if (stypeMisson[idOrder] == 1)
            {
                for (int i = 0; i < mission[idOrder].metarial.Length; i++)
                {
                    int idstype = mission[idOrder].metarial[i].stypeIDYC;
                    int idYC = mission[idOrder].metarial[i].IdYc;
                    int Amount = mission[idOrder].metarial[i].Amount;
                    if (AmountMarket(idstype, idYC) < Amount)
                    {
                        DoneOrder = false;
                        break;
                    }
                }
            }

            return DoneOrder;
        }

        private int AmountMarket(int idstype, int idYC)
        {
            int amount = 0;
            switch (idstype)
            {
                case 0:
                    amount = ManagerMarket.instance.QuantityItemSeeds[idYC];
                    break;
                case 1:
                    amount = ManagerMarket.instance.QuantityItemPets[idYC];
                    break;
                case 2:
                    amount = ManagerMarket.instance.QuantityItemFactory[idYC];
                    break;
                case 3:
                    amount = ManagerMarket.instance.QuantityItemOldTree[idYC];
                    break;
            }

            return amount;
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

        public void CheckAddOrder(int level)
        {
            if (LevelAddOrder[NumberOrder] == level) AddOrder();
        }

        public void NewItem()
        {
            NumberFinal = 0;
            PlayerPrefs.SetInt("NumderFinal", NumberFinal);
        }

        public void CargoBack()
        {
            Status = 2;
            PlayerPrefs.SetInt("StatusPerForm", Status);
        }

        public void CarFreeTime()
        {
            Status = 0;
            PlayerPrefs.SetInt("StatusPerForm", Status);
        }

        private void CalculationCoin(int idOrder)
        {
            for (int i = 0; i < mission[idOrder].metarial.Length; i++)
            {
                int stypeIDYC = mission[idOrder].metarial[i].stypeIDYC;
                int IdYc = mission[idOrder].metarial[i].IdYc;
                switch (stypeIDYC)
                {
                    case 0:
                        mission[idOrder].coin += (int) (1.5 * mission[idOrder].metarial[i].Amount *
                                                        ManagerData.instance.seeds.SeedDatas[IdYc].sell);
                        break;
                    case 1:
                        mission[idOrder].coin += (int) (1.5 * mission[idOrder].metarial[i].Amount *
                                                        ManagerData.instance.petCollection.Pet[IdYc].itemPet.sell);
                        break;
                    case 2:
                        mission[idOrder].coin += (int) (1.5 * mission[idOrder].metarial[i].Amount *
                                                        ManagerData.instance.facetoryItems.FacetoryItemDatas[IdYc]
                                                            .sell);
                        break;
                    case 3:
                        mission[idOrder].coin += (int) (1.5 * mission[idOrder].metarial[i].Amount *
                                                        ManagerData.instance.trees.data[IdYc].ItemTree.sell);
                        break;
                }
            }

            CoinOrderText[idOrder].text = mission[idOrder].coin.ToString();
        }

        private void CalculationExp(int idOrder)
        {
            for (int i = 0; i < mission[idOrder].metarial.Length; i++)
            {
                int stypeIDYC = mission[idOrder].metarial[i].stypeIDYC;
                int IdYc = mission[idOrder].metarial[i].IdYc;
                switch (stypeIDYC)
                {
                    case 0:
                        mission[idOrder].exp += 2 * mission[idOrder].metarial[i].Amount *
                                                ManagerData.instance.seeds.SeedDatas[IdYc].exp;
                        break;
                    case 1:
                        mission[idOrder].exp += 2 * mission[idOrder].metarial[i].Amount *
                                                ManagerData.instance.petCollection.Pet[IdYc].detailPet.exp;
                        break;
                    case 2:
                        mission[idOrder].exp += 2 * mission[idOrder].metarial[i].Amount *
                                                ManagerData.instance.facetoryItems.FacetoryItemDatas[IdYc].exp;
                        break;
                    case 3:
                        mission[idOrder].exp += 2 * mission[idOrder].metarial[i].Amount *
                                                ManagerData.instance.trees.data[IdYc].ItemTree.exp;
                        break;
                }
            }

            ExpOrderText[idOrder].text = mission[idOrder].exp.ToString();
        }

        private void HideDetailOrder()
        {
            AddressText.text = "";
            CoinMissionText.text = "";
            ExpMissionText.text = "";
            if (SuggestGO.activeSelf == false) SuggestGO.SetActive(true);
            for (int i = 0; i < MissionGO.Length; i++)
            {
                if (MissionGO[i].activeSelf == true) MissionGO[i].SetActive(false);
                MissionText[i].text = "";
                MissionImage[i].sprite = null;
                if (MissionTick[i].activeSelf == true) MissionTick[i].SetActive(false);
                if (MissionGO[i].activeSelf == true) MissionGO[i].SetActive(false);
            }
        }

        public void ButtonUseGem()
        {
            if (CouterClickUseGem == 0)
            {
                CouterClickUseGem += 1;
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Nhấn lại để xác nhận";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Tekan lagi untuk konfirmasi";
                else str = "Tap again to confirm";
                Notification.Instance.dialogBelow(str);
            }
            else if (CouterClickUseGem == 1)
            {
                CouterClickUseGem = 0;
                statusOrder[OrderSelected] = 0;
                PlayerPrefs.SetInt("StatusOrder" + OrderSelected, statusOrder[OrderSelected]);
                StopCoroutine(IETimeNewOrder[OrderSelected]);
                MissionOrder(OrderSelected);
                FinishWaitTime(OrderSelected);
                CheckShowTime = false;
                DetailMission(OrderSelected);
                for (int i = 0; i < StatusMission.Length; i++)
                {
                    if (i == statusOrder[OrderSelected]) StatusMission[i].SetActive(true);
                    else if (i != statusOrder[OrderSelected]) StatusMission[i].SetActive(false);
                }

                ManagerGem.Instance.MunisGem(3);
            }
        }

        private void InitData()
        {
            if (PlayerPrefs.HasKey("NumberOrder") == false)
            {
                PlayerPrefs.SetInt("OrderSelected", OrderSelected);
                PlayerPrefs.SetInt("StatusPerForm", Status);
                PlayerPrefs.SetInt("StypeItem", 0);
                PlayerPrefs.SetInt("CheckRun", 0);
                PlayerPrefs.SetInt("NumderFinal", NumberFinal);
                PlayerPrefs.SetInt("NumberOrder", 0);
                PlayerPrefs.SetInt("MissionFinal", MissionFinal);
                PlayerPrefs.SetInt("CheckPerForm", 0);
                PlayerPrefs.SetInt("OrderPerForm", OrderPerForm);
                for (int i = 0; i < statusOrder.Length; i++)
                {
                    PlayerPrefs.SetInt("StatusOrder" + i, 0);
                }

                for (int i = 0; i < itemUse.Length; i++)
                {
                    PlayerPrefs.SetInt("ItemUse" + i, itemUse[i]);
                }

                for (int i = 0; i < NumberCombination.Length; i++)
                {
                    PlayerPrefs.SetInt("NumberCombination" + i, NumberCombination[i]);
                }

                for (int i = 0; i < idStypeNow.Length; i++)
                {
                    PlayerPrefs.SetInt("IdStypeNow" + i, idStypeNow[i]);
                }

                for (int i = 0; i < idItemNow.Length; i++)
                {
                    PlayerPrefs.SetInt("IdItemNow" + i, idItemNow[i]);
                }
            }
            else if (PlayerPrefs.HasKey("NumberOrder") == true)
            {
                Status = PlayerPrefs.GetInt("StatusPerForm");
                OrderSelected = PlayerPrefs.GetInt("OrderSelected");
                if (PlayerPrefs.GetInt("CheckRun") == 1) CheckRun = true;
                if (PlayerPrefs.GetInt("CheckPerForm") == 1) CheckPerForm = true;
                OrderPerForm = PlayerPrefs.GetInt("OrderPerForm");
                StypeItem = PlayerPrefs.GetInt("StypeItem");
                NumberFinal = PlayerPrefs.GetInt("NumderFinal");
                for (int i = 0; i < itemUse.Length; i++)
                {
                    itemUse[i] = PlayerPrefs.GetInt("ItemUse" + i);
                }

                for (int i = 0; i < NumberCombination.Length; i++)
                {
                    NumberCombination[i] = PlayerPrefs.GetInt("NumberCombination" + i);
                }

                for (int i = 0; i < idStypeNow.Length; i++)
                {
                    idStypeNow[i] = PlayerPrefs.GetInt("IdStypeNow" + i);
                }

                for (int i = 0; i < idItemNow.Length; i++)
                {
                    idItemNow[i] = PlayerPrefs.GetInt("IdItemNow" + i);
                }

                NumberOrder = PlayerPrefs.GetInt("NumberOrder");
                MissionFinal = PlayerPrefs.GetInt("MissionFinal");
                for (int i = 0; i < NumberOrder; i++)
                {
                    statusOrder[i] = PlayerPrefs.GetInt("StatusOrder" + i);
                    if (statusOrder[i] == 0)
                    {
                        stypeMisson[i] = PlayerPrefs.GetInt("StypeMission" + i);
                        if (stypeMisson[i] == 0)
                        {
                            idMissionOrder[i] = PlayerPrefs.GetInt("IdMissionOrder" + i);
                            CoinOrderText[i].text = "" + missions.mission[idMissionOrder[i]].coin;
                            ExpOrderText[i].text = "" + missions.mission[idMissionOrder[i]].exp;
                        }
                        else if (stypeMisson[i] == 1)
                        {
                            int numberitem = PlayerPrefs.GetInt("NumberItem" + i);
                            int adreesOrder = PlayerPrefs.GetInt("AdreesOrder" + i);
                            mission[i].name = Address[adreesOrder].Name;
                            mission[i].engName = Address[adreesOrder].EngName;
                            mission[i].metarial = new Metarial[numberitem];
                            for (int k = 0; k < mission[i].metarial.Length; k++)
                            {
                                mission[i].metarial[k].stypeIDYC = PlayerPrefs.GetInt("IdStypeItem" + i + k);
                                mission[i].metarial[k].IdYc = PlayerPrefs.GetInt("IdItem" + i + k);
                                mission[i].metarial[k].Amount = PlayerPrefs.GetInt("QuantityItem" + i + k);
                            }

                            CalculationCoin(i);
                            CalculationExp(i);
                        }
                    }
                    else if (statusOrder[i] == 1)
                    {
                        int timeNow = ManagerGame.Instance.RealTime();
                        int time = timeNow - PlayerPrefs.GetInt("TimeLastOrder" + i);
                        timeNewOrder[i] = PlayerPrefs.GetInt("TimeLiveOrder" + i) - time;
                        if (timeNewOrder[i] <= 0)
                        {
                            statusOrder[i] = 0;
                            MissionOrder(i);
                        }
                        else if (timeNewOrder[i] > 0)
                        {
                            DeleteOrder(i);
                            IETimeNewOrder[i] = CouterTimeNewOrder(i);
                            StartCoroutine(IETimeNewOrder[i]);
                        }
                    }

                    Order[i].SetActive(true);
                    OrderWordSpace[i].SetActive(true);
                }
            }

            if (CheckPerForm == true) Order[OrderSelected].SetActive(false);
            LoadData = true;
            CheckDoneOrder();
        }
    }


    [System.Serializable]
    public struct idItemUnlock
    {
        public int[] IdItem;
    }

    [System.Serializable]
    public struct DeliveryAddress
    {
        public string Name;
        public string EngName;
        public string InsName;
    }
}