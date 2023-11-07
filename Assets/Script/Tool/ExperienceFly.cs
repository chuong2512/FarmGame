using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class ExperienceFly : MonoBehaviour
    {
        [SerializeField] int numberPointsExp, indexArrayExp;
        public int numberExp;
        private Transform pointerExp;
        [SerializeField] float speed;
        [SerializeField] bool isRun;
        [SerializeField] Text txtNumberExp;
        [SerializeField] GameObject objExp;
        [SerializeField] Transform pointerLeft;
        [SerializeField] Vector3[] positionExp;

        // Use this for initialization

        void OnEnable()
        {
            pointerExp = Experience.Instance.PointerExp;
        }

        void Start()
        {
            positionExp = new Vector3[numberPointsExp];
            txtNumberExp.text = "" + numberExp;
            DrawQuadraticCurveExp();
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
        // Update is called once per frame


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
            if (Vector3.Distance(objExp.transform.position, positionExp[indexArrayExp]) < 0.1f)
            {
                if (indexArrayExp < positionExp.Length - 1)
                {
                    indexArrayExp = indexArrayExp + 1;
                }
            }

            if (indexArrayExp == numberPointsExp - 1)
            {
                if (Vector3.Distance(objExp.transform.position, positionExp[numberPointsExp - 1]) < 0.1f)
                {
                    Experience.Instance.reciveExp(numberExp);
                    Destroy(gameObject);
                }
            }

            objExp.transform.position = Vector3.MoveTowards(objExp.transform.position, positionExp[indexArrayExp],
                Time.deltaTime * speed);
        }

        void Update()
        {
            if (isRun == true)
            {
                ExpFly();
            }
        }
    }
}