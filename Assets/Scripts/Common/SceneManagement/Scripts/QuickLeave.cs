using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickLeave : MonoBehaviour
{
	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManagement.instance.LoadScene(0, 5f, 2f, null, 2.5f);
    }
}
