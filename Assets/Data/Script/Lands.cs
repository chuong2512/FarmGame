using UnityEngine;

public class Lands : ScriptableObject 
{
	[System.Serializable]
	public struct DetailLand
	{
        public string name;
        public string engName;
        public string nameINS;
        public int purchase;
        public int mutiGold;
        public int levelOpen;
        public int distanceLvOpen;
        public int amountOpen;
		public Sprite iconStore;
	}
	public DetailLand[] Land;

}
