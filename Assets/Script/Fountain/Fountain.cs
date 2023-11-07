using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NongTrai
{
    public class Fountain : MonoBehaviour
    {
        private Vector3 _camfirstPos;

        [SerializeField] OrderPro[] sprFountain;

        public void Order()
        {
            float order = transform.position.y * (-100);
            for (int i = 0; i < sprFountain.Length; i++)
            {
                foreach (var t in sprFountain[i].SprRenderer)
                {
                    t.sortingOrder = (int) order + sprFountain[i].order;
                }
            }
        }

        private void ColorS(float r, float g, float b, float a)
        {
            for (int i = 0; i < sprFountain.Length; i++)
            {
                foreach (var t in sprFountain[i].SprRenderer)
                {
                    t.color = new Color(r, g, b, a);
                }
            }
        }

        void OnMouseDown()
        {
            if (Camera.main != null) _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseDrag()
        {
            if (Camera.main != null &&
                Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
            {
                ColorS(1f, 1f, 1f, 1f);
            }
        }

        void OnMouseUp()
        {
            if (Camera.main == null ||
                !(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;
            ColorS(1f, 1f, 1f, 1f);
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Đài Phun Nước được xây dựng khi đạt cấp độ " +
                      (ManagerFountain.Instance.levelBuildFountain + 1);
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Air mancur terbuka di level " + (ManagerFountain.Instance.levelBuildFountain + 1);
            else
                str = "Fountain have build when you get level " + (ManagerFountain.Instance.levelBuildFountain + 1);
            Notification.Instance.dialogBelow(str);
        }
    }
}