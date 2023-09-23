using UnityEngine;

public class Cargo : MonoBehaviour
{
    Vector3 camfirstPos;
    [SerializeField] SpriteRenderer sprRenderer;

    void Start()
    {

    }

    void OnMouseDown()
    {
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
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
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            ManagerCargo.instance.OpenLoadCargo();   
        }
    }
}
