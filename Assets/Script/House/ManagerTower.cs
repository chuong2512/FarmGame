using UnityEngine;
using UnityEngine.UI;

public class ManagerTower : MonoBehaviour
{
    public static ManagerTower instance;
    private int status;
    private int count;
    private int idUseGem;
    private bool eligible;
    [SerializeField] Text UpdateText;
    [SerializeField] Text NameTowerText;
    [SerializeField] Text IncreaseText;
    [SerializeField] GameObject Tower;
    [SerializeField] GameObject MainTower;
    [SerializeField] GameObject MainUpdate;
    [SerializeField] GameObject TowerHouse;
    [SerializeField] Text[] ConditionText;
    [SerializeField] Text[] ConditinGemText;
    [SerializeField] Image[] ConditionImage;
    [SerializeField] GameObject[] GemUse;
    [SerializeField] GameObject[] Condition;
    [SerializeField] GameObject[] TickEgiliable;
    [SerializeField] Text[] itemCrops;
    [SerializeField] Text[] itemOldTrees;
    [SerializeField] Text[] itemFlowers;
    private DetailStoreHouse DataUpdate;
    public int levelTower
    {
        get { if (!PlayerPrefs.HasKey("LevelTower")) PlayerPrefs.SetInt("LevelTower", 0); return PlayerPrefs.GetInt("LevelTower"); }
        set { PlayerPrefs.SetInt("LevelTower", value); }
    }
    public int capacity
    {
        get { if (!PlayerPrefs.HasKey("CapacityTower")) PlayerPrefs.SetInt("CapacityTower", 60); return PlayerPrefs.GetInt("CapacityTower"); }
        set { PlayerPrefs.SetInt("CapacityTower", value); }
    }
    // --------------------------------------------------
    void Awake()
    {
        DataUpdate.metarial = new MetarialUpdateDepot[3];
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        if (levelTower < 10)
        {
            if (levelTower > 0)
            {
                Destroy(TowerHouse);
                TowerHouse = Instantiate(ManagerData.instance.tower.Tower[levelTower - 1].depot, transform.position, Quaternion.identity, transform);
            }
            DataUpdate.capacity = ManagerData.instance.tower.Tower[levelTower].capacity;
            DataUpdate.depot = ManagerData.instance.tower.Tower[levelTower].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                DataUpdate.metarial[i].IdItem = ManagerData.instance.tower.Tower[levelTower].metarial[i].IdItem;
                DataUpdate.metarial[i].QuantityItem = ManagerData.instance.tower.Tower[levelTower].metarial[i].QuantityItem;
            }
        }
        else if (levelTower >= 10)
        {
            DataUpdate.capacity = PlayerPrefs.GetInt("MetarialUpdateTowerCapacity");
            DataUpdate.depot = ManagerData.instance.tower.Tower[9].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                DataUpdate.metarial[i].IdItem = PlayerPrefs.GetInt("MetarialUpdateTowerIdItem" + i);
                DataUpdate.metarial[i].QuantityItem = 30 + (levelTower - 9) * 3;
            }
            Destroy(TowerHouse);
            TowerHouse = Instantiate(DataUpdate.depot, transform.position, Quaternion.identity, transform);
        }
        ShowNameTower();
        ButtonUpdateText();
    }
    public void ShowNameTower()
    {
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
            NameTowerText.text = "Tháp Nông Sản " + ManagerMarket.instance.QuantityItemTower + "/" + ManagerMarket.instance.QuantityTotalItemTower;
        else if (Application.systemLanguage == SystemLanguage.Indonesian)
            NameTowerText.text = "Penyimpanan Silo " + ManagerMarket.instance.QuantityItemTower + "/" + ManagerMarket.instance.QuantityTotalItemTower;
        else
            NameTowerText.text = "Farm Item " + ManagerMarket.instance.QuantityItemTower + "/" + ManagerMarket.instance.QuantityTotalItemTower;
    }
    public void OpenTower()
    {
        MainCamera.instance.DisableAll();
        MainCamera.instance.lockCam();
        ManagerAudio.instance.PlayAudio(Audio.ClickOpen);
        Tower.SetActive(true);
    }

    public void ExitTower()
    {
        status = 0;
        Tower.SetActive(false);
        MainTower.SetActive(true);
        MainUpdate.SetActive(false);
        MainCamera.instance.unLockCam();
        ManagerAudio.instance.PlayAudio(Audio.ClickExit);
        ButtonUpdateText();
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
    public void ShowQuantity(int stype, int iditem, int quantity)
    {
        if (stype == 0) itemCrops[iditem].text = "" + quantity;
        else if (stype == 3) itemOldTrees[iditem].text = "" + quantity;
        else if (stype == 6) itemFlowers[iditem].text = "" + quantity;
    }

    public void ButtonUpdate()
    {
        if (status == 0)
        {
            status = 1;
            CheckEligible();
            MainTower.SetActive(false);
            MainUpdate.SetActive(true);
            ButtonUpdateText();
            DescriptionText();
        }
        else if (status == 1)
        {
            status = 0;
            CheckEligible();
            MainTower.SetActive(true);
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
                    if (checkEligible == true) checkEligible = false;
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
            Destroy(TowerHouse);
            TowerHouse = Instantiate(DataUpdate.depot, transform.position, Quaternion.identity, transform);
            levelTower += 1;
            UpdateMetarial();
            CheckEligible();
            ManagerMarket.instance.UpgradeQuantityItemTower();
            ShowNameTower();
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
                if (Gem.instance.GemLive >= TotalGem)
                {
                    ManagerMarket.instance.ReciveItem(4, DataUpdate.metarial[id].IdItem, TotalMiss, false);
                    CheckEligible();
                    Gem.instance.MunisGem(TotalGem);
                }
                else if (Gem.instance.GemLive < TotalGem)
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
        if (levelTower < 10)
        {
            DataUpdate.capacity = ManagerData.instance.tower.Tower[levelTower].capacity;
            DataUpdate.depot = ManagerData.instance.tower.Tower[levelTower].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                DataUpdate.metarial[i].IdItem = ManagerData.instance.tower.Tower[levelTower].metarial[i].IdItem;
                DataUpdate.metarial[i].QuantityItem = ManagerData.instance.tower.Tower[levelTower].metarial[i].QuantityItem;
            }
        }
        else if (levelTower >= 10)
        {
            DataUpdate.capacity += 50;
            PlayerPrefs.SetInt("MetarialUpdateTowerCapacity", DataUpdate.capacity);
            DataUpdate.depot = ManagerData.instance.tower.Tower[9].depot;
            for (int i = 0; i < DataUpdate.metarial.Length; i++)
            {
                int randomIdItem = Random.Range(0, 6);
                while (CheckLoop(i, randomIdItem) == false) { randomIdItem = Random.Range(0, 6); }
                DataUpdate.metarial[i].IdItem = randomIdItem;
                PlayerPrefs.SetInt("MetarialUpdateTowerIdItem" + i, DataUpdate.metarial[i].IdItem);
                DataUpdate.metarial[i].QuantityItem = 30 + (levelTower - 9) * 3;
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
