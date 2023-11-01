using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-99)]
public class ManagerGem : MonoBehaviour
{
    public static ManagerGem instance = null;
    [SerializeField] Text ShowDiamondText;
    [SerializeField] GameObject GemFly;
    public Transform PoiterGem;
    public int GemLive
    {
        get { if (PlayerPrefs.HasKey("Gem") == false) PlayerPrefs.SetInt("Gem", 30); return PlayerPrefs.GetInt("Gem"); }
        set { PlayerPrefs.SetInt("Gem", value); }
    }
    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        ShowDiamondText.text = "" + GemLive;
    }
    public void ReciveGem(int value)
    {
        GemLive += value;
        ShowDiamondText.text = "" + GemLive;
    }
    public void MunisGem(int value)
    {
        GemLive -= value;
        ShowDiamondText.text = "" + GemLive;
    }
    public void RegisterGemSingle(int value, Vector3 target)
    {
        GameObject obj = Instantiate(GemFly, target, Quaternion.identity);
        obj.GetComponent<GemFly>().numberGem = value;
    }
}
