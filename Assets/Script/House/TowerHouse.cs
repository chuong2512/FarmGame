using UnityEngine;

public class TowerHouse : MonoBehaviour
{
    private bool isRunIE;
    private bool overlap;
    private bool dragging;
    
    private SpriteRenderer sprRenderer;
    private SpriteRenderer sprShadow;
    private Vector3 oldPos;
    private Vector3 camfirstPos;
    private Animator Ani;
    private Rigidbody2D rgd2D;
    [SerializeField] GameObject Tower;
    [SerializeField] GameObject Shadow;
    // Use this for initialization
    void Start()
    {
        Ani = GetComponent<Animator>();
        sprRenderer = Tower.GetComponent<SpriteRenderer>();
        sprShadow = Shadow.GetComponent<SpriteRenderer>(); 
        float order = transform.position.y * (-100);
        sprRenderer.sortingOrder = (int)order;
        sprShadow.sortingOrder = (int)order - 1;
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
            Ani.SetTrigger("isClick");
            sprRenderer.color = Color.white;
            ManagerTower.instance.OpenTower();
        }
    }
}
