using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MetroidvaniaCamera : CameraController
{
	public Transform target;

	protected override void OnValidate()
	{
		base.OnValidate();

		if (!target.TryGetComponent<Collider2D>(out _))
		{
			Debug.LogError("SimpleRougeCamera Error: target requires Collider2D component");
		}
	}

	protected override void Awake()
	{
		base.Awake();

		if (!target.TryGetComponent<Collider2D>(out _))
		{
			Debug.LogError("SimpleRougeCamera Error: capsuleCollider2D added to " + target.name);
			target.AddComponent<CapsuleCollider2D>();
		}
	}

	private void Update()
	{
		RaycastHit2D up_hit = Physics2D.Raycast(target.transform.position, Vector2.up, 500f, 1 << LayerMask.NameToLayer("Wall"));
		RaycastHit2D down_hit = Physics2D.Raycast(target.transform.position, Vector2.down, 500f, 1 << LayerMask.NameToLayer("Wall"));
		RaycastHit2D left_hit = Physics2D.Raycast(target.transform.position, Vector2.left, 500f, 1 << LayerMask.NameToLayer("Wall"));
		RaycastHit2D right_hit = Physics2D.Raycast(target.transform.position, Vector2.right, 500f, 1 << LayerMask.NameToLayer("Wall"));

		Vector2 size = target.GetComponent<Collider2D>().bounds.size * 0.5f;
		float upFavor = (up_hit.distance - size.y) / (up_hit.distance + down_hit.distance - size.y * 2f);
		float rightFavor = (right_hit.distance - size.x) / (left_hit.distance + right_hit.distance - size.x * 2f);

		Vector3 pos = target.transform.position;

		pos.x = Mathf.Lerp(right_hit.point.x, left_hit.point.x, rightFavor);
		pos.y = Mathf.Lerp(up_hit.point.y, down_hit.point.y, upFavor);
		pos.z = SmartCamera.instance.transform.position.z;

		float scale = Vector2.Distance(SmartCamera.instance.main.WorldToViewportPoint(target.transform.position), Vector3.one * 0.5f) * 4f;
		setPos = Vector3.Lerp(SmartCamera.instance.transform.position, pos, Time.deltaTime * scale);
		setRot = transform.rotation;
	}
}
