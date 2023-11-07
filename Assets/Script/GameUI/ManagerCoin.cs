using Script.GameUI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace NongTrai
{
    [DefaultExecutionOrder(-99)]
    public class ManagerCoin : Singleton<ManagerCoin>
    {
        [SerializeField] Text ShowGoldText;
        public Transform pointerGold;
        public GameObject goldFly;

        public int Coin
        {
            get
            {
                if (PlayerPrefs.HasKey("Coin") == false) PlayerPrefs.SetInt("Coin", 250);
                return PlayerPrefs.GetInt("Coin");
            }
            set
            {
                PlayerPrefs.SetInt("Coin", value);
                GameManager.OnChangeCoin?.Invoke();
            }
        }


        void Start()
        {
            ShowGoldText.text = "" + Coin;
        }

        [Button]
        public void ReciveGold(int value)
        {
            Coin += value;
            ShowGoldText.text = "" + Coin;
        }

        [Button]
        public void MunisGold(int value)
        {
            Coin -= value;
            ShowGoldText.text = "" + Coin;
        }

        public void RegisterGoldSingle(int value, Vector3 target)
        {
            GameObject obj = Instantiate(goldFly, target, Quaternion.identity);
            obj.GetComponent<GoldFly>().numberGold = value;
        }
    }
}