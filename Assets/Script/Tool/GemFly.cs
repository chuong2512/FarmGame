using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GemFly : MonoBehaviour
{

    [SerializeField] int numberPointsGem;
    [SerializeField] int indexArrayGem;
    public int numberGem;
    [SerializeField] float speed;
    [SerializeField] bool isRun;
    [SerializeField] Text NumberGemText;
    [SerializeField] GameObject objGem;
    [SerializeField] Transform pointerLeft, pointerGem;
    [SerializeField] Vector3[] positionGem;

    // Use this for initialization

    void OnEnable()
    {
        pointerGem = Gem.instance.PoiterGem;
    }

    void Start()
    {
        positionGem = new Vector3[numberPointsGem];
        NumberGemText.text = "" + numberGem;
        DrawQuadraticCurveExp();
        StartCoroutine(waitRun());
    }

    void DrawQuadraticCurveExp()
    {
        for (int i = 1; i < numberPointsGem + 1; i++)
        {
            float t = i / (float)numberPointsGem;
            positionGem[i - 1] = CalculateQuadraticBezierPoint(t, transform.position, pointerLeft.position, pointerGem.position);
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
        if (Vector3.Distance(objGem.transform.position, positionGem[indexArrayGem]) < 0.1f)
        {
            if (indexArrayGem < positionGem.Length - 1) indexArrayGem = indexArrayGem + 1;
        }
        if (indexArrayGem == numberPointsGem - 1)
        {
            if (Vector3.Distance(objGem.transform.position, positionGem[numberPointsGem - 1]) < 0.1f)
            {
                Gem.instance.ReciveGem(numberGem);
                Destroy(gameObject);
            }
        }
        objGem.transform.position = Vector3.MoveTowards(objGem.transform.position, positionGem[indexArrayGem], Time.deltaTime * speed);

    }

    void Update()
    {
        if (isRun == true) ExpFly();
    }
}
