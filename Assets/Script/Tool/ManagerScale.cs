using UnityEngine;

public class ManagerScale : MonoBehaviour
{
    float defaultOtherGrapSize = 2.7f;
    Vector3 defaultSize;

    void Awake()
    {
        defaultSize = transform.localScale;
    }

    void OnEnable()
    {
        float ratio = MainCamera.instance.mcam.orthographicSize / defaultOtherGrapSize;
        transform.localScale = defaultSize * ratio;
    }
}
