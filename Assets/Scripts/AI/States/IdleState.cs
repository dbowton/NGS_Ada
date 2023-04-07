using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
	public IdleState(StateAgent owner, string name) : base(owner, name)
	{

	}

	public override void OnEnter()
	{
		owner.movement.Stop();
		owner.movement.velocity = Vector3.zero;
		owner.timer.value = 0;
		owner.animator.SetBool("IsIdle", true);
	}

	public override void OnExit()
	{
		owner.animator.SetBool("IsIdle", false);
	}

	public override void OnUpdate()
	{

	}
}
