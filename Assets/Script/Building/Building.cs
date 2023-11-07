namespace NongTrai
{
    using UnityEngine;
    using System.Collections;

    public class Building : MonoBehaviour
    {
        private int status;
        private Vector3 camfirstPos;
        private IEnumerator IETimeLive;
        [SerializeField] Animator Ani;
        [SerializeField] OrderPro[] OrderBulding;
        [HideInInspector] public int seriBuilding;
        [HideInInspector] public string NameProduct;
        [HideInInspector] public int idStype;
        [HideInInspector] public int idProduct;
        [HideInInspector] public int idSeriProduct;
        [HideInInspector] public int lighting;
        [HideInInspector] public int timeLive;
        [HideInInspector] public int totalTime;

        void Start()
        {
            if (PlayerPrefs.HasKey("StatusBuilding" + seriBuilding) == false)
            {
                timeLive = totalTime;
                PlayerPrefs.SetInt("StatusBuilding" + seriBuilding, status);
                PlayerPrefs.SetInt("StypeBuilding" + seriBuilding, idStype);
                PlayerPrefs.SetInt("ProductBuilding" + seriBuilding, idProduct);
                PlayerPrefs.SetInt("SeriProductBuilding" + seriBuilding, idSeriProduct);
                PlayerPrefs.SetInt("TotalTimeBuilding" + seriBuilding, totalTime);
                PlayerPrefs.SetInt("TimeLiveBuilding" + seriBuilding, timeLive);
                PlayerPrefs.SetInt("TimeLastBuilding" + seriBuilding, ManagerGame.Instance.RealTime());
                PlayerPrefs.SetInt("LightingBuilling" + seriBuilding, lighting);
                PlayerPrefs.SetFloat("PosBuildingX" + seriBuilding, transform.position.x);
                PlayerPrefs.SetFloat("PosBuildingY" + seriBuilding, transform.position.y);
                PlayerPrefs.SetFloat("PosBuildingZ" + seriBuilding, transform.position.z);
                IETimeLive = TimeFinish();
                StartCoroutine(IETimeLive);
            }
            else if (PlayerPrefs.HasKey("StatusBuilding" + seriBuilding))
            {
                status = PlayerPrefs.GetInt("StatusBuilding" + seriBuilding);
                idStype = PlayerPrefs.GetInt("StypeBuilding" + seriBuilding);
                idProduct = PlayerPrefs.GetInt("ProductBuilding" + seriBuilding);
                idSeriProduct = PlayerPrefs.GetInt("SeriProductBuilding" + seriBuilding);
                totalTime = PlayerPrefs.GetInt("TotalTimeBuilding" + seriBuilding);
                lighting = PlayerPrefs.GetInt("LightingBuilling" + seriBuilding);
                if (status == 0)
                {
                    int timeNow = ManagerGame.Instance.RealTime();
                    int time = timeNow - PlayerPrefs.GetInt("TimeLastBuilding" + seriBuilding);
                    timeLive = PlayerPrefs.GetInt("TimeLiveBuilding" + seriBuilding) - time;
                    if (timeLive > 0)
                    {
                        IETimeLive = TimeFinish();
                        StartCoroutine(IETimeLive);
                    }
                    else if (timeLive <= 0)
                    {
                        status = 1;
                        PlayerPrefs.SetInt("StatusBuilding" + seriBuilding, status);
                        FinishBuild();
                    }
                }
                else if (status == 1)
                {
                    FinishBuild();
                }
                else if (status == 2)
                {
                    Destroy(gameObject);
                }
            }

            OrderBuilding();
        }

        public void OrderBuilding()
        {
            int order = (int) (transform.position.y * (-100));
            for (int i = 0; i < OrderBulding.Length; i++)
            {
                for (int k = 0; k < OrderBulding[i].SprRenderer.Length; k++)
                {
                    OrderBulding[i].SprRenderer[k].sortingOrder = order + OrderBulding[i].order;
                }
            }
        }

        void OnMouseDown()
        {
            camfirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void OnMouseUp()
        {
            if (!(Vector3.Distance(camfirstPos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.2f)) return;
            if (status == 0)
            {
                Ani.SetTrigger("IsClick");
                Vector3 target = new Vector3(transform.position.x, transform.position.y, 0);
                ManagerTool.instance.RegisterShowClock(3, 0, 0, seriBuilding, NameProduct, target, gameObject);
                ManagerTool.instance.ShowClockBuilding(timeLive, totalTime);
            }
            else if (status == 1)
            {
                MainCamera.instance.DisableAll();
                status = 2;
                PlayerPrefs.SetInt("StatusBuilding" + seriBuilding, status);
                if (idStype == 0 && idSeriProduct == 0 && ManagerGuide.Instance.GuideClickCageChicken == 0)
                {
                    ManagerGuide.Instance.GuideClickCageChicken = 1;
                    ManagerGuide.Instance.DoneGuide();
                }

                Instantiate(ManagerBuilding.Instance.EffectBuildFinish, transform.position,
                    Quaternion.identity);
                StartCoroutine(CallBuilding());
            }
        }

        private IEnumerator CallBuilding()
        {
            yield return new WaitForSeconds(0.2f);
            if (idStype == 0) ManagerShop.instance.BuildCage(idProduct, idSeriProduct, transform.position);
            else if (idStype == 1) ManagerShop.instance.BuildFactory(idProduct, idSeriProduct, transform.position);
            else if (idStype == 2) ManagerFountain.Instance.BuildFountain(transform.position);
            Destroy(gameObject);
        }

        private IEnumerator TimeFinish()
        {
            yield return new WaitForSeconds(1f);
            timeLive -= 1;
            PlayerPrefs.SetInt("TimeLiveBuilding" + seriBuilding, timeLive);
            PlayerPrefs.SetInt("TimeLastBuilding" + seriBuilding, ManagerGame.Instance.RealTime());
            if (ManagerTool.instance.showClock.CheckShow == true)
                if (ManagerTool.instance.showClock.CheckStype[3] == true)
                    if (ManagerTool.instance.showClock.IdShow == seriBuilding)
                        ManagerTool.instance.ShowClockBuilding(timeLive, totalTime);
            if (timeLive <= 0)
            {
                if (timeLive > 0) yield break;
                status = 1;
                PlayerPrefs.SetInt("StatusBuilding" + seriBuilding, status);
                FinishBuild();
            }
            else
            {
                IETimeLive = TimeFinish();
                StartCoroutine(IETimeLive);
            }
        }

        private void FinishBuild()
        {
            OrderBulding[0].SprRenderer[0].sprite = ManagerBuilding.Instance.BuildFinish;
            OrderBulding[1].SprRenderer[0].gameObject.SetActive(false);
            OrderBulding[2].SprRenderer[0].gameObject.SetActive(false);
        }

        public void UseGem()
        {
            switch (status)
            {
                case 0:
                    StopCoroutine(IETimeLive);
                    timeLive = 0;
                    PlayerPrefs.SetInt("TimeLiveBuilding" + seriBuilding, timeLive);
                    status = 1;
                    PlayerPrefs.SetInt("StatusBuilding" + seriBuilding, status);
                    FinishBuild();
                    ManagerTool.instance.ShowClockBuilding(timeLive, totalTime);
                    break;
                case 1:
                {
                    string str;
                    if (Application.systemLanguage == SystemLanguage.Vietnamese)
                        str = "Đã xây xong!";
                    else if (Application.systemLanguage == SystemLanguage.Indonesian)
                        str = "Selesai!";
                    else str = "Builiding Finish!";
                    Notification.Instance.dialogBelow(str);
                    break;
                }
            }
        }
    }
}