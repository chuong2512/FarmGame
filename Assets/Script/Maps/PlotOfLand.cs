using NongTrai;
using UnityEngine;
using UnityEngine.Serialization;

namespace NongTrai
{
    public class PlotOfLand : MonoBehaviour
    {
        private bool _dragging;
        private Vector3 _camfirstPos;
        private Color _defaultColor;
        [SerializeField] int idPOL;
        [FormerlySerializedAs("SprRenderer")] [SerializeField] SpriteRenderer sprRenderer;

        private void OnMouseDown()
        {
            _defaultColor = sprRenderer.color;
            _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            sprRenderer.color = new Color(127f / 255, 127f / 255, 127f / 255, 1);
        }

        private void OnMouseDrag()
        {
            if (_dragging) return;
            if (!(Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >
                  0.1f)) return;
            _dragging = true;
            sprRenderer.color = _defaultColor;
        }

        private void OnMouseUp()
        {
            sprRenderer.color = _defaultColor;
            switch (_dragging)
            {
                case false:
                    switch (ManagerMaps.ins.GetStatusPol(idPOL))
                    {
                        case 0:
                            string str = Application.systemLanguage switch
                            {
                                SystemLanguage.Vietnamese => "Ô đất được mở khóa khi bạn đạt cấp độ " +
                                                             (ManagerData.instance.plotOfLands.Data[idPOL]
                                                                 .LevelUnlock + 1),
                                SystemLanguage.Indonesian => "Tanah terbuka di level " +
                                                             (ManagerData.instance.plotOfLands.Data[idPOL]
                                                                 .LevelUnlock + 1),
                                _ => "Land is unlocked when you reach the level " +
                                     (ManagerData.instance.plotOfLands.Data[idPOL].LevelUnlock + 1)
                            };
                            Notification.Instance.dialogBelow(str);
                            break;
                        case 1:
                            ManagerMaps.ins.RegisterExpland(idPOL);
                            break;
                        case 2:
                            string strOne = Application.systemLanguage switch
                            {
                                SystemLanguage.Vietnamese =>
                                    "Mảnh đất của bạn đang có cây hoang và đá, hãy loại bỏ chúng để bắt đầu sử dụng!",
                                SystemLanguage.Indonesian =>
                                    "Tanah Anda memiliki tumbuhan dan bebatuan liar, singkirkan untuk mulai menggunakan!",
                                _ => "Your Plot having wild plant and rocky, please remove it to begin use"
                            };
                            Notification.Instance.dialogBelow(strOne);
                            break;
                    }

                    break;
                case true:
                    _dragging = false;
                    break;
            }
        }
    }
}