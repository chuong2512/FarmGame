using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class ItemFly : MonoBehaviour
    {
        private int indexArrayExp;
        private float speed;
        private bool isRun;
        private Transform pointerDepot;
        private int numberPointsItem = 20;
        public int numberItem;
        public Image ItemImage;
        [SerializeField] Text NumberItemText;
        [SerializeField] GameObject objItem;
        [SerializeField] Transform pointerLeft;
        [SerializeField] Vector3[] positionItem;

        // Use this for initialization


        private void Awake()
        {
            pointerDepot = Experience.Instance.PoiterDepot;
        }

        void Start()
        {
            positionItem = new Vector3[numberPointsItem];
            NumberItemText.text = "" + numberItem;
            DrawQuadraticCurveExp();
            StartCoroutine(waitRun());
        }

        void DrawQuadraticCurveExp()
        {
            for (int i = 1; i < numberPointsItem + 1; i++)
            {
                float t = i / (float) numberPointsItem;
                positionItem[i - 1] = CalculateQuadraticBezierPoint(t, transform.position, pointerLeft.position,
                    pointerDepot.position);
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
            speed += 1.5f;
            StartCoroutine(upSpeed());
        }

        IEnumerator waitRun()
        {
            yield return new WaitForSeconds(1f);
            isRun = true;
            StartCoroutine(upSpeed());
        }

        void ExpFly()
        {
            DrawQuadraticCurveExp();
            if (Vector3.Distance(objItem.transform.position, positionItem[indexArrayExp]) < 0.1f)
            {
                if (indexArrayExp < positionItem.Length - 1)
                {
                    indexArrayExp = indexArrayExp + 1;
                }
            }

            if (indexArrayExp == numberPointsItem - 1)
            {
                if (Vector3.Distance(objItem.transform.position, positionItem[numberPointsItem - 1]) < 0.1f)
                {
                    Destroy(gameObject);
                }
            }

            objItem.transform.position = Vector3.MoveTowards(objItem.transform.position, positionItem[indexArrayExp],
                Time.deltaTime * speed);
        }

        void Update()
        {
            if (isRun == true) ExpFly();
        }
    }
}