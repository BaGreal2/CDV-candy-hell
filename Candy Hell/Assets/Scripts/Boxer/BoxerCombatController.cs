using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxerCombatController : MonoBehaviour
{
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public HealthController healthController;
	public float attackRange = 0.5f;
	public float attackDamage = 30f;
	public float pushForce = 10f;
	public float maxHealth = 100f;
	public float attackRate = 2f;
	float nextAttackTime = 0f;

	bool isHit;
	float currentHealth;
	void Start()
	{
		currentHealth = maxHealth;
		healthController.SetMaxHealth(maxHealth);
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
		healthController.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			Lose();
		}
	}

	void Lose()
	{
		SceneManager.LoadScene("GameScene");
		Debug.Log("Lose!");
	}
}