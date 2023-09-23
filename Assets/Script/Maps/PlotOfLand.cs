using UnityEngine;

public class PlotOfLand : MonoBehaviour
{
    private bool dragging;
    private Vector3 camfirstPos;
    private Color defaultColor;
    [SerializeField] int idPOL;
    [SerializeField] SpriteRenderer SprRenderer;
    private void OnMouseDown()
    {
        defaultColor = SprRenderer.color;
        camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SprRenderer.color = new Color(127f / 255, 127f / 255, 127f / 255, 1);
    }

    private void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                SprRenderer.color = defaultColor;
            }
        }
    }
    private void OnMouseUp()
    {
        SprRenderer.color = defaultColor;
        if (dragging == false)
        {
            switch (ManagerMaps.ins.GetStatusPOL(idPOL))
            {
                case 0:
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Ô đất được mở khóa khi bạn đạt cấp độ " + (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1);
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Tanah terbuka di level " + (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1);
                    else str = "Land is unlocked when you reach the level " + (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1);
                    Notification.instance.dialogBelow(str);
                    break;
                case 1:
                    ManagerMaps.ins.RegisterExpland(idPOL);
                    break;
                case 2:
                    string strOne;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        strOne = "Mảnh đất của bạn đang có cây hoang và đá, hãy loại bỏ chúng để bắt đầu sử dụng!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        strOne = "Tanah Anda memiliki tumbuhan dan bebatuan liar, singkirkan untuk mulai menggunakan!";
                    else strOne = "Your Plot having wild plant and rocky, please remove it to begin use";
                    Notification.instance.dialogBelow(strOne);
                    break;
            }
        }
        else if (dragging == true) dragging = false;
    }
}
