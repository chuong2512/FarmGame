using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DataDecorates : ScriptableObject
{
    [FormerlySerializedAs("Data")] public DataDecorate[] Datas;
}

[Serializable]
public struct DataDecorate
{
    public string NameVNS;
    [FoldoutGroup("MoreData")] public string NameENG;
    [FoldoutGroup("MoreData")] public string NameINS;
    [FoldoutGroup("MoreData")] public int LevelUnlock;
    [FoldoutGroup("MoreData")] public int Purchase;
    [FoldoutGroup("MoreData")] public Sprite IconStore;
}