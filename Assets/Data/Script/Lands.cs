using System;
using Sirenix.OdinInspector;

namespace NongTrai
{
    using UnityEngine;

    public class Lands : ScriptableObject
    {
        [Serializable]
        public struct DetailLand
        {
            public string name;
            [FoldoutGroup("MoreData")] public string engName;
            [FoldoutGroup("MoreData")] public string nameINS;
            [FoldoutGroup("MoreData")] public int purchase;
            [FoldoutGroup("MoreData")] public int mutiGold;
            [FoldoutGroup("MoreData")] public int levelOpen;
            [FoldoutGroup("MoreData")] public int distanceLvOpen;
            [FoldoutGroup("MoreData")] public int amountOpen;
            [FoldoutGroup("MoreData")] public Sprite iconStore;
        }

        public DetailLand[] Land;
    }
}