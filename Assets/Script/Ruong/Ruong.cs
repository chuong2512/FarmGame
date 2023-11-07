using UnityEngine;
using System.Collections;
using NongTrai;

namespace NongTrai
{
    public class Ruong : MonoBehaviour
    {
        private int status;
        private int idSeed;
        private int timeLive;
        private int countOverlap;
        private float distanceX;
        private float distanceY;
        private Vector2 oldPos;
        private Vector2 camfirstPos;
        private bool isRunIE;
        private IEnumerator onDrag;
        private IEnumerator IETimeLive;
        private Rigidbody2D rgb2D;
        public int idRuong;
        public bool dragging, overlap;
        [SerializeField] GameObject Crops;
        [SerializeField] GameObject Shadow;
        public SpriteRenderer sprRenderer;

        public SpriteRenderer sprRendererCrop;

        //-------------------------------------------------------------
        void Start()
        {
            distanceX = ManagerGame.Instance.DistaneX;
            distanceY = ManagerGame.Instance.DistaneY;

            if (PlayerPrefs.HasKey("StatusField" + idRuong))
            {
                if (PlayerPrefs.GetInt("StatusField" + idRuong) == 0)
                {
                    status = 0;
                }
                else if (PlayerPrefs.GetInt("StatusField" + idRuong) == 1)
                {
                    status = 1;
                    idSeed = PlayerPrefs.GetInt("IdSeedField" + idRuong);

                    int timeNow = ManagerGame.Instance.RealTime();
                    int time = timeNow - PlayerPrefs.GetInt("TimeLastField" + idRuong);
                    timeLive = PlayerPrefs.GetInt("TimeLiveField" + idRuong) - time;
                    if (timeLive > ManagerData.instance.seeds.SeedDatas[idSeed].time * 3 / 4)
                        sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop1;
                    else if (timeLive <= ManagerData.instance.seeds.SeedDatas[idSeed].time * 3 / 4 &&
                             timeLive > ManagerData.instance.seeds.SeedDatas[idSeed].time * 1 / 4)
                        sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop2;
                    else if (timeLive <= ManagerData.instance.seeds.SeedDatas[idSeed].time * 1 / 4 && timeLive > 0)
                        sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop3;
                    StartCoroutine(countTime());
                }
                else if (PlayerPrefs.GetInt("StatusField" + idRuong) == 2)
                {
                    status = 2;
                    idSeed = PlayerPrefs.GetInt("IdSeedField" + idRuong);
                    sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop4;
                }
            }
            else if (!PlayerPrefs.HasKey("StatusField" + idRuong))
            {
                if (idRuong < 6)
                {
                    status = 2;
                    sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop4;
                    PlayerPrefs.SetInt("StatusField" + idRuong, 2);
                    PlayerPrefs.SetInt("IdSeedField" + idRuong, 0);
                    PlayerPrefs.SetInt("TimeLastField" + idRuong, 0);
                    PlayerPrefs.SetInt("TimeLiveField" + idRuong, 0);
                }
                else
                {
                    PlayerPrefs.SetInt("StatusField" + idRuong, 0);
                    PlayerPrefs.SetInt("IdSeedField" + idRuong, 0);
                    PlayerPrefs.SetInt("TimeLastField" + idRuong, 0);
                    PlayerPrefs.SetInt("TimeLiveField" + idRuong, 0);
                }
            }

            if (idRuong < 6)
            {
                if (PlayerPrefs.HasKey("PosFieldX" + idRuong))
                {
                    transform.position = new Vector2(PlayerPrefs.GetFloat("PosFieldX" + idRuong),
                        PlayerPrefs.GetFloat("PosFieldY" + idRuong));
                }
                else if (!PlayerPrefs.HasKey("PosFieldX" + idRuong))
                {
                    PlayerPrefs.SetFloat("PosFieldX" + idRuong, transform.position.x);
                    PlayerPrefs.SetFloat("PosFieldY" + idRuong, transform.position.y);
                }
            }

            if (idRuong == 0 && ManagerGuide.Instance.GuideClickFieldHavestCrop == 0)
                ManagerGuide.Instance.CallArrowDown(transform.position);
            Order();
        }

        public void Order()
        {
            int order = (int) (transform.position.y * (-100));
            sprRenderer.sortingOrder = order;
            sprRendererCrop.sortingOrder = order + 1;
        }

        private void ColorS(float r, float g, float b, float a)
        {
            sprRenderer.color = new Color(r, g, b, a);
            sprRendererCrop.color = new Color(r, g, b, a);
        }

        public void StartMove()
        {
            dragging = true;
            sprRenderer.sortingLayerName = "Move";
            sprRendererCrop.sortingLayerName = "Move";
            rgb2D = Shadow.AddComponent<Rigidbody2D>();
            rgb2D.bodyType = RigidbodyType2D.Kinematic;
        }

