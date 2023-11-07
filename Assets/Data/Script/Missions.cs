using System;
using UnityEngine;

public class Missions : ScriptableObject
{
    public Mission[] mission;
}

[Serializable]
public struct Mission
{
    public string name;
    public string engName;
    public string nameINS;
    public int coin;
    public int exp;
    public Metarial[] metarial;
}
