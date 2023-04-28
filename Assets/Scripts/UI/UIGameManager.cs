using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
	public static UIGameManager Instance;

	public Slider healthBar;

	[SerializeField] List<Image> towerIcons = new List<Image>();
	[SerializeField] List<TMPro.TMP_Text> towerCosts = new List<TMPro.TMP_Text>();
	[SerializeField] Image miniMap;

	public GameObject timer;
	public TMPro.TMP_Text waveCounter;
	public TMPro.TMP_Text remainingEnemies;
	public TMPro.TMP_Text currency;

	[SerializeField] TMPro.TMP_Text gameTimer;

	[SerializeField] GameObject towerUI;
	[SerializeField] TMPro.TMP_Text towerTargetPrevButton;
	[SerializeField] TMPro.TMP_Text towerTargetNextButton;
	[SerializeField] TMPro.TMP_Text towerTargetMode;

	private void Awake()
	{
		if (Instance == null) Instance = this;

		foreach(var cost in towerCosts)
			cost.text = string.Empty;
	}

	public void SetTowerIcons(List<(Sprite image, string cost)> towerInfo)
	{
		for (int i = 0; i < towerIcons.Count && i < towerInfo.Count; i++)
		{
			towerIcons[i].sprite = towerInfo[i].image;
			towerCosts[i].text = towerInfo[i].cost;
		}
	}

	public void ShowTowerInfo(Tower tower)
	{
		if(tower != null)
		{
			towerTargetMode.text = tower.targetMode.ToString();
			towerUI.SetActive(true);
		}
		else
			towerUI.SetActive(false);
	}
}
