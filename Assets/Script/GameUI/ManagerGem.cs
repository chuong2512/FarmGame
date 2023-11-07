using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NongTrai
{
    [DefaultExecutionOrder(-99)]
    public class ManagerGem : Singleton<ManagerGem>
    {
        [FormerlySerializedAs("ShowDiamondText")] [SerializeField]
        Text showDiamondText;

        [FormerlySerializedAs("GemFly")] [SerializeField]
        GameObject gemFly;

        [FormerlySerializedAs("PoiterGem")] public Transform poiterGem;

        public int GemLive
        {
            get
            {
                if (PlayerPrefs.HasKey("Gem") == false) PlayerPrefs.SetInt("Gem", 30);
                return PlayerPrefs.GetInt("Gem");
            }
            set => PlayerPrefs.SetInt("Gem", value);
        }

        void Start()
        {
            showDiamondText.text = "" + GemLive;
        }

        public void ReciveGem(int value)
        {
            GemLive += value;
            showDiamondText.text = "" + GemLive;
        }

        public void MunisGem(int value)
        {
            GemLive -= value;
            showDiamondText.text = "" + GemLive;
        }

        public void RegisterGemSingle(int value, Vector3 target)
        {
            GameObject obj = Instantiate(gemFly, target, Quaternion.identity);
            obj.GetComponent<GemFly>().numberGem = value;
        }
    }
}