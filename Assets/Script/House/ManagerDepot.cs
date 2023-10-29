using UnityEngine;
using UnityEngine.UI;

public class ManagerDepot : MonoBehaviour
{
    public static ManagerDepot instance;
    private int status;
    private int count;
    private int idUseGem;
    private bool eligible;
    [SerializeField] Text UpdateText;
    [SerializeField] Text NameDepotText;
    [SerializeField] Text IncreaseText;
    [SerializeField] GameObject Depot;
    [SerializeField] GameObject MainDepot;
    [SerializeField] GameObject MainUpdate;
    [SerializeField] GameObject DepotHouse;
    [SerializeField] Text[] ConditionText;
    [SerializeField] Text[] ConditinGemText;
    [SerializeField] Image[] ConditionImage;
    [SerializeField] GameObject[] GemUse;
    [SerializeField] GameObject[] Condition;
    [SerializeField] GameObject[] TickEgiliable;
    [SerializeField] Text[] itemBreads;
    [SerializeField] Text[] itemFactory;
    [SerializeField] Text[] itemItemBuilding;
    [SerializeField] Text[] itemToolDecorate;
    private DetailStoreHouse DataUpdate;
    public int levelDepot
    {
        get { if (!PlayerPrefs.HasKey("LevelDepot")) PlayerPrefs.SetInt("LevelDepot", 0); return PlayerPrefs.GetInt("LevelDepot"); }
        set { PlayerPrefs.SetInt("LevelDepot", value); }
    }
    public int capacity
    {
        get { if (!PlayerPrefs.HasKey("CapacityDepot")) PlayerPrefs.SetInt("CapacityDepot", 60); return PlayerPrefs.GetInt("CapacityDepot"); }
        set { PlayerPrefs.SetInt("CapacityDepot", value); }
    }

    // -----------------------------------------------------

