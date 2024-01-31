using NongTrai;
using UnityEngine;
using UnityEngine.UI;

public class CompAds : MonoBehaviour
{
    [HideInInspector] public int idStype;

    [HideInInspector] public GameObject objUseGem;
    //-------------------------------------------------------
    
    void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);

        QuangCao.Instance.ShowReAds(ReceiveReward);
    }

    void ReceiveReward()
    {
        switch (idStype)
        {
            case 0:
                objUseGem.GetComponent<Ruong>().UseDiamond();
                break;
            case 1:
                objUseGem.GetComponent<Animal>().UseDiamond();
                break;
            case 2:
                objUseGem.GetComponent<OldTree>().UseDiamond();
                break;
            case 3:
                objUseGem.GetComponent<Building>().UseGem();
                break;
            case 4:
                objUseGem.GetComponent<RuongHoa>().UseDiamond();
                break;
        }
    }
    
    void OnMouseDown()
    {
        transform.localScale = new Vector3(0.7f, 0.8f, 1f);
    }
}