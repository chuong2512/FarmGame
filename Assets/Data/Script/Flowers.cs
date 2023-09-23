using UnityEngine;

public class Flowers : ScriptableObject
{
    public Flower[] Data;
}


[System.Serializable]
public struct Flower
{
    public DetailFlower detailFlower;
    public DetailItemFlower DetailItemFlower;
}

[System.Serializable]
public struct DetailFlower
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int exp;
    public int ValueStart;
    public int quantity;
    public int levelOpen;
    public int donate;
    public Sprite crop1;
    public Sprite crop2;
    public Sprite crop3;
    public Sprite crop4;
    public Sprite iconStore;
}
[System.Serializable]
public struct DetailItemFlower
{
    public string name;
    public string engName;
    public string nameINS;
    public int Sell;
    public Sprite item;
}