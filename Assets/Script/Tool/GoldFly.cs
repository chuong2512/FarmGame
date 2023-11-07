using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class GoldFly : MonoBehaviour
    {
        [SerializeField] int numberPointsGold;
        [SerializeField] int indexArrayGold;
        public int numberGold;
        [SerializeField] float speed;
        [SerializeField] bool isRun;
        [SerializeField] Text txtNumberGold;
        [SerializeField] GameObject objGold;
        [SerializeField] Transform pointerLeft, pointerGold;
        [SerializeField] Vector3[] positionExp;

        // Use this for initialization

        void OnEnable()
        {
            pointerGold = ManagerCoin.Instance.pointerGold;
        }

        void Start()
        {
            positionExp = new Vector3[numberPointsGold];
            txtNumberGold.text = "" + numberGold;
            DrawQuadraticCurveExp();
            StartCoroutine(waitRun());
        }

        void DrawQuadraticCurveExp()
        {
            for (int i = 1; i < numberPointsGold + 1; i++)
            {
                float t = i / (float) numberPointsGold;
                positionExp[i - 1] =
                    CalculateQuadraticBezierPoint(t, transform.position, pointerLeft.position, pointerGold.position);
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
            yield return new WaitForSeconds(0.2f);
            speed += 3f;
            StartCoroutine(upSpeed());
        }

        IEnumerator waitRun()
        {
            yield return new WaitForSeconds(0.5f);
            isRun = true;
            StartCoroutine(upSpeed());
        }

        void ExpFly()
        {
            DrawQuadraticCurveExp();
            if (Vector3.Distance(objGold.transform.position, positionExp[indexArrayGold]) < 0.1f)
            {
                if (indexArrayGold < positionExp.Length - 1)
                {
                    indexArrayGold = indexArrayGold + 1;
                }
            }

            if (indexArrayGold == numberPointsGold - 1)
            {
                if (Vector3.Distance(objGold.transform.position, positionExp[numberPointsGold - 1]) < 0.1f)
                {
                    ManagerCoin.Instance.ReciveGold(numberGold);
                    Destroy(gameObject);
                }
            }

            objGold.transform.position = Vector3.MoveTowards(objGold.transform.position, positionExp[indexArrayGold],
                Time.deltaTime * speed);
        }

        void Update()
        {
            if (isRun == true) ExpFly();
        }
    }
}