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

	private void Update()
	{
		if (awaitingPlacement)
		{
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, placementRange))
			{
				print("hit");
				if(awaitingTower == null) awaitingTower = Instantiate(towerPrefabs[index]);
				awaitingTower.transform.position = hitInfo.point;

				List<Collider> collisions = Physics.OverlapSphere(hitInfo.point, awaitingTower.GetComponent<Tower>().towerSize).ToList();
				if (!(collisions.Any(x => x.CompareTag("Path") || (x.TryGetComponent<Tower>(out _) && !x.gameObject.Equals(awaitingTower)))))
				{
					if (Input.GetMouseButton(1))
					{
						// set tower color to default
						print("default");
						awaitingTower = null;
						awaitingPlacement = false;
					}
					else
					{
						// set tower color to blue
						print("blue");
					}
				}
				else
				{
					// set tower color to red
					print("red");
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
			awaitingPlacement = true;
			if (awaitingTower) Destroy(awaitingTower);
			index = num;
		}
	}
}
