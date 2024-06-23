using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LevelParameters
{
	public string levelName;
	public int enemiesCount;
	public float minSpawnDelay;
	public float maxSpawnDelay;
}

[System.Serializable]
public class LevelJSON
{
	public LevelParameters[] data;
}

public class LevelController : MonoBehaviour
{
	public int currentLevelNumber = 0;
	public GameObject levelCompleteScreen;
	public GameObject templateLevel;
	public TMP_Text levelNameText;
	public TextAsset jsonFile;
	public bool firstEnemySpawned = false;
	bool isInitialized = false;
	LevelParameters[] levelsData;

	void Awake()
	{
		currentLevelNumber = PlayerPrefs.GetInt("levelNumber", 0);
	}

	void Start()
	{
		levelsData = JsonUtility.FromJson<LevelJSON>(jsonFile.text).data;
		SpawnLevel();
	}

	void Update()
	{
		if (!isInitialized && firstEnemySpawned)
		{
			isInitialized = true;
			firstEnemySpawned = false;
		}
		if (isInitialized)
		{
			Debug.Log("Checking");
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyTag");
			List<GameObject> deadEnemies = new List<GameObject>();

			foreach (GameObject enemy in enemies)
			{
				// Assuming the Health component is attached to the enemy GameObject
				EnemyStatsController stats = enemy.GetComponent<EnemyStatsController>();

				if (stats != null && stats.currentHealth <= 0)
				{
					deadEnemies.Add(enemy);
				}
			}

			if (deadEnemies.Count == levelsData[currentLevelNumber].enemiesCount)
			{
				Debug.Log("No enemies");
				levelCompleteScreen.SetActive(true);
				isInitialized = false;
			}

		}
	}

	public void NextLevel()
	{
		if (currentLevelNumber < levelsData.Length)
		{
			currentLevelNumber++;
			PlayerPrefs.SetInt("levelNumber", currentLevelNumber);
			SceneManager.LoadScene("GameScene");
		}
	}

	public void ResetLevel()
	{
		PlayerPrefs.SetInt("levelNumber", currentLevelNumber);
		SceneManager.LoadScene("GameScene");
	}

	public void SpawnLevel()
	{
		if (currentLevelNumber >= 0 && currentLevelNumber < levelsData.Length)
		{
			GameObject level = Instantiate(templateLevel, new Vector3(-0.7125f, -1.4785f, -7.416f), Quaternion.identity);
			LevelParameters levelData = levelsData[currentLevelNumber];
			levelNameText.text = levelData.levelName;

			foreach (Transform child in level.transform)
			{
				EnemySpawnController script = child.GetComponent<EnemySpawnController>();
				if (script != null)
				{
					script.maxAliveAmount = levelData.enemiesCount / 2;
					script.minSpawnDelay = levelData.minSpawnDelay;
					script.maxSpawnDelay = levelData.maxSpawnDelay;
				}
			}
		}
		else
		{
			Debug.Log("Level number out of bounds!");
		}
	}
}