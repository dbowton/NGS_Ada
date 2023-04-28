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

		camera.transform.parent = null; // 
		player.SetActive(true);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined; //

	}

	public void Pause()
	{
		UI.gameObject.SetActive(true);
		Time.timeScale = 0;
		isPaused = true;


		camera.transform.parent = null;
		player.SetActive(false);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;

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
