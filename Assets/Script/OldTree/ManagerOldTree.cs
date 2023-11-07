using UnityEngine;

namespace NongTrai
{
    [System.Serializable]
    public class ManagerOldTree : MonoBehaviour
    {
        public static ManagerOldTree instance;

        // Use this for initialization
        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            for (int i = 0; i < ManagerShop.instance.Tree.Length; i++)
            {
                if (PlayerPrefs.GetInt("amountTree" + i) > 0)
                {
                    int amount = PlayerPrefs.GetInt("amountTree" + i);
                    for (int j = 0; j < amount; j++)
                    {
                        Vector3 target = new Vector3(PlayerPrefs.GetFloat("PosTreeX" + i + "" + j),
                            PlayerPrefs.GetFloat("PosTreeY" + i + "" + j), (-0.5f));
                        GameObject obj = Instantiate(ManagerShop.instance.Tree[i], target, Quaternion.identity,
                            ManagerShop.instance.parentTree[i]);
                        obj.GetComponent<OldTree>().idAmountOldTree = j;
                    }
                }
            }
        }
    }
}