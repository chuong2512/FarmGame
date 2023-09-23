using UnityEngine;
using UnityEngine.UI;

public class ManagerCoin : MonoBehaviour
{
    public static ManagerCoin instance;
    [SerializeField] Text ShowGoldText;
    public Transform pointerGold;
    public GameObject goldFly;
    public int Coin
    {
        get { if (PlayerPrefs.HasKey("Coin") == false) PlayerPrefs.SetInt("Coin", 250); return PlayerPrefs.GetInt("Coin"); }
        set { PlayerPrefs.SetInt("Coin", value); }
    }
    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        ShowGoldText.text = "" + Coin;
    }
    public void reciveGold(int value)
    {
        Coin += value;
        ShowGoldText.text = "" + Coin;
    }
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
