using System;
using UnityEngine;

namespace NongTrai
{
    [Serializable]
    public class InformationItemFactory
    {
        public InfoItemFactory[] info;
    }

    [Serializable]
    public struct InfoItemFactory
    {
        public int status;
        public SpriteRenderer sprRenderer;
    }

    [Serializable]
    public class InformationItemSeeds
    {
        public InfoItemSeeds[] info;
    }

    [Serializable]
    public struct InfoItemSeeds
    {
        public int status;
        public SpriteRenderer sprRenderer;
        public GameObject quantity;
    }


    [Serializable]
    public struct InfoItemFlowers
    {
        public int status;
        public SpriteRenderer sprRenderer;
        public GameObject quantity;
    }

    [Serializable]
    public class InformationItemFlowers
    {
        public InfoItemFlowers[] info;
    }
}