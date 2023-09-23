using UnityEngine;

public class ManagerRuong : MonoBehaviour
{
    public static ManagerRuong instance;
    public int landSpace;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < ManagerShop.instance.Field.Length; i++)
        {
            if (PlayerPrefs.HasKey("amountField" + i) == true)
            {
                int amountField = PlayerPrefs.GetInt("amountField" + i);
                if (amountField > 6)
                {
                    for (int j = 6; j < amountField; j++)
                    {
                        Vector2 target = new Vector2(PlayerPrefs.GetFloat("PosFieldX" + j), PlayerPrefs.GetFloat("PosFieldY" + j));
                        GameObject objField = Instantiate(ManagerShop.instance.Field[i], target, Quaternion.identity, ManagerShop.instance.parentField[i]);
                        objField.GetComponent<Ruong>().idRuong = j;
                    }
                }
            }
        }
        if (!PlayerPrefs.HasKey("LandSpace")) PlayerPrefs.SetInt("LandSpace", landSpace);
        else if (PlayerPrefs.HasKey("LandSpace")) landSpace = PlayerPrefs.GetInt("LandSpace");
    }

    public void CompleteCrop()
    {
        landSpace += 1;
        PlayerPrefs.SetInt("LandSpace", landSpace);
    }

    public void CompleteHarvestCrop()
    {
        landSpace -= 1;
        PlayerPrefs.SetInt("LandSpace", landSpace);
    }
}
