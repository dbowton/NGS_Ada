using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[SerializeField] List<GameObject> towerPrefabs;
	[SerializeField] float placementRange;

	bool awaitingPlacement = false;
	int index = 0;

	GameObject awaitingTower;
	[SerializeField] LayerMask targetLayer;
	[SerializeField] LayerMask excludeLayer;
	Transform cam;

	[SerializeField] Material validMat;
	[SerializeField] Material invalidMat;

	float yRot = 0;

	public int currency = 0;

	public int Currency { get { return currency; } set 
		{ 
			currency = value;
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

		List<Sprite> towerMats = new List<Sprite>();
		foreach (var tower in towerPrefabs)
			towerMats.Add(tower.GetComponent<TowerRotation>().towerIconMaterial);

		UIGameManager.Instance.SetTowerIcons(towerMats);
	}

	private void Update()
	{
		if (awaitingPlacement)
		{
			if(Input.GetKeyDown(KeyCode.Escape)) 
			{
				Destroy(awaitingTower);
				awaitingPlacement = false;
				return;
			}

			if (Input.GetKey(KeyCode.Q)) yRot -= Time.deltaTime * 180f;
			if (Input.GetKey(KeyCode.E)) yRot += Time.deltaTime * 180f;

			if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, placementRange, targetLayer))
			{
				if(awaitingTower == null) awaitingTower = Instantiate(towerPrefabs[index]);
				awaitingTower.transform.position = hitInfo.point;
				awaitingTower.transform.localEulerAngles = Vector3.up * yRot;

				List<Collider> collisions = Physics.OverlapSphere(hitInfo.point, awaitingTower.GetComponent<TowerRotation>().towerSize).ToList();
				if (collisions.Any(x => x.CompareTag("Path")) || 
					(collisions.Any(x => x.transform.root.CompareTag("Tower") && x.transform.root.gameObject != awaitingTower) ||
					awaitingTower.GetComponent<TowerRotation>().cost > currency))
				{
					foreach (var rend in awaitingTower.GetComponentsInChildren<Renderer>())
						rend.material = invalidMat;
				}
				else
				{
					if (Input.GetMouseButton(1))
					{
						Currency -= awaitingTower.GetComponent<TowerRotation>().cost;

						Destroy(awaitingTower);
						awaitingTower = null;
						awaitingPlacement = false;

						GameObject placedTower = Instantiate(towerPrefabs[index]);
						placedTower.transform.position = hitInfo.point;
						placedTower.transform.localEulerAngles = Vector3.up * yRot;
						placedTower.GetComponent<TowerRotation>().Placed = true;
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

		CheckKey(KeyCode.Alpha1, 0);
		CheckKey(KeyCode.Alpha2, 1);
		CheckKey(KeyCode.Alpha3, 2);
		CheckKey(KeyCode.Alpha4, 3);
	}

	private void CheckKey(KeyCode code, int num)
	{
		if (Input.GetKeyDown(code) && towerPrefabs.Count > num)
		{
			yRot = 0;
			awaitingPlacement = true;
			if (awaitingTower) Destroy(awaitingTower);
			index = num;
		}
	}
}
