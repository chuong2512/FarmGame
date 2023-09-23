using UnityEngine;

public class BreadFeeds : ScriptableObject {
	[System.Serializable]
	public struct DetailBreadFeed{
		public int id;
		public string name, engName;
		public int time, sell;
		public int stypeIDYC1, IdYc1, stypeIDYC2, IdYc2;
		public Sprite item;
	}

	public DetailBreadFeed[] BreadFeed;
}
