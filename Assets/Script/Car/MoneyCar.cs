using UnityEngine;
using System.Collections;
namespace NongTrai
{
public class MoneyCar : MonoBehaviour
{

    [SerializeField] private VisualWaypoint visuaWP;
    Animator Ani;
    Transform targetVector;
    [SerializeField] int wayIndex, status;
    [SerializeField] GameObject body;
    [SerializeField] GameObject smokeOne;
    [SerializeField] GameObject smokeTwo;
    [SerializeField] OrderPro[] oderCar;
    [SerializeField] ParticleSystemRenderer psRendererSmokeOne;
    [SerializeField] ParticleSystemRenderer psRendererSmokeTow;
    // Use this for initialization

    void OnEnable()
    {
        smokeOne.SetActive(true);
        smokeTwo.SetActive(true);
        StartCoroutine(carStart());
    }

    void Start()
    {
        Ani = GetComponent<Animator>();
    }

    private void OrderCar()
    {
        float order = transform.position.y * (-100);
        psRendererSmokeOne.sortingOrder = (int)order + 1;
        psRendererSmokeTow.sortingOrder = (int)order + 1;
        for (int i = 0; i < oderCar.Length; i++)
        {
            for (int k = 0; k < oderCar[i].SprRenderer.Length; k++)
            {
                oderCar[i].SprRenderer[k].sortingOrder = (int)order + oderCar[i].order;
            }
        }
    }

    private void ColorS(float r, float g, float b, float a)
    {
        for (int i = 0; i < oderCar.Length; i++)
        {
            for (int k = 0; k < oderCar[i].SprRenderer.Length; k++)
            {
                oderCar[i].SprRenderer[k].color = new Color(r, g, b, a);
            }
        }
    }

    public void OnMouseDown()
    {
        ColorS(0.3f, 0.3f, 0.3f, 1f);
    }

    public void OnMouseUp()
    {
        if (status == 0)
        {
            ColorS(1f, 1f, 1f, 1f);
        }
        else if (status == 1)
        {
            status = 0;
            ColorS(1f, 1f, 1f, 1f);
            ManagerTool.instance.RegisterExperienceCoin(ManagerCar.instance.Coin, ManagerCar.instance.Exp, transform.position);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = visuaWP.node[0].position;
            ManagerMission.instance.CarFreeTime();
            ManagerCar.instance.startEmpty();
        }
    }

    void Update()
    {
        OrderCar();
    }

    IEnumerator carStart()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(moveCarOne());
    }

    IEnumerator moveCarOne()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", visuaWP.node[1].position, "time", 18f, "easetype", iTween.EaseType.linear, "islocal", true));
        yield return new WaitForSeconds(18f);
        StartCoroutine(moveCarTwo());
    }

    IEnumerator moveCarTwo()
    {
        iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0, 0, 0), "time", 0f, "easetype", iTween.EaseType.linear));
        iTween.MoveTo(gameObject, iTween.Hash("position", visuaWP.node[2].position, "time", 6f, "easetype", iTween.EaseType.linear, "islocal", true));
        yield return new WaitForSeconds(6f);
        Ani.SetTrigger("isGetMoney");
        status = 1;
        smokeOne.SetActive(false);
        smokeTwo.SetActive(false);
        Destroy(GetComponent<iTween>());
        ManagerMission.instance.CargoBack();
    }
}
}
