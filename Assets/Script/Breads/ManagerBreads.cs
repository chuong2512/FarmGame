using UnityEngine;


public class ManagerBreads : MonoBehaviour
{
    public static ManagerBreads instance;
    private int[] NumberCage = new int[7];

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        for (int i = 0; i < NumberCage.Length; i++)
        {
            if (PlayerPrefs.HasKey("NumberCage" + i) == false)
            {
                PlayerPrefs.SetInt("NumberCage" + i, 0);
            }
            else if (PlayerPrefs.HasKey("NumberCage" + i) == true)
            {
                if (PlayerPrefs.GetInt("NumberCage" + i) > 0)
                {
                    NumberCage[i] = PlayerPrefs.GetInt("NumberCage" + i);
                    for (int j = 0; j < NumberCage[i]; j++)
                    {
                        Vector2 target = new Vector2(PlayerPrefs.GetFloat("PosCageX" + i + "" + j), PlayerPrefs.GetFloat("PosCageY" + i + "" + j));
                        GameObject obj = Instantiate(ManagerShop.instance.Cage[i], target, Quaternion.identity, ManagerShop.instance.parentCage[i]);
                        obj.transform.GetChild(0).GetComponent<HomeAnimal>().idAmountHome = j;
                    }
                }
            }
        }
    }
    public void UpdateNumberCage(int idCage)
    {
        NumberCage[idCage] += 1;
        PlayerPrefs.SetInt("NumberCage" + idCage, NumberCage[idCage]);
    }
}

