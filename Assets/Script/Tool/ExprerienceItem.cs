using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace NongTrai
{
    public class ExprerienceItem : MonoBehaviour
    {
        private int indexArrayItem;
        private int indexArrayExp;
        private int numberPointsItem = 20;
        private int numberPointsExp = 20;
        private float speed = 0.5f;
        private bool PointedExp;
        private bool PointedItem;
        private bool isRun;
        private Transform pointerExp;
        private Transform pointerKho;
        private Vector3[] positionExp;
        private Vector3[] positionItem;
        public Image iconItem;
        [SerializeField] GameObject Item;
        [SerializeField] GameObject Exprerience;
        [SerializeField] Text txtNumberItem;
        [SerializeField] Text txtNumberExp;
        [SerializeField] Transform pointerLeft;
        [SerializeField] Transform pointerRight;
        [HideInInspector] public int numberItem;

        [HideInInspector] public int numberExp;
        // Use this for initialization

        void OnEnable()
        {
            pointerExp = Experience.Instance.PointerExp;
            pointerKho = Experience.Instance.PoiterDepot;
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
        
        void Start()
        {
            positionExp = new Vector3[numberPointsExp];
            positionItem = new Vector3[numberPointsItem];
            txtNumberExp.text = "" + numberExp;
            txtNumberItem.text = "" + numberItem;
            DrawQuadraticCurveExp();
            DrawQuadraticCurveItem();
            StartCoroutine(waitRun());
        }

        

        void DrawQuadraticCurveItem()
        {
            for (int i = 1; i < numberPointsItem + 1; i++)
            {
                float t = i / (float) numberPointsItem;
                positionItem[i - 1] =
                    CalculateQuadraticBezierPoint(t, transform.position, pointerRight.position, pointerKho.position);
            }
        }
        
        void ItemFly()
        {
            DrawQuadraticCurveItem();
            if (Vector3.Distance(Item.transform.position, positionItem[indexArrayItem]) < 0.1f)
            {
                if (indexArrayItem < positionItem.Length - 1) indexArrayItem = indexArrayItem + 1;
            }

            if (Vector3.Distance(Item.transform.position, positionItem[numberPointsItem - 1]) < 0.1f)
            {
                Item.SetActive(false);
                PointedItem = true;
            }

            Item.transform.position = Vector3.MoveTowards(Item.transform.position, positionItem[indexArrayItem],
                Time.deltaTime * speed);
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

            if (indexArrayExp == numberPointsExp - 1)
            {
                if (Vector3.Distance(Exprerience.transform.position, positionExp[numberPointsExp - 1]) < 0.1f)
                {
                    if (PointedExp == false) Experience.Instance.reciveExp(numberExp);
                    PointedExp = true;
                    Exprerience.SetActive(false);
                }
            }

            Exprerience.transform.position = Vector3.MoveTowards(Exprerience.transform.position,
                positionExp[indexArrayExp], Time.deltaTime * speed);
        }

        

        void Update()
        {
            if (isRun == true)
            {
                ExpFly();
                ItemFly();
                if (PointedExp == true && PointedItem == true) Destroy(this.gameObject);
            }
        }
    }
}