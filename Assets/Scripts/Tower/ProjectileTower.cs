using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileTower : Tower
{

	[SerializeField] float height = 0.5f;
	[SerializeField] float range = 3.5f;
	[SerializeField] float viewAngle = 180;
	[SerializeField] int steps = 10;

	[SerializeField] float turnRate = 6f;

	[SerializeField] LineRenderer lineRenderer;

	[SerializeField] Transform xRotObj;
	[SerializeField] Transform yRotObj;

	Timer reloadTimer;
	Timer attackTimer;
	[SerializeField] float attackRate = 1f;


	[SerializeField] float projectileSpeed = 10f;
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] GameObject projectileSpawn;
	[SerializeField] ParticleSystem particleSystem;
	GameObject projectile;

	public override bool Placed
	{
		get { return placed; }
		set
		{
			placed = value;

			if (placed)
			{
				lineRenderer.enabled = false;
			}
		}
	}

	private void Start()
	{
		lineRenderer.positionCount = (int)(viewAngle * 4 / 15) + 7;
		attackTimer = new Timer(attackRate);
		attackTimer.End();

		reloadTimer = new Timer(attackRate / 2, () =>
		{
			if (projectile == null || !projectile.TryGetComponent<Projectile>(out Projectile oldProjectile) || oldProjectile.fired)
			{
				projectile = Instantiate(projectilePrefab, projectileSpawn.transform);
				projectile.GetComponent<Projectile>().damage = this.damage;
				attackTimer.Reset();
			}
		});

		reloadTimer.End();
		projectile = Instantiate(projectilePrefab, projectileSpawn.transform);
	}

	protected override void Update()
	{
		if (!placed)
		{
			int j = 0;
			Vector3 angle;
			lineRenderer.SetPosition(0, transform.position + Vector3.up * height);
			j++;
			// Draw Left Segments
			for (int i = 0; i < viewAngle / 20;)
			{
				angle = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.Deg2Rad * 10 /* (viewAngle / steps) */ * i, 0);
				lineRenderer.SetPosition(j, transform.position + (angle * range) + Vector3.up * height);
				j++;

				i++;
				angle = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.Deg2Rad * 10 /* (viewAngle / steps) */ * i, 0);
				lineRenderer.SetPosition(j, transform.position + (angle * range) + Vector3.up * height);
				j++;

				lineRenderer.SetPosition(j, transform.position + Vector3.up * height);
				j++;
			}
			// Draw Riht Segments
			for (int i = 0; i < viewAngle / 20;)
			{
				angle = Vector3.RotateTowards(transform.forward, transform.right, Mathf.Deg2Rad * 10 /* (viewAngle / steps) */ * i, 0);
				lineRenderer.SetPosition(j, transform.position + (angle * range) + Vector3.up * height);
				j++;

				i++;
				angle = Vector3.RotateTowards(transform.forward, transform.right, Mathf.Deg2Rad * 10 /* (viewAngle / steps) */ * i, 0);
				lineRenderer.SetPosition(j, transform.position + (angle * range) + Vector3.up * height);
				j++;

				lineRenderer.SetPosition(j, transform.position + Vector3.up * height);
				j++;
			}
		}

		if (placed)
		{
			List<Transform> detectedEnemies = new List<Transform>();

			Vector3 angle;
			// Detect Right Enemies
			for (int i = 0; i < steps / 4; i++)
			{
				angle = Vector3.RotateTowards(transform.forward, -transform.right, Mathf.Deg2Rad * (viewAngle / steps) * i, 0);

				foreach (var enemy in Physics.SphereCastAll(transform.position + Vector3.up * height, 1f, (angle * range), range, targetLayer))
					if(!detectedEnemies.Contains(enemy.collider.transform.root))
						detectedEnemies.Add(enemy.collider.transform.root);
			}
			// Detect Left Enemies
			for (int i = 0; i < steps / 4; i++)
			{
				angle = Vector3.RotateTowards(transform.forward, transform.right, Mathf.Deg2Rad * (viewAngle / steps) * i, 0);
				foreach (var enemy in Physics.SphereCastAll(transform.position + Vector3.up * height, 1f, (angle * range), range, targetLayer))
					if (!detectedEnemies.Contains(enemy.collider.transform.root))
						detectedEnemies.Add(enemy.collider.transform.root);
			}

			Transform target = null;
			while (detectedEnemies.Count > 0 && target == null)
			{
				switch (targetMode)
				{
					case TargetMode.Closest:
						target = detectedEnemies.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
						break;
					case TargetMode.Healthiest:
						target = detectedEnemies.OrderBy(x => x.transform.root.GetComponent<Health>().CurrentHealth).First();
						break;
					case TargetMode.First:
						break;
					case TargetMode.Last:
						break;
					default:
						break;
				}

				if (target.GetComponent<Health>().CurrentHealth <= 0)
				{
					detectedEnemies.Remove(target);
					target = null;
				}
			}

			if (LookAt(target) && attackTimer.IsOver && reloadTimer.IsOver && target)
			{
				if (projectile && projectile.TryGetComponent<Projectile>(out Projectile oldProjectile) && !oldProjectile.fired)
				{
					projectile.transform.parent = null;
					projectile.GetComponent<Projectile>().Fired();
					projectile.GetComponent<Rigidbody>().isKinematic = false;
					projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileSpeed, ForceMode.Acceleration);
					if(particleSystem != null)
					{
						particleSystem.Play();
					}
				}

				reloadTimer.Reset();
			}
		}
	}

	public bool LookAt(Transform target)
	{
		Vector3 targetPos;
		if (target)
		{
			targetPos = target.position;
		}
		else
			targetPos = transform.position + Vector3.up * height + transform.forward * 2f;

		Vector3 targetYForward = ((targetPos - Vector3.up * targetPos.y) - (transform.position - Vector3.up * transform.position.y)).normalized;

		yRotObj.forward = Vector3.Lerp(yRotObj.forward, targetYForward,
			turnRate * Time.deltaTime);

		Vector3 calcLook = xRotObj.forward;
		calcLook.y = 0;
		calcLook.Normalize();

		calcLook *= Vector3.Distance(xRotObj.position, targetPos);
		calcLook.y = targetPos.y;
		calcLook.y -= xRotObj.position.y;

		xRotObj.forward = Vector3.Lerp(xRotObj.forward,
			calcLook.normalized,
			turnRate * Time.deltaTime);

		return Vector3.Angle(yRotObj.forward, targetYForward) < 2f;
	}
}
