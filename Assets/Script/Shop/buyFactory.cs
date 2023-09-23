using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class buyFactory : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler
{
    private int status;
    private float distanceX;
    private float distanceY;
    private bool dragging;
    private Vector3 olfPos;
    private Vector3 camOldPos;
    private Image img;
    private GameObject obj;
    private NhaMay nm;
    [SerializeField] int idFactory;
    // Use this for initialization
    void Start()
    {
        img = this.GetComponent<Image>();
        distanceX = ManagerGame.instance.DistaneX;
        distanceY = ManagerGame.instance.DistaneY;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (ManagerShop.instance.infoFactory.info[idFactory].status == 1)
        {
            camOldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localScale = new Vector3(1f, 1.1f, 1f);
            dragging = true;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragging == false) ManagerShop.instance.scrollRectFactory.OnBeginDrag(eventData);
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
                        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        obj = Instantiate(ManagerShop.instance.obj, target, Quaternion.identity);
                        obj.GetComponent<SpriteRenderer>().sprite = img.sprite;
                        img.color = new Color(1, 1, 1, 0);
                    }
                    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - camOldPos.y <= 0)
                    {
                        status = 3;
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        ManagerShop.instance.scrollRectFactory.OnBeginDrag(eventData);
                    }
                    break;
                case 1:
                    Vector2 targetOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    obj.transform.position = targetOne;
                    if (eventData.pointerEnter == null)
                    {
                        status = 2;
                        Destroy(obj);
                        obj = Instantiate(ManagerShop.instance.Factory[idFactory], targetOne, Quaternion.identity, ManagerShop.instance.parentFactory[idFactory]);
                        nm = obj.GetComponent<NhaMay>();
                        nm.idSoNhaMay = ManagerShop.instance.infoFactory.info[idFactory].amount;
                        nm.StartMove();
                        ManagerShop.instance.isBuying();
                    }
                    break;
                case 2:
                    Vector2 PosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 targetTwo = new Vector2(((int)(PosCam.x / distanceX)) * distanceX, ((int)(PosCam.y / distanceY)) * distanceY);
                    obj.transform.position = targetTwo;
                    break;
                case 3:
                    ManagerShop.instance.scrollRectFactory.OnDrag(eventData);
                    break;
            }
        }
        else if (dragging == false) ManagerShop.instance.scrollRectFactory.OnDrag(eventData);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragging == false || status == 3) ManagerShop.instance.scrollRectFactory.OnEndDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragging == true)
        {
            dragging = false;
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
                    if (nm.overlap == true)
                    {
                        Destroy(obj);
                        string str;
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            str = "Không còn khoảng trống, xin hãy thử lại!";
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            str = "Tidak ada lagi ruang, silakan coba lagi!";
                        else str = "No space left, please try again!";
                        Notification.instance.dialogBelow(str);
                    } 
                    else if (nm.overlap == false)
                    {
                        if (ManagerCoin.instance.Coin >= ManagerShop.instance.infoFactory.info[idFactory].goldPrice)
                        {
                            int idProduct = ManagerShop.instance.infoFactory.info[idFactory].amount;
                            int time = ManagerData.instance.facetorys.Facetory[idFactory].time;
                            int lighting = ManagerData.instance.facetorys.Facetory[idFactory].lighting;
                            ManagerBuilding.instance.RegisterBuilding(1, idFactory, idProduct, time, lighting, obj.transform.position);
                            ManagerShop.instance.buyFactory(idFactory);
                            Destroy(obj);
                        }
                        else
                        {
                            Destroy(obj);
                            string str;
                            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                str = "Bạn không đủ vàng!";
                            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                str = "Kamu tidak punya cukup emas!";
                            else str = "You haven't enough gold!";
                            Notification.instance.dialogBelow(str);
                        }
                    }
                    break;
                case 3:
                    status = 0;
                    break;
            }
        }
    }
}
