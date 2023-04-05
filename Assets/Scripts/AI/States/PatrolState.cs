using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
	public PatrolState(StateAgent owner, string name) : base(owner, name)
	{

	}

	public override void OnEnter()
	{
		if (owner.path)
		{
			owner.path.targetNode = owner.path.pathNodes.GetNearestNode(owner.transform.position);
			owner.movement.Resume();
			owner.timer.value = Random.Range(5, 10);
		}
	}

	public override void OnExit()
	{
		owner.movement.Stop();
	}

	public override void OnUpdate()
	{
		if(owner.path)
		{
			owner.path.Move(owner.movement);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			owner.stateMachine.SetState(owner.stateMachine.StateFromName("idle"));
		}
	}
}
