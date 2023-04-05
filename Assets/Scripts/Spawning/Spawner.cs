using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[System.Serializable]
	public class EnemyWave
	{
		public GameObject EnemyPrefab;
		public int count;
	}

	[System.Serializable]
	public class Wave
	{
		public List<EnemyWave> wave = new List<EnemyWave>();
	}

	[SerializeField] List<Wave> waves = new List<Wave>();

	int currentWave = -1;
	[SerializeField] GameObject firstNode;

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
		currentWave++;

		if (currentWave >= waves.Count || waves[currentWave].wave.Count == 0)
			SpawnManager.Instance.WaveComplete();
		else
		spawnTimer = new Timer(
			0.25f,
			() =>
			{
				if (currentWave < waves.Count && waves[currentWave].wave.Count > 0)
				{
					EnemyWave selected = waves[currentWave].wave[Random.Range(0, waves[currentWave].wave.Count)];
					GameObject spawnedEnemy = Instantiate(selected.EnemyPrefab, transform.position, Quaternion.identity);
					//			spawnedEnemy.GetComponent<Enemy>().targetNode = firstNode;
					selected.count--;

					if (selected.count <= 0)
					{
						waves[currentWave].wave.Remove(selected);
						if (waves[currentWave].wave.Count <= 0)
						{
							SpawnManager.Instance.WaveComplete();
							spawnTimer.Remove();
							spawnTimer = null;
						}
					}
				}
			}, false, true);
	}
}
