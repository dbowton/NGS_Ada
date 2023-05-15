using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLevelButtonManager : MonoBehaviour
{

	[System.Serializable]
	public class SaveUIData
	{
		public string levelName = "";
		public Button levelButton;
		public GameObject lockedUI;
	}

	[SerializeField] bool unlockAllLevels = false;
	[SerializeField] string startingLevelName = "";
	[SerializeField] List<SaveUIData> data;

	private void Start()
	{
		foreach(var item in data) 
		{ 
			if (unlockAllLevels || (PlayerPrefs.HasKey(item.levelName) && PlayerPrefs.GetInt(item.levelName) == 1) || item.levelName.Equals(startingLevelName))
			{
				//	enable

				item.levelButton.enabled = true;
				if(item.lockedUI)
					item.lockedUI.SetActive(false);

			}
			else
			{
				//	disable
				//	should be done by default only used as safeguard

				item.levelButton.enabled = false;
				if (item.lockedUI)
					item.lockedUI.SetActive(true);
			}
		}
	}
}
