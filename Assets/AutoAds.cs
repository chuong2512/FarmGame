using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAds : MonoBehaviour
{
    private int _timeClick = 0;

    void Start()
    {
        //StartCoroutine(CoShowAds());
    }

    private IEnumerator CoShowAds()
    {
        yield return new WaitForSeconds(15);

        QuangCaoGoogle.Instance.ShowInterAds();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _timeClick++;

            CheckTimeToShowAds();
        }
    }

    private void CheckTimeToShowAds()
    {
        if (_timeClick % 35 == 0)
        {
            QuangCaoGoogle.Instance.ShowInterAds();
        }
    }
}