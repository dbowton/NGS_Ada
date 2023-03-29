using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedKeysTest : MonoBehaviour
{
	[SerializeField] KeyData keys;
	[SerializeField] TMPro.TMP_Text warningField;
	[SerializeField] GameObject ui;
	[SerializeField] GameObject KeyPrefab;

	UIKeyInfo workingInfo = null;
	public static SavedKeysTest instance = null;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	private void Start()
	{
		foreach (var pair in keys.GetKeys())
		{
			UIKeyInfo working = Instantiate(KeyPrefab, ui.transform).GetComponent<UIKeyInfo>();
			working.SetName(pair.reference);
			working.SetID(pair.reference);
			working.SetKey(pair.key);
		}
	}

	private void Update()
	{
		if (workingInfo)
		{
			if (KeyData.GetPressed(out KeyCode code))
			{
				keys.AddKey(workingInfo.GetId(), code);
				workingInfo.SetKey(code);
				workingInfo = null;

				if (keys.FindAllKeys(code).Count > 1)
				{
					warningField.text = "Warning: Multiple Keys Tied to " + code + "\n{" + string.Join(", ", keys.FindAllKeys(code)) + "}";
				}
				else
					warningField.text = "";
			}
		}
	}

	public void Listen(UIKeyInfo info)
	{
		if (workingInfo) return;
		workingInfo = info;
		info.SetKey(KeyCode.None);
	}
}
