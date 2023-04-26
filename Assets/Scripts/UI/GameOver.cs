using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public GameObject UI;
	GameObject player;
	GameObject camera;

	public void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		camera = GameObject.FindGameObjectWithTag("Camera");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Test_AI")
		{
			EndGame();
		}
		
	}

	//on death invoke goes here
	public void EndGame()
	{
		//Debug.Log("gameover");
		player = GameObject.FindGameObjectWithTag("Player");
		camera = Camera.main.gameObject;
		UI.gameObject.SetActive(true);
		camera.transform.parent = null;
		player.SetActive(false);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
		Time.timeScale = 0;		
	}


	public void Restart()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
