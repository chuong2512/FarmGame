using UnityEngine;
using System.Collections;

public class PassingCar : MonoBehaviour 
{
	
	[SerializeField] private VisualWaypoint visuaWP;
	SpriteRenderer sprRenderer;
	Transform targetVector;
	[SerializeField] GameObject body;
    [SerializeField] OrderPro[] oderCar;
    // Use this for initialization

	void OnEnable ()
	{
		transform.position = visuaWP.node[0].position;
		StartCoroutine (carStart());
	}
	void Start()
	{
		sprRenderer = body.GetComponent<SpriteRenderer> ();
	}

	private void OrderCar()
    {
        float order = transform.position.y * (-100);
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
    void Update()
    {
        OrderCar();
    }
	IEnumerator carStart()
	{
		yield return new WaitForSeconds(0.5f);
		StartCoroutine (moveCarOne());
	}
	IEnumerator moveCarOne()
	{
		iTween.MoveTo(gameObject, iTween.Hash("position", visuaWP.node [1].position, "time", 18f, "easetype", iTween.EaseType.linear, "islocal", true));
		yield return new WaitForSeconds (18f);
		iTween.MoveTo(gameObject, iTween.Hash("position", visuaWP.node [2].position, "time", 18f, "easetype", iTween.EaseType.linear, "islocal", true));
		yield return new WaitForSeconds (18f);
		gameObject.SetActive(false); //todo move to pool
	}

}
