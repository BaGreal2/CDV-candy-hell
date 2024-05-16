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
	public bool isInverted = false;


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


			GameObject enemySpawned = Instantiate(enemyPrefab, position, transform.rotation);
			enemySpawned.transform.localScale = new Vector3(enemySpawned.transform.localScale.x, -1 * enemySpawned.transform.localScale.y, enemySpawned.transform.localScale.z);
			aliveAmount++;

			yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
		}
	}

}
