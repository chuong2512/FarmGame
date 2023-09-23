using UnityEngine;

public class Facetorys : ScriptableObject
{
    public DetailFacetory[] Facetory;
}

[System.Serializable]
public struct DetailFacetory
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int Exp;
    public int purchase;
    public int mutiGold;
    public int levelOpen;
    public int distanceLvOpen;
    public int amountOpen;
    public int lighting;
    public Sprite iconStore;
}

[System.Serializable]
public struct OrderPro
{
    public int order;
    public SpriteRenderer[] SprRenderer;
}
