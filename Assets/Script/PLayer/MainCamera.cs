using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera instance;
    private bool MoveCamera;
    private bool StatusNow;
    Vector3 TargetMove;
    public Camera mcam;
    Vector3 oldPos, panOrigin;
    float speed = 5f;
    Vector2?[] oldTouchPositions =
    {
        null,
        null
    };
    [SerializeField] float maxZoom, minZoom, speedScreen;
    Vector2 oldTouchVector;
    float oldTouchDistance;
    [SerializeField] float dragSpeed, outerLeft, outerRight;
    public bool camLock;
    [SerializeField] bool bDragging;
    [SerializeField] bool cameraDragging, camDrag;
    Vector3 dragOrigin;
    [SerializeField] Transform mocRight;
    [SerializeField] Transform mocLeft;
    [SerializeField] Transform mocUp;
    [SerializeField] Transform mocDown;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (camLock == false)
        {
            if (Input.touchCount == 0)
            {
                oldTouchPositions[0] = null;
                oldTouchPositions[1] = null;
            }
            //else
            //if (Input.touchCount == 1)
            //{
            //    if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //    {
            //        oldTouchPositions[0] = Input.GetTouch(0).position;
            //        oldTouchPositions[1] = null;
            //        Vector3 worlPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //        RaycastHit2D hitInfo = Physics2D.Raycast(worlPoint, Vector3.zero);
            //        if (hitInfo.collider == null)
            //        {
            //            DisableAll();
            //        }
            //    }

            //    if (Input.GetTouch(0).phase == TouchPhase.Moved)
            //    {
            //        Vector2 newTouchPosition = Input.GetTouch(0).position;
            //        transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * mcam.orthographicSize / mcam.pixelHeight * speedScreen));
            //        oldTouchPositions[0] = newTouchPosition;
            //    }
            //}
            else
            if (Input.touchCount == 2)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    DisableAll();
                }
                if (oldTouchPositions[1] == null)
                {
                    oldTouchPositions[0] = Input.GetTouch(0).position;
                    oldTouchPositions[1] = Input.GetTouch(1).position;
                    oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                    oldTouchDistance = oldTouchVector.magnitude;
                }
                else
                {
                    Vector2 screen = new Vector2(mcam.pixelWidth, mcam.pixelHeight);
                    Vector2[] newTouchPositions =
                    {
                            Input.GetTouch(0).position,
                            Input.GetTouch(1).position
                        };
                    Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
                    float newTouchDistance = newTouchVector.magnitude;
                    //transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * mcam.orthographicSize / screen.y));
                    //transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(Mathf.Clamp((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f));
                    mcam.orthographicSize *= oldTouchDistance / newTouchDistance;
                    mcam.orthographicSize = Mathf.Clamp(mcam.orthographicSize, minZoom, maxZoom);
                    //transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * mcam.orthographicSize / screen.y);
                    oldTouchPositions[0] = newTouchPositions[0];
                    oldTouchPositions[1] = newTouchPositions[1];
                    oldTouchVector = newTouchVector;
                    oldTouchDistance = newTouchDistance;
                }
            }

            // Mouse move -------------------------------------------------------------------
            if (Input.GetMouseButtonDown(0))
            {
                bDragging = true;
                oldPos = transform.position;
                panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                Vector3 worlPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(worlPoint, Vector3.zero);
                if (hitInfo.collider == null)
                {
                    DisableAll();
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
            //-------------------------------------------------------------------------------------
            if (transform.position.x + (mcam.orthographicSize * ((float)Screen.width / Screen.height))
                    - mocRight.position.x >= 0)
            {
                float oldSize = mcam.orthographicSize;
                float distanceSize = mcam.orthographicSize * ((float)Screen.width / Screen.height) + (mcam.orthographicSize - oldSize) * ((float)Screen.width / Screen.height);
                transform.position = new Vector3(mocRight.position.x - distanceSize, transform.position.y, -10);
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
        else if (camLock == true)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                oldTouchPositions[0] = transform.position;
                Vector3 worlPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(worlPoint, Vector3.zero);
                if (hitInfo.collider == null) DisableAll();
            }
        }

        // Move Camera -----------------------------------------------------------------
        if (MoveCamera == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetMove, 0.1f);
            if (Vector3.Distance(transform.position, TargetMove) <= 0.05f)
            {
                MoveCamera = false;
                if (ManagerGuide.instance.GuideClickCageChicken == 0)
                {
                    Vector3 target = TargetMove;
                    target.z = 0;
                    ManagerGuide.instance.CallArrowDown(target);
                }
            }
        }
    }

    public void DisableAll()
    {
        unLockCam();
        ManagerTool.instance.HideTool();
        ManagerTool.instance.HideClock();
        ManagerShop.instance.HideShop();
    }

    public void lockCam()
    {
        camLock = true;
    }

    public void unLockCam()
    {
        camLock = false;
    }

    public void MoveCameraTarget(Vector3 target)
    {
        target.z = -10;
        TargetMove = target;
        MoveCamera = true;
    }

    public void UnlockCamLevelUp()
    {
        camLock = StatusNow;
    }

    public void LockCamLevelUp()
    {
        StatusNow = camLock;
        if (camLock == false) camLock = true;
    }
}
