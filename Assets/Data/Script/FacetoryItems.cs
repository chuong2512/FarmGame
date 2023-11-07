using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class FacetoryItems : ScriptableObject
{
    [FormerlySerializedAs("FacetoryItem")] public DetailFacetoryItem[] FacetoryItemDatas;
}

[Serializable]
public struct DetailFacetoryItem
{
    public string name;
    [FoldoutGroup("MoreData")] public string engName;
    [FoldoutGroup("MoreData")] public string nameINS;
    [FoldoutGroup("MoreData")] public int time;
    [FoldoutGroup("MoreData")] public int sell;
    [FoldoutGroup("MoreData")] public int exp;
    [FoldoutGroup("MoreData")] public int valueStart;
    [FoldoutGroup("MoreData")] public int donate;
    [FoldoutGroup("MoreData")] public int quantity;
    [FoldoutGroup("MoreData")] public int levelOpen;
    [FoldoutGroup("MoreData")] public Metarial[] metarial;
    [FoldoutGroup("MoreData")] public Sprite item, itemBlack;
}

[Serializable]
public struct Metarial
{
    public int stypeIDYC, IdYc, Amount;
}