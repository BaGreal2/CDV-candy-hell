using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
	public Animator animator;
	public float maxHealth = 100f;
	Rigidbody2D rb;
	float currentHealth;
	public bool isHit;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (isHit && rb.velocity.magnitude < 1f)
		{
			rb.velocity = Vector2.zero;
			isHit = false;
		}
	}

	public void TakeDamage(float damage, Vector3 damageDirection, float pushForce)
	{
		isHit = true;
		currentHealth -= damage;
		rb.velocityX = 0f;
		rb.AddForce(damageDirection * pushForce, ForceMode2D.Impulse);
		Debug.Log(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		animator.SetBool("IsDead", true);
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<EnemyMoveController>().enabled = false;
		enabled = false;
	}
}
