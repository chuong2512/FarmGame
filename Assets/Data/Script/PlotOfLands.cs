using System;

namespace NongTrai
{
    using UnityEngine;

    public class PlotOfLands : ScriptableObject
    {
        public DetailPlotOfLand[] Datas;
    }

    [Serializable]
    public struct DetailPlotOfLand
    {
        public int LevelUnlock;
        public DetailBuy[] InforBuy;
    }

    [Serializable]
    public struct DetailBuy
    {
        public bool isGem;
        public int Purchase;
    }
}