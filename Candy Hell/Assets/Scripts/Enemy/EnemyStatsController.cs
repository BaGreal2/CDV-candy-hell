using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
	public Transform attackPoint;
	public LayerMask playerLayers;
	public Animator animator;
	public float attackRange = 0.5f;
	public float attackDamage = 20f;
	public float maxHealth = 100f;
	public bool isHit;
	Rigidbody2D rb;
	float currentHealth;

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

		GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (distanceToPlayer <= GetComponent<EnemyMoveController>().avoidanceDistance + 0.2f)
		{
			Attack();
		}
	}

	public void TakeDamage(float damage, Vector3 damageDirection, float pushForce)
	{
		isHit = true;
		currentHealth -= damage;
		rb.velocityX = 0f;
		rb.AddForce(damageDirection * pushForce, ForceMode2D.Impulse);

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

	void Attack()
	{
		Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);

		hitPlayer.GetComponent<BoxerCombatController>().TakeDamage(attackDamage);
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}
