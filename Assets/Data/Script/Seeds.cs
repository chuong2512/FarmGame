using UnityEngine;

public class Seeds : ScriptableObject
{
    public DetailSeed[] Seed;
}

[System.Serializable]
public struct DetailSeed
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int purchase;
    public int sell;
    public int exp;
    public int ValueStart;
    public int quantity;
    public int levelOpen;
    public int quantityOpen;
    public int lighting;
    public Sprite crop1;
    public Sprite crop2;
    public Sprite crop3;
    public Sprite crop4;
    public Sprite item;
    public Sprite iconStore;
}
