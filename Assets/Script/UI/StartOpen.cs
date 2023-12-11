using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOpen : MonoBehaviour
{
    public GameObject openObj;

    // Start is called before the first frame update
    void Start()
    {
        openObj.SetActive(true);
    }
}
