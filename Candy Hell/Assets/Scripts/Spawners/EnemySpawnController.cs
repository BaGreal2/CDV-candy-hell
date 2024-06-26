using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawnController : MonoBehaviour
{
	private BoxCollider2D spawnArea;

	public GameObject enemyPrefab;

	public float minSpawnDelay = 7f;
	public float maxSpawnDelay = 14f;
	public int maxAliveAmount = 5;
	private int aliveAmount = 0;


	private void Awake()
	{
		spawnArea = GetComponent<BoxCollider2D>();
	}

	private void OnEnable()
	{
		StartCoroutine(Spawn());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator Spawn()
	{
		yield return new WaitForSeconds(2f);

		while (enabled && aliveAmount < maxAliveAmount)
		{

			Vector3 position = new Vector3
			{
				x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
				y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
			};


			Instantiate(enemyPrefab, position, transform.rotation);
			aliveAmount++;

			yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
		}
	}

}
