using UnityEngine;
namespace NongTrai
{
public class objVatNuoi : MonoBehaviour {

	[SerializeField] SpriteRenderer sprRenderer;


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
	
	void Awake()
	{
		sprRenderer = this.GetComponent<SpriteRenderer> ();
	}
}
}
