using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
	private static SpawnManager instance;
	[SerializeField] UnityEvent onWavesComplete;
	[SerializeField] bool isEndlessMode = false;

	List<Spawner> spawners = new List<Spawner>();
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

		if (isEndlessMode)
			UIGameManager.Instance.waveCounter.text = "∞/∞";
		else
			UIGameManager.Instance.waveCounter.text = "Wave: 1/" + spawners.Max(x => x.waveCount());

		waveTimer = new Timer(60, () => StartWaves(), true);

		AudioManager.instance.Stop("Action");
		AudioManager.instance.Play("Theme");
	}

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
			if (Input.GetKeyDown(KeyCode.G))
			{
				waveTimer.Remove();
				waveTimer = null;

				StartWaves();
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

		AudioManager.instance.Stop("Action");
		AudioManager.instance.Play("Theme");

		runningWave = false;
		completedWaves++;

		if (completedWaves >= spawners.Max(x => x.waveCount()))
		{
			print("All Waves Complete");

			if (isEndlessMode)
				UIGameManager.Instance.waveCounter.text = "∞/∞";
			else
				UIGameManager.Instance.waveCounter.text = "Wave: " + completedWaves + "/" + spawners.Max(x => x.waveCount());

			onWavesComplete.Invoke();
		}
		else
		{
			if (isEndlessMode)
			{
				UIGameManager.Instance.waveCounter.text = "∞/∞";
				Replay();
			}
			else
				UIGameManager.Instance.waveCounter.text = "Wave: " + (completedWaves + 1) + "/" + spawners.Max(x => x.waveCount());
			waveTimer = new Timer(60, () => StartWaves(), true, false, false);
		}
		
	}

	[SerializeField] float enemyCountMulti = 1f;
	[SerializeField] float enemyHealthMulti = 1f;

	private void Replay()
	{
		foreach (var spawn in spawners) spawn.Replay(enemyCountMulti, enemyHealthMulti);
	}

	private void StartWaves()
	{
		runningWave = true;

		AudioManager.instance.Stop("Theme");
		AudioManager.instance.Play("Action");

		foreach (var spawn in spawners) spawn.BeginWave();
	}
}
