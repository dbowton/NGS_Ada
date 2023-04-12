using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
	public static SceneManagement instance = null;

	public static int SceneCount { get { return SceneManager.sceneCountInBuildSettings; } }

	[SerializeField] GameObject defaultFadeScreenPrefab;
	GameObject fadeScreenPrefab;
	GameObject fadeScreen;

	bool ready = false;
	Timer waitTimer = null;

	string tempSceneName;
	bool fadeOut = true;
	float totalFadeOutTime = 0f;
	float totalFadeInTime = 0f;
	float workingTime = 0f;

	string preppedScene = null;

	public UnityAction onSceneChange;
	public UnityAction onSceneLoad;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(this);
	}

	AsyncOperation loading = null;

	private void Update()
	{
		if (loading != null)
		{
			if (fadeScreen && fadeScreen.TryGetComponent<ProgressScreen>(out ProgressScreen progress))
				progress.Progress = Mathf.Min(loading.progress / 0.9f, Mathf.Min(waitTimer.GetElapsed, 1));

			if (loading.progress >= 0.9f && ready)
			{
				loading.allowSceneActivation = true;
				ready = false;
			}
		}


		if (fadeOut && preppedScene != null)
		{
			if (totalFadeOutTime > 0 && workingTime == 0)
			{
				Destroy(fadeScreen);

				if(fadeScreenPrefab)
					fadeScreen = Instantiate(fadeScreenPrefab);
				else
					fadeScreen = Instantiate(defaultFadeScreenPrefab);

				fadeScreenPrefab = null;
				DontDestroyOnLoad(fadeScreen);
			}

			workingTime += Time.deltaTime;
			if(fadeScreen)
			foreach (var image in fadeScreen.GetComponentsInChildren<Image>())
			{
				Color color = image.color;
				color.a = Mathf.Min(workingTime / totalFadeOutTime, 1);
				image.color = color;
			}

			if (workingTime >= totalFadeOutTime)
			{
				onSceneLoad?.Invoke();

				fadeOut = false;
				loading = SceneManager.LoadSceneAsync(preppedScene);
				loading.allowSceneActivation = false;
				tempSceneName = preppedScene;
				preppedScene = null;
				workingTime = 0;
				totalFadeOutTime = 0;
			}
		}
		else if(loading != null && loading.isDone && totalFadeInTime > 0)
		{
			if (fadeScreen == null)
			{
				if (fadeScreenPrefab)
					fadeScreen = Instantiate(fadeScreenPrefab);
				else
					fadeScreen = Instantiate(defaultFadeScreenPrefab);

				fadeScreenPrefab = null;
			}

			workingTime += Time.deltaTime;
			if (workingTime >= totalFadeInTime)
			{
				tempSceneName = "";
				Destroy(fadeScreen);
				loading = null;
				fadeOut = true;
				totalFadeInTime = 0;
			}
			else
			{
				foreach (var image in fadeScreen.GetComponentsInChildren<Image>())
				{
					Color color = image.color;
					color.a = 1 - Mathf.Min(workingTime / totalFadeInTime, 1);
					image.color = color;
				}
			}
		}
	}

	public void LoadScene(string sceneName, float fadeOut = 0f, float fadeIn = 0f, GameObject fadescreen = null, float minTime = 0f)
	{
		if (preppedScene != null) return;
		onSceneChange?.Invoke();
		if (fadeOut == 0)
			Destroy(fadeScreen);

		ready = false;
		if (minTime == 0)
			ready = true;
		else
			waitTimer = new Timer(minTime, () => ready = true, true);


		fadeScreenPrefab = fadescreen;
		this.fadeOut = true;
		workingTime = 0;
		totalFadeOutTime = fadeOut;
		totalFadeInTime = fadeIn;
		preppedScene = sceneName;
	}

	public void LoadScene(int index, float fadeOut = 0f, float fadeIn = 0f, GameObject fadescreen = null, float minTime = 0f)
	{
		LoadScene(GetSceneName(index), fadeOut, fadeIn, fadescreen, minTime);
	}

	public static List<string> GetSceneNames()
	{
		List<string> names = new List<string>();

		for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) 
			names.Add(GetSceneName(i));

		return names;
	}

	public static string GetSceneName(int index) 
	{
		string path = SceneUtility.GetScenePathByBuildIndex(index);
		int slash = path.LastIndexOf('/');
		string name = path.Substring(slash + 1);
		int dot = name.LastIndexOf('.');
		return name.Substring(0, dot);
	}
}
