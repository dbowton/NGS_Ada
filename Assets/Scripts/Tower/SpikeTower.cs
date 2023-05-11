using System.Collections.Generic;
using UnityEngine;

public class SpikeTower : Tower
{
	List<GameObject> hitObjects = new List<GameObject>();
	float timeAccumulator = 0f;
	bool isActive = false;

	[SerializeField] float activeTime = 1f;
	[SerializeField] float cooldownTime = 2.5f;

	[SerializeField] Animator animator;

	protected override void Update()
	{
		if(!placed) return;

		timeAccumulator += Time.deltaTime;

		if(isActive)
		{
			if (timeAccumulator >= activeTime)
			{
				//	Disable
				isActive = false;
				timeAccumulator -= activeTime;

				hitObjects.Clear();
				animator.SetBool("active", false);
			}
		}
		else
		{
			if(timeAccumulator >= cooldownTime)
			{
				//	enable
				isActive = true;
				timeAccumulator -= cooldownTime;

				animator.SetBool("active", true);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isActive) return;
		if (!hitObjects.Contains(other.transform.root.gameObject) && targetLayer == (targetLayer | (1 << other.transform.root.gameObject.layer)))
		{
			hitObjects.Add(other.transform.root.gameObject);
			other.transform.root.GetComponent<Health>().Damage(damage);
		}
	}
}
