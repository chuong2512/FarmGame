using UnityEngine;

public class Trees : ScriptableObject 
{
    public DataTree[] data;
}

[System.Serializable]
public struct DataTree
{
    public DetailTree Tree;
    public DetailItemTree ItemTree;
}

[System.Serializable]
public struct DetailTree
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int purchase;
    public int quantity;
    public int mutiGold;
    public int levelOpen;
    public int distanceLvOpen;
    public int quantityOpen;
    public Sprite crop1;
    public Sprite crop2;
    public Sprite crop3;
    public Sprite crop4;
    public Sprite crop5;
    public Sprite iconStore;
}

[System.Serializable]
public struct DetailItemTree
{
    public string name;
    public string engName;
    public string nameINS;
    public int sell;
    public int exp;
    public Sprite item;
}
