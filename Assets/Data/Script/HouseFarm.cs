using UnityEngine;

public class HouseFarm : ScriptableObject
{
    public DetailStoreHouse[] Tower;
}

[System.Serializable]
public struct DetailStoreHouse
{
    public int capacity;
    public GameObject depot;
    public MetarialUpdateDepot[] metarial;
}

[System.Serializable]
public struct MetarialUpdateDepot
{
    public int IdItem;
    public int QuantityItem;
}