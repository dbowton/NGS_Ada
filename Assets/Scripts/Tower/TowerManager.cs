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

	private void Start()
	{
		cam = Camera.main.transform;

		return;
		List<Material> towerMats = new List<Material>();
		foreach (var tower in towerPrefabs)
			towerMats.Add(tower.GetComponent<Tower>().towerIcon);

//		UIGameManager.Instance.SetTowerIcons(towerMats);
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
				print("hit");
				if(awaitingTower == null) awaitingTower = Instantiate(towerPrefabs[index]);
				awaitingTower.transform.position = hitInfo.point;
				awaitingTower.transform.localEulerAngles = Vector3.up * yRot;

				List<Collider> collisions = Physics.OverlapSphere(hitInfo.point, awaitingTower.GetComponent<TowerRotation>().towerSize).ToList();
				if (collisions.Any(x => x.CompareTag("Path")) || (collisions.Any(x => x.CompareTag("Tower") && x.transform.root.gameObject != awaitingTower)))
				{
					// set tower color to red
					print("red");

					foreach (var rend in awaitingTower.GetComponentsInChildren<Renderer>())
						rend.material = invalidMat;
				}
				else
				{
					if (Input.GetMouseButton(1))
					{
						// set tower color to default
						print("default");

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
						// set tower color to blue
						print("blue");
						foreach (var rend in awaitingTower.GetComponentsInChildren<Renderer>())
							rend.material = validMat;

					}
				}
			}
			else
			{
				print("nothing");
				Destroy(awaitingTower);
			}
		}

		CheckKey(KeyCode.Alpha1, 0);
		CheckKey(KeyCode.Alpha2, 1);
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
