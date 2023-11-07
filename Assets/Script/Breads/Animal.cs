using UnityEngine;
using System.Collections;
using NongTrai;
using Spine.Unity;
public class Animal : MonoBehaviour
{
    [SerializeField] int idAmountAnimal;
    private int idBread;
    private int idAmountBreads;
    private int status;
    private int timelive;
    private SkeletonAnimation ske;
    private Vector3 camfirstPos;
    private bool dragging;
    private bool isClick;
    [SerializeField] Audio audio;

    // ------------------------------------------------------------

    void Start()
    {
        ske = GetComponent<SkeletonAnimation>();
        idBread = transform.parent.GetChild(0).GetComponent<HomeAnimal>().idHomeAnimal;
        idAmountBreads = transform.parent.GetChild(0).GetComponent<HomeAnimal>().idAmountHome;

        if (PlayerPrefs.HasKey("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) == true)
        {
            if (PlayerPrefs.GetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) == 0)
            {
                status = 0;
                ske.AnimationName = "doi";
            }
            else if (PlayerPrefs.GetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) == 1)
            {
                status = 1;
                ske.AnimationName = "an";
                int timeNow = ManagerGame.instance.RealTime();
                int time = timeNow - PlayerPrefs.GetInt("TimeLastAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal);
                timelive = PlayerPrefs.GetInt("TimeLiveAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) - time;
                StartCoroutine(countTime());
            }
            else if (PlayerPrefs.GetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) == 2)
            {
                status = 2;
                ske.AnimationName = "lo";
            }
        }
        else if (PlayerPrefs.HasKey("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) == false)
        {
            PlayerPrefs.SetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, status);
        }
    }

    void OnMouseDown()
    {
        isClick = true;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(transform.localScale.x + 0.0002f, transform.localScale.y + 0.0002f, 1f);
    }

    void OnMouseDrag()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f && isClick == true)
        {
            isClick = false;
            transform.localScale = new Vector3(transform.localScale.x - 0.0002f, transform.localScale.y - 0.0002f, 1f);
        }
    }

