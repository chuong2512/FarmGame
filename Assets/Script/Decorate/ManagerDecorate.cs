using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NongTrai
{
    public class ManagerDecorate : MonoBehaviour
    {
        void Start()
        {
            for (int i = 0; i < ManagerShop.instance.Decorate.Length; i++)
            {
                if (PlayerPrefs.HasKey("amountDCRStore" + i) != true) continue;

                var amountDcr = PlayerPrefs.GetInt("amountDCRStore" + i);
                var DCR = ManagerShop.instance.Decorate[i];
                var parent = ManagerShop.instance.parentDCR[i];

                for (int n = 0; n < amountDcr; n++)
                {
                    var x = PlayerPrefs.GetFloat("PosDecorateX" + i + "" + n);
                    var y = PlayerPrefs.GetFloat("PosDecorateY" + i + "" + n);
                    var target = new Vector2(x, y);
                    var dcrCreate = Instantiate(DCR, target, Quaternion.identity, parent);
                    var dcr = dcrCreate.GetComponent<Decorate>();
                    dcr.idSerial = n;
                }
            }
        }
    }
}