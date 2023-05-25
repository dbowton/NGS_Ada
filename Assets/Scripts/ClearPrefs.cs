using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPrefs : MonoBehaviour
{
	[SerializeField] List<string> keys = new List<string>();

	public void Clear()
	{
		foreach (var key in keys)
			PlayerPrefs.DeleteKey(key);
	}
}
