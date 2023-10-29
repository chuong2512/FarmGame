using UnityEngine;
using UnityEngine.UI;

public class Lighting : MonoBehaviour
{
    public Text GemText;
    public Text TitleText;
    [HideInInspector] public int status;
    [HideInInspector] public int idStype;
    [HideInInspector] public int quantityGem;
    [HideInInspector] public GameObject objUseGem;
    //-------------------------------------------------------
    void OnMouseDown()
    {
        transform.localScale = new Vector3(0.7f, 0.8f, 1f);
    }

    void OnMouseUp()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        switch (status)
        {
            case 0:
                status = 1;
                string str;
                if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    str = "Nhấn thêm một lần nữa để xác nhận!";
                else if (Application.systemLanguage == SystemLanguage.Indonesian)
                    str = "Ketuk Sekali lagi untuk mengonfirmasi! ";
                else str = "Press one more to confirm!";
                Notification.instance.dialogBelow(str);
                break;
            case 1:
                status = 0;
                if (ManagerGem.instance.GemLive >= quantityGem)
                {
                    ManagerGem.instance.MunisGem(quantityGem);
                    switch (idStype)
                    {
                        case 0: objUseGem.GetComponent<Ruong>().UseDiamond(); break;
                        case 1: objUseGem.GetComponent<Animal>().UseDiamond(); break;
                        case 2: objUseGem.GetComponent<OldTree>().UseDiamond(); break;
                        case 3: objUseGem.GetComponent<Building>().UseGem(); break;
                        case 4: objUseGem.GetComponent<RuongHoa>().UseDiamond(); break;
                    }
                }
                else if (ManagerGem.instance.GemLive < quantityGem)
                {
                    string strOne;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        strOne = "Bạn không đủ gem!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        strOne = "Anda tidak memiliki cukup permata!";
                    else strOne = "You haven't enough gem!";
                    Notification.instance.dialogBelow(strOne);
                }
                break;
        }
    }
}
