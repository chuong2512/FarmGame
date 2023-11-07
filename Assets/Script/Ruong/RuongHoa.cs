using UnityEngine;
using System.Collections;
using NongTrai;

public class RuongHoa : MonoBehaviour
{
    private int status;
    private int idFlower;
    private int timeLive;
    private bool dragging;
    [SerializeField] int idPOL;
    [SerializeField] int idRuongHoa;
    private Vector3 camfirstPos;
    private IEnumerator IETimeLive;
    public SpriteRenderer sprRenderer;
    public SpriteRenderer sprRendererCrop;
    //-------------------------------------------------------------
    void Start()
    {
        if (PlayerPrefs.HasKey("StatusFieldFlower" + idPOL + "" + idRuongHoa))
        {
            status = PlayerPrefs.GetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa);
            if (status == 1)
            {
                status = 1;
                idFlower = PlayerPrefs.GetInt("IdSeedFieldFlower" + idPOL + "" + idRuongHoa);
                int timeNow = ManagerGame.instance.RealTime();
                int time = timeNow - PlayerPrefs.GetInt("TimeLastFieldFlower" + idPOL + "" + idRuongHoa);
                timeLive = PlayerPrefs.GetInt("TimeLiveFieldFlower" + idPOL + "" + idRuongHoa) - time;
                if (timeLive > ManagerData.instance.flowers.Data[idFlower].detailFlower.time * 3 / 4)
                    sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop1;
                else if (timeLive <= ManagerData.instance.flowers.Data[idFlower].detailFlower.time * 3 / 4 && timeLive > ManagerData.instance.seeds.SeedDatas[idFlower].time * 1 / 4)
                    sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop2;
                else if (timeLive <= ManagerData.instance.flowers.Data[idFlower].detailFlower.time * 1 / 4 && timeLive > 0)
                    sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop3;
                StartCoroutine(countTime());
            }
            else if (status == 2)
            {
                status = 2;
                idFlower = PlayerPrefs.GetInt("IdSeedFieldFlower" + idPOL + "" + idRuongHoa);
                sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop4;
            }
        }
        else if (PlayerPrefs.HasKey("StatusFieldFlower" + idPOL + "" + idRuongHoa) == false)
        {
            if (idPOL == 0)
            {
                status = 2;
                sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop4;
                PlayerPrefs.SetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa, status);
                PlayerPrefs.SetInt("IdSeedFieldFlower" + idPOL + "" + idRuongHoa, 0);
                PlayerPrefs.SetInt("TimeLastFieldFlower" + idPOL + "" + idRuongHoa, 0);
                PlayerPrefs.SetInt("TimeLiveFieldFlower" + idPOL + "" + idRuongHoa, 0);
            }
            else if (idPOL > 0)
            {
                PlayerPrefs.SetInt("StatusField" + idPOL + "" + idRuongHoa, 0);
                PlayerPrefs.SetInt("IdSeedField" + idPOL + "" + idRuongHoa, 0);
                PlayerPrefs.SetInt("TimeLastField" + idPOL + "" + idRuongHoa, 0);
                PlayerPrefs.SetInt("TimeLiveField" + idPOL + "" + idRuongHoa, 0);
            }
        }
        Order();
    }

    public void Order()
    {
        int order = (int)(transform.position.y * (-100));
        sprRenderer.sortingOrder = order;
        sprRendererCrop.sortingOrder = order + 1;
    }

    private void ColorS(float r, float g, float b, float a)
    {
        sprRenderer.color = new Color(r, g, b, a);
        sprRendererCrop.color = new Color(r, g, b, a);
    }

    void OnMouseDown()
    {
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ColorS(0.3f, 0.3f, 0.3f, 1f);
    }

    void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
            {
                dragging = true;
                ColorS(1f, 1f, 1f, 1f);
            }
        }
    }

    void OnMouseUp()
    {
        if (dragging == false)
        {
            ColorS(1f, 1f, 1f, 1f);
            if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                switch (status)
                {
                    case 0:
                        ManagerAudio.instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        ManagerTool.instance.idPOL = idPOL;
                        ManagerTool.instance.idRuongHoa = idRuongHoa;
                        ManagerTool.instance.OpenToolFlowers(transform.position);
                        break;
                    case 1:
                        ManagerAudio.instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        string nameItem;
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            nameItem = ManagerData.instance.flowers.Data[idFlower].detailFlower.name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            nameItem = ManagerData.instance.flowers.Data[idFlower].detailFlower.nameINS;
                        else nameItem = ManagerData.instance.flowers.Data[idFlower].detailFlower.engName;
                        int totalTime = ManagerData.instance.flowers.Data[idFlower].detailFlower.time;
                        ManagerTool.instance.RegisterShowClock(4, 0, idPOL, idRuongHoa, nameItem, transform.position, gameObject);
                        ManagerTool.instance.ShowClockCropFlower(timeLive, totalTime);
                        break;
                    case 2:
                        ManagerAudio.instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        ManagerTool.instance.idPOL = idPOL;
                        ManagerTool.instance.idRuongHoa = idRuongHoa;
                        ManagerTool.instance.showToolHarvestCrops(transform.position);
                        break;
                }
                if (ManagerTool.instance.ClickUseGemBuySeed != 0) ManagerTool.instance.ClickUseGemBuySeed = 0;
            }
        }
        else if (dragging == true) dragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FlowerSeeds" && ManagerTool.instance.dragging == true && status == 0)
        {
            if (idRuongHoa == ManagerTool.instance.idRuongHoa)
            {
                int idFlower = ManagerTool.instance.idFlower;
                if (ManagerMarket.instance.QuantityItemFlower[idFlower] > 0) PlantTree();
                else if (ManagerMarket.instance.QuantityItemFlower[idFlower] <= 0)
                    ManagerUseGem.instance.RegisterUseGemBuySeeds(StypeUseGemBuySeed.Flower, idFlower, gameObject);
            }
            if (ManagerTool.instance.checkCollider == true)
            {
                int idFlower = ManagerTool.instance.idFlower;
                if (ManagerMarket.instance.QuantityItemFlower[idFlower] > 0) PlantTree();
            }
        }
        if (other.tag == "ToolHarvestCrop" && ManagerTool.instance.dragging == true && status == 2)
        {
            if (idRuongHoa == ManagerTool.instance.idRuongHoa && idPOL == ManagerTool.instance.idPOL)
            {
                int quantity = ManagerData.instance.flowers.Data[idFlower].detailFlower.quantity;
                if (ManagerMarket.instance.QuantityItemTower + quantity <= ManagerMarket.instance.QuantityTotalItemTower)
                {
                    Harvest();
                }
                else if (ManagerMarket.instance.QuantityItemTower + quantity > ManagerMarket.instance.QuantityTotalItemTower)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Vật phẩm nông trại đầy!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Silo penuh!";
                    else str = "Farm Item Full!";
                    Notification.instance.dialogBetween(str);
                    Notification.instance.dialogTower();
                }
            }
            if (ManagerTool.instance.checkCollider == true)
            {
                int quantity = ManagerData.instance.flowers.Data[idFlower].detailFlower.quantity;
                if (ManagerMarket.instance.QuantityItemTower + quantity <= ManagerMarket.instance.QuantityTotalItemTower)
                {
                    Harvest();
                }
                else if (ManagerMarket.instance.QuantityItemTower + quantity > ManagerMarket.instance.QuantityTotalItemTower)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Vật phẩm nông trại đầy!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Silo penuh!";
                    else str = "Farm Item Full!";
                    Notification.instance.dialogBetween(str);
                    Notification.instance.dialogTower();
                }
            }
        }
    }

    private void PlantTree()
    {
        if (status == 0)
        {
            status = 1;
            PlayerPrefs.SetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa, 1);
            if (ManagerTool.instance.checkCollider == false) ManagerTool.instance.checkCollider = true;
            ManagerTool.instance.CloseBorderField();
            idFlower = ManagerTool.instance.idFlower;
            PlayerPrefs.SetInt("IdSeedFieldFlower" + idPOL + "" + idRuongHoa, idFlower);
            sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop1;
            ManagerMarket.instance.MinusItem(6, idFlower, 1);
            ManagerMarket.instance.RecieveCropFlower(idFlower);
            ManagerTool.instance.UpdateQuantityFlower();
            timeLive = ManagerData.instance.flowers.Data[idFlower].detailFlower.time;
            IETimeLive = countTime();
            StartCoroutine(IETimeLive);
            ManagerTool.instance.RegisterEatOne(1, ManagerData.instance.flowers.Data[idFlower].detailFlower.iconStore, transform.position);
        }
    }

    private void Harvest()
    {
        status = 0;
        PlayerPrefs.SetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa, 0);
        if (ManagerTool.instance.checkCollider == false) ManagerTool.instance.checkCollider = true;
        ManagerTool.instance.CloseBorderField();
        sprRendererCrop.sprite = null;
        Sprite spr = ManagerData.instance.flowers.Data[idFlower].detailFlower.iconStore;
        int exp = ManagerData.instance.flowers.Data[idFlower].detailFlower.exp;
        int quantity = ManagerData.instance.flowers.Data[idFlower].detailFlower.quantity;
        ManagerMarket.instance.ReciveItem(6, idFlower, quantity, true);
        ManagerMarket.instance.MinusCropsFlower(idFlower);
        Experience.instance.registerExp(spr, exp, quantity, transform.position);
        if (ManagerGame.instance.RandomItem() == true)
        {
            int IdItemBuilding = Random.Range(0, 6);
            ManagerMarket.instance.ReciveItem(4, IdItemBuilding, 1, false);
            Sprite sprIcon = ManagerData.instance.itemBuildings.Data[IdItemBuilding].Icon;
            ManagerTool.instance.RegisterItemSingle(1, sprIcon, transform.position);
        }
    }
    IEnumerator countTime()
    {
        yield return new WaitForSeconds(1f);
        timeLive -= 1;
        PlayerPrefs.SetInt("TimeLiveFieldFlower" + idPOL + "" + idRuongHoa, timeLive);
        PlayerPrefs.SetInt("TimeLastFieldFlower" + idPOL + "" + idRuongHoa, ManagerGame.instance.RealTime());
        if (timeLive == ManagerData.instance.flowers.Data[idFlower].detailFlower.time * 3 / 4)
            sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop2;
        if (timeLive == ManagerData.instance.flowers.Data[idFlower].detailFlower.time / 4)
            sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop3;
        if (timeLive <= 0)
        {
            status = 2;
            PlayerPrefs.SetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa, 2);
            sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop4;
        }
        if (ManagerTool.instance.showClock.CheckShow == true)
            if (ManagerTool.instance.showClock.CheckStype[4] == true)
                if (ManagerTool.instance.showClock.IdProduct == idPOL)
                    if (ManagerTool.instance.showClock.IdShow == idRuongHoa)
                    {
                        int totalTime = ManagerData.instance.flowers.Data[idFlower].detailFlower.time;
                        ManagerTool.instance.ShowClockCrop(timeLive, totalTime);
                    }
        if (timeLive > 0)
        {
            IETimeLive = countTime();
            StartCoroutine(IETimeLive);
        }
    }

    public void UseDiamond()
    {
        StopCoroutine(IETimeLive);
        timeLive = 0;
        PlayerPrefs.SetInt("TimeLiveFieldFlower" + idPOL + "" + idRuongHoa, timeLive);
        status = 2;
        PlayerPrefs.SetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa, 2);
        sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop4;
        int totalTime = ManagerData.instance.flowers.Data[idFlower].detailFlower.time;
        ManagerTool.instance.ShowClockCrop(timeLive, totalTime);
    }

    public void UseGemBuySeeds()
    {
        if (ManagerGem.instance.GemLive >= 3)
        {
            status = 1;
            PlayerPrefs.SetInt("StatusFieldFlower" + idPOL + "" + idRuongHoa, 1);
            ManagerTool.instance.CloseBorderField();
            idFlower = ManagerTool.instance.idFlower;
            PlayerPrefs.SetInt("IdSeedFieldFlower" + idPOL + "" + idRuongHoa, idFlower);
            ManagerRuong.instance.CompleteCrop();
            sprRendererCrop.sprite = ManagerData.instance.flowers.Data[idFlower].detailFlower.crop1;
            ManagerMarket.instance.RecieveCropFlower(idFlower);
            timeLive = ManagerData.instance.flowers.Data[idFlower].detailFlower.time;
            IETimeLive = countTime();
            StartCoroutine(IETimeLive);
            ManagerTool.instance.RegisterEatOne(1, ManagerData.instance.flowers.Data[idFlower].detailFlower.iconStore, transform.position);
            ManagerGem.instance.MunisGem(3);
        }
        else if (ManagerGem.instance.GemLive < 3)
        {
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Bạn không đủ gem!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Anda tidak memiliki cukup permata!";
            else str = "You haven't enough gem!";
            Notification.instance.dialogBelow(str);
        }
    }
    public void DontUseGemBuySeeds()
    {
        ManagerTool.instance.OpenToolFlowers(transform.position);
    }
}
