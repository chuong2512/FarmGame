using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class buyPet : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IBeginDragHandler,
        IEndDragHandler
    {
        [SerializeField] int idPet;
        private int status;
        private Vector3 olfPos;
        private Vector3 camOldPos;
        private bool dragging;
        private Image img;

        private GameObject obj;

        // Use this for initialization
        void Start()
        {
            img = GetComponent<Image>();
            if (idPet == 0 && ManagerGuide.Instance.GuideClickPetsBuyChicken == 0)
                ManagerGuide.Instance.CallArrowPetChicken();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (ManagerShop.instance.infoPet.info[idPet].status != 1) return;
            dragging = true;
            ManagerShop.instance.idPet = idPet;
            ManagerShop.instance.buyingPet = true;
            camOldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            if (idPet != 0 || ManagerGuide.Instance.GuideClickPetsBuyChicken != 0) return;
            ManagerGuide.Instance.DoneArrowPetChicken();
            ManagerGuide.Instance.CallArrowCageChicken();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (dragging == false) ManagerShop.instance.scrollRectPet.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dragging == true)
            {
                switch (status)
                {
                    case 0:
                        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - camOldPos.y > 0.05f)
                        {
                            status = 1;
                            transform.localScale = new Vector3(1f, 1f, 1f);
                            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            target.z = 0;
                            obj = Instantiate(ManagerShop.instance.obj, target, Quaternion.identity);
                            obj.GetComponent<SpriteRenderer>().sprite = img.sprite;
                            img.color = new Color(1, 1, 1, 0);
                        }

                        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - camOldPos.y <= 0)
                        {
                            status = 3;
                            transform.localScale = new Vector3(1f, 1f, 1f);
                            ManagerShop.instance.scrollRectPet.OnBeginDrag(eventData);
                        }

                        break;
                    case 1:
                        Vector3 targetOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        targetOne.z = 0;
                        obj.transform.position = targetOne;
                        if (eventData.pointerEnter == null)
                        {
                            status = 2;
                            Destroy(obj);
                            obj = Instantiate(ManagerShop.instance.objPet, targetOne, Quaternion.identity);
                            obj.GetComponent<SpriteRenderer>().sprite = img.sprite;
                            ManagerShop.instance.isBuying();
                        }

                        break;
                    case 2:
                        Vector3 targetTwo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        targetTwo.z = 0;
                        obj.transform.position = targetTwo;
                        break;
                    case 3:
                        ManagerShop.instance.scrollRectPet.OnDrag(eventData);
                        break;
                }
            }
            else if (dragging == false) ManagerShop.instance.scrollRectPet.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragging == false || status == 3) ManagerShop.instance.scrollRectPet.OnEndDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (dragging == true)
            {
                dragging = false;
                ManagerShop.instance.buyingPet = false;
                switch (status)
                {
                    case 0:
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        break;
                    case 1:
                        status = 0;
                        img.color = Color.white;
                        Destroy(obj);
                        break;
                    case 2:
                        status = 0;
                        img.color = Color.white;
                        ManagerShop.instance.isBuying();
                        Destroy(obj);
                        break;
                    case 3:
                        status = 0;
                        break;
                }
            }

            if (idPet != 0 || ManagerGuide.Instance.GuideClickPetsBuyChicken != 0) return;
            if (idPet != 0 || ManagerGuide.Instance.GuideClickPetsBuyChicken != 0) return;
            ManagerGuide.Instance.CallArrowPetChicken();
            ManagerGuide.Instance.DoneGuide();
        }
    }
}