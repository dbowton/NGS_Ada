using System.Collections.Generic;
using UnityEngine;

public class SpikeTower : Tower
{
	List<GameObject> hitObjects = new List<GameObject>();
	float timeAccumulator = 0f;
	bool isActive = false;

	[SerializeField] int hitNumber = 5;
	int hitCount = 0;

	[SerializeField] float cooldownTime = 2.5f;

	[SerializeField] Animator animator;

	protected override void Update()
	{
		if(!placed) return;


		if(isActive)
		{
			if (hitCount >= hitNumber)
			{
				//	Disable
				isActive = false;

				hitObjects.Clear();
				animator.SetBool("active", false);
			}
		}
		else
		{
			timeAccumulator += Time.deltaTime;
			if(timeAccumulator >= cooldownTime)
			{
				//	enable
				isActive = true;
				timeAccumulator = 0;
				hitCount= 0;

				animator.SetBool("active", true);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isActive) return;
		if (!hitObjects.Contains(other.transform.root.gameObject) && targetLayer == (targetLayer | (1 << other.transform.root.gameObject.layer)))
		{
			hitCount++;
			hitObjects.Add(other.transform.root.gameObject);
			other.transform.root.GetComponent<Health>().Damage(damage);
		}
	}
}
