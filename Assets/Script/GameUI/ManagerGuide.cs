using UnityEngine;

public class ManagerGuide : MonoBehaviour
{
    public static ManagerGuide instance;
    private Vector3 TargetCageChicken;
    [SerializeField] Transform TfCageChicken;
    [SerializeField] Transform TfFoodsChicken;
    [SerializeField] Transform TfSeedsRice;
    [SerializeField] GameObject ArrowDown;
    [SerializeField] GameObject ArrowDownIncuneLeft;
    [SerializeField] GameObject ArrowDownIncuneRigt;
    [SerializeField] GameObject ArrowShop;
    [SerializeField] GameObject ArrowShopPet;
    [SerializeField] GameObject ArrowPetsChicken;
    [SerializeField] Transform[] TfField;
    private GameObject obj;
    public bool ArrowLive;
    public int GuideClickFieldHavestCrop
    {
        get { if (PlayerPrefs.HasKey("GuideClickFieldHavestCrop") == false) PlayerPrefs.SetInt("GuideClickFieldHavestCrop", 0); return PlayerPrefs.GetInt("GuideClickFieldHavestCrop"); }
        set { PlayerPrefs.SetInt("GuideClickFieldHavestCrop", value); }
    }
    public int GuideClickCutting
    {
        get { if (PlayerPrefs.HasKey("GuideClickCutting") == false) PlayerPrefs.SetInt("GuideClickCutting", 0); return PlayerPrefs.GetInt("GuideClickCutting"); }
        set { PlayerPrefs.SetInt("GuideClickCutting", value); }
    }
    public int GuideClickFieldCrop
    {
        get { if (PlayerPrefs.HasKey("GuideClickFieldCrop") == false) PlayerPrefs.SetInt("GuideClickFieldCrop", 0); return PlayerPrefs.GetInt("GuideClickFieldCrop"); }
        set { PlayerPrefs.SetInt("GuideClickFieldCrop", value); }
    }
    public int GuideClickSeeds
    {
        get { if (PlayerPrefs.HasKey("GuideClickSeeds") == false) PlayerPrefs.SetInt("GuideClickSeeds", 0); return PlayerPrefs.GetInt("GuideClickSeeds"); }
        set { PlayerPrefs.SetInt("GuideClickSeeds", value); }
    }
    public int MoveCameraCageChicken
    {
        get { if (PlayerPrefs.HasKey("MoveCameraCageChicken") == false) PlayerPrefs.SetInt("MoveCameraCageChicken", 0); return PlayerPrefs.GetInt("MoveCameraCageChicken"); }
        set { PlayerPrefs.SetInt("MoveCameraCageChicken", value); }
    }
    public int GuideClickCageChicken
    {
        get { if (PlayerPrefs.HasKey("GuideClickCageChicken") == false) PlayerPrefs.SetInt("GuideClickCageChicken", 0); return PlayerPrefs.GetInt("GuideClickCageChicken"); }
        set { PlayerPrefs.SetInt("GuideClickCageChicken", value); }
    }
    public int GuideClickShopBuyChicken
    {
        get { if (PlayerPrefs.HasKey("GuideClickShopBuyChicken") == false) PlayerPrefs.SetInt("GuideClickShopBuyChicken", 0); return PlayerPrefs.GetInt("GuideClickShopBuyChicken"); }
        set { PlayerPrefs.SetInt("GuideClickShopBuyChicken", value); }
    }
    public int GuideClickShopPetsBuyChicken
    {
        get { if (PlayerPrefs.HasKey("GuideClickShopPetsBuyChicken") == false) PlayerPrefs.SetInt("GuideClickShopPetsBuyChicken", 0); return PlayerPrefs.GetInt("GuideClickShopPetsBuyChicken"); }
        set { PlayerPrefs.SetInt("GuideClickShopPetsBuyChicken", value); }
    }
    public int GuideClickPetsBuyChicken
    {
        get { if (PlayerPrefs.HasKey("GuideClickPetsBuyChicken") == false) PlayerPrefs.SetInt("GuideClickPetsBuyChicken", 0); return PlayerPrefs.GetInt("GuideClickPetsBuyChicken"); }
        set { PlayerPrefs.SetInt("GuideClickPetsBuyChicken", value); }
    }
    public int GuideClickChicken
    {
        get { if (PlayerPrefs.HasKey("GuideClickChicken") == false) PlayerPrefs.SetInt("GuideClickChicken", 0); return PlayerPrefs.GetInt("GuideClickChicken"); }
        set { PlayerPrefs.SetInt("GuideClickChicken", value); }
    }
    public int GuideClickFoodChicken
    {
        get { if (PlayerPrefs.HasKey("GuideClickFoodChicken") == false) PlayerPrefs.SetInt("GuideClickFoodChicken", 0); return PlayerPrefs.GetInt("GuideClickFoodChicken"); }
        set { PlayerPrefs.SetInt("GuideClickFoodChicken", value); }
    }

    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start()
    {
        TargetCageChicken = TfCageChicken.position;
    }

