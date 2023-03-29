using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RegisteredKeys", menuName = "Data/RegisteredKeys/KeyData")]
public class KeyData : ScriptableObject
{
	[SerializeField] List<KeyPairs> registeredKeys = new List<KeyPairs>();

	[System.Serializable]
	public class KeyPairs
	{
		public string reference;
		public KeyCode key;
	}

	public KeyCode GetKey(string reference)
	{
		for(int i = 0; i < registeredKeys.Count; i++) 
			if (registeredKeys[i].reference.Equals(reference)) return registeredKeys[i].key;

		return KeyCode.None;
	}

	public List<string> FindAllKeys(KeyCode code)
	{
		List<string> keys = new List<string>();

		for (int i = 0; i < registeredKeys.Count; i++)
			if (registeredKeys[i].key.Equals(code)) keys.Add(registeredKeys[i].reference);

		return keys;
	}

	public List<KeyPairs> GetKeys()
	{
		return registeredKeys;
	}

	public void AddKey(string reference, KeyCode key) 
	{
		bool found = false;
		for (int i = 0; i < registeredKeys.Count; i++)
			if (registeredKeys[i].reference.Equals(reference))
			{
				registeredKeys[i].key = key;
				found = true;
				break;
			}

		if (!found)
			registeredKeys.Add(new KeyPairs { reference = reference, key = key });
	}

	public static bool GetPressed(out KeyCode keyCode)
	{
		keyCode = KeyCode.None;
		foreach (var key in Enum.GetValues(typeof(KeyCode)))
			if (Input.GetKeyDown((KeyCode)key))
			{
				keyCode = (KeyCode)key;
				return true;
			}

		return false;
	}
}