    void Awake()
    {
        DataUpdate.metarial = new MetarialUpdateDepot[3];
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        if (levelDepot < 10)
        {
            if (levelDepot > 0)
            {
                Destroy(DepotHouse);
                DepotHouse = Instantiate(ManagerData.instance.depot.Depot[levelDepot - 1].depot, transform.position, Quaternion.identity, transform);
            }
            DataUpdate.capacity = ManagerData.instance.depot.Depot[levelDepot].capacity;
            DataUpdate.depot = ManagerData.instance.depot.Depot[levelDepot].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                DataUpdate.metarial[i].IdItem = ManagerData.instance.depot.Depot[levelDepot].metarial[i].IdItem;
                DataUpdate.metarial[i].QuantityItem = ManagerData.instance.depot.Depot[levelDepot].metarial[i].QuantityItem;
            }
        }
        else if (levelDepot >= 10)
        {
            DataUpdate.capacity = PlayerPrefs.GetInt("MetarialUpdateDepotCapacity");
            DataUpdate.depot = ManagerData.instance.depot.Depot[9].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                DataUpdate.metarial[i].IdItem = PlayerPrefs.GetInt("MetarialUpdateDepotIdItem" + i);
                DataUpdate.metarial[i].QuantityItem = 25 + (levelDepot - 9) * 5;
            }
            Destroy(DepotHouse);
            DepotHouse = Instantiate(DataUpdate.depot, transform.position, Quaternion.identity, transform);
        }
        ShowNameDepot();
        ButtonUpdateText();
    }
    public void ShowNameDepot()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            NameDepotText.text = "Kho Vật Phẩm " + ManagerMarket.instance.QuantityItemDepot + "/" + ManagerMarket.instance.QuantityTotalItemDepot;
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
            NameDepotText.text = "Penyimpanan Lumbung " + ManagerMarket.instance.QuantityItemDepot + "/" + ManagerMarket.instance.QuantityTotalItemDepot;
        else
            NameDepotText.text = "Depot Item " + ManagerMarket.instance.QuantityItemDepot + "/" + ManagerMarket.instance.QuantityTotalItemDepot;
    }
    private void ButtonUpdateText()
    {
        if (status == 0)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                UpdateText.text = "Nâng Cấp";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                UpdateText.text = "Meningkatkan";
            else UpdateText.text = "Update";
        }
        else if (status == 1)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                UpdateText.text = "Trở về";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                UpdateText.text = "Kembali";
            else UpdateText.text = "Back";
        }
    }
    private void DescriptionText()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            IncreaseText.text = "Tăng Sức Chứa " + DataUpdate.capacity;
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
            IncreaseText.text = "Tingkatkan Penyimpanan menjadi " + DataUpdate.capacity;
        else IncreaseText.text = "Increase Store " + DataUpdate.capacity;
    }
    public void OpenDepot()
    {
        MainCamera.instance.DisableAll();
        MainCamera.instance.lockCam();
        ManagerAudio.instance.PlayAudio(Audio.ClickOpen);
        Depot.SetActive(true);
    }
    public void ExitDepot()
    {
        status = 0;
        Depot.SetActive(false);
        MainDepot.SetActive(true);
        MainUpdate.SetActive(false);
        MainCamera.instance.unLockCam();
        ManagerAudio.instance.PlayAudio(Audio.ClickExit);
        ButtonUpdateText();
    }

    public void ShowQuantity(int stype, int iditem, int amount)
    {
        if (stype == 1) itemBreads[iditem].text = "" + amount;
        else if (stype == 2) itemFactory[iditem].text = "" + amount;
        else if (stype == 4) itemItemBuilding[iditem].text = "" + amount;
        else if (stype == 5) itemToolDecorate[iditem].text = "" + amount;
    }

    public void ButtonUpdate()
    {
        if (status == 0)
        {
            status = 1;
            CheckEligible();
            MainDepot.SetActive(false);
            MainUpdate.SetActive(true);
            ButtonUpdateText();
            DescriptionText();
        }
        else if (status == 1)
        {
            status = 0;
            CheckEligible();
            MainDepot.SetActive(true);
            MainUpdate.SetActive(false);
            ButtonUpdateText();
        }
    }

    private void CheckEligible()
    {
        bool checkEligible = true;
        for (int i = 0; i < Condition.Length; i++)
        {
            if (i < DataUpdate.metarial.Length)
            {
                ConditionImage[i].sprite = ManagerData.instance.itemBuilding.Data[DataUpdate.metarial[i].IdItem].Icon;
                ConditionText[i].text = ManagerMarket.instance.QuantityItemBuilding[DataUpdate.metarial[i].IdItem] + "/" + DataUpdate.metarial[i].QuantityItem;
                if (ManagerMarket.instance.QuantityItemBuilding[DataUpdate.metarial[i].IdItem] < DataUpdate.metarial[i].QuantityItem)
                {
                    checkEligible = false;
                    if (TickEgiliable[i].activeSelf == true) TickEgiliable[i].SetActive(false);
                    int TotalGem = DataUpdate.metarial[i].QuantityItem - ManagerMarket.instance.QuantityItemBuilding[DataUpdate.metarial[i].IdItem];
                    ConditinGemText[i].text = "" + TotalGem * ManagerData.instance.itemBuilding.Data[DataUpdate.metarial[i].IdItem].Purchase;
                    GemUse[i].SetActive(true);
                }
                else
                {
                    if (GemUse[i].activeSelf == true) GemUse[i].SetActive(false);
                    if (TickEgiliable[i].activeSelf == false) TickEgiliable[i].SetActive(true);
                }
                if (Condition[i].activeSelf == false) Condition[i].SetActive(true);
            }
            else { if (Condition[i].activeSelf == true) Condition[i].SetActive(false); }
        }
        eligible = checkEligible;
    }

    public void BtnBackMainTower()
    {
        MainDepot.SetActive(true);
        MainUpdate.SetActive(false);
        ManagerAudio.instance.PlayAudio(Audio.Click);
    }

    public void ButtonYesUpdate()
    {
        if (eligible == false)
        {
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Thiếu dung cụ hoặc vật vật liệu nâng cấp";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Bahan peningkatan kurang";
            else str = "Failed to upgrade tool or physical materials";
            Notification.instance.dialogBelow(str);
        }
        else if (eligible == true)
        {
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                ManagerMarket.instance.MinusItem(4, DataUpdate.metarial[i].IdItem, DataUpdate.metarial[i].QuantityItem);
            }
            capacity = DataUpdate.capacity;
            Debug.Log(capacity);
            Destroy(DepotHouse);
            DepotHouse = Instantiate(DataUpdate.depot, transform.position, Quaternion.identity, transform);
            levelDepot += 1;
            Debug.Log(levelDepot);
            UpdateMetarial();
            CheckEligible();
            ManagerMarket.instance.UpgradeQuantityItemDepot();
            ShowNameDepot();
            DescriptionText();
        }
    }

    public void UseGemDown(int id)
    {
        GemUse[id].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void UseGemUp(int id)
    {
        GemUse[id].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        if (id == idUseGem)
        {
            if (count == 0)
            {
                count = 1;
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Nhấn lại để xác nhận";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Tekan lagi untuk konfirmasi";
                else str = "Tap again to confirm";
                Notification.instance.dialogBelow(str);
            }
            else if (count == 1)
            {
                count = 0;
                int TotalMiss = DataUpdate.metarial[id].QuantityItem - ManagerMarket.instance.QuantityItemBuilding[DataUpdate.metarial[id].IdItem];
                int TotalGem = TotalMiss * ManagerData.instance.itemBuilding.Data[DataUpdate.metarial[id].IdItem].Purchase;
                if (ManagerGem.instance.GemLive >= TotalGem)
                {
                    ManagerMarket.instance.ReciveItem(4, DataUpdate.metarial[id].IdItem, TotalMiss, false);
                    CheckEligible();
                    ManagerGem.instance.MunisGem(TotalGem);
                }
                else if (ManagerGem.instance.GemLive < TotalGem)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Bạn không có đủ Gem";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Anda tidak memiliki cukup Gem";
                    else str = "You dont have enough Gem";
                    Notification.instance.dialogBelow(str);
                }
            }
        }
        else if (id != idUseGem)
        {
            count = 1;
            idUseGem = id;
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Nhấn lại để xác nhận";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Tekan lagi untuk konfirmasi";
            else str = "Tap again to confirm";
            Notification.instance.dialogBelow(str);
        }
    }

    private void UpdateMetarial()
    {
        if (levelDepot < 10)
        {
            DataUpdate.capacity = ManagerData.instance.depot.Depot[levelDepot].capacity;
            DataUpdate.depot = ManagerData.instance.depot.Depot[levelDepot].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                DataUpdate.metarial[i].IdItem = ManagerData.instance.depot.Depot[levelDepot].metarial[i].IdItem;
                DataUpdate.metarial[i].QuantityItem = ManagerData.instance.depot.Depot[levelDepot].metarial[i].QuantityItem;
            }
        }
        else if (levelDepot >= 10)
        {
            DataUpdate.capacity += 50;
            PlayerPrefs.SetInt("MetarialUpdateDepotCapacity", DataUpdate.capacity);
            DataUpdate.depot = ManagerData.instance.depot.Depot[9].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                int randomIdItem = Random.Range(0, 6);
                while (CheckLoop(i, randomIdItem) == false) { randomIdItem = Random.Range(0, 6); }
                DataUpdate.metarial[i].IdItem = randomIdItem;
                PlayerPrefs.SetInt("MetarialUpdateDepotIdItem" + i, DataUpdate.metarial[i].IdItem);
                DataUpdate.metarial[i].QuantityItem = 25 + (levelDepot - 9) * 5;
            }
        }
    }

    private bool CheckLoop(int possion, int value)
    {
        bool condition = true;
        for (int i = 0; i < possion; i++)
        {
            if (DataUpdate.metarial[i].IdItem == value) { condition = false; break; }
        }
        return condition;
    }
}
