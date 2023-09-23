using UnityEngine;

public class Houses : ScriptableObject
{
    
    public DetailHouse[] Home;
}

[System.Serializable]
public struct DetailHouse
{
    public string name;
    public int levelOpen;
    public GameObject house;
}