    public void CallArrowDown(Vector3 target)
    {
        if (ArrowLive == false)
        {
            ArrowLive = true;
            obj = Instantiate(ArrowDown, target, Quaternion.identity);
        }
        else if (ArrowLive == true)
        {
            Destroy(obj);
            obj = Instantiate(ArrowDown, target, Quaternion.identity);
        }
    }

    public void CallArrowDownIncuneLeft(Vector3 target)
    {
        if (ArrowLive == false)
        {
            ArrowLive = true;
            obj = Instantiate(ArrowDownIncuneLeft, target, Quaternion.identity);
        }
        else if (ArrowLive == true)
        {
            Destroy(obj);
            obj = Instantiate(ArrowDownIncuneLeft, target, Quaternion.identity);
        }
    }

    public void CallArrowDownIncuneRight(Vector3 target)
    {
        if (ArrowLive == false)
        {
            ArrowLive = true;
            obj = Instantiate(ArrowDownIncuneRigt, target, Quaternion.identity);
        }
        else if (ArrowLive == true)
        {
            Destroy(obj);
            obj = Instantiate(ArrowDownIncuneRigt, target, Quaternion.identity);
        }
    }

    public void CallArrowField()
    {
        Vector3 target = TfField[0].position;
        CallArrowDown(target);
    }

    public void CallMoveCameraCageChicken()
    {
        MoveCameraCageChicken = 1;
        Vector3 target = TfCageChicken.transform.position;
        MainCamera.instance.MoveCameraTarget(target);
    }

    public void CallArrowShop()
    {
        ArrowShop.SetActive(true);
    }

    public void CallArrowShopPet()
    {
        ArrowShopPet.SetActive(true);
    }

    public void CallArrowPetChicken()
    {
        ArrowPetsChicken.SetActive(true);
    }

    public void DoneArrowShop()
    {
        ArrowShop.SetActive(false);
    }

    public void DoneArrowShopPet()
    {
        ArrowShopPet.SetActive(false);
    }

    public void DoneArrowPetChicken()
    {
        ArrowPetsChicken.SetActive(false);
    }

    public void CallArrowCageChicken()
    {
        CallArrowDown(TargetCageChicken);
    }

    public void CallArrowFoodsChicken()
    {
        Vector3 target = TfFoodsChicken.position;
        CallArrowDownIncuneRight(target);
    }

    public void CallArrowFieldEat()
    {
        bool CheckDone = true;
        for (int i = 0; i < TfField.Length; i++)
        {
            if (GetArrowField(i) == 0)
            {
                Vector3 target = TfField[i].position;
                CallArrowDown(target);
                CheckDone = false;
                break;
            }
        }
        if (CheckDone == true)
        {
            DoneGuide();
            GuideClickSeeds = 1;
        }
    }

    public void CallArowSeedsRice()
    {
        Vector3 target = TfSeedsRice.position;
        CallArrowDownIncuneRight(target);
    }

    public int GetArrowField(int id)
    {
        if (PlayerPrefs.HasKey("ArrowField" + id) == false) PlayerPrefs.SetInt("ArrowField" + id, 0);
        return PlayerPrefs.GetInt("ArrowField" + id);
    }

    public void SetArrowField(int id, int valua)
    {
        PlayerPrefs.SetInt("ArrowField" + id, valua);
    }

    public void DoneGuide()
    {
        Destroy(obj);
    }
}
