using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
	public float moveSpeed;
	public float yScaleFactor;
	public float avoidanceDistance = 1.5f;

	void Start()
	{
	}

	void Update()
	{
		GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
		GameObject[] otherEnemies = GameObject.FindGameObjectsWithTag("EnemyTag");

		MoveTowardsPlayer(player);
		AvoidOtherEnemies(otherEnemies);
	}

	void MoveTowardsPlayer(GameObject player)
	{
		Transform playerTransform = player.transform;
		Vector3 playerPosition = playerTransform.position;
		Vector3 playerVectorDistance = playerPosition - transform.position;
		Vector3 moveDirection = playerVectorDistance.normalized;
		moveDirection.y *= yScaleFactor;
		float playerScalarDistance = playerVectorDistance.magnitude;
		if (playerScalarDistance > avoidanceDistance)
		{
			transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
		}
	}

	void AvoidOtherEnemies(GameObject[] otherEnemies)
	{
		foreach (GameObject otherEnemy in otherEnemies)
		{
			if (otherEnemy != gameObject) // Skip self
			{
				Vector3 vectorDistance = otherEnemy.transform.position - transform.position;
				Vector3 enemyMoveDirection = vectorDistance.normalized;
				Vector3 reversedEnemyMoveDirection = new Vector3(-enemyMoveDirection.x, -enemyMoveDirection.y, -enemyMoveDirection.z);
				float scalarDistance = vectorDistance.magnitude;
				if (scalarDistance < avoidanceDistance)
				{
					transform.Translate(reversedEnemyMoveDirection * moveSpeed * Time.deltaTime);
				}
			}
		}
	}
}
