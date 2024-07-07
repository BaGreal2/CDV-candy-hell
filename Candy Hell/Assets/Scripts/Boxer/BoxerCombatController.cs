using UnityEngine;

public class BoxerCombatController : MonoBehaviour
{
	public Animator animator;
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public HealthController healthController;
	public GameObject gameOverScreen;
	public float attackRange = 0.5f;
	public float attackDamage = 30f;
	public float pushForce = 10f;
	public float maxHealth = 100f;
	public float attackRate = 0.5f;
	float nextAttackTime = 0f;
	int rightHandAttackCount = 0;

	float currentHealth;
	void Start()
	{
		currentHealth = maxHealth;
		healthController.SetMaxHealth(maxHealth);
	}

	void Update()
	{
		if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Space))
		{
			Attack();
			nextAttackTime = Time.time + 1f / attackRate;
		}
	}

	void Attack()
	{
		if (!animator.GetBool("isLeftPunch"))
		{
			rightHandAttackCount++;
		}
		if (rightHandAttackCount >= 2)
		{
			rightHandAttackCount = 0;
			animator.SetBool("isLeftPunch", true);
		}
		else
		{
			animator.SetBool("isLeftPunch", false);
		}

		animator.SetTrigger("Attack");


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
		currentHealth -= damage;
		healthController.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			Lose();
		}
	}

	void Lose()
	{
		animator.SetBool("isDead", true);
		gameOverScreen.SetActive(true);
	}
}