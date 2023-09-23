using UnityEngine;

public class ManagerItem : MonoBehaviour
{
    public static ManagerItem instance = null;
    private int status;
    public int[] totalItem = new int[4];
    public idItemUnlock[] idItemUnlock = new idItemUnlock[4];

    private void Awake()
    {
        InitData();
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {

    }

    public void UpdateItem(int stype, int id)
    {
        idItemUnlock[stype].IdItem[totalItem[stype]] = id;
        PlayerPrefs.SetInt("IdItemUnlock" + stype + "" + totalItem[stype], id);
        totalItem[stype] += 1;
        PlayerPrefs.SetInt("totalItem" + stype, totalItem[stype]);
        ManagerMission.instance.NewItem();
    }

    private void InitData()
    {
        if (PlayerPrefs.HasKey("StatusManagerItem") == false)
        {
            PlayerPrefs.SetInt("StatusManagerItem", status);
            for (int i = 0; i < idItemUnlock.Length; i++)
            {
                PlayerPrefs.SetInt("totalItem" + i, totalItem[i]);
                for (int k = 0; k < idItemUnlock[i].IdItem.Length; k++)
                {
                    PlayerPrefs.SetInt("IdItemUnlock" + i + "" + k, idItemUnlock[i].IdItem[k]);
                }
            }
        }
        else if (PlayerPrefs.HasKey("StatusManagerItem") == true)
        {
            status = PlayerPrefs.GetInt("StatusManagerItem");
            for (int i = 0; i < idItemUnlock.Length; i++)
            {
                totalItem[i] = PlayerPrefs.GetInt("totalItem" + i);
                for (int k = 0; k < idItemUnlock[i].IdItem.Length; k++)
                {
                    idItemUnlock[i].IdItem[k] = PlayerPrefs.GetInt("IdItemUnlock" + i + "" + k);
                }
            }
        }
    }
}
