using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[System.Serializable]
	public class EnemyWave
	{
		public GameObject EnemyPrefab;
		public int enemyValue;
		public int count;
	}

	[System.Serializable]
	public class Wave
	{
		public bool allowClusterSpawning = false;
		public int countBetweenClusters;
		public int minClusterEnemyCount;
		public int maxClusterEnemyCount;
		[Range(0,1)] public float spawnChance = 0.5f;

		public List<EnemyWave> wave = new List<EnemyWave>();
	}

	[SerializeField] List<Wave> waves = new List<Wave>();

	float enemyHealthMulti = 1f;
	int currentWave = -1;
	[SerializeField] string pathName;

	Timer spawnTimer;
	[SerializeField] float spawnTime = 0.25f;

	public int waveCount()
	{
		return waves.Count;
	}

	private void Awake()
	{
		SpawnManager.Instance.AddSpawner(this);
	}

	int spawnCount = 0;

	public void BeginWave()
	{		
		currentWave++;

		if (currentWave < waves.Count)
			SpawnManager.Instance.remainingEnemies += waves[currentWave].wave.Sum(x => x.count);


		if (currentWave < waves.Count && waves[currentWave].wave.Count > 0)
		spawnTimer = new Timer(
			spawnTime,
			() =>
			{
				SpawnEnemy();
			}, false, true);
	}

	public void Replay(float enemyCountMulti, float enemyHealthMulti)
	{
		this.enemyHealthMulti = enemyHealthMulti;

		foreach (var wave in waves[1].wave)
		{
			EnemyWave newWave = new EnemyWave();
			newWave.enemyValue = wave.enemyValue;
			newWave.count = wave.count;
			newWave.EnemyPrefab = wave.EnemyPrefab;

			waves[0].wave.Add(newWave);
		}

		currentWave--;

		foreach(var wave in waves[0].wave) 
		{ 
			wave.count = Mathf.RoundToInt(wave.count * enemyCountMulti);
		}
	}

	public void SpawnEnemy()
	{

		if (currentWave < waves.Count && waves[currentWave].wave.Count > 0)
		{
			spawnCount++;

			int count = 1;
			if(waves[currentWave].allowClusterSpawning && spawnCount >= waves[currentWave].countBetweenClusters && 
				Random.Range(0,1) <= waves[currentWave].spawnChance)
			{
				spawnCount = 0;
				count = Random.Range(waves[currentWave].minClusterEnemyCount, waves[currentWave].maxClusterEnemyCount);

			}

			for (int i = 0; i < count; i++)
			{
				if (waves[currentWave].wave.Count == 0) break;
				EnemyWave selected = waves[currentWave].wave[Random.Range(0, waves[currentWave].wave.Count)];

				Vector3 ranPos = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);
				GameObject spawnedEnemy = Instantiate(selected.EnemyPrefab, transform.position + ranPos, Quaternion.identity);

				spawnedEnemy.GetComponent<Health>().ModifyMaxHealth(enemyHealthMulti);
				spawnedEnemy.GetComponent<Health>().OnHurt.AddListener(() => AudioManager.instance.Play("EnemyHurt"));
				spawnedEnemy.GetComponent<Health>().FullHeal();

				spawnedEnemy.GetComponent<Health>().OnDeath.AddListener(() =>
				{
					TowerManager.instance.Currency += selected.enemyValue;
					SpawnManager.Instance.remainingEnemies--;
				});

				spawnedEnemy.GetComponent<PathFollower>().Begin(pathName);
			
				selected.count--;
				if (selected.count <= 0)
				{
					waves[currentWave].wave.Remove(selected);
					if (waves[currentWave].wave.Count <= 0)
					{
						spawnTimer.Remove();
						spawnTimer = null;
					}
				}
			}

		}
	}
}
