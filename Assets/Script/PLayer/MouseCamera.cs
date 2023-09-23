using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    [SerializeField] Camera mcam;
    Vector3 oldPos, panOrigin;
    float speed = 5f;
    [SerializeField] float maxZoom, minZoom, speedScreen = 2f;
    Vector2 oldTouchVector;
    float oldTouchDistance;
    [SerializeField] float dragSpeed, outerLeft, outerRight;
    [SerializeField] bool camLock, bDragging;
    [SerializeField] bool cameraDragging, camDrag;
    Vector3 dragOrigin;
    [SerializeField] Transform mocRight;
    [SerializeField] Transform mocLeft;
    [SerializeField] Transform mocUp;
    [SerializeField] Transform mocDown;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera.instance.camLock == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bDragging = true;
                oldPos = transform.position;
                panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                Vector3 worlPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(worlPoint, Vector3.zero);
                if (hitInfo.collider == null)
                {
                    //DisableAll();

                }
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;
                transform.position = oldPos - pos * speed;
            }
            if (Input.GetMouseButtonUp(0))
            {
                bDragging = false;
            }

            if (transform.position.x + (mcam.orthographicSize * ((float)Screen.width / Screen.height))
                    - mocRight.position.x >= 0)
            {
                float oldSize = mcam.orthographicSize;
                float distanceSize = mcam.orthographicSize * ((float)Screen.width / Screen.height) + (mcam.orthographicSize - oldSize) * ((float)Screen.width / Screen.height);
                transform.position = new Vector3(mocRight.position.x - distanceSize, transform.position.y, -10);
            }

            if ((transform.position.x - (mcam.orthographicSize * Screen.width / Screen.height))
                - mocLeft.position.x <= 0)
            {
                float oldSize = mcam.orthographicSize;
                float distanceSize = mcam.orthographicSize * ((float)Screen.width / Screen.height) + (mcam.orthographicSize - oldSize) * ((float)Screen.width / Screen.height);
                transform.position = new Vector3(mocLeft.position.x + distanceSize, transform.position.y, -10);
            }

            if ((transform.position.y + mcam.orthographicSize) - mocUp.position.y >= 0)
            {
                float oldSize = mcam.orthographicSize;
                float distanceSize = mcam.orthographicSize + (mcam.orthographicSize - oldSize) * mcam.orthographicSize;
                transform.position = new Vector3(transform.position.x, mocUp.position.y - distanceSize, -10);
            }

            if ((transform.position.y - mcam.orthographicSize) - mocDown.position.y <= 0)
            {
                float oldSize = mcam.orthographicSize;
                float distanceSize = mcam.orthographicSize + (mcam.orthographicSize - oldSize) * mcam.orthographicSize;
                transform.position = new Vector3(transform.position.x, mocDown.position.y + distanceSize, -10);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                oldPos = transform.position;
                Vector3 worlPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(worlPoint, Vector3.zero);
                if (hitInfo.collider == null)
                {
                    //DisableAll();
                }
            }
        }
    }
}
