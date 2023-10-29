using UnityEngine;

public class ToolBuyFlower : MonoBehaviour
{
    private bool dragging;
    private Vector3 firstPosCam;
    [SerializeField] int idFlower;

    private void OnMouseDown()
    {
        firstPosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }
    private void OnMouseDrag()
    {
        if (dragging == false)
        {
            if (Vector3.Distance(firstPosCam, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
            {
                dragging = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
    private void OnMouseUp()
    {
        if (dragging == false)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            if (ManagerTool.instance.ClickUseGemBuyFlower == 0)
            {
                ManagerTool.instance.ClickUseGemBuyFlower += 1;
                string txtString = Application.systemLanguage == SystemLanguage.Vietnamese ?
                        "Nhấn thêm một lần nữa để xác nhận?" : "Press one more to confirm?";
                Notification.instance.dialogBelow(txtString);
            }
            else if (ManagerTool.instance.ClickUseGemBuyFlower == 1)
            {
                if (ManagerGem.instance.GemLive >= 2)
                {
                    ManagerGem.instance.MunisGem(2);
                    ManagerTool.instance.ClickUseGemBuyFlower = 0;
                    Vector3 target = new Vector3(transform.position.x, transform.position.y, 0);
                    ManagerMarket.instance.BuySeeds(idFlower, target);
                }
                else
                {
                    string txtString = Application.systemLanguage == SystemLanguage.Vietnamese
                        ? "Bạn không đủ kim cương!" : "You haven't enough diamonds!";
                    Notification.instance.dialogBelow(txtString);
                }
            }
        }
        else if (dragging == true) dragging = false;
    }
}
