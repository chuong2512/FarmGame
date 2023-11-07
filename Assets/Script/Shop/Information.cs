using System;
using UnityEngine;
using UnityEngine.UI;

namespace NongTrai
{
    [Serializable]
    public class Information
    {
        [Serializable]
        public struct Info
        {
            public string name;
            public int status, levelOpen, amount, total, goldPrice;
            public Text txtName, txtInfo, txtGoldPrice, txtAmount;
            public Image icon;
        }

        public Info[] info;
    }
}