using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class BreadFeeds : ScriptableObject
{
    public DetailBreadFeed[] BreadFeed;
}

[Serializable]
public struct DetailBreadFeed
{
    public int id;
    [FoldoutGroup("DataBread")] public string name, engName;
    [FoldoutGroup("DataBread")] public int time, sell;
    [FoldoutGroup("DataBread")] public int stypeIDYC1, IdYc1, stypeIDYC2, IdYc2;
    [FoldoutGroup("DataBread")] public Sprite item;
}