using UnityEngine;

public class AlcoholController : MonoBehaviour
{
	public GameObject player;
	void OnMouseDown()
	{
		Debug.Log("Clicked!");
		player.GetComponent<BoxerCombatController>().TakeDamage(15f);
		Destroy(gameObject);
	}
}