using UnityEngine;

public class ToolDecorate : ScriptableObject
{
    public DetailToolDecorate[] Data;
}

[System.Serializable]
public struct DetailToolDecorate
{
    public string NameItem;
    public string EngName;
    public string nameINS;
    public int ValueStart;
    public int Purchare;
    public int Sell;
    public Sprite Icon;
}
