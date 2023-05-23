using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePlayer : MonoBehaviour
{
	[SerializeField] string sceneName;
	[SerializeField] float fadeOut = 0f;
	[SerializeField] float fadeIn = 0f;
	[SerializeField] GameObject fadescreen;
	[SerializeField] float minTime = 0f;

	public void loadScene()
	{
		SceneManagement.instance.LoadScene(sceneName, fadeOut, fadeIn, fadescreen, minTime);
		AudioManager.instance.StopAll();

		if (sceneName == "Title")
		{
			AudioManager.instance.Play("MainMenuTheme");
		}
	}
}
