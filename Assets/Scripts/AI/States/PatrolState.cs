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
			//owner.path.targetNode = owner.path.pathNodes.GetNearestNode(owner.transform.position);
			if (owner.path.targetNode == null) owner.path.targetNode = owner.path.pathNodes.getStartNode();
			owner.movement.Resume();
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
	}
}
