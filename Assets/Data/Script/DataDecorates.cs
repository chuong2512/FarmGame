using UnityEngine;

public class DataDecorates : ScriptableObject
{
    public DataDecorate[] Data;
}

[System.Serializable]
public struct DataDecorate
{
    public string NameVNS;
    public string NameENG;
    public string NameINS;
    public int LevelUnlock;
    public int Purchase;
    public Sprite IconStore;
}
