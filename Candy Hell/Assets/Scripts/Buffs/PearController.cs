using UnityEngine;

public class PearController : MonoBehaviour
{
	void OnMouseDown()
	{
		ScoreManager.instance.AddPoints(10);
		Destroy(gameObject);
	}
}