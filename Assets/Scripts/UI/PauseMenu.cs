using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
	public GameObject UI;
	GameObject player;
	GameObject camera;

	public static PauseMenu instance;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		camera = GameObject.FindGameObjectWithTag("Camera");
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused) Resume();
			else Pause();
		}
    }

    public void Resume()
	{
		UI.gameObject.SetActive(false);
		Time.timeScale = 1;
		isPaused = false;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

	}

	public void Pause()
	{
		UI.gameObject.SetActive(true);
		Time.timeScale = 0;
		isPaused = true;

		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	public void Menu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
