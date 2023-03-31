using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDamage : MonoBehaviour
{
	[SerializeField] string tagName;
	[SerializeField] float distance;
	[SerializeField] float damage;

	public void Damage()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, distance);
		foreach(Collider collider in colliders)
		{
			if (collider.gameObject == gameObject) continue;

			if (tagName == "" || collider.CompareTag(tagName))
			{
				if (collider.gameObject.TryGetComponent<StateAgent>(out StateAgent stateAgent))
				{
					stateAgent.health.value -= damage;
				}
			}
		}
	}
}
