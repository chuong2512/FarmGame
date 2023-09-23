using UnityEngine;

public class ManagerBuilding : MonoBehaviour
{
    public static ManagerBuilding instance = null;
    private int SeriBuilding = 1;
    public Sprite BuildFinish;
    [SerializeField] GameObject Build;
    public GameObject EffectBuildFinish;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("SeriBuilding") == false)
        {
            PlayerPrefs.SetInt("SeriBuilding", 1);
        }
        else if (PlayerPrefs.HasKey("SeriBuilding") == true)
        {
            SeriBuilding = PlayerPrefs.GetInt("SeriBuilding");
            for (int i = 1; i < SeriBuilding; i++)
            {
                if (PlayerPrefs.GetInt("StatusBuilding" + i) < 2)
                {
                    Vector3 target = new Vector3(PlayerPrefs.GetFloat("PosBuildingX" + i), PlayerPrefs.GetFloat("PosBuildingY" + i), PlayerPrefs.GetFloat("PosBuildingZ" + i));
                    GameObject build = Instantiate(Build, target, Quaternion.identity, transform);
                    Building bl = build.GetComponent<Building>();
                    bl.seriBuilding = i;
                }
            }
        }
    }

    private void UpdateSeriBuilding()
    {
        SeriBuilding += 1;
        PlayerPrefs.SetInt("SeriBuilding", SeriBuilding);
    }

    public void RegisterBuilding(int Stype, int Product, int SeriProduct, int time, int lighting, Vector3 target)
    {
        GameObject build = Instantiate(Build, target, Quaternion.identity, transform);
        Building bl = build.GetComponent<Building>();
        bl.seriBuilding = SeriBuilding;
        bl.idStype = Stype;
        bl.idProduct = Product;
        bl.idSeriProduct = SeriProduct;
        bl.totalTime = time;
        bl.lighting = lighting;
        UpdateSeriBuilding();
    }
}
