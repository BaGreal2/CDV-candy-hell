using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
	public float maxHealth = 100f;
	float currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(float damage, Vector3 damageDirection, float pushForce)
	{
		currentHealth -= damage;
		transform.Translate(damageDirection * pushForce * Time.deltaTime);
		Debug.Log(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		GetComponent<Collider2D>().enabled = false;
		GetComponent<EnemyMoveController>().enabled = false;
		this.enabled = false;
	}

}
