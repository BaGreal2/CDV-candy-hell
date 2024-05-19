using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
	public float moveSpeed;
	public float avoidanceDistance = 1.5f;
	public float maxVelocity;
	Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
		GameObject[] otherEnemies = GameObject.FindGameObjectsWithTag("EnemyTag");

		MoveTowardsPlayer(player);
		// AvoidOtherEnemies(otherEnemies);
	}

	void MoveTowardsPlayer(GameObject player)
	{
		applyForceRelativeToObject(player.transform);
	}

	void AvoidOtherEnemies(GameObject[] otherEnemies)
	{
		foreach (GameObject otherEnemy in otherEnemies)
		{
			if (otherEnemy != gameObject)
			{
				applyForceRelativeToObject(otherEnemy.transform, true);
			}
		}
	}

	void applyForceRelativeToObject(Transform destinationObject, bool isReversed = false)
	{
		Vector3 vectorDistance = destinationObject.position - transform.position;
		Vector3 moveDirection = vectorDistance.normalized;
		if (isReversed)
		{
			moveDirection = -moveDirection;
		}
		float scalarDistance = vectorDistance.magnitude;
		Vector3 force;
		if (isReversed ^ (scalarDistance > avoidanceDistance))
		{
			force = moveDirection * scalarDistance;
			Vector2 rbVelocityAbs = new Vector2(Math.Abs(rb.velocityX), Math.Abs(rb.velocityY));
			Vector2 forceX = new Vector2(force.x, 0f);
			Vector2 forceY = new Vector2(0f, force.y);

			if (rbVelocityAbs.x < maxVelocity)
			{
				rb.AddForce(forceX * moveSpeed, ForceMode2D.Force);
			}
			else
			{
				rb.velocityX = maxVelocity * moveDirection.x;
			}

			if (rbVelocityAbs.y < maxVelocity)
			{
				rb.AddForce(forceY * moveSpeed, ForceMode2D.Force);
			}
			else
			{
				rb.velocityY = maxVelocity * moveDirection.y;
			}
		}
		else
		{
			if (vectorDistance.x < avoidanceDistance && !GetComponent<EnemyStatsController>().isHit)
			{
				rb.velocityX = 0f;
			}
			if (vectorDistance.y < avoidanceDistance && !GetComponent<EnemyStatsController>().isHit)
			{
				rb.velocityY = 0f;
			}
		}

	}
}
