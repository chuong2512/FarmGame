using UnityEngine;
using System.Collections;

namespace NongTrai
{
    public class Decorate : MonoBehaviour
    {
        private int _countOverlap;
        private bool _isIEDrag;
        private bool _dragging;
        private float _distanceX;
        private float _distanceY;
        private Vector3 _oldPos;
        private Vector3 _camfirstPos;
        private IEnumerator _onDrag;
        private Rigidbody2D _rgb2D;

        [SerializeField] bool isRoration;
        [SerializeField] int idDecorate;
        [SerializeField] GameObject Foot;

        [SerializeField] OrderPro[] orderPro;

        [HideInInspector] public int idSerial;
        [HideInInspector] public bool overlap;

        void Start()
        {
            _distanceX = ManagerGame.Instance.DistaneX;
            _distanceY = ManagerGame.Instance.DistaneY;
        }

        private void OnMouseDown()
        {
            _oldPos = transform.position;
            if (Camera.main != null) _camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ColorS(0.3f, 0.3f, 0.3f, 1f);
            _onDrag = WaitDrag();
            StartCoroutine(_onDrag);
        }

        private IEnumerator WaitDrag()
        {
            _isIEDrag = true;
            yield return new WaitForSeconds(0.1f);
            Vector3 target = new Vector3(_camfirstPos.x, _camfirstPos.y + 0.5f, 0);
            ManagerTool.instance.RegisterTimeMove(target);
            yield return new WaitForSeconds(0.55f);
            if (Camera.main != null &&
                Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
            {
                MainCamera.instance.DisableAll();
                MainCamera.instance.lockCam();
                ManagerTool.instance.CloseTimeMove();
                ColorS(1f, 1f, 1f, 1f);
                StartMove();
            }
        }

        private void OnMouseDrag()
        {
            if (_dragging)
            {
                Vector2 PosCam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 target = new Vector2(((int) (PosCam.x / _distanceX)) * _distanceX,
                    ((int) (PosCam.y / _distanceY)) * _distanceY);
                transform.position = target;
            }
            else if (Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) >= 0.2f)
            {
                if (!_isIEDrag) return;
                _isIEDrag = false;
                StopCoroutine(_onDrag);
                ColorS(1f, 1f, 1f, 1f);
                ManagerTool.instance.CloseTimeMove();
            }
        }

        public void Overlap()
        {
            if (_dragging == true)
            {
                _countOverlap += 1;
                if (_countOverlap == 1)
                {
                    overlap = true;
                    ColorS(1f, 127f / 255, 127f / 255, 1f);
                }
            }
        }

        public void UnOverlap()
        {
            if (_dragging != true) return;
            _countOverlap -= 1;
            if (_countOverlap == 0)
            {
                overlap = false;
                ColorS(1f, 1f, 1f, 1f);
            }
        }

        private void OnMouseUp()
        {
            if (_isIEDrag)
            {
                _isIEDrag = false;
                StopCoroutine(_onDrag);
                ManagerTool.instance.CloseTimeMove();
            }

            switch (_dragging)
            {
                case false:
                {
                    if (Camera.main != null &&
                        Vector3.Distance(_camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)
                    {
                        ColorS(1f, 1f, 1f, 1f);
                        ManagerAudio.Instance.PlayAudio(Audio.Click);
                        MainCamera.instance.DisableAll();
                        if (isRoration)
                        {
                            transform.localScale = transform.localScale.x == 1
                                ? new Vector3(-1, 1, 1)
                                : new Vector3(1, 1, 1);
                        }
                    }

                    break;
                }
                case true:
                {
                    if (overlap == false)
                    {
                        PlayerPrefs.SetFloat("PosDecorateX" + idDecorate + "" + idSerial, transform.position.x);
                        PlayerPrefs.SetFloat("PosDecorateY" + idDecorate + "" + idSerial, transform.position.y);
                    }
                    else if (overlap == true)
                    {
                        overlap = false;
                        transform.position = _oldPos;
                        ColorS(1f, 1f, 1f, 1f);
                    }

                    MainCamera.instance.unLockCam();
                    _countOverlap = 0;
                    DoneMove();
                    break;
                }
            }
        }

        public void StartMove()
        {
            _dragging = true;
            for (int i = 0; i < orderPro.Length; i++)
            {
                foreach (var spr in orderPro[i].SprRenderer)
                    spr.sortingLayerName = "Move";
            }

            _rgb2D = Foot.AddComponent<Rigidbody2D>();
            _rgb2D.bodyType = RigidbodyType2D.Kinematic;
        }

        public void DoneMove()
        {
            _dragging = false;
            for (int i = 0; i < orderPro.Length; i++)
            {
                foreach (var spr in orderPro[i].SprRenderer)
                    spr.sortingLayerName = "Default";
            }

            Order();
            Destroy(_rgb2D);
        }

        public void Order()
        {
            int order = (int) (transform.position.y * (-100));
            for (int i = 0; i < orderPro.Length; i++)
            {
                for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                    orderPro[i].SprRenderer[j].sortingOrder = order + orderPro[i].order;
            }
        }

        private void ColorS(float r, float g, float b, float a)
        {
            for (int i = 0; i < orderPro.Length; i++)
            {
                for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                    orderPro[i].SprRenderer[j].color = new Color(r, g, b, a);
            }
        }
    }
}