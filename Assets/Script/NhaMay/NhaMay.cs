using UnityEngine;
using System.Collections;
using NongTrai;
using UnityEngine.UI;

public class NhaMay : MonoBehaviour
{
    private int NumberMFTFinal;
    private int NumberMFT;
    private int CountMFT;
    private int CountDone;
    private int CountConfirmMFT;
    private int CountConfirmGem;
    private int countOverlap;
    private int GemWU;
    private bool isRunIE;
    private bool itemStay;
    private bool dragging;
    private Vector3 oldPos;
    private Vector3 camfirstPos;
    private Rigidbody2D rgb2D;
    private IEnumerator onDrag;
    private IEnumerator IETime;
    private float distanceX;
    private float distanceY;

    private int[] IdItemMFT;
    private int[] idItemDone;

    private Text LightingText;
    private Text[] GemMTFText;
    private Text[] EmptyMFTText;
    private Image[] IconItemMFTImage;
    private GameObject[] ItemMTF;
    private GameObject[] ButtonItemMTF;
    private Image[] IconItemDone;

    [SerializeField] int status;
    [SerializeField] int idNhaMay;
    [SerializeField] int NumberMFTBegin;

    [SerializeField] Animator Ani;
    [SerializeField] Text nameItemFirst;
    [SerializeField] Text timeItemFirst;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject barefoot;
    [SerializeField] GameObject Lighting;
    [SerializeField] GameObject ContentItemMTF;
    [SerializeField] GameObject ContentItemDone;
    [SerializeField] ParticleSystemRenderer psRendererSmoke;
    [SerializeField] OrderPro[] orderFactory;

    [HideInInspector] public int idSoNhaMay;
    [HideInInspector] public bool overlap;
    void Start()
    {
        distanceX = ManagerGame.instance.DistaneX;
        distanceY = ManagerGame.instance.DistaneY;

        InitData();
        Order();
    }

