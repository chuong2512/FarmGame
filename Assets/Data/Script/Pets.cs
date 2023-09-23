using UnityEngine;

public class Pets : ScriptableObject
{
    public DetailPets[] Pet;
}

[System.Serializable]
public struct DetailPets
{
    public DetailPet detailPet;
    public DetailItemPet itemPet;
}

[System.Serializable]
public struct DetailPet
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int purchase;
    public int exp;
    public int product;
    public int mutiGold;
    public int levelOpen;
    public int distanceLvOpen;
    public int quantityOpen;
    public Sprite iconStore;
}

[System.Serializable]
public struct DetailItemPet
{
    public string name;
    public string engName;
    public string nameINS;
    public int sell;
    public Sprite item;
}
