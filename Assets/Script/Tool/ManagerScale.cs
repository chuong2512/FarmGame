using UnityEngine;
namespace NongTrai
{
public class ManagerScale : MonoBehaviour
{
    float defaultOtherGrapSize = 2.7f;
    Vector3 defaultSize;

    
    void OnEnable()
    {
        float ratio = MainCamera.instance.mcam.orthographicSize / defaultOtherGrapSize;
        transform.localScale = defaultSize * ratio;
    }
    void Awake()
    {
        defaultSize = transform.localScale;
    }

    
    }
}
