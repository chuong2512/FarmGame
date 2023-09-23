using UnityEngine;

public class Language : ScriptableObject
{
    public Translate[] Data;
}

[System.Serializable]
public struct Translate
{
    public string[] vocabulary;
}