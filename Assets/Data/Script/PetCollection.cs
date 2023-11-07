namespace NongTrai
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class PetCollection : ScriptableObject
    {
        public DetailPets[] Pet;
    }

    [Serializable]
    public struct DetailPets
    {
        public DetailPet detailPet;
        public DetailItemPet itemPet;
    }

    [Serializable]
    public struct DetailPet
    {
        public string name;
        [FoldoutGroup("MoreData")] public string engName;
        [FoldoutGroup("MoreData")] public string nameINS;
        [FoldoutGroup("MoreData")] public int time;
        [FoldoutGroup("MoreData")] public int purchase;
        [FoldoutGroup("MoreData")] public int exp;
        [FoldoutGroup("MoreData")] public int product;
        [FoldoutGroup("MoreData")] public int mutiGold;
        [FoldoutGroup("MoreData")] public int levelOpen;
        [FoldoutGroup("MoreData")] public int distanceLvOpen;
        [FoldoutGroup("MoreData")] public int quantityOpen;
        [FoldoutGroup("MoreData")] public Sprite iconStore;
    }

    [Serializable]
    public struct DetailItemPet
    {
        public string name;
        public string engName;
        public string nameINS;
        public int sell;
        public Sprite item;
    }
}