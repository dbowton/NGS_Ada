using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
	public DeathState(StateAgent owner, string name) : base(owner, name)
	{

	}

	public override void OnEnter()
	{
		owner.movement.Stop();
		owner.movement.velocity = Vector3.zero;
		owner.GetComponent<Rigidbody>().Sleep();

		owner.animator.SetTrigger("death");
		GameObject.Destroy(owner.gameObject, 3);
	}

	public override void OnExit()
	{

	}

	public override void OnUpdate()
	{

	}
}
