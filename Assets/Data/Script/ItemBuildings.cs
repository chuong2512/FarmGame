namespace NongTrai
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public struct DetailItemBiuld
    {
        public string NameItem;
        [FoldoutGroup("MoreData")] public string EngName;
        [FoldoutGroup("MoreData")] public string nameINS;
        [FoldoutGroup("MoreData")] public int ValueStart;
        [FoldoutGroup("MoreData")] public int Purchase;
        [FoldoutGroup("MoreData")] public int Sell;
        [FoldoutGroup("MoreData")] public Sprite Icon;
    }

    public class ItemBuildings : ScriptableObject
    {
        public DetailItemBiuld[] Data;
    }
}