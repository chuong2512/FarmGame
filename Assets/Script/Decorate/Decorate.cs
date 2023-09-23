using UnityEngine;
using System.Collections;

public class Decorate : MonoBehaviour
{

    private int countOverlap;
    private bool isIEDrag;
    private bool dragging;
    private float distanceX;
    private float distanceY;
    private Vector3 oldPos;
    private Vector3 camfirstPos;
    private IEnumerator onDrag;
    private Rigidbody2D rgb2D;
    [SerializeField] bool isRoration;
    [SerializeField] int idDecorate;
    [SerializeField] GameObject Foot;
    [SerializeField] OrderPro[] orderPro;
    [HideInInspector] public int idSerial;
    [HideInInspector] public bool overlap;
    void Start()
    {
        distanceX = ManagerGame.instance.DistaneX;
        distanceY = ManagerGame.instance.DistaneY;
    }
    private void OnMouseDown()
    {
        oldPos = transform.position;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ColorS(0.3f, 0.3f, 0.3f, 1f);
        onDrag = waitDrag();
        StartCoroutine(onDrag);
    }
    IEnumerator waitDrag()
    {
        isIEDrag = true;
        yield return new WaitForSeconds(0.1f);
        Vector3 target = new Vector3(camfirstPos.x, camfirstPos.y + 0.5f, 0);
        ManagerTool.instance.RegisterTimeMove(target);
        yield return new WaitForSeconds(0.55f);
        if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
        {
            MainCamera.instance.DisableAll();
            MainCamera.instance.lockCam();
            ManagerTool.instance.CloseTimeMove();
            ColorS(1f, 1f, 1f, 1f);
            StartMove();
        }
    }
    private void OnMouseDrag()
    {
        if (dragging == true)
        {
            Vector2 PosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 target = new Vector2(((int)(PosCam.x / distanceX)) * distanceX, ((int)(PosCam.y / distanceY)) * distanceY);
            transform.position = target;
        }
        else if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
        {
            if (isIEDrag == true)
            {
                isIEDrag = false;
                StopCoroutine(onDrag);
                ColorS(1f, 1f, 1f, 1f);
                ManagerTool.instance.CloseTimeMove();
            }
        }
    }
    private void OnMouseUp()
    {
        if (isIEDrag == true)
        {
            isIEDrag = false;
            StopCoroutine(onDrag);
            ManagerTool.instance.CloseTimeMove();
        }
        if (dragging == false)
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                ColorS(1f, 1f, 1f, 1f);
                ManagerAudio.instance.PlayAudio(Audio.Click);
                MainCamera.instance.DisableAll();
                if (isRoration)
                {
                    transform.localScale = transform.localScale.x == 1
                        ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
                }
            }
        }
        else if (dragging == true)
        {
            if (overlap == false)
            {
                PlayerPrefs.SetFloat("PosDecorateX" + idDecorate + "" + idSerial, transform.position.x);
                PlayerPrefs.SetFloat("PosDecorateY" + idDecorate + "" + idSerial, transform.position.y);
            }
            else if (overlap == true)
            {
                overlap = false;
                transform.position = oldPos;
                ColorS(1f, 1f, 1f, 1f);
            }
            MainCamera.instance.unLockCam();
            countOverlap = 0;
            DoneMove();
        }
    }
    public void StartMove()
    {
        dragging = true;
        for (int i = 0; i < orderPro.Length; i++)
        {
            for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                orderPro[i].SprRenderer[j].sortingLayerName = "Move";
        }
        rgb2D = Foot.AddComponent<Rigidbody2D>();
        rgb2D.bodyType = RigidbodyType2D.Kinematic;
    }
    public void DoneMove()
    {
        dragging = false;
        for (int i = 0; i < orderPro.Length; i++)
        {
            for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                orderPro[i].SprRenderer[j].sortingLayerName = "Default";
        }
        Order();
        Destroy(rgb2D);
    }
    public void Order()
    {
        int order = (int)(transform.position.y * (-100));
        for (int i = 0; i < orderPro.Length; i++)
        {
            for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                orderPro[i].SprRenderer[j].sortingOrder = order + orderPro[i].order;
        }
    }
    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < orderPro.Length; i++)
        {
            for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                orderPro[i].SprRenderer[j].color = new Color(r, g, b, a);
        }
    }
    public void Overlap()
    {
        if (dragging == true)
        {
            countOverlap += 1;
            if (countOverlap == 1)
            {
                overlap = true;
                ColorS(1f, 127f / 255, 127f / 255, 1f);
            }
        }
    }
    public void UnOverlap()
    {
        if (dragging == true)
        {
            countOverlap -= 1;
            if (countOverlap == 0)
            {
                overlap = false;
                ColorS(1f, 1f, 1f, 1f);
            }
        }
    }
}
