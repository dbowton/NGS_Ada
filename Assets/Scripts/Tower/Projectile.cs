using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Projectile : MonoBehaviour
{
	public float damage = 100f;
	public string targetTag = "Enemy";

	protected Timer destroyTimer;
	public bool fired = false;

	public virtual void Fired()
	{
		fired = true;
		destroyTimer = new Timer(0.5f, () => Destroy(gameObject));
	}

	public virtual void OnTriggerEnter(Collider other)
	{		
		if (fired && other.transform.root.gameObject.CompareTag(targetTag) && other.transform.root.TryGetComponent<Health>(out Health health))
		{
			health.Damage(damage);
			gameObject.transform.parent = other.transform;
			Destroy(gameObject.GetComponent<Rigidbody>());

			destroyTimer.Remove();
			Destroy(this);
		}
	}
}
