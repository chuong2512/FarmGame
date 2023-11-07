using UnityEngine;
using System.Collections;
using NongTrai;

public class OldTree : MonoBehaviour
{
    [SerializeField] int status;
    private int timeLive;
    private int totalTime;
    private int CouterFruite;
    private int countOverlap;
    private Vector3 oldPos;
    private Vector3 camfirstPos;
    private bool isRunIE;
    private bool isHarvest = true;
    [SerializeField] int idOldTree;
    [SerializeField] float DistanceX;
    [SerializeField] float DistanceY;
    public SpriteRenderer sprRenderer;
    [SerializeField] GameObject foot;
    [SerializeField] OrderPro[] sprOldTree;
    [SerializeField] GameObject[] Fruit;
    [SerializeField] GameObject[] ColliderFruit;
    [HideInInspector] public int idAmountOldTree;
    [HideInInspector] public bool dragging;
    [HideInInspector] public bool overlap;
    Rigidbody2D rgb2D;
    IEnumerator onDrag;
    IEnumerator IETimeLive;
    private float distanceX;
    private float distanceY;
    // Use this for initialization
    void Start()
    {
        distanceX = ManagerGame.instance.DistaneX;
        distanceY = ManagerGame.instance.DistaneY;
        if (PlayerPrefs.HasKey("StatusTree" + idOldTree + "" + idAmountOldTree))
        {
            status = PlayerPrefs.GetInt("StatusTree" + idOldTree + "" + idAmountOldTree);
            totalTime = ManagerData.instance.trees.data[idOldTree].Tree.time;
            if (status == 0)
            {
                int timeNow = ManagerGame.instance.RealTime();
                int time = timeNow - PlayerPrefs.GetInt("TimeLastTree" + idOldTree + "" + idAmountOldTree);
                timeLive = PlayerPrefs.GetInt("TimeLiveTree" + idOldTree + "" + idAmountOldTree) - time;
                if (timeLive > totalTime * 1 / 3 && timeLive <= totalTime * 2 / 3)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop2;
                if (timeLive > 0 && timeLive <= totalTime * 1 / 3)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop3;
                IETimeLive = TimeLive();
                StartCoroutine(IETimeLive);
            }
            else if (status == 1)
            {
                CouterFruite = PlayerPrefs.GetInt("CouterFruite" + idOldTree + "" + idAmountOldTree);
                sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop4;
                for (int i = 0; i < Fruit.Length; i++)
                {
                    int statusFruit = PlayerPrefs.GetInt("StatusFruit" + idOldTree + "" + idAmountOldTree + "" + i);
                    if (statusFruit == 0)
                    {
                        Fruit[i].SetActive(true);
                        ColliderFruit[i].SetActive(true);
                    }
                }
            }
            else if (status == 2)
            {
                int timeNow = ManagerGame.instance.RealTime();
                int time = timeNow - PlayerPrefs.GetInt("TimeLastTree" + idOldTree + "" + idAmountOldTree);
                timeLive = PlayerPrefs.GetInt("TimeLiveTree" + idOldTree + "" + idAmountOldTree) - time;
                if (timeLive > totalTime * 1 / 3 && timeLive <= totalTime * 2 / 3)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop2;
                if (timeLive > 0 && timeLive <= totalTime * 1 / 3)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop3;
                IETimeLive = TimeLive();
                StartCoroutine(IETimeLive);
                sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop4;
            }
        }
        else if (!PlayerPrefs.HasKey("StatusTree" + idOldTree + "" + idAmountOldTree))
        {
            totalTime = ManagerData.instance.trees.data[idOldTree].Tree.time;
            startCounterTime();
            PlayerPrefs.SetInt("StatusTree" + idOldTree + "" + idAmountOldTree, 0);
        }
        Order();
    }

    public void Order()
    {
        int order = (int)(transform.position.y * (-100));
        for (int i = 0; i < sprOldTree.Length; i++)
        {
            for (int k = 0; k < sprOldTree[i].SprRenderer.Length; k++)
            {
                sprOldTree[i].SprRenderer[k].sortingOrder = (int)order + sprOldTree[i].order;
            }
        }
    }

