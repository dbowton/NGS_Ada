using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerSize;
	[SerializeField] float attackSize = 1;
	[SerializeField] LayerMask targetLayer;
	[SerializeField] Transform hitCenter;


	public TargetMode targetMode;
	public Material towerIcon;

	public enum TargetMode
	{
		Closest,
		Healthiest,
		First,
		Last
	}

	Timer attackTimer;
	[SerializeField] float attackRate = 1f;
	[SerializeField] float damage = 2f;

	GameObject hitSphere = null;

	private void Start()
	{
		attackTimer = new Timer(attackRate);
		attackTimer.End();
	}

	[SerializeField] GameObject spherePrefab;

	private void Update()
	{
		if (!attackTimer.IsOver) return;

		if (hitSphere == null)
		{
			hitSphere = Instantiate(spherePrefab);
			hitSphere.transform.position = hitCenter.transform.position;
			hitSphere.transform.localScale = Vector3.one * attackSize;
		}

		List<Collider> hits = Physics.OverlapSphere(hitCenter.position, attackSize, targetLayer).ToList();

		if (hits.Count == 0) return;

		GameObject targetObject = null;
		switch (targetMode)
		{
			case TargetMode.Closest:
				targetObject = hits.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First().gameObject;
				break;
			case TargetMode.Healthiest:
				targetObject = hits.OrderBy(x => x.transform.root.GetComponent<Health>().CurrentHealth).First().gameObject;
				break;
			case TargetMode.First:
				break;
			case TargetMode.Last:
				break;
			default:
				break;
		}

		if (targetObject != null)
		{
			print("attacked: " + targetObject.name);
			targetObject.transform.root.GetComponent<Health>().Damage(damage);
			Destroy(hitSphere);
			hitSphere = null;
			attackTimer.Reset();
		}
	}

	private void OnDestroy()
	{
		if (attackTimer == null) attackTimer.Remove();
		if(hitSphere) Destroy(hitSphere);
	}
}
