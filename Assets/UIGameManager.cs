using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
	public static UIGameManager Instance;

	[SerializeField] List<Image> towerIcons = new List<Image>();
	[SerializeField] Image miniMap;

	public TMPro.TMP_Text timer;
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
	}

	public void SetTowerIcons(List<Material> towerMaterials)
	{
		for (int i = 0; i < towerIcons.Count && i < towerMaterials.Count; i++)
		{
			towerIcons[i].material = towerMaterials[i];
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
