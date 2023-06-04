using UnityEngine;

public class Projectile : MonoBehaviour
{
	[HideInInspector] public float damage = 100f;
	public string targetTag = "Enemy";

	protected Timer destroyTimer;
	public bool fired = false;

	[SerializeField] GameObject trail;

	public virtual void Fired()
	{
		fired = true;
		AudioManager.instance.Play("BallistaFire");
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
			if (trail) Destroy(trail);
			Destroy(this);
		}
	}
}
