using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    private Vector3 camfirstPos;
    [SerializeField] OrderPro[] sprFountain;
    // Use this for initialization
    void Start()
    {

    }

    public void Order()
    {
        float order = transform.position.y * (-100);
        for (int i = 0; i < sprFountain.Length; i++)
        {
            for (int k = 0; k < sprFountain[i].SprRenderer.Length; k++)
            {
                sprFountain[i].SprRenderer[k].sortingOrder = (int)order + sprFountain[i].order;
            }
        }
    }

    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < sprFountain.Length; i++)
        {
            for (int k = 0; k < sprFountain[i].SprRenderer.Length; k++)
            {
                sprFountain[i].SprRenderer[k].color = new Color(r, g, b, a);
            }
        }
    }

    void OnMouseDown()
    {
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
        {
            ColorS(1f, 1f, 1f, 1f);
        }
    }

    void OnMouseUp()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
        {
            ColorS(1f, 1f, 1f, 1f);
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Đài Phun Nước được xây dựng khi đạt cấp độ " + (ManagerFountain.instance.LevelBuildFountain + 1);
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Air mancur terbuka di level " + (ManagerFountain.instance.LevelBuildFountain + 1);
            else str = "Fountain have build when you get level " + (ManagerFountain.instance.LevelBuildFountain + 1);
            Notification.instance.dialogBelow(str);
        }
    }
}
