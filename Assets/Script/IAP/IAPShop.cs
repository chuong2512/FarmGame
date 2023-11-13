using NongTrai;
using Script.IAP;
using Sirenix.OdinInspector;
using UnityEngine;

public class IAPShop : MonoBehaviour
{
    [SerializeField] private GameObject _blur;

    [InlineEditor()] [SerializeField] private BaseShop _coinShop, _gemShop;

    public void OpenGemShop()
    {
        /*_blur.gameObject.SetActive(true);
        _gemShop.gameObject.SetActive(true);
        _coinShop.gameObject.SetActive(false);*/
        Notification.Instance.dialogBelow("Coming soon!");
    }

    public void OpenCoinShop()
    {
        /*_blur.gameObject.SetActive(true);
        _coinShop.gameObject.SetActive(true);
        _gemShop.gameObject.SetActive(false);*/
        Notification.Instance.dialogBelow("Coming soon!");
    }
    
    public void Close()
    {
        _blur.gameObject.SetActive(false);
        _gemShop.gameObject.SetActive(false);
        _coinShop.gameObject.SetActive(false);
    }
}