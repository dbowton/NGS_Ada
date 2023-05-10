using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereCastPerception : Perception
{
	[SerializeField] Transform raycastTransform;
	[SerializeField][Range(2, 50)] public int numRaycast = 2;
	[SerializeField][Range(0, 5)] public float radius = 2;
	public NavMeshMovement navMeshMovement;
	public float maxPathLength = 10;

	public override GameObject[] GetGameObjects()
	{
		List<GameObject> result = new List<GameObject>();

		float angleOffset = (angle * 2) / (numRaycast - 1);
		for (int i = 0; i < numRaycast; i++)
		{
			Quaternion rotation = Quaternion.AngleAxis(-angle + (angleOffset * i), Vector3.up);
			Vector3 direction = rotation * raycastTransform.forward;
			Ray ray = new Ray(raycastTransform.position, direction);
			if (Physics.SphereCast(ray, radius, out RaycastHit raycastHit, distance))
			{
				if (tagname == "" || raycastHit.collider.transform.root.CompareTag(tagname))
				{
					if (navMeshMovement.pathToPlayer(raycastHit.collider.transform) < maxPathLength)
					{
						Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
						//Debug.Log(navMeshMovement.pathToPlayer(raycastHit.collider.transform));
						result.Add(raycastHit.collider.gameObject);
					}
					//Debug.DrawRay(ray.origin, ray.direction * raycastHit.distance, Color.red);
					//result.Add(raycastHit.collider.gameObject);
				}
			}

			else
			{
				Debug.DrawRay(ray.origin, ray.direction * distance, Color.white);
			}
		}
		return result.ToArray();
	}
}
