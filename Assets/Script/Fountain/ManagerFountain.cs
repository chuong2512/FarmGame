using UnityEngine;

public class ManagerFountain : MonoBehaviour
{
    public static ManagerFountain instance;
    public int LevelBuildFountain;
    [SerializeField] GameObject Fountain;
    [SerializeField] GameObject Foundation;
    private int status
    {
        get { if (PlayerPrefs.HasKey("StatusFountain") == false) PlayerPrefs.SetInt("StatusFountain", 0); return PlayerPrefs.GetInt("StatusFountain"); }
        set { PlayerPrefs.SetInt("StatusFountain", value); }
    }
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        if (status == 1) Destroy(Foundation);
        else if (status == 2)
        {
            Instantiate(Fountain, Foundation.transform.position, Quaternion.identity, transform);
            Destroy(Foundation);
        }
    }
    public void RegisterBuild(int level)
    {
        if (level == LevelBuildFountain)
        {
            status = 1;
            ManagerBuilding.instance.RegisterBuilding(2, 0, 0, 259200, 100, Foundation.transform.position);
            Destroy(Foundation);
        }
    }
    public void BuildFountain(Vector3 target)
    {
        status = 2;
        Instantiate(Fountain, target, Quaternion.identity, transform);
    }
}
