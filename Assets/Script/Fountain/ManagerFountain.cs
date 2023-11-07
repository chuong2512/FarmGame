using UnityEngine;
using UnityEngine.Serialization;

namespace NongTrai
{
    public class ManagerFountain : Singleton<ManagerFountain>
    {
        [FormerlySerializedAs("LevelBuildFountain")]
        public int levelBuildFountain;

        [SerializeField] GameObject Fountain;
        [SerializeField] GameObject Foundation;

        private static int Status
        {
            get
            {
                if (PlayerPrefs.HasKey("StatusFountain") == false) PlayerPrefs.SetInt("StatusFountain", 0);
                return PlayerPrefs.GetInt("StatusFountain");
            }
            set => PlayerPrefs.SetInt("StatusFountain", value);
        }

        public void RegisterBuild(int level)
        {
            if (level != levelBuildFountain) return;
            Status = 1;
            ManagerBuilding.Instance.RegisterBuilding(2, 0, 0, 259200, 100, Foundation.transform.position);
            Destroy(Foundation);
        }

        public void BuildFountain(Vector3 target)
        {
            Status = 2;
            Instantiate(Fountain, target, Quaternion.identity, transform);
        }

        void Start()
        {
            switch (Status)
            {
                case 1:
                    Destroy(Foundation);
                    break;
                case 2:
                    Instantiate(Fountain, Foundation.transform.position, Quaternion.identity, transform);
                    Destroy(Foundation);
                    break;
            }
        }
    }
}