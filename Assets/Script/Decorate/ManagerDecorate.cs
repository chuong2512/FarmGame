using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDecorate : MonoBehaviour
{

    void Start()
    {
        for (int i = 0; i < ManagerShop.instance.Decorate.Length; i++)
        {
            if (PlayerPrefs.HasKey("amountDCRStore" + i) == true)
            {
                int amountDCR = PlayerPrefs.GetInt("amountDCRStore" + i);
                for (int n = 0; n < amountDCR; n++)
                {
                    float x = PlayerPrefs.GetFloat("PosDecorateX" + i + "" + n);
                    float y = PlayerPrefs.GetFloat("PosDecorateY" + i + "" + n);
                    Vector2 target = new Vector2(x, y);
                    GameObject DCR = ManagerShop.instance.Decorate[i];
                    Transform parent = ManagerShop.instance.parentDCR[i];
                    GameObject DCRCreate = Instantiate(DCR, target, Quaternion.identity, parent);
                    Decorate dcr = DCRCreate.GetComponent<Decorate>();
                    dcr.idSerial = n;
                }
            }
        }
    }
}
