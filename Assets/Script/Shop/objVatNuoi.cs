using UnityEngine;

public class objVatNuoi : MonoBehaviour {

	[SerializeField] SpriteRenderer sprRenderer;
	void Awake()
	{
		sprRenderer = this.GetComponent<SpriteRenderer> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "HomeAnimal" && ManagerShop.instance.idPet == ManagerShop.instance.idHomeAnimal)
		{
			sprRenderer.color = Color.white;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		sprRenderer.color = new Color (1f, 0f, 0f, 1f);
	}
}
