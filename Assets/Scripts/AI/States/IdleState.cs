using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    float timer;

	public IdleState(StateAgent owner, string name) : base(owner, name)
	{

	}

	public override void OnEnter()
	{
		owner.movement.Stop();
		owner.timer.value = 2;
	}

	public override void OnExit()
	{
		
	}

	public override void OnUpdate()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			owner.stateMachine.SetState(owner.stateMachine.StateFromName("patrol"));
		}
	}
}
