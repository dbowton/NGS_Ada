using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonProjectile : Projectile
{
	public override void Fired()
	{
		fired = true;
	}

	public override void OnTriggerEnter(Collider other)
	{
		foreach(var collider in Physics.OverlapSphere(gameObject.transform.position, 100000))
		{
			if (collider.transform.root.gameObject.CompareTag(targetTag) && collider.transform.root.TryGetComponent<Health>(out Health health))
			{
				health.Damage(damage);
				Destroy(gameObject);
				Destroy(this);
			}
		}
	}
}
