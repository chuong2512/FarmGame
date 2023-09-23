using UnityEngine;

public class FacetoryItems : ScriptableObject
{
    public DetailFacetoryItem[] FacetoryItem;
}

[System.Serializable]
public struct DetailFacetoryItem
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int sell;
    public int exp;
    public int valueStart;
    public int donate;
    public int quantity;
    public int levelOpen;
    public Metarial[] metarial;
    public Sprite item, itemBlack;
}

[System.Serializable]
public struct Metarial
{
    public int stypeIDYC, IdYc, Amount;
}
