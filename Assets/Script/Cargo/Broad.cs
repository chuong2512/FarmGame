using UnityEngine;

public class Broad : MonoBehaviour
{
    private Vector3 camfirstPos;
    [SerializeField] SpriteRenderer SprRenderer;
    void Start()
    {

    }

    private void Order()
    {
        float order = transform.position.y * (-100);
        SprRenderer.sortingOrder = (int)order;
    }

    private void ColorS(float r, float g, float b, float a)
    {
        SprRenderer.color = new Color(r, g, b, a);
    }

    private void OnMouseDown()
    {
        ColorS(0.7f, 0.7f, 0.7f, 1f);
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
            ManagerCargo.instance.OpenLoadCargo();
        }
    }
}
