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
		public List<EnemyWave> wave = new List<EnemyWave>();
	}

	[SerializeField] List<Wave> waves = new List<Wave>();

	int currentWave = -1;
	[SerializeField] string pathName;

	Timer spawnTimer;

	public int waveCount()
	{
		return waves.Count;
	}

	private void Awake()
	{
		SpawnManager.Instance.AddSpawner(this);
	}

	public void BeginWave()
	{
		AudioManager.instance.Stop("Theme");
		AudioManager.instance.Play("Action");
		
		currentWave++;

		if (currentWave < waves.Count)
			SpawnManager.Instance.remainingEnemies += waves[currentWave].wave.Sum(x => x.count);


		if (currentWave < waves.Count && waves[currentWave].wave.Count > 0)
		spawnTimer = new Timer(
			0.25f,
			() =>
			{
				if (currentWave < waves.Count && waves[currentWave].wave.Count > 0)
				{
					EnemyWave selected = waves[currentWave].wave[Random.Range(0, waves[currentWave].wave.Count)];
					GameObject spawnedEnemy = Instantiate(selected.EnemyPrefab, transform.position, Quaternion.identity);

					spawnedEnemy.GetComponent<Health>().OnDeath.AddListener(() =>
						{
							TowerManager.instance.currency += selected.enemyValue;
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
			}, false, true);
	}
}
