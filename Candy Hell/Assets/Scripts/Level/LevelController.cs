using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using TMPro;

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
	public GameObject[] levels;
	public TMP_Text levelNameText;
	public TextAsset jsonFile;
	LevelParameters[] levelsData;

	void Start()
	{
		levelsData = JsonUtility.FromJson<LevelJSON>(jsonFile.text).data;
		SpawnLevel();
	}

	void Update()
	{

	}

	public void SpawnLevel()
	{
		if (currentLevelNumber >= 0 && currentLevelNumber < levels.Length)
		{
			GameObject level = Instantiate(levels[currentLevelNumber], new Vector3(-0.7125f, -1.4785f, -7.416f), Quaternion.identity);
			LevelParameters levelData = levelsData[currentLevelNumber + 1];

			foreach (Transform child in level.transform)
			{
				EnemySpawnController script = child.GetComponent<EnemySpawnController>();
				if (script != null)
				{
					script.maxAliveAmount = levelData.enemiesCount / 2;
					script.minSpawnDelay = levelData.minSpawnDelay;
					script.maxSpawnDelay = levelData.maxSpawnDelay;
					levelNameText.text = levelData.levelName;
				}
			}
		}
		else
		{
			Debug.Log("Level number out of bounds!");
		}
	}
}