    void OnMouseUp()
    {
        if (isClick == true)
        {
            isClick = false;
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                ManagerAudio.instance.PlayAudio(audio);
                transform.localScale = new Vector3(transform.localScale.x - 0.0002f, transform.localScale.y - 0.0002f, 1f);
                string nameItem = Application.systemLanguage == SystemLanguage.Vietnamese
                        ? ManagerData.instance.petCollection.Pet[idBread].detailPet.name : ManagerData.instance.petCollection.Pet[idBread].detailPet.engName;
                int totalTime = ManagerData.instance.petCollection.Pet[idBread].detailPet.time;
                Vector3 target = new Vector3(transform.position.x, transform.position.y - 0.2f, 0);
                if (status == 0)
                {
                    MainCamera.instance.DisableAll();
                    ManagerTool.instance.showToolCage(idBread, transform.parent.position);
                    ManagerTool.instance.RegisterShowClock(1, idBread, idAmountBreads, idAmountAnimal, nameItem, target, gameObject);
                    ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
                    if (ManagerGuide.instance.GuideClickPetsBuyChicken == 0) { ManagerGuide.instance.GuideClickPetsBuyChicken = 1; ManagerGuide.instance.DoneArrowPetChicken(); }
                    if (idBread == 0 && idAmountBreads == 0 && ManagerGuide.instance.GuideClickChicken == 0) ManagerGuide.instance.GuideClickChicken = 1;
                    if (ManagerGuide.instance.GuideClickChicken == 1 && ManagerGuide.instance.GuideClickFoodChicken == 0) ManagerGuide.instance.CallArrowFoodsChicken();
                }
                else if (status == 1)
                {
                    MainCamera.instance.DisableAll();
                    ManagerTool.instance.showToolCage(idBread, transform.parent.position);
                    ManagerTool.instance.RegisterShowClock(1, idBread, idAmountBreads, idAmountAnimal, nameItem, target, gameObject);
                    ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
                }
                else if (status == 2)
                {
                    MainCamera.instance.DisableAll();
                    ManagerTool.instance.showToolCage(idBread, transform.parent.position);
                    ManagerTool.instance.RegisterShowClock(1, idBread, idAmountBreads, idAmountAnimal, nameItem, target, gameObject);
                    ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "AnimalFeeds" && ManagerTool.instance.dragging == true)
        {
            if (idBread == ManagerMarket.instance.idAnimalFood && status == 0)
            {
                int idItemFactory = ManagerMarket.instance.idItemFactoryAnimalUse;
                if (ManagerMarket.instance.QuantityItemFactory[idItemFactory] > 0)
                {
                    status = 1;
                    PlayerPrefs.SetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, 1);
                    ske.AnimationName = "an";
                    ManagerMarket.instance.MinusItem(2, ManagerMarket.instance.idItemFactoryAnimalUse, 1);
                    timelive = ManagerData.instance.petCollection.Pet[idBread].detailPet.time;
                    PlayerPrefs.SetInt("TimeLiveAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, timelive);
                    PlayerPrefs.SetInt("TimeLastAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, ManagerGame.instance.RealTime());
                    StartCoroutine(countTime());
                    Sprite spr = ManagerData.instance.facetoryItems.FacetoryItemDatas[ManagerMarket.instance.idItemFactoryAnimalUse].item;
                    Vector3 possition = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
                    ManagerTool.instance.RegisterEatOne(1, spr, possition);
                    if (ManagerGuide.instance.GuideClickFoodChicken == 0) ManagerGuide.instance.GuideClickFoodChicken = 1;
                }
                else if (ManagerMarket.instance.QuantityItemFactory[idItemFactory] <= 0)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Hết thức ăn";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Kehabisan makanan";
                    else str = "No food left";
                    Notification.instance.dialogBelow(str);
                }
            }
        }

        if (other.tag == "HarvestBread" && status == 2 && ManagerTool.instance.idToodHarvestBread == idBread && ManagerTool.instance.dragging == true)
        {
            if (ManagerMarket.instance.QuantityItemDepot < ManagerMarket.instance.QuantityTotalItemDepot)
            {
                status = 0;
                PlayerPrefs.SetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, 0);
                ManagerMarket.instance.ReciveItem(1, idBread, ManagerData.instance.petCollection.Pet[idBread].detailPet.product, true);
                Sprite spr = ManagerData.instance.petCollection.Pet[idBread].itemPet.item;
                int exp = ManagerData.instance.petCollection.Pet[idBread].detailPet.exp;
                int product = ManagerData.instance.petCollection.Pet[idBread].detailPet.product;
                Vector3 possition = transform.position;
                Experience.instance.registerExp(spr, exp, product, possition);
                ske.AnimationName = "doi";
                if (ManagerTool.instance.showClock.CheckShow == true)
                {
                    if (ManagerTool.instance.showClock.CheckStype[1] == true)
                        if (ManagerTool.instance.showClock.Product == idBread)
                            if (ManagerTool.instance.showClock.IdProduct == idAmountBreads)
                                if (ManagerTool.instance.showClock.IdShow == idAmountAnimal)
                                {
                                    int totalTime = ManagerData.instance.petCollection.Pet[idBread].detailPet.time;
                                    ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
                                }
                }
                if (ManagerGame.instance.RandomItem() == true)
                {
                    int IdItemBuilding = Random.Range(0, 6);
                    ManagerMarket.instance.ReciveItem(4, IdItemBuilding, 1, false);
                    Sprite sprIcon = ManagerData.instance.itemBuildings.Data[IdItemBuilding].Icon;
                    ManagerTool.instance.RegisterItemSingle(1, sprIcon, transform.position);
                }
            }
            else
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Kho vật phẩm đầy!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Lumbung penuh";
                else str = "Depot Full!";
                Notification.instance.dialogBetween(str);
                Notification.instance.dialogDepot();
            }
        }
    }

    IEnumerator countTime()
    {
        yield return new WaitForSeconds(1f);
        timelive -= 1;
        PlayerPrefs.SetInt("TimeLiveAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, timelive);
        PlayerPrefs.SetInt("TimeLastAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, ManagerGame.instance.RealTime());
        if (timelive <= 0)
        {
            status = 2;
            PlayerPrefs.SetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, 2);
            ske.AnimationName = "lo";
            if (ManagerTool.instance.showClock.CheckShow == true)
            {
                if (ManagerTool.instance.showClock.CheckStype[1] == true)
                    if (ManagerTool.instance.showClock.Product == idBread)
                        if (ManagerTool.instance.showClock.IdProduct == idAmountBreads)
                            if (ManagerTool.instance.showClock.IdShow == idAmountAnimal)
                            {
                                int totalTime = ManagerData.instance.petCollection.Pet[idBread].detailPet.time;
                                ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
                            }
            }
        }
        else if (timelive > 0)
        {
            if (ManagerTool.instance.showClock.CheckShow == true)
            {
                if (ManagerTool.instance.showClock.CheckStype[1] == true)
                    if (ManagerTool.instance.showClock.Product == idBread)
                        if (ManagerTool.instance.showClock.IdProduct == idAmountBreads)
                            if (ManagerTool.instance.showClock.IdShow == idAmountAnimal)
                            {
                                int totalTime = ManagerData.instance.petCollection.Pet[idBread].detailPet.time;
                                ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
                            }
            }
            StartCoroutine(countTime());
        }
    }

    public void UseDiamond()
    {
        timelive = 0;
        PlayerPrefs.SetInt("TimeLiveAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, timelive);
        status = 2;
        PlayerPrefs.SetInt("StatusAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal, 2);
        ske.AnimationName = "lo";
        int totalTime = ManagerData.instance.petCollection.Pet[idBread].detailPet.time;
        ManagerTool.instance.ShowClockBread(status, timelive, totalTime);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == true)
        {
            if (status == 1)
            {
                int timeNow = ManagerGame.instance.RealTime();
                int time = timeNow - PlayerPrefs.GetInt("TimeLastAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal);
                timelive = PlayerPrefs.GetInt("TimeLiveAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) - time;
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == false)
        {
            if (status == 1)
            {
                int timeNow = ManagerGame.instance.RealTime();
                int time = timeNow - PlayerPrefs.GetInt("TimeLastAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal);
                timelive = PlayerPrefs.GetInt("TimeLiveAnimal" + idBread + "" + idAmountBreads + "" + idAmountAnimal) - time;
            }
        }
    }
}
