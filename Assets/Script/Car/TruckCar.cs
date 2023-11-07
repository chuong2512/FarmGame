namespace NongTrai
{
    using UnityEngine;
    using System.Collections;

    public class TruckCar : MonoBehaviour
    {
        [SerializeField] private VisualWaypoint visuaWP;
        SpriteRenderer sprRenderer;
        Transform targetVector;
        [SerializeField] GameObject body;
        [SerializeField] GameObject smokeOne;
        [SerializeField] GameObject smokeTwo;
        [SerializeField] OrderPro[] oderCar;
        [SerializeField] ParticleSystemRenderer psRendererSmokeOne;

        [SerializeField] ParticleSystemRenderer psRendererSmokeTow;
        // Use this for initialization

        void OnEnable()
        {
            StartCoroutine(carStart());
        }

        void Start()
        {
            sprRenderer = body.GetComponent<SpriteRenderer>();
        }

        void OnMouseDown()
        {
            ColorS(0.3f, 0.3f, 0.3f, 1f);
        }

        void OnMouseUp()
        {
            ColorS(1f, 1f, 1f, 1f);
        }

        private void OrderCar()
        {
            float order = transform.position.y * (-100);
            psRendererSmokeOne.sortingOrder = (int) order + 1;
            psRendererSmokeTow.sortingOrder = (int) order + 1;
            for (int i = 0; i < oderCar.Length; i++)
            {
                for (int k = 0; k < oderCar[i].SprRenderer.Length; k++)
                {
                    oderCar[i].SprRenderer[k].sortingOrder = (int) order + oderCar[i].order;
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

        void Update()
        {
            OrderCar();
        }

        IEnumerator carStart()
        {
            ManagerAudio.Instance.PlayAudio(Audio.CarRun);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(moveCarOne());
        }

        IEnumerator moveCarOne()
        {
            iTween.MoveTo(gameObject,
                iTween.Hash("position", visuaWP.node[1].position, "time", 6f, "easetype", iTween.EaseType.linear,
                    "islocal", true));
            yield return new WaitForSeconds(6f);
            StartCoroutine(moveCarTwo());
        }

        IEnumerator moveCarTwo()
        {
            iTween.RotateTo(gameObject,
                iTween.Hash("rotation", new Vector3(0, 0f, 0), "time", 0f, "easetype", iTween.EaseType.linear));
            iTween.MoveTo(gameObject,
                iTween.Hash("position", visuaWP.node[2].position, "time", 18f, "easetype", iTween.EaseType.linear,
                    "islocal", true));
            yield return new WaitForSeconds(18f);
            iTween.RotateTo(gameObject,
                iTween.Hash("rotation", new Vector3(0, 180f, 0), "time", 0f, "easetype", iTween.EaseType.linear));
            Destroy(GetComponent<iTween>());
            transform.position = visuaWP.node[0].position;
            ManagerCar.instance.startMoneygo();
        }
    }
}