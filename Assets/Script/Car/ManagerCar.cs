using UnityEngine;

public class ManagerCar : MonoBehaviour
{
    public static ManagerCar instance;
    private int status;
    public int Coin;
    public int Exp;
    [SerializeField] GameObject CarEmpty;
    [SerializeField] GameObject TruckCar;
    [SerializeField] GameObject MoneyCar;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("StatusCar") == true)
        {
            Coin = PlayerPrefs.GetInt("CoinCar");
            Exp = PlayerPrefs.GetInt("ExpCar");
            if (PlayerPrefs.GetInt("StatusCar") == 1) startCargo(Coin, Exp);
            else if (PlayerPrefs.GetInt("StatusCar") == 2) startMoneygo();
        }
        else if (PlayerPrefs.HasKey("StatusCar") == false)
        {
            PlayerPrefs.SetInt("StatusCar", status);
            PlayerPrefs.SetInt("CoinCar", Coin);
            PlayerPrefs.SetInt("ExpCar", Exp);
        } 
    }
    public void startEmpty()
    {
        CarEmpty.SetActive(true);
        TruckCar.SetActive(false);
        MoneyCar.SetActive(false);
        PlayerPrefs.SetInt("StatusCar", 0);
    }

    public void startCargo(int coin, int exp)
    {
        status = 1;
        PlayerPrefs.SetInt("StatusCar", status);
        Coin = coin;
        PlayerPrefs.SetInt("CoinCar", Coin);
        Exp = exp;
        PlayerPrefs.SetInt("ExpCar", Exp);
        CarEmpty.SetActive(false);
        TruckCar.SetActive(true);
        MoneyCar.SetActive(false);
    }

    public void startMoneygo()
    {
        status = 2;
        PlayerPrefs.SetInt("StatusCar", status);
        MoneyCar.SetActive(true);
        TruckCar.SetActive(false);
        CarEmpty.SetActive(false);
    }

}
