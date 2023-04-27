using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
	private static SpawnManager instance;
	[SerializeField] UnityEvent onWavesComplete;

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

		UIGameManager.Instance.waveCounter.text = "Wave: 1/" + spawners.Max(x => x.waveCount());

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

		UIGameManager.Instance.remainingEnemies.text = "Enemies Left: " + remainingEnemies;

		if (waveTimer == null || waveTimer.IsOver)
		{
			UIGameManager.Instance.timer.SetActive(false);
		}
		else
		{
			UIGameManager.Instance.timer.SetActive(true);
			UIGameManager.Instance.timer.GetComponentInChildren<TMPro.TMP_Text>().text = (waveTimer.GetTime() - waveTimer.GetElapsed * waveTimer.GetTime()).ToString("F1");
		}

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

			UIGameManager.Instance.waveCounter.text = "Wave: " + completedWaves + "/" + spawners.Max(x => x.waveCount());

			onWavesComplete.Invoke();
		}
		else
		{
			UIGameManager.Instance.waveCounter.text = "Wave: " + (completedWaves + 1) + "/" + spawners.Max(x => x.waveCount());
			waveTimer = new Timer(60, () =>
			{
				runningWave = true;
				foreach (var spawn in spawners) spawn.BeginWave();
			}, true);
		}
		
	}
}
