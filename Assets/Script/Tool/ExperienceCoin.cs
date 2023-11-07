using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class ExperienceCoin : MonoBehaviour
    {
        private int indexArrayCoin;
        private int indexArrayExp;
        private int numberPointsCoin = 15;
        private int numberPointsExp = 15;
        private float speed = 0.5f;
        private bool PointedExp;
        private bool PointedCoin;
        private bool isRun;
        private Transform pointerExp;
        private Transform pointerCoin;
        private Vector3[] positionExp;
        private Vector3[] positionCoin;
        [SerializeField] GameObject Coin;
        [SerializeField] GameObject Exprerience;
        [SerializeField] Text txtNumberCoin;
        [SerializeField] Text txtNumberExp;
        [SerializeField] Transform pointerLeft;
        [SerializeField] Transform pointerRight;
        public int numberCoin;

        public int numberExp;
        // Use this for initialization

        void OnEnable()
        {
            pointerExp = Experience.Instance.PointerExp;
            pointerCoin = ManagerCoin.Instance.pointerGold;
        }

        void Start()
        {
            positionExp = new Vector3[numberPointsExp];
            positionCoin = new Vector3[numberPointsCoin];
            txtNumberExp.text = "" + numberExp;
            txtNumberCoin.text = "" + numberCoin;
            DrawQuadraticCurveExp();
            DrawQuadraticCurveItem();
            StartCoroutine(waitRun());
        }

        void DrawQuadraticCurveExp()
        {
            for (int i = 1; i < numberPointsExp + 1; i++)
            {
                float t = i / (float) numberPointsExp;
                positionExp[i - 1] =
                    CalculateQuadraticBezierPoint(t, transform.position, pointerLeft.position, pointerExp.position);
            }
        }

        void DrawQuadraticCurveItem()
        {
            for (int i = 1; i < numberPointsCoin + 1; i++)
            {
                float t = i / (float) numberPointsCoin;
                positionCoin[i - 1] = CalculateQuadraticBezierPoint(t, transform.position, pointerRight.position,
                    pointerCoin.position);
            }
        }

        Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            p.z = 0;
            return p;
        }

        IEnumerator upSpeed()
        {
            yield return new WaitForSeconds(0.3f);
            speed += 2f;
            StartCoroutine(upSpeed());
        }

        IEnumerator waitRun()
        {
            yield return new WaitForSeconds(1f);
            isRun = true;
            StartCoroutine(upSpeed());
            yield return new WaitForSeconds(0.3f);
            Exprerience.SetActive(true);
        }

        void ExpFly()
        {
            DrawQuadraticCurveExp();
            if (Vector3.Distance(Exprerience.transform.position, positionExp[indexArrayExp]) < 0.1f)
            {
                if (indexArrayExp < positionExp.Length - 1) indexArrayExp = indexArrayExp + 1;
            }

            if (Vector3.Distance(Exprerience.transform.position, positionExp[numberPointsExp - 1]) < 0.1f)
            {
                if (PointedExp == false)
                {
                    Experience.Instance.reciveExp(numberExp);
                    PointedExp = true;
                    Exprerience.SetActive(false);
                }
            }

            Exprerience.transform.position = Vector3.MoveTowards(Exprerience.transform.position,
                positionExp[indexArrayExp], Time.deltaTime * speed);
        }

        void ItemFly()
        {
            DrawQuadraticCurveItem();
            if (Vector3.Distance(Coin.transform.position, positionCoin[indexArrayCoin]) < 0.1f)
            {
                if (indexArrayCoin < positionCoin.Length - 1) indexArrayCoin = indexArrayCoin + 1;
            }

            if (Vector3.Distance(Coin.transform.position, positionCoin[numberPointsCoin - 1]) < 0.1f)
            {
                if (PointedCoin == false)
                {
                    ManagerCoin.Instance.ReciveGold(numberCoin);
                    Coin.SetActive(false);
                    PointedCoin = true;
                }
            }

            Coin.transform.position = Vector3.MoveTowards(Coin.transform.position, positionCoin[indexArrayCoin],
                Time.deltaTime * speed);
        }

        void Update()
        {
            if (isRun == true)
            {
                ExpFly();
                ItemFly();
                if (PointedExp == true && PointedCoin == true) Destroy(this.gameObject);
            }
        }
    }
}