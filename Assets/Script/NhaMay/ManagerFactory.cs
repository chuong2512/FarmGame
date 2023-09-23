using UnityEngine;

public class ManagerFactory : MonoBehaviour
{
    public static ManagerFactory instance = null;

    private int[] NumberFactory = new int[11];

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < NumberFactory.Length; i++)
        {
            if (PlayerPrefs.HasKey("NumberFactory" + i) == false)
            {
                PlayerPrefs.SetInt("NumberFactory" + i, 0);
            }
            else if (PlayerPrefs.HasKey("NumberFactory" + i) == true)
            {
                if (PlayerPrefs.GetInt("NumberFactory" + i) > 0)
                {
                    NumberFactory[i] = PlayerPrefs.GetInt("NumberFactory" + i);
                    for (int j = 0; j < NumberFactory[i]; j++)
                    {
                        Vector2 target = new Vector2(PlayerPrefs.GetFloat("PosFactoryX" + i + "" + j), PlayerPrefs.GetFloat("PosFactoryY" + i + "" + j));
                        GameObject obj = Instantiate(ManagerShop.instance.Factory[i], target, Quaternion.identity, ManagerShop.instance.parentFactory[i]);
                        obj.GetComponent<NhaMay>().idSoNhaMay = j;
                    }
                }
            }
        }
    }

    public void UpdateNumberFactory(int idFactory)
    {
        NumberFactory[idFactory] += 1;
        PlayerPrefs.SetInt("NumberFactory" + idFactory, NumberFactory[idFactory]);
    }
}
