using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGameManager : MonoBehaviour
{
	[SerializeField] Transform playerSpawn;
	[SerializeField] GameObject playerPrefab;
	[HideInInspector] public GameObject player = null;

	public static CameraGameManager instance = null;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	void Update()
	{
		if (player == null && Input.GetKeyDown(KeyCode.Space))
			player = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
	}
}
