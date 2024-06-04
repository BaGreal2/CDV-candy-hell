using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerCombatController : MonoBehaviour
{
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public float attackRange = 0.5f;
	public float attackDamage = 30f;
	public float pushForce = 10f;
	public float maxHealth = 100f;

	bool isHit;
	float currentHealth;
	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Attack();
		}

	}

	void Attack()
	{
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
		foreach (Collider2D enemy in hitEnemies)
		{
			Transform playerTransform = player.transform;
			Vector3 playerPosition = playerTransform.position;
			Vector3 playerVectorDistance = playerPosition - enemy.transform.position;
			Vector3 damageDirection = new Vector3(-playerVectorDistance.normalized.x, 0f, 0f);
			enemy.GetComponent<EnemyStatsController>().TakeDamage(attackDamage, damageDirection, pushForce);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}

	public void TakeDamage(float damage)
	{
		Debug.Log("Got hit");
		isHit = true;
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Lose();
		}
	}

	void Lose()
	{
		Debug.Log("Lose!");
	}
}