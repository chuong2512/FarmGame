using UnityEngine;
using System.Collections;
using NongTrai;
using UnityEngine.Serialization;

namespace NongTrai
{
    public class RockPlotOfLand : MonoBehaviour
    {
        [FormerlySerializedAs("idPOL")] [SerializeField] int idPol;
        [SerializeField] private int idSeri;
        [SerializeField] private int idDecorate;
        private int _status;
        private bool _dragging;
        private GameObject _obj;
        private SpriteRenderer _sprRenderer;
        private Vector3 _firstCamPos;

        void Start()
        {
            _sprRenderer = this.GetComponent<SpriteRenderer>();
            float order = transform.position.y * (-100);
            _sprRenderer.sortingOrder = (int) order;
        }

        void OnMouseDown()
        {
            _firstCamPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _sprRenderer.color = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

        

        void OnMouseUp()
        {
            if (_dragging == false)
            {
                _sprRenderer.color = Color.white;
                switch (ManagerMaps.ins.GetStatusPol(idPol))
                {
                    case 0:
                        string str;
                        if (Application.systemLanguage == SystemLanguage.Vietnamese)
                            str = "Ô đất được mở khóa khi bạn đạt cấp độ " +
                                  (ManagerData.instance.plotOfLands.Datas[idPol].LevelUnlock + 1);
                        else if (Application.systemLanguage == SystemLanguage.Indonesian)
                            str = "Tanah terbuka di level " +
                                  (ManagerData.instance.plotOfLands.Datas[idPol].LevelUnlock + 1);
                        else
                            str = "Land is unlocked when you reach the level " +
                                  (ManagerData.instance.plotOfLands.Datas[idPol].LevelUnlock + 1);
                        Notification.Instance.dialogBelow(str);
                        break;
                    case 1:
                        ManagerMaps.ins.RegisterExpland(idPol);
                        break;
                    case 2:
                        Vector2 target = new Vector2(transform.position.x - 0.7f, transform.position.y + 0.5f);
                        ManagerTool.instance.ShowToolPlotOfLand(idDecorate, idPol, idSeri, target);
                        break;
                }
            }
            else _dragging = false;
        }

        void OnMouseDrag()
        {
            if (_dragging == false)
            {
                if (Vector3.Distance(_firstCamPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.1f)
                {
                    _dragging = true;
                    _sprRenderer.color = Color.white;
                }
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "ToolDecorate" && ManagerTool.instance.dragging == true && _status == 0
                && idPol == ManagerTool.instance.idPOL
                && idSeri == ManagerTool.instance.idDecoratePOL
                && idDecorate == ManagerTool.instance.idDecorate)
            {
                if (ManagerMarket.instance.QuantityToolDecorate[idDecorate] > 0)
                {
                    ManagerTool.instance.checkCollider = true;
                    ManagerMarket.instance.MinusItem(5, idDecorate, 1);
                    ConditionEnough();
                }
                else if (ManagerMarket.instance.QuantityToolDecorate[idDecorate] == 0)
                {
                    ManagerTool.instance.checkCollider = true;
                    int Purchase = ManagerData.instance.toolDecorate.Datas[idDecorate].Purchare;
                    ManagerUseGem.Instance.ShowDialogUseDiamond(idDecorate, StypeUseGem.DecorateRockBig, Purchase,
                        gameObject);
                }
            }
        }

        
        IEnumerator DestroyDecorate()
        {
            yield return new WaitForSeconds(1.6f);
            Destroy(_obj);
            if (Experience.Instance.level < 7) Experience.Instance.registerExpSingle(1, transform.position);
            else Experience.Instance.registerExpSingle(5, transform.position);
            ManagerMaps.ins.DestroyDone(idPol);
            Destroy(gameObject);
        }

        
        public void ConditionEnough()
        {
            _status = 1;
            PlayerPrefs.SetInt("StatusRockBigPOL" + idPol + "" + idSeri, _status);
            Vector2 target = new Vector2(transform.position.x, transform.position.y + 0.3f);
            _obj = Instantiate(ManagerTool.instance.objBombSmall, target, Quaternion.identity);
            ManagerMaps.ins.RegisterDestroyDecorate(idPol);
            StartCoroutine(DestroyDecorate());
        }

        
        /*private void InitData()
        {
            if (PlayerPrefs.HasKey("StatusRockBigPOL" + idPol + "" + idSeri) == false)
            {
                _status = 0;
                PlayerPrefs.SetInt("StatusRockBigPOL" + idPol + "" + idSeri, _status);
            }
            else if (PlayerPrefs.HasKey("StatusRockBigPOL" + idPol + "" + idSeri) == true)
            {
                _status = PlayerPrefs.GetInt("StatusRockBigPOL" + idPol + "" + idSeri);
                if (_status == 1) Destroy(gameObject);
            }
        }*/
    }
}