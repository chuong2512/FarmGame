using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Information
{
	[System.Serializable]
	public struct Info
	{
		public string name;
		public int status, levelOpen, amount, total, goldPrice;
		public Text txtName, txtInfo, txtGoldPrice, txtAmount;
		public Image icon;
	}
	public Info [] info;
}