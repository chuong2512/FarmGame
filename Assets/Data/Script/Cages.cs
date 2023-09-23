using UnityEngine;

public class Cages : ScriptableObject
{
    public DetailCage[] Cage;
}

[System.Serializable]
public struct DetailCage
{
    public string name;
    public string engName;
    public string nameINS;
    public int time;
    public int Exp;
    public int amountPet;
    public int purchase;
    public int mutiGold;
    public int levelOpen;
    public int distanceLvOpen;
    public int amountOpen;
    public Sprite iconStore;
}
