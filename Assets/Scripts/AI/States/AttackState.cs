using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
	public AttackState(StateAgent owner, string name) : base(owner, name)
	{

	}

	public override void OnEnter()
	{
		owner.movement.Stop();
		owner.animator.SetTrigger("Attack");
		owner.timer.value = 2;
		owner.GetComponent<AgentDamage>().Damage();
	}

	public override void OnExit()
	{
		
	}

	public override void OnUpdate()
	{
	
	}
}
