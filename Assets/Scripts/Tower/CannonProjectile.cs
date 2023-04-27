using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonProjectile : Projectile
{
	public override void Fired()
	{
		fired = true;
		destroyTimer = new Timer(0.5f, () => Destroy(gameObject));
	}

	public override void OnTriggerEnter(Collider other)
	{
		bool hit = false;
		foreach(var collider in Physics.OverlapSphere(gameObject.transform.position, 2))
		{
			if (collider.transform.root.gameObject.CompareTag(targetTag) && collider.transform.root.TryGetComponent<Health>(out Health health))
			{
				health.Damage(damage);
				hit = true;
			}
		}

		if (hit)
		{
			destroyTimer.Remove();
			Destroy(gameObject);
		}
	}
}
