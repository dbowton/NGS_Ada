using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
	[SerializeField] float height = 0.5f;
	[SerializeField] float range = 3.5f;
	[SerializeField] float viewAngle = 180;
	[SerializeField] int steps = 10;

	[SerializeField] LineRenderer lineRenderer;
	[SerializeField] LayerMask targetLayer;

	[SerializeField] Transform xRotObj;
	[SerializeField] Transform yRotObj;

	Timer reloadTimer;
	Timer attackTimer;
	[SerializeField] float attackRate = 1f;
	[SerializeField] float damage = 2f;

	[SerializeField] TargetMode targetMode;

	[SerializeField] float projectileSpeed = 10f;
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] GameObject projectileSpawn;
	GameObject projectile;

	public enum TargetMode
	{
		Closest,
		Healthiest,
		First,
		Last
	}

	private bool placed = false;
	public bool Placed { get { return placed; } 
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
		lineRenderer.positionCount = steps * 3 + 1;
		attackTimer = new Timer(attackRate);
		attackTimer.End();

		reloadTimer = new Timer(attackRate / 2, () =>
		{
			projectile = Instantiate(projectilePrefab, projectileSpawn.transform);
			attackTimer.Reset();
		});

		reloadTimer.End();
		projectile = Instantiate(projectilePrefab, projectileSpawn.transform);
	}


	[SerializeField] bool trigger = false;

	private void Update()
	{
		if (trigger)
		{
			trigger = false;
			Placed = true;
			return;
		}


		if(!placed) 
		{ 
			lineRenderer.SetPosition(0, transform.position + Vector3.up * height);
			for (int i = 0, j = 1; i < steps; i++)
			{
				Vector3 angle = Vector3.RotateTowards(-transform.right, transform.right, -Mathf.Deg2Rad * ((viewAngle / steps * i) + (180 - viewAngle) / 2f), 0);
				lineRenderer.SetPosition(j, transform.position + (angle * range) + Vector3.up * height);
				j++;

				angle = Vector3.RotateTowards(-transform.right, transform.right, -Mathf.Deg2Rad * ((viewAngle / steps * (i + 1)) + (180 - viewAngle) / 2f), 0);
				lineRenderer.SetPosition(j, transform.position + (angle * range) + Vector3.up * height);
				j++;

				lineRenderer.SetPosition(j, transform.position + Vector3.up * height);
				j++;
			}
		}

		if (placed)
		{
			List<Transform> detectedEnemies = new List<Transform>();

			for(int i = 0; i < steps; i++) 
			{
				Vector3 angle = Vector3.RotateTowards(-transform.right, transform.right, -Mathf.Deg2Rad * ((viewAngle / steps * i) + (180 - viewAngle) / 2f), 0);

				if (Physics.SphereCast(transform.position + Vector3.up * height, 0.5f, (angle * range), out RaycastHit hitInfo, range, targetLayer))
				{
					detectedEnemies.Add(hitInfo.collider.transform.root);
				}
			}

			Transform target = null;
			if(detectedEnemies.Count > 0)
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

			LookAt(target);

			if (attackTimer.IsOver && reloadTimer.IsOver && target)
			{
				if(projectile)
				{
					projectile.transform.parent = null;
					projectile.GetComponent<Projectile>().Fired();
					projectile.GetComponent<Rigidbody>().isKinematic = false;
					projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileSpeed, ForceMode.Acceleration);
				}

				reloadTimer.Reset();
			}
		}
	}

	public void LookAt(Transform target)
	{
		Vector3 targetPos;
		if (target)
		{
			targetPos = target.position;
		}
		else
			targetPos = transform.position + transform.forward * 2f;

		yRotObj.forward = Vector3.Lerp(yRotObj.forward, 
			((targetPos - Vector3.up * targetPos.y) - (transform.position - Vector3.up * transform.position.y)).normalized, 
			6 * Time.deltaTime);

		Vector3 calcLook = xRotObj.forward;
		calcLook.y = 0;
		calcLook.Normalize();

		calcLook *= Vector3.Distance(xRotObj.position, targetPos);
		calcLook.y = targetPos.y;
		calcLook.y -= xRotObj.position.y;

		xRotObj.forward = Vector3.Lerp(xRotObj.forward, 
			calcLook.normalized, 
			6 * Time.deltaTime);
	}
}
