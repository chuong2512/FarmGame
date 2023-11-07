using System;

namespace NongTrai
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class Seeds : ScriptableObject
    {
        [FormerlySerializedAs("Seed")] public DetailSeed[] SeedDatas;

        [Button]
        public void SetIDs()
        {
            for (int i = 0; i < SeedDatas.Length; i++)
            {
                SeedDatas[i].ID = i;
            }
        }
    }

    [Serializable]
    public struct DetailSeed
    {
        public int ID;
        public string name;

        [FoldoutGroup("Data")] public string engName;
        [FoldoutGroup("Data")] public string nameINS;
        [FoldoutGroup("Data")] public int time;
        [FoldoutGroup("Data")] public int purchase;
        [FoldoutGroup("Data")] public int sell;
        [FoldoutGroup("Data")] public int exp;
        [FoldoutGroup("Data")] public int ValueStart;
        [FoldoutGroup("Data")] public int quantity;
        [FoldoutGroup("Data")] public int levelOpen;
        [FoldoutGroup("Data")] public int quantityOpen;
        [FoldoutGroup("Data")] public int lighting;
        [FoldoutGroup("Data")] public Sprite crop1;
        [FoldoutGroup("Data")] public Sprite crop2;
        [FoldoutGroup("Data")] public Sprite crop3;
        [FoldoutGroup("Data")] public Sprite crop4;
        [FoldoutGroup("Data")] public Sprite item;
        [FoldoutGroup("Data")] public Sprite iconStore;
    }
}