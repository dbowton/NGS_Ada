using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyInfo : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text funcitonName;
	[SerializeField] TMPro.TMP_Text mappedKey;
	[SerializeField] Button button;
	[SerializeField] string ID;

	private void OnValidate()
	{
		if (string.IsNullOrWhiteSpace(ID) ^ string.IsNullOrWhiteSpace(funcitonName.text))
		{
			if(string.IsNullOrWhiteSpace(ID)) ID = funcitonName.text;
			else funcitonName.text = ID;
		}
	}

	public void SetID(string id)
	{
		ID = id;
	}

	public string GetId()
	{
		return ID;
	}

	public string GetName()
	{
		return funcitonName.text;
	}

	public void SetName(string name)
	{
		funcitonName.text = name;
	}

	public void SetKey(KeyCode code)
	{
		if (code == KeyCode.None)
			mappedKey.text = "[Listening]";
		else
			mappedKey.text = code.ToString();
	}

	private void Start()
	{
		button.onClick.AddListener(() => SavedKeysTest.instance.Listen(this));
	}
}
