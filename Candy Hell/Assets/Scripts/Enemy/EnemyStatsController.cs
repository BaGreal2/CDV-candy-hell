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
	public float attackRate = 2f;
	float nextAttackTime = 0f;
	Rigidbody2D rb;
	public float currentHealth;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
	}

	void Update()
	{
		GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if (Time.time >= nextAttackTime && distanceToPlayer <= GetComponent<EnemyMoveController>().avoidanceDistance + 0.2f)
		{
			Attack();
			nextAttackTime = Time.time + 1f / attackRate;
		}
	}

	IEnumerator StopMovementAfterHit()
	{
		yield return new WaitForSeconds(0.3f);
		isHit = false;
		rb.velocity = Vector2.zero;
	}

	public void TakeDamage(float damage, Vector3 damageDirection, float pushForce)
	{
		animator.SetTrigger("Hurt");
		isHit = true;
		currentHealth -= damage;
		ScoreManager.instance.AddPoint();
		rb.velocity = Vector2.zero;
		rb.AddForce(damageDirection * pushForce, ForceMode2D.Impulse);

		StartCoroutine(StopMovementAfterHit());

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
		GetComponent<SpriteRenderer>().sortingOrder = 1;
		enabled = false;
	}

	void Attack()
	{
		animator.SetTrigger("Attack");
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
