using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HammerTower : Tower
{
	[SerializeField] float cooldownTime = 1.5f;
	float timeAccumulator = 0f;

	[SerializeField] Transform hammerHitPos;
	[SerializeField] float radius = 1.5f;
	[SerializeField] Animator animator;

	[SerializeField] ParticleSystem impact;

	[SerializeField] bool hit = false;

	private void Start()
	{
		animator.SetBool("forcedDown", true);
	}

	protected override void Update()
	{
		if (!placed) return;
		animator.SetBool("forcedDown", false);

		if (hit) Hit();
		if(timeAccumulator < cooldownTime) timeAccumulator += Time.deltaTime;
	}
	private void OnTriggerEnter(Collider other)
	{
		if(timeAccumulator >= cooldownTime) 
		{ 
			if(targetLayer == (targetLayer | (1 << other.transform.root.gameObject.layer)))
			{
				timeAccumulator -= cooldownTime;

				animator.SetTrigger("Hit");
				impact.Play();
			}
		}
	}

	public void Hit()
	{
		print("Hit");
		hit = false;
		List<Health> healths = new List<Health>();

		foreach (var c in Physics.OverlapSphere(hammerHitPos.position, radius, targetLayer))
		{
			if (c.transform.root.TryGetComponent<Health>(out Health enemyHealth))
			{
				if (!healths.Contains(enemyHealth))
				{
					print("Damaged: " + c.transform.root.gameObject.name);
					healths.Add(enemyHealth);
					enemyHealth.Damage(damage);
				}
			}
		}
	}
}
