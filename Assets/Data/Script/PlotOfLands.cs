using UnityEngine;

public class PlotOfLands : ScriptableObject
{
    public DetailPlotOfLand[] Data;
}

[System.Serializable]
public struct DetailPlotOfLand
{
    public int LevelUnlock;
    public DetailBuy[] InforBuy;
}

[System.Serializable]
public struct DetailBuy
{
    public bool isGem;
    public int Purchase;
}
