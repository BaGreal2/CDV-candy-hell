using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
	public Animator animator;
	public float maxHealth = 100f;
	Rigidbody2D rb;
	float currentHealth;
	bool isHit;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (isHit && rb.velocity.magnitude < 0.2f)
		{
			rb.velocity = Vector2.zero;
			isHit = false;
		}
	}

	public void TakeDamage(float damage, Vector3 damageDirection, float pushForce)
	{
		isHit = true;
		currentHealth -= damage;
		rb.AddForce(damageDirection * pushForce, ForceMode2D.Impulse);
		// transform.Translate(damageDirection * pushForce * Time.deltaTime);
		Debug.Log(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		animator.SetBool("IsDead", true);
		// transform.Translate(Vector2.down * 350f * Time.deltaTime);
		GetComponent<Collider2D>().enabled = false;
		GetComponent<EnemyMoveController>().enabled = false;
		enabled = false;
	}

}
