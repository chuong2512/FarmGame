namespace NongTrai
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using Spine.Unity;

    public class HomeAnimal : MonoBehaviour
    {
        public int idHomeAnimal, idAmountHome;
        public bool dragging;
        public bool overlap;

        private int countOverlap;
        private bool isRunIE;
        private float distanceX;
        private float distanceY;
        private Vector3 oldPos;
        private Vector3 camfirstPos;
        private Rigidbody2D rgb2D;
        private IEnumerator onDrag;


        [SerializeField] int status;
        [SerializeField] int animalNow;
        [SerializeField] bool showClock;
        [SerializeField] GameObject Foot;
        [SerializeField] SpriteRenderer sprRenderer;
        [SerializeField] SpriteRenderer sprRendererFoot;
        [SerializeField] Audio audio;
        [SerializeField] Renderer[] ren;
        [SerializeField] SkeletonAnimation[] metarials;
        [SerializeField] GameObject[] Animal;

        void Start()
        {
            distanceX = ManagerGame.Instance.DistaneX;
            distanceY = ManagerGame.Instance.DistaneY;
            if (PlayerPrefs.HasKey("AnimalNow" + idHomeAnimal + "" + idAmountHome))
            {
                animalNow = PlayerPrefs.GetInt("AnimalNow" + idHomeAnimal + "" + idAmountHome);
                if (animalNow > 0)
                    for (int i = 0; i < animalNow; i++)
                    {
                        Animal[i].SetActive(true);
                    }
            }
            else PlayerPrefs.SetInt("AnimalNow" + idHomeAnimal + "" + idAmountHome, 0);

            SetOrder();
            if (idHomeAnimal == 0 && idAmountHome == 0 && ManagerGuide.Instance.GuideClickShopBuyChicken == 0)
                ManagerGuide.Instance.CallArrowShop();
            if (idHomeAnimal == 0 && idAmountHome == 0 && ManagerGuide.Instance.GuideClickPetsBuyChicken == 1 &&
                ManagerGuide.Instance.GuideClickChicken == 0)
            {
                Vector3 target = Animal[0].transform.position;
                ManagerGuide.Instance.CallArrowDown(target);
            }
        }

        public void StartMove()
        {
            dragging = true;
            sprRenderer.sortingLayerName = "Move";
            sprRendererFoot.sortingLayerName = "Move";
            for (int i = 0; i < Animal.Length; i++)
            {
                ren[i].sortingLayerName = "Move";
            }

            rgb2D = Foot.AddComponent<Rigidbody2D>();
            rgb2D.bodyType = RigidbodyType2D.Kinematic;
        }

        public void DoneMove()
        {
            dragging = false;
            sprRenderer.sortingLayerName = "Default";
            sprRendererFoot.sortingLayerName = "Default";
            for (int i = 0; i < Animal.Length; i++)
            {
                ren[i].sortingLayerName = "Default";
            }

            SetOrder();
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
            Vector3 target = new Vector3(camfirstPos.x, camfirstPos.y + 0.3f, 0);
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
                transform.parent.position = target;
            }
            else if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
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
                    ManagerAudio.Instance.PlayAudio(audio);
                    MainCamera.instance.DisableAll();
                    ManagerTool.instance.showToolCage(idHomeAnimal, transform.position);
                    if (idHomeAnimal == 0 && idAmountHome == 0 && ManagerGuide.Instance.GuideClickChicken == 0)
                        ManagerGuide.Instance.GuideClickChicken = 1;
                    if (ManagerGuide.Instance.GuideClickChicken == 1 &&
                        ManagerGuide.Instance.GuideClickFoodChicken == 0)
                        ManagerGuide.Instance.CallArrowFoodsChicken();
                }
            }
            else if (dragging == true)
            {
                if (overlap == false)
                {
                    PlayerPrefs.SetFloat("PosCageX" + idHomeAnimal + "" + idAmountHome, transform.parent.position.x);
                    PlayerPrefs.SetFloat("PosCageY" + idHomeAnimal + "" + idAmountHome, transform.parent.position.y);
                }
                else if (overlap == true)
                {
                    overlap = false;
                    transform.parent.position = oldPos;
                    ColorS(1f, 1f, 1f, 1f);
                }

                MainCamera.instance.unLockCam();
                countOverlap = 0;
                DoneMove();
            }
        }

        private void SetOrder()
        {
            int order = (int) (transform.position.y * (-100));
            sprRenderer.sortingOrder = (order);
            sprRendererFoot.sortingOrder = (order + Animal.Length + 1);
            for (int i = 0; i < Animal.Length; i++)
            {
                ren[i].sortingOrder = (order + i + 1);
            }
        }

        private void ColorS(float r, float g, float b, float a)
        {
            sprRenderer.color = new Color(r, g, b, a);
            sprRendererFoot.color = new Color(r, g, b, a);
            for (int i = 0; i < metarials.Length; i++)
            {
                if (metarials[i].skeleton != null)
                    metarials[i].skeleton.SetColor(new Color(r, g, b, a));
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Pet") ManagerShop.instance.idHomeAnimal = idHomeAnimal;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Pet")
            {
                if (ManagerShop.instance.buyingPet)
                {
                }
                else if (ManagerShop.instance.buyingPet == false &&
                         ManagerShop.instance.idPet == ManagerShop.instance.idHomeAnimal)
                {
                    if (animalNow < Animal.Length)
                    {
                        if (ManagerCoin.Instance.Coin >= ManagerShop.instance.infoPet.info[idHomeAnimal].goldPrice)
                        {
                            Animal[animalNow].SetActive(true);
                            animalNow += 1;
                            PlayerPrefs.SetInt("AnimalNow" + idHomeAnimal + "" + idAmountHome, animalNow);
                            ManagerShop.instance.buyPet(idHomeAnimal);
                            if (ManagerGuide.Instance.GuideClickPetsBuyChicken == 0 && animalNow == 5)
                                ManagerGuide.Instance.GuideClickPetsBuyChicken = 1;
                            if (ManagerGuide.Instance.GuideClickPetsBuyChicken == 1 &&
                                ManagerGuide.Instance.GuideClickChicken == 0)
                            {
                                Vector3 target = Animal[0].transform.position;
                                ManagerGuide.Instance.CallArrowDown(target);
                            }
                        }
                        else if (ManagerCoin.Instance.Coin < ManagerShop.instance.infoPet.info[idHomeAnimal].goldPrice)
                        {
                            string str;
                            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                str = "Bạn không đủ vàng!";
                            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                str = "Kamu tidak punya cukup emas!";
                            else str = "You haven't enough gold!";
                            Notification.Instance.dialogBelow(str);
                        }
                    }
                    else if (animalNow >= Animal.Length)
                    {
                        string str;
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            str = "Không đủ sức chứa!";
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            str = "Penyimpanan penuh";
                        else str = "Not enough capacity!";
                        Notification.Instance.dialogBelow(str);
                    }
                }
            }
        }
    }
}