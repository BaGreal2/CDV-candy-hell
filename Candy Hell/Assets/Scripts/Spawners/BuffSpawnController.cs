using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BuffSpawnController : MonoBehaviour
{
	private BoxCollider2D spawnArea;

	public GameObject pearPrefab;
	public GameObject alcoholPrefab;
	[Range(0f, 1f)] public float alcoholChance = 0.05f;

	public float minSpawnDelay = 0.25f;
	public float maxSpawnDelay = 1f;

	public float minAngle = -15f;
	public float maxAngle = 15f;

	public float minForce = 18f;
	public float maxForce = 22f;

	public float maxLifetime = 5f;

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

		while (enabled)
		{
			GameObject prefab = pearPrefab;

			if (Random.value < alcoholChance)
			{
				prefab = alcoholPrefab;
			}

			Vector3 position = new Vector3
			{
				x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
				y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
			};

			Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

			GameObject buff = Instantiate(prefab, position, rotation);
			Destroy(buff, maxLifetime);

			float force = Random.Range(minForce, maxForce);
			buff.GetComponent<Rigidbody2D>().AddForce(buff.transform.up * force, ForceMode2D.Impulse);

			yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
		}
	}

}
