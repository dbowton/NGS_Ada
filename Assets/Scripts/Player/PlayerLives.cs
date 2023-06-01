using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
	[SerializeField] GameObject spawner1;
	[SerializeField] GameObject spawner2;
	public int playerLives = 3;
	public GameObject gameOverUI;

	[SerializeField] List<GameObject> livesUI = new List<GameObject>();

    public void OnDeath()
	{
		playerLives--;

		livesUI[playerLives].SetActive(false);

		GetSpawner(spawner1, spawner2);
		if (playerLives == 0)
		{
			gameOverUI.GetComponent<GameOver>().EndGame();
			
		}
	}

	public void GetSpawner(GameObject spawner1, GameObject spawner2)
	{
		if (Vector3.Distance(this.transform.position, spawner1.transform.position) > Vector3.Distance(this.transform.position, spawner2.transform.position))
		{
			spawner1.GetComponent<Respawn>().Trigger();
		}
		else
		{
			spawner2.GetComponent<Respawn>().Trigger();
		}
	}
}
