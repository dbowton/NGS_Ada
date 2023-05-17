using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[SerializeField] List<GameObject> towerPrefabs;
	[SerializeField] float placementRange;

	int index = -1;

	GameObject awaitingTower;
	[SerializeField] LayerMask targetLayer;
	[SerializeField] LayerMask excludeLayer;
	Transform cam;

	[SerializeField] Material validMat;
	[SerializeField] Material invalidMat;

	bool hasYRot = false;
	float yRot = 0;

	[SerializeField] private int currency = 0;

	public int Currency { get { return currency; } set 
		{
			currency = value;
			UpdateTowerColors();
			UIGameManager.Instance.currency.text = "Currency: " + currency.ToString();
		} 
	}

	public static TowerManager instance;

	private void OnDestroy()
	{
		instance = null;
	}

	private void Start()
	{
		if(instance == null) 
		{
			instance = this;
		}

		Currency = currency;

		cam = Camera.main.transform;

		List<(Sprite, string)> towerInfo = new List<(Sprite, string)>();
		foreach (var tower in towerPrefabs)
		{
			towerInfo.Add((tower.GetComponent<Tower>().towerIconMaterial, tower.GetComponent<Tower>().cost.ToString()));
		}

		UpdateTowerColors();

		UIGameManager.Instance.SetTowerIcons(towerInfo);
	}


	public void UpdateTowerColors()
	{
		if (towerPrefabs.Count > 0 && Currency >= towerPrefabs[0].GetComponent<Tower>().cost)
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha1, (index == 0) ? Color.blue : Color.green);
		else
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha1, (index == 0) ? Colors.orange : Color.red);

		if (towerPrefabs.Count > 1 && Currency >= towerPrefabs[1].GetComponent<Tower>().cost)
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha2, (index == 1) ? Color.blue : Color.green);
		else
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha2, (index == 1) ? Colors.orange : Color.red);

		if (towerPrefabs.Count > 2 && Currency >= towerPrefabs[2].GetComponent<Tower>().cost)
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha3, (index == 2) ? Color.blue : Color.green);
		else
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha3, (index == 2) ? Colors.orange : Color.red);

		if (towerPrefabs.Count > 3 && Currency >= towerPrefabs[3].GetComponent<Tower>().cost)
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha4, (index == 3) ? Color.blue : Color.green);
		else
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha4, (index == 3) ? Colors.orange : Color.red);

	}

	private void Update()
	{
		CheckKey(KeyCode.Alpha1, 0);
		CheckKey(KeyCode.Alpha2, 1);
		CheckKey(KeyCode.Alpha3, 2);
		CheckKey(KeyCode.Alpha4, 3);

		if (index >= 0 && index < towerPrefabs.Count)
		{
			if (Input.GetKey(KeyCode.Q))
			{
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Q, Color.blue);
				yRot -= Time.deltaTime * 180f;
				hasYRot = true;
			}
			else
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Q, Color.green);

			if (Input.GetKey(KeyCode.E))
			{
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.E, Color.blue);
				yRot += Time.deltaTime * 180f;
				hasYRot = true;
			}
			else
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.E, Color.green);


			if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, placementRange, targetLayer))
			{
				if (awaitingTower == null)
				{
					awaitingTower = Instantiate(towerPrefabs[index]);

					if (!hasYRot)
					{
						yRot = 360 - Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up);
					}
				}

				List<Collider> deleteColliders = awaitingTower.GetComponentsInChildren<Collider>().ToList();

				for (int i = 0; i < deleteColliders.Count;)
				{
					Destroy(deleteColliders[i]);
					deleteColliders.RemoveAt(i);
				}

				awaitingTower.transform.position = hitInfo.point;
				awaitingTower.transform.localEulerAngles = Vector3.up * yRot;

				List<Collider> collisions = Physics.OverlapSphere(hitInfo.point, awaitingTower.GetComponent<Tower>().towerSize).ToList();
				if (collisions.Any(x => x.CompareTag("Path") && awaitingTower.GetComponent<Tower>().onPath == false) || 
					(collisions.Any(x => x.transform.root.CompareTag("Tower") && x.transform.root.gameObject != awaitingTower)) ||
					awaitingTower.GetComponent<Tower>().cost > currency)
				{
					foreach (var rend in awaitingTower.GetComponentsInChildren<Renderer>())
						rend.material = invalidMat;
				}
				else
				{
					if (Input.GetMouseButtonDown(1))
					{
						hasYRot = true;
						Currency -= awaitingTower.GetComponent<Tower>().cost;

						Destroy(awaitingTower);
						awaitingTower = null;

						GameObject placedTower = Instantiate(towerPrefabs[index]);
						placedTower.transform.position = hitInfo.point;
						placedTower.transform.localEulerAngles = Vector3.up * yRot;
						placedTower.GetComponent<Tower>().Placed = true;
					}
					else
					{
						foreach (var rend in awaitingTower.GetComponentsInChildren<Renderer>())
							rend.material = validMat;
					}
				}
			}
			else
			{
				Destroy(awaitingTower);
			}
		}
	}

	private void CheckKey(KeyCode code, int num)
	{
		if (Input.GetKeyDown(code) && towerPrefabs.Count > num)
		{
			if (num == index)
			{
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Q, Color.black);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.E, Color.black);

				index = -1;
				if (awaitingTower) Destroy(awaitingTower);
			}
			else
			{
				hasYRot = false;
				yRot = 0;
				index = num;
				if (awaitingTower) Destroy(awaitingTower);
				awaitingTower = null;
			}
			UpdateTowerColors();
		}
	}
}