        public void DoneMove()
        {
            dragging = false;
            sprRenderer.sortingLayerName = "Default";
            sprRendererCrop.sortingLayerName = "Default";
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
                Vector2 target = new Vector2(((int) (PosCam.x / distanceX)) * distanceX,
                    ((int) (PosCam.y / distanceY)) * distanceY);
                transform.position = target;
            }
            else if (dragging == false)
            {
                if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
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
                if (Vector2.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
                {
                    switch (status)
                    {
                        case 0:
                            ManagerAudio.Instance.PlayAudio(Audio.Click);
                            MainCamera.instance.DisableAll();
                            ManagerTool.instance.idRuong = idRuong;
                            ManagerTool.instance.OpenToolCrops(transform.position);
                            if (ManagerGuide.Instance.GuideClickFieldCrop == 0)
                                ManagerGuide.Instance.GuideClickFieldCrop = 1;
                            if (ManagerGuide.Instance.GuideClickFieldCrop == 1 &&
                                ManagerGuide.Instance.GuideClickSeeds == 0) ManagerGuide.Instance.CallArowSeedsRice();
                            break;
                        case 1:
                            ManagerAudio.Instance.PlayAudio(Audio.Click);
                            MainCamera.instance.DisableAll();
                            string nameItem;
                            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                nameItem = ManagerData.instance.seeds.SeedDatas[idSeed].name;
                            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                nameItem = ManagerData.instance.seeds.SeedDatas[idSeed].nameINS;
                            else nameItem = ManagerData.instance.seeds.SeedDatas[idSeed].engName;
                            int totalTime = ManagerData.instance.seeds.SeedDatas[idSeed].time;
                            ManagerTool.instance.RegisterShowClock(0, 0, 0, idRuong, nameItem, transform.position,
                                gameObject);
                            ManagerTool.instance.ShowClockCrop(timeLive, totalTime);
                            break;
                        case 2:
                            ManagerAudio.Instance.PlayAudio(Audio.Click);
                            MainCamera.instance.DisableAll();
                            ManagerTool.instance.idRuong = idRuong;
                            ManagerTool.instance.showToolHarvestCrops(transform.position);
                            if (ManagerGuide.Instance.GuideClickFieldHavestCrop == 0)
                                ManagerGuide.Instance.GuideClickFieldHavestCrop = 1;
                            break;
                    }

                    if (ManagerTool.instance.ClickUseGemBuySeed != 0) ManagerTool.instance.ClickUseGemBuySeed = 0;
                }
            }
            else if (dragging == true)
            {
                if (overlap == false)
                {
                    PlayerPrefs.SetFloat("PosFieldX" + idRuong, transform.position.x);
                    PlayerPrefs.SetFloat("PosFieldY" + idRuong, transform.position.y);
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

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Seeds" && ManagerTool.instance.dragging == true && status == 0)
            {
                if (idRuong == ManagerTool.instance.idRuong)
                {
                    int idSeedNow = ManagerTool.instance.idSeeds;
                    if (ManagerMarket.instance.QuantityItemSeeds[idSeedNow] > 0) PlantTree();
                    else
                        ManagerUseGem.Instance.RegisterUseGemBuySeeds(StypeUseGemBuySeed.SortDay, idSeedNow,
                            gameObject);
                }

                if (ManagerTool.instance.checkCollider == true)
                {
                    int idSeedNow = ManagerTool.instance.idSeeds;
                    if (ManagerMarket.instance.QuantityItemSeeds[idSeedNow] > 0) PlantTree();
                }
            }

            if (other.tag == "ToolHarvestCrop" && ManagerTool.instance.dragging == true && status == 2 &&
                (idRuong == ManagerTool.instance.idRuong || ManagerTool.instance.checkCollider == true))
            {
                int quantity = ManagerData.instance.seeds.SeedDatas[idSeed].quantity;
                if (ManagerMarket.instance.quantityItemTower + quantity <=
                    ManagerMarket.instance.QuantityTotalItemTower)
                {
                    status = 0;
                    PlayerPrefs.SetInt("StatusField" + idRuong, 0);
                    if (ManagerTool.instance.checkCollider == false) ManagerTool.instance.checkCollider = true;
                    ManagerTool.instance.CloseBorderField();
                    sprRendererCrop.sprite = null;
                    ManagerMarket.instance.ReciveItem(0, idSeed, quantity, true);
                    ManagerMarket.instance.MinusCropsSeeds(idSeed);
                    Sprite spr = ManagerData.instance.seeds.SeedDatas[idSeed].item;
                    int exp = ManagerData.instance.seeds.SeedDatas[idSeed].exp;
                    Experience.Instance.registerExp(spr, exp, quantity, transform.position);
                    ManagerRuong.instance.CompleteHarvestCrop();
                    if (ManagerGame.Instance.RandomItem() == true)
                    {
                        int IdItemBuilding = Random.Range(0, 6);
                        ManagerMarket.instance.ReciveItem(4, IdItemBuilding, 1, false);
                        Sprite sprIcon = ManagerData.instance.itemBuildings.Data[IdItemBuilding].Icon;
                        ManagerTool.instance.RegisterItemSingle(1, sprIcon, transform.position);
                    }

                    if (ManagerGuide.Instance.GuideClickFieldCrop == 0 && ManagerRuong.instance.landSpace == 0)
                        ManagerGuide.Instance.CallArrowField();
                }
                else if (ManagerMarket.instance.quantityItemTower + quantity >
                         ManagerMarket.instance.QuantityTotalItemTower)
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Vật phẩm nông trại đầy!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Silo penuh!";
                    else str = "Farm Item Full!";
                    Notification.Instance.dialogBetween(str);
                    Notification.Instance.dialogTower();
                }
            }
        }

        private void PlantTree()
        {
            if (status == 0)
            {
                status = 1;
                PlayerPrefs.SetInt("StatusField" + idRuong, 1);
                if (ManagerTool.instance.checkCollider == false) ManagerTool.instance.checkCollider = true;
                ManagerTool.instance.CloseBorderField();
                idSeed = ManagerTool.instance.idSeeds;
                PlayerPrefs.SetInt("IdSeedField" + idRuong, idSeed);
                ManagerRuong.instance.CompleteCrop();
                sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop1;
                ManagerMarket.instance.MinusItem(0, idSeed, 1);
                ManagerMarket.instance.RecieveCropSeeds(idSeed);
                ManagerTool.instance.UpdateQuantitySeeds();
                timeLive = ManagerData.instance.seeds.SeedDatas[idSeed].time;
                IETimeLive = countTime();
                StartCoroutine(IETimeLive);
                ManagerTool.instance.RegisterEatOne(1, ManagerData.instance.seeds.SeedDatas[idSeed].item,
                    transform.position);
                if (ManagerGuide.Instance.GetArrowField(idRuong) == 0) ManagerGuide.Instance.SetArrowField(idRuong, 1);
                if (ManagerGuide.Instance.GuideClickSeeds == 0) ManagerGuide.Instance.CallArrowFieldEat();
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

        IEnumerator countTime()
        {
            yield return new WaitForSeconds(1f);
            timeLive -= 1;
            PlayerPrefs.SetInt("TimeLiveField" + idRuong, timeLive);
            PlayerPrefs.SetInt("TimeLastField" + idRuong, ManagerGame.Instance.RealTime());
            if (timeLive == ManagerData.instance.seeds.SeedDatas[idSeed].time * 3 / 4)
                sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop2;
            if (timeLive == ManagerData.instance.seeds.SeedDatas[idSeed].time / 4)
                sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop3;
            if (timeLive <= 0)
            {
                status = 2;
                PlayerPrefs.SetInt("StatusField" + idRuong, 2);
                sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop4;
            }

            if (ManagerTool.instance.showClock.CheckShow == true)
                if (ManagerTool.instance.showClock.CheckStype[0] == true)
                    if (ManagerTool.instance.showClock.IdShow == idRuong)
                    {
                        int totalTime = ManagerData.instance.seeds.SeedDatas[idSeed].time;
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
            PlayerPrefs.SetInt("TimeLiveField" + idRuong, timeLive);
            status = 2;
            PlayerPrefs.SetInt("StatusField" + idRuong, 2);
            sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop4;
            int lighting = ManagerData.instance.seeds.SeedDatas[idSeed].lighting;
            int totalTime = ManagerData.instance.seeds.SeedDatas[idSeed].time;
            ManagerTool.instance.ShowClockCrop(timeLive, totalTime);
        }

        public void UseGemBuySeeds()
        {
            if (ManagerGem.Instance.GemLive >= 2)
            {
                status = 1;
                PlayerPrefs.SetInt("StatusField" + idRuong, 1);
                ManagerTool.instance.CloseBorderField();
                idSeed = ManagerTool.instance.idSeeds;
                PlayerPrefs.SetInt("IdSeedField" + idRuong, idSeed);
                ManagerRuong.instance.CompleteCrop();
                sprRendererCrop.sprite = ManagerData.instance.seeds.SeedDatas[idSeed].crop1;
                ManagerMarket.instance.RecieveCropSeeds(idSeed);
                timeLive = ManagerData.instance.seeds.SeedDatas[idSeed].time;
                IETimeLive = countTime();
                StartCoroutine(IETimeLive);
                ManagerTool.instance.RegisterEatOne(1, ManagerData.instance.seeds.SeedDatas[idSeed].item,
                    transform.position);
                ManagerGem.Instance.MunisGem(2);
            }
            else
            {
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Bạn không đủ gem!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Anda tidak memiliki cukup permata!";
                else str = "You haven't enough gem!";
                Notification.Instance.dialogBelow(str);
            }
        }

        public void DontUseGemBuySeeds()
        {
            ManagerTool.instance.OpenToolCrops(transform.position);
        }
    }
}