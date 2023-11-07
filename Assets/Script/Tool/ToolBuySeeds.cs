using UnityEngine;

namespace NongTrai
{
    public class ToolBuySeeds : MonoBehaviour
    {
        private bool dragging;
        private Vector3 firstPosCam;
        [SerializeField] int idSeed;

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
            switch (dragging)
            {
                case false:
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    switch (ManagerTool.instance.ClickUseGemBuySeed)
                    {
                        case 0:
                        {
                            ManagerTool.instance.ClickUseGemBuySeed += 1;
                            string txtString = Application.systemLanguage == SystemLanguage.Vietnamese
                                ? "Nhấn thêm một lần nữa để xác nhận?"
                                : "Press one more to confirm?";
                            Notification.Instance.dialogBelow(txtString);
                            break;
                        }
                        case 1 when ManagerGem.Instance.GemLive >= 2:
                        {
                            ManagerGem.Instance.MunisGem(2);
                            ManagerTool.instance.ClickUseGemBuySeed = 0;
                            Vector3 target = new Vector3(transform.position.x, transform.position.y, 0);
                            ManagerMarket.instance.BuySeeds(idSeed, target);
                            break;
                        }
                        case 1:
                        {
                            string txtString = Application.systemLanguage == SystemLanguage.Vietnamese
                                ? "Bạn không đủ kim cương!"
                                : "You haven't enough diamonds!";
                            Notification.Instance.dialogBelow(txtString);
                            break;
                        }
                    }

                    break;
                }
                case true:
                    dragging = false;
                    break;
            }
        }
    }
}