﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NongTrai
{
    public class buyField : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IBeginDragHandler,
        IEndDragHandler
    {
        [SerializeField] int idField;
        private int status;
        private float distanceX;
        private float distanceY;
        private Vector2 olfPos;
        private Vector2 camOldPos;
        private bool dragging;
        private Image img;
        private GameObject obj;

        private Ruong rg;

        // Use this for initialization
        void Start()
        {
            img = GetComponent<Image>();
            distanceX = ManagerGame.Instance.DistaneX;
            distanceY = ManagerGame.Instance.DistaneY;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (ManagerShop.instance.infoField.info[idField].status == 1)
            {
                camOldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.localScale = new Vector3(1f, 1.1f, 1f);
                dragging = true;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (dragging == false) ManagerShop.instance.scrollRectCage.OnBeginDrag(eventData);
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
                            ManagerShop.instance.scrollRectCage.OnBeginDrag(eventData);
                        }

                        break;
                    case 1:
                        Vector2 targetOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        obj.transform.position = targetOne;
                        if (eventData.pointerEnter == null)
                        {
                            status = 2;
                            Destroy(obj);
                            obj = Instantiate(ManagerShop.instance.Field[idField], targetOne, Quaternion.identity,
                                ManagerShop.instance.parentField[idField]);
                            rg = obj.GetComponent<Ruong>();
                            rg.idRuong = ManagerShop.instance.infoField.info[idField].amount;
                            rg.StartMove();
                            ManagerShop.instance.isBuying();
                        }

                        break;
                    case 2:
                        Vector2 PosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector2 targetTwo = new Vector2(((int) (PosCam.x / distanceX)) * distanceX,
                            ((int) (PosCam.y / distanceY)) * distanceY);
                        obj.transform.position = targetTwo;
                        rg.Order();
                        break;
                    case 3:
                        ManagerShop.instance.scrollRectCage.OnDrag(eventData);
                        break;
                }
            }
            else if (dragging == false)
            {
                ManagerShop.instance.scrollRectCage.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (dragging == false || status == 3) ManagerShop.instance.scrollRectCage.OnEndDrag(eventData);
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
                        if (rg.overlap == true)
                        {
                            Destroy(obj);
                            string str;
                            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                str = "Không còn khoảng trống, xin hãy thử lại!";
                            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                str = "Tidak ada lagi ruang, silakan coba lagi!";
                            else str = "No space left, please try again!";
                            Notification.Instance.dialogBelow(str);
                        }
                        else if (rg.overlap == false)
                        {
                            if (ManagerCoin.Instance.Coin >= ManagerShop.instance.infoField.info[idField].goldPrice)
                            {
                                ManagerShop.instance.buyField(idField);
                                rg.DoneMove();
                                PlayerPrefs.SetFloat(
                                    "PosFieldX" + ((int) ManagerShop.instance.infoField.info[idField].amount - 1),
                                    obj.transform.position.x);
                                PlayerPrefs.SetFloat(
                                    "PosFieldY" + ((int) ManagerShop.instance.infoField.info[idField].amount - 1),
                                    obj.transform.position.y);
                            }
                            else if (ManagerCoin.Instance.Coin < ManagerShop.instance.infoField.info[idField].goldPrice)
                            {
                                Destroy(obj);
                                string str;
                                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                                    str = "Bạn không đủ vàng!";
                                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                                    str = "Kamu tidak punya cukup emas!";
                                else str = "You haven't enough gold!";
                                Notification.Instance.dialogBelow(str);
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
}