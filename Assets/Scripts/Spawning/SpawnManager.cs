using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private static SpawnManager instance;

	public static SpawnManager Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject go = new GameObject();
				go.name = "SpawnManager";
				instance = go.AddComponent<SpawnManager>();
			}

			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		if (this != instance) return;

		waveTimer = new Timer(60, () => 
		{
			runningWave = true;
			foreach (var spawn in spawners) spawn.BeginWave(); 
		}, true);
	}

	List<Spawner> spawners = new List<Spawner>();
	public void AddSpawner(Spawner spawner)
	{
		if (this != instance)
		{
			Instance.AddSpawner(spawner);
			return;
		}

		spawners.Add(spawner);
	}

	private void Update()
	{
		if (this != instance) return;

		if (runningWave && remainingEnemies <= 0)
			WaveComplete();

		if (waveTimer != null)
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				waveTimer.Remove();
				waveTimer = null;
				runningWave = true;
				foreach (var spawn in spawners) 
					spawn.BeginWave();
			}
		}
	}

	bool runningWave = false;
	int completedWaves = 0;
	public int remainingEnemies = 0;
	Timer waveTimer;

	public void WaveComplete()
	{
		if (this != instance)
		{
			Instance.WaveComplete();
			return;
		}

		runningWave = false;
		completedWaves++;

		if (completedWaves >= spawners.Max(x => x.waveCount()))
		{
			print("All Waves Complete");
		}
		else
		{
			waveTimer = new Timer(60, () =>
			{
				runningWave = true;
				foreach (var spawn in spawners) spawn.BeginWave();
			}, true);
		}
		
	}
}
