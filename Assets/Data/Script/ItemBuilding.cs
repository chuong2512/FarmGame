using UnityEngine;

public class ItemBuilding : ScriptableObject
{
    public DetailItemBiuld[] Data;
}

[System.Serializable]
public struct DetailItemBiuld
{
    public string NameItem;
    public string EngName;
    public string nameINS;
    public int ValueStart;
    public int Purchase;
    public int Sell;
    public Sprite Icon;
}
