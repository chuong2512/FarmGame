﻿using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NongTrai
{
    public class Trees : ScriptableObject
    {
        public DataTree[] data;
    }

    [Serializable]
    public struct DataTree
    {
        public DetailTree Tree;
        public DetailItemTree ItemTree;
    }

    [Serializable]
    public struct DetailTree
    {
        public string name;
        [FoldoutGroup("MoreData")] public string engName;
        [FoldoutGroup("MoreData")] public string nameINS;
        [FoldoutGroup("MoreData")] public int time;
        [FoldoutGroup("MoreData")] public int purchase;
        [FoldoutGroup("MoreData")] public int quantity;
        [FoldoutGroup("MoreData")] public int mutiGold;
        [FoldoutGroup("MoreData")] public int levelOpen;
        [FoldoutGroup("MoreData")] public int distanceLvOpen;
        [FoldoutGroup("MoreData")] public int quantityOpen;
        [FoldoutGroup("MoreData")] public Sprite crop1;
        [FoldoutGroup("MoreData")] public Sprite crop2;
        [FoldoutGroup("MoreData")] public Sprite crop3;
        [FoldoutGroup("MoreData")] public Sprite crop4;
        [FoldoutGroup("MoreData")] public Sprite crop5;
        [FoldoutGroup("MoreData")] public Sprite iconStore;
    }

    [Serializable]
    public struct DetailItemTree
    {
        public string name;
        public string engName;
        public string nameINS;
        public int sell;
        public int exp;
        public Sprite item;
    }
}