    public void StartMove()
    {
        dragging = true;
        for (int i = 0; i < sprOldTree.Length; i++)
        {
            for (int k = 0; k < sprOldTree[i].SprRenderer.Length; k++)
            {
                sprOldTree[i].SprRenderer[k].sortingLayerName = "Move";
            }
        }
        rgb2D = foot.AddComponent<Rigidbody2D>();
        rgb2D.bodyType = RigidbodyType2D.Kinematic;
    }

    public void DoneMove()
    {
        dragging = false;
        for (int i = 0; i < sprOldTree.Length; i++)
        {
            for (int k = 0; k < sprOldTree[i].SprRenderer.Length; k++)
            {
                sprOldTree[i].SprRenderer[k].sortingLayerName = "Default";
            }
        }
        Order();
        Destroy(rgb2D);
    }

    void OnMouseDown()
    {
        oldPos = transform.position;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ColorS(0.3f, 0.3f, 0.3f, 1f);
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
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
        {
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
        }
        else if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.2f)
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
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                ColorS(1f, 1f, 1f, 1f);
                if (status == 0)
                {
                    ManagerAudio.instance.PlayAudio(Audio.Click);
                    MainCamera.instance.DisableAll();
                    Vector2 target = new Vector2(transform.position.x, transform.position.y - 0.5f);
                    string nameItem;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        nameItem = ManagerData.instance.trees.data[idOldTree].Tree.name;
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        nameItem = ManagerData.instance.trees.data[idOldTree].Tree.nameINS;
                    else nameItem = ManagerData.instance.trees.data[idOldTree].Tree.engName;
                    ManagerTool.instance.RegisterShowClock(2, 0, idOldTree, idAmountOldTree, nameItem, target, gameObject);
                    ManagerTool.instance.ShowClockOldTree(timeLive, totalTime);
                    Vector3 targetOne = new Vector3(transform.position.x + 0.7f, transform.position.y + 0.7f, transform.position.z);
                }
                else if (status == 1)
                {
                    ManagerAudio.instance.PlayAudio(Audio.Click);
                    MainCamera.instance.DisableAll();
                    ManagerTool.instance.idOldTree = idOldTree;
                    Vector3 target = new Vector3(transform.position.x - DistanceX, transform.position.y + DistanceY, transform.position.z);
                    ManagerTool.instance.ShowToolOldTree(target);
                }
            }
        }
        else if (dragging == true)
        {
            if (overlap == false)
            {
                PlayerPrefs.SetFloat("PosTreeX" + idOldTree + "" + idAmountOldTree, transform.position.x);
                PlayerPrefs.SetFloat("PosTreeY" + idOldTree + "" + idAmountOldTree, transform.position.y);
                PlayerPrefs.SetFloat("PosTreeZ" + idOldTree + "" + idAmountOldTree, transform.position.z);
            }
            else if (overlap == true)
            {
                overlap = false;
                transform.position = oldPos;
                ColorS(1f, 1f, 1f, 1f);
            }
            MainCamera.instance.unLockCam();
            countOverlap = 0;
            DoneMove();
        }
    }

    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < sprOldTree.Length; i++)
        {
            for (int k = 0; k < sprOldTree[i].SprRenderer.Length; k++)
            {
                sprOldTree[i].SprRenderer[k].color = new Color(r, g, b, a);
            }
        }
    }

    void startCounterTime()
    {
        timeLive = totalTime;
        IETimeLive = TimeLive();
        StartCoroutine(IETimeLive);
    }

    IEnumerator TimeLive()
    {
        yield return new WaitForSeconds(1f);
        timeLive = timeLive - 1;
        PlayerPrefs.SetInt("TimeLiveTree" + idOldTree + "" + idAmountOldTree, timeLive);
        PlayerPrefs.SetInt("TimeLastTree" + idOldTree + "" + idAmountOldTree, ManagerGame.instance.RealTime());
        if (timeLive > 0)
        {
            if (status == 0)
            {
                if (timeLive == totalTime * 2 / 3)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop2;
                if (timeLive == totalTime * 1 / 3)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop3;
            }
            else if (status == 2)
            {
                if (timeLive == totalTime / 2)
                    sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop3;
            }
        }
        else if (timeLive <= 0)
        {
            status = 1;
            PlayerPrefs.SetInt("StatusTree" + idOldTree + "" + idAmountOldTree, 1);
            CreateFruit();
            sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop4;
        }
        if (ManagerTool.instance.showClock.CheckShow == true)
            if (ManagerTool.instance.showClock.CheckStype[2] == true)
                if (ManagerTool.instance.showClock.IdProduct == idOldTree)
                    if (ManagerTool.instance.showClock.IdShow == idAmountOldTree)
                    {
                        int totalTime = ManagerData.instance.trees.data[idOldTree].Tree.time;
                        ManagerTool.instance.ShowClockOldTree(timeLive, totalTime);
                    }
        if (timeLive > 0)
        {
            IETimeLive = TimeLive();
            StartCoroutine(IETimeLive);
        }
    }

    private void CreateFruit()
    {
        for (int i = 0; i < Fruit.Length; i++)
        {
            Fruit[i].SetActive(true);
            ColliderFruit[i].SetActive(true);
            PlayerPrefs.SetInt("StatusFruit" + idOldTree + "" + idAmountOldTree + "" + i, 0);
        }
    }

    public void HarvestFruit(int id)
    {
        if (status == 1 && idOldTree == ManagerTool.instance.idOldTree)
        {
            if (ManagerMarket.instance.QuantityItemTower < ManagerMarket.instance.QuantityTotalItemTower)
            {
                if (isHarvest == true)
                {
                    isHarvest = false;
                    CouterFruite += 1;
                    PlayerPrefs.SetInt("CouterFruite" + idOldTree + "" + idAmountOldTree, CouterFruite);
                    Fruit[id].SetActive(false);
                    ColliderFruit[id].SetActive(false);
                    PlayerPrefs.SetInt("StatusFruit" + idOldTree + "" + idAmountOldTree + "" + id, 1);
                    ManagerMarket.instance.ReciveItem(3, idOldTree, ManagerData.instance.trees.data[idOldTree].Tree.quantity, true);
                    Sprite spr = ManagerData.instance.trees.data[idOldTree].ItemTree.item;
                    int exp = ManagerData.instance.trees.data[idOldTree].ItemTree.exp;
                    int quantity = ManagerData.instance.trees.data[idOldTree].Tree.quantity;
                    Experience.instance.registerExp(spr, exp, quantity, ColliderFruit[id].transform.position);
                    StartCoroutine(PauseHarvest());
                    if (CouterFruite == 3)
                    {
                        status = 2;
                        PlayerPrefs.SetInt("StatusTree" + idOldTree + "" + idAmountOldTree, 0);
                        CouterFruite = 0;
                        PlayerPrefs.SetInt("CouterFruite" + idOldTree + "" + idAmountOldTree, CouterFruite);
                        startCounterTime();
                    }
                }
            }
            else
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

    IEnumerator PauseHarvest()
    {
        yield return new WaitForSeconds(0.2f);
        isHarvest = true;
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

    public void UseDiamond()
    {
        timeLive = 0;
        PlayerPrefs.SetInt("TimeLiveTree" + idOldTree + "" + idAmountOldTree, timeLive);
        status = 1;
        PlayerPrefs.SetInt("StatusTree" + idOldTree + "" + idAmountOldTree, 1);
        StopCoroutine(IETimeLive);
        sprRenderer.sprite = ManagerData.instance.trees.data[idOldTree].Tree.crop4;
        CreateFruit();
        Vector2 target = new Vector2(transform.position.x, transform.position.y - 0.5f);
        int totalTime = ManagerData.instance.trees.data[idOldTree].Tree.time;
        ManagerTool.instance.ShowClockOldTree(timeLive, totalTime);
    }
}
