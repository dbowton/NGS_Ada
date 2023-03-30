using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
	public ChaseState(StateAgent owner, string name) : base(owner, name)
	{

	}

	public override void OnEnter()
	{
		owner.movement.Resume();
	}

	public override void OnExit()
	{
		
	}

	public override void OnUpdate()
	{
		if (owner.enemy != null)
		{
			owner.movement.MoveTowards(owner.enemy.transform.position);
		}
	}
}
