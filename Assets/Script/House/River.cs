using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    private bool isRunIE;
    private bool overlap;
    private bool dragging;
    private Vector3 oldPos;
    private Vector3 camfirstPos;
    private Animator Ani;
    private Rigidbody2D rgb2D;
    private SpriteRenderer sprRenderer;
    [SerializeField] GameObject Store;
    // Use this for initialization
    void Start()
    {
        Ani = GetComponent<Animator>();
        sprRenderer = Store.GetComponent<SpriteRenderer>();
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
            Ani.SetTrigger("isClick");
            sprRenderer.color = Color.white;
            Notification.instance.dialog("River is unlocked when you reach the level 70");
        }
    }

    public void onTriggerStay2D()
    {
        if (dragging == true)
        {
            overlap = true;
            sprRenderer.color = new Color(1f, 0f, 0f, 1f);
        }
    }

    public void onTriggerExit2D()
    {
        if (dragging == true)
        {
            overlap = false;
            sprRenderer.color = Color.white;
        }
    }
}
