using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Facetorys : ScriptableObject
{
    public DetailFacetory[] Facetory;
}

[Serializable]
public struct DetailFacetory
{
    public string name;
    [FoldoutGroup("MoreData")] public string engName;
    [FoldoutGroup("MoreData")] public string nameINS;
    [FoldoutGroup("MoreData")] public int time;
    [FoldoutGroup("MoreData")] public int Exp;
    [FoldoutGroup("MoreData")] public int purchase;
    [FoldoutGroup("MoreData")] public int mutiGold;
    [FoldoutGroup("MoreData")] public int levelOpen;
    [FoldoutGroup("MoreData")] public int distanceLvOpen;
    [FoldoutGroup("MoreData")] public int amountOpen;
    [FoldoutGroup("MoreData")] public int lighting;
    [FoldoutGroup("MoreData")] public Sprite iconStore;
}

[Serializable]
public struct OrderPro
{
    public int order;
    public SpriteRenderer[] SprRenderer;
}