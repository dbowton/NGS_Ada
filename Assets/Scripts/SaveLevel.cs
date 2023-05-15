using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLevel : MonoBehaviour
{
	[SerializeField] string levelName = "";

	public void Save()
	{
		PlayerPrefs.SetInt(levelName, 1);
	}
}
