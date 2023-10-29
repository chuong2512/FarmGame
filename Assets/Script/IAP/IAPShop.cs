using Script.IAP;
using Sirenix.OdinInspector;
using UnityEngine;

public class IAPShop : MonoBehaviour
{
    [InlineEditor()][SerializeField] private BaseShop _coinShop, _gemShop;

    public void OpenGemShop()
    {
        _gemShop.gameObject.SetActive(true);
        _coinShop.gameObject.SetActive(false);
    }
    
    public void OpenCoinShop()
    {
        _coinShop.gameObject.SetActive(true);
        _gemShop.gameObject.SetActive(false);
    }
}
