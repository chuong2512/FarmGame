using UnityEngine;

[System.Serializable]
public class InformationItemFactory
{
    public InfoItemFactory[] info;
}

[System.Serializable]
public struct InfoItemFactory
{
    public int status;
    public SpriteRenderer sprRenderer;
}

[System.Serializable]
public class InformationItemSeeds
{
    public InfoItemSeeds[] info;
}

[System.Serializable]
public struct InfoItemSeeds
{
    public int status;
    public SpriteRenderer sprRenderer;
    public GameObject quantity;
}


[System.Serializable]
public struct InfoItemFlowers
{
    public int status;
    public SpriteRenderer sprRenderer;
    public GameObject quantity;
}

[System.Serializable]
public class InformationItemFlowers
{
    public InfoItemFlowers[] info;
}
