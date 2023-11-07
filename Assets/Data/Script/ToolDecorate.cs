﻿using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace NongTrai
{
    using UnityEngine;

    public class ToolDecorate : ScriptableObject
    {
        [FormerlySerializedAs("Data")] public DetailToolDecorate[] Datas;
    }

    [Serializable]
    public struct DetailToolDecorate
    {
        public string NameItem;

        [FoldoutGroup("MoreData")] public string EngName;
        [FoldoutGroup("MoreData")] public string nameINS;
        [FoldoutGroup("MoreData")] public int ValueStart;
        [FoldoutGroup("MoreData")] public int Purchare;
        [FoldoutGroup("MoreData")] public int Sell;
        [FoldoutGroup("MoreData")] public Sprite Icon;
    }
}