    public void Order()
    {
        int order = (int)(transform.position.y * (-100));
        psRendererSmoke.sortingOrder = order + 1;
        for (int i = 0; i < orderFactory.Length; i++)
        {
            for (int k = 0; k < orderFactory[i].SprRenderer.Length; k++)
            {
                orderFactory[i].SprRenderer[k].sortingOrder = order + orderFactory[i].order;
            }
        }
    }

    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < orderFactory.Length; i++)
        {
            for (int k = 0; k < orderFactory[i].SprRenderer.Length; k++)
            {
                orderFactory[i].SprRenderer[k].color = new Color(r, g, b, a);
            }
        }
    }

    public void StartMove()
    {
        dragging = true;
        psRendererSmoke.sortingLayerName = "Move";
        for (int i = 0; i < orderFactory.Length; i++)
        {
            for (int k = 0; k < orderFactory[i].SprRenderer.Length; k++)
            {
                orderFactory[i].SprRenderer[k].sortingLayerName = "Move";
            }
        }
        rgb2D = barefoot.AddComponent<Rigidbody2D>();
        rgb2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void DoneMove()
    {
        dragging = false;
        psRendererSmoke.sortingLayerName = "Default";
        for (int i = 0; i < orderFactory.Length; i++)
        {
            for (int k = 0; k < orderFactory[i].SprRenderer.Length; k++)
            {
                orderFactory[i].SprRenderer[k].sortingLayerName = "Default";
            }
        }
        Order();
        Destroy(rgb2D);
    }

    void OnMouseDown()
    {
        ColorS(0.3f, 0.3f, 0.3f, 1f);
        oldPos = transform.position;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        onDrag = waitDrag();
        StartCoroutine(onDrag);
    }

    IEnumerator waitDrag()
    {
        isRunIE = true;
        yield return new WaitForSeconds(0.1f);
        Vector3 target = new Vector3(camfirstPos.x, camfirstPos.y + 0.2f, 0);
        ManagerTool.instance.RegisterTimeMove(target);
        yield return new WaitForSeconds(0.55f);
        if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
        {
            MainCamera.instance.DisableAll();
            MainCamera.instance.lockCam();
            ManagerTool.instance.CloseTimeMove();
            ColorS(1f, 1f, 1f, 1f);
            StartMove();
        }
    }

    void OnMouseDrag()
    {
        if (dragging == true)
        {
            Vector2 PosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 target = new Vector2(((int)(PosCam.x / distanceX)) * distanceX, ((int)(PosCam.y / distanceY)) * distanceY);
            transform.position = target;
            Order();
        }
        else if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
        {
            if (isRunIE == true)
            {
                isRunIE = false;
                StopCoroutine(onDrag);
                ColorS(1f, 1f, 1f, 1f);
                ManagerTool.instance.CloseTimeMove();
            }
        }
    }

    void OnMouseUp()
    {
        if (isRunIE == true)
        {
            isRunIE = false;
            StopCoroutine(onDrag);
            ManagerTool.instance.CloseTimeMove();
        }

        if (dragging == false)
        {
            ColorS(1f, 1f, 1f, 1f);
            if (CountConfirmMFT != 0) CountConfirmMFT = 0;
            if (CountConfirmGem != 0) CountConfirmGem = 0;
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                switch (status)
                {
                    case 0:
                        ManagerAudio.instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        ManagerTool.instance.idFactory = idSoNhaMay;
                        ContentItemMTF.SetActive(true);
                        Ani.SetTrigger("IsClick");
                        ManagerTool.instance.ShowToolFactory(idNhaMay, transform.position);
                        ManagerTool.instance.ShowedItemFactory(ContentItemMTF);
                        break;
                    case 1:
                        ManagerAudio.instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        ManagerTool.instance.idFactory = idSoNhaMay;
                        ContentItemMTF.SetActive(true);
                        ManagerTool.instance.ShowToolFactory(idNhaMay, transform.position);
                        ManagerTool.instance.ShowedItemFactory(ContentItemMTF);
                        break;
                    case 2:
                        ManagerAudio.instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        int quantiy = ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone - 1]].quantity;
                        if ((ManagerMarket.instance.QuantityItemDepot + quantiy) <= ManagerMarket.instance.QuantityTotalItemDepot)
                        {
                            if (CountDone > 1)
                            {
                                CountDone -= 1;
                                PlayerPrefs.SetInt("CountDone" + idNhaMay + "" + idSoNhaMay, CountDone);
                                Experience.instance.registerExp(ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].item, ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].exp, ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].quantity, IconItemDone[CountDone].transform.position);
                                ManagerMarket.instance.ReciveItem(2, idItemDone[CountDone], quantiy, true);
                                idItemDone[CountDone] = 0;
                                PlayerPrefs.SetInt("IdItemDone" + idNhaMay + "" + idSoNhaMay + "" + CountDone, 0);
                                IconItemDone[CountDone].gameObject.SetActive(false);
                                if (CountDone == idItemDone.Length - 1 && CountMFT > 0)
                                {
                                    status = 2;
                                    PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, 0);
                                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                        nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].name;
                                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                        nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].nameINS;
                                    else nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].engName;
                                    int timeLive = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].time;
                                    timeItemFirst.text = ManagerGame.instance.TimeText(timeLive);
                                    LightingText.text = ManagerGame.instance.CalcalutorGem(timeLive).ToString();
                                    Lighting.SetActive(true);
                                    if (idNhaMay < 6) smoke.SetActive(true);
                                    if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsRun");
                                    IETime = timeLiveFirstItem(timeLive);
                                    StartCoroutine(IETime);
                                }
                            }
                            else if (CountDone == 1)
                            {
                                if (CountMFT > 0) status = 1;
                                else if (CountMFT == 0) status = 0;
                                PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, status);
                                CountDone = CountDone - 1;
                                PlayerPrefs.SetInt("CountDone" + idNhaMay + "" + idSoNhaMay, CountDone);
                                Experience.instance.registerExp(ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].item, ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].exp, ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].quantity, IconItemDone[CountDone].transform.position);
                                ManagerMarket.instance.ReciveItem(2, idItemDone[CountDone], ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[CountDone]].quantity, true);
                                idItemDone[CountDone] = 0;
                                PlayerPrefs.SetInt("IdItemDone" + idNhaMay + "" + idSoNhaMay + "" + CountDone, 0);
                                IconItemDone[CountDone].gameObject.SetActive(false);
                            }
                        }
                        else if ((ManagerMarket.instance.QuantityItemDepot + quantiy) > ManagerMarket.instance.QuantityTotalItemDepot)
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
                        break;
                }
            }
        }
        else if (dragging == true)
        {
            if (overlap == false)
            {
                PlayerPrefs.SetFloat("PosFactoryX" + idNhaMay + "" + idSoNhaMay, transform.position.x);
                PlayerPrefs.SetFloat("PosFactoryY" + idNhaMay + "" + idSoNhaMay, transform.position.y);
            }
            else if (overlap == true)
            {
                overlap = false;
                ColorS(1f, 1f, 1f, 1f);
                transform.position = oldPos;
            }
            MainCamera.instance.unLockCam();
            countOverlap = 0;
            DoneMove();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ItemNhaMay" && ManagerTool.instance.idTypeFactory == idNhaMay && ManagerTool.instance.idFactory == idSoNhaMay)
        {
            if (ManagerTool.instance.dragging == true)
            {
                if (CountMFT < NumberMFTFinal)
                {
                    itemStay = true;
                    IconItemMFTImage[CountMFT].sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[ManagerTool.instance.idItem].item;
                    IconItemMFTImage[CountMFT].color = Color.white;
                    EmptyMFTText[CountMFT].text = "";
                    if (CountMFT == 0)
                    {
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[ManagerTool.instance.idItem].name;
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[ManagerTool.instance.idItem].nameINS;
                        else nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[ManagerTool.instance.idItem].engName;
                        int timeLive = ManagerData.instance.facetoryItems.FacetoryItemDatas[ManagerTool.instance.idItem].time;
                        timeItemFirst.text = ManagerGame.instance.TimeText(timeLive);
                    }
                }
                else itemStay = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "ItemNhaMay" && ManagerTool.instance.idTypeFactory == idNhaMay && ManagerTool.instance.idFactory == idSoNhaMay)
        {
            if (ManagerTool.instance.dragging == true)
            {
                if (itemStay == true)
                {
                    itemStay = false;
                    if (CountMFT < NumberMFTFinal)
                    {
                        EmptyMFTText[CountMFT].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                        IconItemMFTImage[CountMFT].color = new Color(1f, 1f, 1f, 0f);
                        if (CountMFT == 0)
                        {
                            nameItemFirst.text = "";
                            timeItemFirst.text = "";
                        }
                    }
                }
            }
            else if (ManagerTool.instance.dragging == false)
            {
                if (itemStay == true)
                {
                    itemStay = false;
                    if (CountMFT < NumberMFTFinal)
                    {
                        if (CheckCondition(ManagerTool.instance.idItem) == true)
                        {
                            IdItemMFT[CountMFT] = ManagerTool.instance.idItem;
                            PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + CountMFT, IdItemMFT[CountMFT]);
                            if (CountMFT == 0)
                            {
                                int timeLive = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].time;
                                timeItemFirst.text = ManagerGame.instance.TimeText(timeLive);
                                LightingText.text = ManagerGame.instance.CalcalutorGem(timeLive).ToString();
                                Lighting.SetActive(true);
                                if (CountDone < idItemDone.Length)
                                {
                                    status = 1;
                                    PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, status);
                                    if (idNhaMay < 6) smoke.SetActive(true);
                                    if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsRun");
                                    IETime = timeLiveFirstItem(timeLive);
                                    StartCoroutine(IETime);
                                }
                            }
                            RegisterSxComplete(IdItemMFT[CountMFT]);
                            CountMFT += 1;
                            PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, CountMFT);
                        }
                        else if (CheckCondition(ManagerTool.instance.idItem) == false)
                        {
                            string str;
                            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                str = "Không đủ nguyên liệu!";
                            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                str = "Bahan baku tidak cukup!";
                            else str = "Not enough raw materials!";
                            Notification.instance.dialogBelow(str);
                            EmptyMFTText[CountMFT].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                            IconItemMFTImage[CountMFT].color = new Color(1f, 1f, 1f, 0f);
                            if (CountMFT == 0)
                            {
                                nameItemFirst.text = "";
                                timeItemFirst.text = "";
                            }
                        }
                    }
                    else if (CountMFT >= NumberMFTFinal)
                    {
                        string str;
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            str = "Các ô sản xuất đây. Chờ, tăng tốc, hoặc mua thêm ô mới!";
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            str = "Semua slot produksi penuh. Tunggu, percepat, atau beli slot baru!";
                        else str = "All production slots are full. Wait, speed up or buy new slots!";
                        Notification.instance.dialogBelow(str);
                    }
                }
            }
        }
    }

    IEnumerator timeLiveFirstItem(int timeLive)
    {
        yield return new WaitForSeconds(1f);
        timeLive = timeLive - 1;
        PlayerPrefs.SetInt("TimeLiveFactory" + idNhaMay + "" + idSoNhaMay, timeLive);
        PlayerPrefs.SetInt("TimeLastFactory" + idNhaMay + "" + idSoNhaMay, ManagerGame.instance.RealTime());
        timeItemFirst.text = ManagerGame.instance.TimeText(timeLive);
        GemWU = ManagerGame.instance.CalcalutorGem(timeLive);
        LightingText.text = GemWU.ToString();
        if (timeLive > 0)
        {
            IETime = timeLiveFirstItem(timeLive);
            StartCoroutine(IETime);
        }
        else if (timeLive <= 0)
        {
            status = 2;
            PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, status);
            idItemDone[CountDone] = IdItemMFT[0];
            PlayerPrefs.SetInt("IdItemDone" + idNhaMay + "" + idSoNhaMay + "" + CountDone, idItemDone[CountDone]);
            IconItemDone[CountDone].sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].item;
            IconItemDone[CountDone].gameObject.SetActive(true);
            CountDone += 1;
            PlayerPrefs.SetInt("CountDone" + idNhaMay + "" + idSoNhaMay, CountDone);
            if (CountMFT > 1)
            {
                CountMFT = CountMFT - 1;
                PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, CountMFT);
                for (int i = 0; i < CountMFT + 1; i++)
                {
                    if (i < CountMFT)
                    {
                        IdItemMFT[i] = IdItemMFT[i + 1];
                        PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i, IdItemMFT[i]);
                        IconItemMFTImage[i].sprite = IconItemMFTImage[i + 1].sprite;
                    }
                    else if (i == CountMFT)
                    {
                        IdItemMFT[i] = 0;
                        PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i, IdItemMFT[i]);
                        IconItemMFTImage[i].sprite = null;
                        IconItemMFTImage[i].color = new Color(1f, 1f, 1f, 0f);
                        EmptyMFTText[i].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                    }
                }
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].nameINS;
                else nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].engName;
                int time = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].time;
                timeItemFirst.text = ManagerGame.instance.TimeText(time);
                if (CountDone < idItemDone.Length)
                {
                    IETime = timeLiveFirstItem(time);
                    StartCoroutine(IETime);
                }
                else if (CountDone >= idItemDone.Length)
                {
                    if (CountMFT == 0)
                    {
                        nameItemFirst.text = "";
                        timeItemFirst.text = "";
                    }
                    Lighting.SetActive(false);
                    if (idNhaMay < 6) smoke.SetActive(false);
                    if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsFinish");
                }
            }
            else if (CountMFT == 1)
            {
                CountMFT -= 1;
                PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, CountMFT);
                EmptyMFTText[CountMFT].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                IconItemMFTImage[CountMFT].color = new Color(1f, 1f, 1f, 0f);
                if (CountMFT == 0)
                {
                    nameItemFirst.text = "";
                    timeItemFirst.text = "";
                }
                Lighting.SetActive(false);
                if (idNhaMay < 6) smoke.SetActive(false);
                if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsFinish");
            }
        }
    }

    public void onTriggerStay2D()
    {
        if (dragging == true)
        {
            countOverlap += 1;
            if (countOverlap == 1)
            {
                overlap = true;
                ColorS(1f, 127f / 255, 127f / 255, 1f);
            }
        }
    }

    public void onTriggerExit2D()
    {
        if (dragging == true)
        {
            countOverlap -= 1;
            if (countOverlap == 0)
            {
                overlap = false;
                ColorS(1f, 1f, 1f, 1f);
            }
        }
    }

    private bool CheckCondition(int id)
    {
        bool condition = true;
        for (int i = 0; i < ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial.Length; i++)
        {
            if (ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].stypeIDYC == 0)
            {
                int AmountSeeds = ManagerMarket.instance.QuantityItemSeeds[ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].IdYc];
                int AmountSeedsRequire = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].Amount;
                if (AmountSeeds < AmountSeedsRequire) condition = false;
            }
            else if (ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].stypeIDYC == 1)
            {
                int AmountPets = ManagerMarket.instance.QuantityItemPets[ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].IdYc];
                int AmountPetsRequire = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].Amount;
                if (AmountPets < AmountPetsRequire) condition = false;
            }
            else if (ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].stypeIDYC == 2)
            {
                int AmountItemFactory = ManagerMarket.instance.QuantityItemFactory[ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].IdYc];
                int AmountItemFactoryRequire = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].Amount;
                if (AmountItemFactory < AmountItemFactoryRequire) condition = false;
            }
            else if (ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].stypeIDYC == 3)
            {
                int AmountItemFactory = ManagerMarket.instance.QuantityItemOldTree[ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].IdYc];
                int AmountItemFactoryRequire = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].Amount;
                if (AmountItemFactory < AmountItemFactoryRequire) condition = false;
            }
        }
        return condition;
    }

    void RegisterSxComplete(int id)
    {
        for (int i = 0; i < ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial.Length; i++)
        {
            int idStype = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].stypeIDYC;
            int idYc = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].IdYc;
            int amount = ManagerData.instance.facetoryItems.FacetoryItemDatas[id].metarial[i].Amount;
            ManagerMarket.instance.MinusItem(idStype, idYc, amount);
        }
    }

    public void ButtonMFT()
    {
        if (CountConfirmMFT == 0)
        {
            CountConfirmMFT += 1;
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Nhấn thêm một lần nữa để xác nhận!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Ketuk Sekali lagi untuk mengonfirmasi! ";
            else str = "Press one more to confirm!";
            Notification.instance.dialogBelow(str);
        }
        else if (CountConfirmMFT == 1)
        {
            CountConfirmMFT = 0;
            if (ManagerGem.instance.GemLive >= NumberMFTFinal * 3)
            {
                ManagerGem.instance.MunisGem(NumberMFTFinal * 3);
                if (NumberMFTFinal < NumberMFT - 1)
                {
                    ButtonItemMTF[NumberMFTFinal].SetActive(false);
                    IconItemMFTImage[NumberMFTFinal].gameObject.SetActive(true);
                    NumberMFTFinal += 1;
                    PlayerPrefs.SetInt("NumberMFTFinal" + idNhaMay + "" + idSoNhaMay, NumberMFTFinal);
                    ItemMTF[NumberMFTFinal].SetActive(true);
                }
                else if (NumberMFTFinal >= NumberMFT - 1)
                {
                    ButtonItemMTF[NumberMFTFinal].SetActive(false);
                    IconItemMFTImage[NumberMFTFinal].gameObject.SetActive(true);
                }
            }
            else if (ManagerGem.instance.GemLive < NumberMFTFinal * 3)
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
    }

    public void ButtonLighting()
    {
        if (CountConfirmGem == 0)
        {
            CountConfirmGem += 1;
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Nhấn thêm một lần nữa để xác nhận!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Ketuk Sekali lagi untuk mengonfirmasi! ";
            else str = "Press one more to confirm!";
            Notification.instance.dialogBelow(str);
        }
        else if (CountConfirmGem == 1)
        {
            Debug.Log(ManagerTool.instance.idTypeFactory);
            Debug.Log(ManagerTool.instance.idFactory);
            CountConfirmGem = 0;
            if (ManagerGem.instance.GemLive >= GemWU)
            {
                StopCoroutine(IETime);
                ManagerGem.instance.MunisGem(GemWU);
                status = 2;
                PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, status);
                idItemDone[CountDone] = IdItemMFT[0];
                PlayerPrefs.SetInt("IdItemDone" + idNhaMay + "" + idSoNhaMay + "" + CountDone, idItemDone[CountDone]);
                IconItemDone[CountDone].sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].item;
                IconItemDone[CountDone].gameObject.SetActive(true);
                CountDone += 1;
                PlayerPrefs.SetInt("CountDone" + idNhaMay + "" + idSoNhaMay, CountDone);
                if (CountMFT > 1)
                {
                    CountMFT = CountMFT - 1;
                    PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, CountMFT);
                    for (int i = 0; i < CountMFT + 1; i++)
                    {
                        if (i < CountMFT)
                        {
                            IdItemMFT[i] = IdItemMFT[i + 1];
                            PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i, IdItemMFT[i]);
                            IconItemMFTImage[i].sprite = IconItemMFTImage[i + 1].sprite;
                        }
                        else if (i == CountMFT)
                        {
                            IdItemMFT[i] = 0;
                            PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i, IdItemMFT[i]);
                            IconItemMFTImage[i].sprite = null;
                            IconItemMFTImage[i].color = new Color(1f, 1f, 1f, 0f);
                            EmptyMFTText[i].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                        }
                    }
                    nameItemFirst.text = Application.systemLanguage == SystemLanguage.Vietnamese
                        ? ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].name
                        : ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].engName;
                    int time = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].time;
                    timeItemFirst.text = ManagerGame.instance.TimeText(time);
                    if (CountDone < idItemDone.Length)
                    {
                        IETime = timeLiveFirstItem(time);
                        StartCoroutine(IETime);
                    }
                    else if (CountDone >= idItemDone.Length)
                    {
                        if (CountMFT == 0)
                        {
                            nameItemFirst.text = "";
                            timeItemFirst.text = "";
                        }
                        Lighting.SetActive(false);
                        if (idNhaMay < 6) smoke.SetActive(false);
                        if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsFinish");
                    }
                }
                else if (CountMFT == 1)
                {
                    CountMFT -= 1;
                    PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, CountMFT);
                    EmptyMFTText[CountMFT].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                    IconItemMFTImage[CountMFT].color = new Color(1f, 1f, 1f, 0f);
                    if (CountMFT == 0)
                    {
                        nameItemFirst.text = "";
                        timeItemFirst.text = "";
                    }
                    Lighting.SetActive(false);
                    if (idNhaMay < 6) smoke.SetActive(false);
                    if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsFinish");
                }
            }
            else if (ManagerGem.instance.GemLive < GemWU)
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
    }

    private void InitData()
    {
        NumberMFT = ContentItemMTF.transform.childCount;
        IdItemMFT = new int[NumberMFT];
        idItemDone = new int[NumberMFT];
        ItemMTF = new GameObject[NumberMFT];
        IconItemMFTImage = new Image[NumberMFT];
        ButtonItemMTF = new GameObject[NumberMFT];
        EmptyMFTText = new Text[NumberMFT];
        GemMTFText = new Text[NumberMFT];
        IconItemDone = new Image[NumberMFT];
        LightingText = Lighting.transform.GetChild(0).GetComponent<Text>();

        for (int i = 0; i < NumberMFT; i++)
        {
            ItemMTF[i] = ContentItemMTF.transform.GetChild(i).gameObject;
            IconItemMFTImage[i] = ContentItemMTF.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            EmptyMFTText[i] = ContentItemMTF.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
            ButtonItemMTF[i] = ContentItemMTF.transform.GetChild(i).GetChild(1).gameObject;
            GemMTFText[i] = ContentItemMTF.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>();
            EmptyMFTText[i].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
            GemMTFText[i].text = (i * 3).ToString();
        }

        for (int i = 0; i < ContentItemDone.transform.childCount; i++)
            IconItemDone[i] = ContentItemDone.transform.GetChild(i).GetComponent<Image>();

        if (PlayerPrefs.HasKey("StatusFactory" + idNhaMay + "" + idSoNhaMay) == true)
        {
            status = PlayerPrefs.GetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay);
            CountMFT = PlayerPrefs.GetInt("CountMFT" + idNhaMay + "" + idSoNhaMay);
            CountDone = PlayerPrefs.GetInt("CountDone" + idNhaMay + "" + idSoNhaMay);
            NumberMFTFinal = PlayerPrefs.GetInt("NumberMFTFinal" + idNhaMay + "" + idSoNhaMay);
            for (int i = 0; i < NumberMFT; i++)
            {
                if (i < NumberMFTFinal)
                {
                    if (IconItemMFTImage[i].gameObject.activeSelf == false) IconItemMFTImage[i].gameObject.SetActive(true);
                    if (ButtonItemMTF[i].activeSelf == true) ButtonItemMTF[i].SetActive(false);
                    if (ItemMTF[i].activeSelf == false) ItemMTF[i].SetActive(true);
                }
                else if (i == NumberMFTFinal)
                {
                    if (IconItemMFTImage[i].gameObject.activeSelf == true) IconItemMFTImage[i].gameObject.SetActive(false);
                    if (ButtonItemMTF[i].activeSelf == false) ButtonItemMTF[i].SetActive(true);
                    if (ItemMTF[i].activeSelf == false) ItemMTF[i].SetActive(true);
                }
                else if (i > NumberMFTFinal)
                {
                    if (IconItemMFTImage[i].gameObject.activeSelf == true) IconItemMFTImage[i].gameObject.SetActive(false);
                    if (ButtonItemMTF[i].activeSelf == false) ButtonItemMTF[i].SetActive(true);
                    if (ItemMTF[i].activeSelf == true) ItemMTF[i].SetActive(false);
                }
            }

            for (int i = 0; i < CountMFT; i++)
            {
                IdItemMFT[i] = PlayerPrefs.GetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i);
                IconItemMFTImage[i].sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[i]].item;
                IconItemMFTImage[i].color = Color.white;
                EmptyMFTText[i].text = "";
            }

            for (int i = 0; i < CountDone; i++)
            {
                idItemDone[i] = PlayerPrefs.GetInt("IdItemDone" + idNhaMay + "" + idSoNhaMay + "" + i);
                IconItemDone[i].sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[idItemDone[i]].item;
                IconItemDone[i].gameObject.SetActive(true);
            }

            int timeNow = ManagerGame.instance.RealTime();
            int time = timeNow - PlayerPrefs.GetInt("TimeLastFactory" + idNhaMay + "" + idSoNhaMay);
            int timeFactory = time - PlayerPrefs.GetInt("TimeLiveFactory" + idNhaMay + "" + idSoNhaMay);
            LoadData(timeFactory);
        }
        else if (PlayerPrefs.HasKey("StatusFactory" + idNhaMay + "" + idSoNhaMay) == false)
        {
            PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, 0);
            PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, 0);
            PlayerPrefs.SetInt("CountDone" + idNhaMay + "" + idSoNhaMay, 0);
            NumberMFTFinal = NumberMFTBegin;
            PlayerPrefs.SetInt("NumberMFTFinal" + idNhaMay + "" + idSoNhaMay, NumberMFTFinal);
            for (int i = 0; i < NumberMFT; i++)
            {
                if (i < NumberMFTBegin)
                {
                    if (IconItemMFTImage[i].gameObject.activeSelf == false) IconItemMFTImage[i].gameObject.SetActive(true);
                    if (ButtonItemMTF[i].activeSelf == true) ButtonItemMTF[i].SetActive(false);
                    if (ItemMTF[i].activeSelf == false) ItemMTF[i].SetActive(true);
                }
                else if (i == NumberMFTBegin)
                {
                    if (IconItemMFTImage[i].gameObject.activeSelf == true) IconItemMFTImage[i].gameObject.SetActive(false);
                    if (ButtonItemMTF[i].activeSelf == false) ButtonItemMTF[i].SetActive(true);
                    if (ItemMTF[i].activeSelf == false) ItemMTF[i].SetActive(true);
                }
                else if (i > NumberMFTBegin)
                {
                    if (IconItemMFTImage[i].gameObject.activeSelf == true) IconItemMFTImage[i].gameObject.SetActive(false);
                    if (ButtonItemMTF[i].activeSelf == false) ButtonItemMTF[i].SetActive(true);
                    if (ItemMTF[i].activeSelf == true) ItemMTF[i].SetActive(false);
                }
            }
        }
    }

    void LoadData(int time)
    {
        if (CountMFT == 1)
        {
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].name;
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].nameINS;
            else nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].engName;
            if (CountDone < idItemDone.Length)
            {
                timeItemFirst.text = ManagerGame.instance.TimeText(-time);
                LightingText.text = ManagerGame.instance.CalcalutorGem(-time).ToString();
                Lighting.SetActive(true);
                Ani.SetTrigger("IsRun");
                if (idNhaMay < 6) smoke.SetActive(true);
                IETime = timeLiveFirstItem(-time);
                StartCoroutine(IETime);
            }
        }
        else if (CountMFT > 1)
        {
            if (time > 0)
            {
                if (CountDone < idItemDone.Length)
                {
                    status = 2;
                    PlayerPrefs.SetInt("StatusFactory" + idNhaMay + "" + idSoNhaMay, status);
                    idItemDone[CountDone] = IdItemMFT[0];
                    PlayerPrefs.SetInt("IdItemDone" + idNhaMay + "" + idSoNhaMay + "" + CountDone, idItemDone[CountDone]);
                    IconItemDone[CountDone].sprite = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].item;
                    IconItemDone[CountDone].gameObject.SetActive(true);
                    CountDone += 1;
                    PlayerPrefs.SetInt("CountDone" + idNhaMay + "" + idSoNhaMay, CountDone);
                    for (int i = 0; i < CountMFT; i++)
                    {
                        if (i + 1 < CountMFT)
                        {
                            IdItemMFT[i] = IdItemMFT[i + 1];
                            PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i, IdItemMFT[i]);
                            IconItemMFTImage[i].sprite = IconItemMFTImage[i + 1].sprite;
                        }
                        else if (i + 1 == CountMFT)
                        {
                            IdItemMFT[i] = 0;
                            PlayerPrefs.SetInt("IdItemMFT" + idNhaMay + "" + idSoNhaMay + "" + i, IdItemMFT[i]);
                            IconItemMFTImage[i].sprite = null;
                            IconItemMFTImage[i].color = new Color(1f, 1f, 1f, 0f);
                            EmptyMFTText[i].text = Application.systemLanguage == SystemLanguage.Vietnamese ? "Trống" : "Empty";
                        }
                    }
                    CountMFT -= 1;
                    PlayerPrefs.SetInt("CountMFT" + idNhaMay + "" + idSoNhaMay, CountMFT);
                    int timeNext = time - ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].time;
                    LoadData(timeNext);
                }
            }
            else if (time <= 0)
            {
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].name;
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].nameINS;
                else nameItemFirst.text = ManagerData.instance.facetoryItems.FacetoryItemDatas[IdItemMFT[0]].engName;
                if (CountDone < idItemDone.Length)
                {
                    if (idNhaMay < 6) smoke.SetActive(true);
                    if (idNhaMay == 1 || idNhaMay == 5 || idNhaMay == 6) Ani.SetTrigger("IsRun");
                    timeItemFirst.text = ManagerGame.instance.TimeText(-time);
                    LightingText.text = ManagerGame.instance.CalcalutorGem(-time).ToString();
                    Lighting.SetActive(true);
                    IETime = timeLiveFirstItem(-time);
                    StartCoroutine(IETime);
                }
            }
        }
    }
}
