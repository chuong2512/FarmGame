using UnityEngine;
using System.Collections;

public class MainHouse : MonoBehaviour
{
    [SerializeField] GameObject house, shadow;
    SpriteRenderer sprRenderer;
    Vector3 camfirstPos;
    // Use this for initialization
    void Start()
    {
        sprRenderer = house.GetComponent<SpriteRenderer>();
        float order = this.transform.position.y * (-100);
        sprRenderer.sortingOrder = (int)order;
    }

    void OnMouseDown()
    {
        sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void OnMouseDrag()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
        {
            if (sprRenderer.color != Color.white) sprRenderer.color = Color.white;
        }
    }
    void OnMouseUp()
    {
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
        {
            sprRenderer.color = Color.white;
            string str;
            if (Application.systemLanguage == SystemLanguage.Vietnamese)
                str = "Sắp có!";
            else if (Application.systemLanguage == SystemLanguage.Indonesian)
                str = "Segera akan datang!";
            else str = "Coming soon!";
            Notification.instance.dialogBelow(str);
        }
    }
}
