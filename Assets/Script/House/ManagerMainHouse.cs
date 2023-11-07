using NongTrai;
using UnityEngine;

namespace NongTrai
{
    public class ManagerMainHouse : MonoBehaviour
    {
        public static ManagerMainHouse instance;
        private int LevelMainHouse;
        [SerializeField] GameObject MainHouse;

        private void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("LevelMainHouse") == false) PlayerPrefs.SetInt("LevelMainHouse", LevelMainHouse);
            else
            {
                LevelMainHouse = PlayerPrefs.GetInt("LevelMainHouse");
                if (LevelMainHouse > 0)
                {
                    Destroy(MainHouse);
                    Instantiate(ManagerData.instance.mainHouses.Home[LevelMainHouse].house, transform.position,
                        Quaternion.identity, transform);
                }
            }
        }

        private void UpdateMainHouse()
        {
            LevelMainHouse += 1;
            PlayerPrefs.SetInt("LevelMainHouse", LevelMainHouse);
            Destroy(MainHouse);
            Instantiate(ManagerData.instance.mainHouses.Home[LevelMainHouse].house, transform.position,
                Quaternion.identity, transform);
        }

        public void CheckUpdate(int level)
        {
            if (LevelMainHouse + 1 < ManagerData.instance.mainHouses.Home.Length - 1)
                if (level == ManagerData.instance.mainHouses.Home[LevelMainHouse + 1].levelOpen)
                    UpdateMainHouse();
        }
    }
}