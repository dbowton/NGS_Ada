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
		owner.movement.velocity = Vector3.zero;
		owner.GetComponent<Rigidbody>().Sleep();

		owner.animator.SetFloat("Speed", 0);

		owner.transform.LookAt(owner.enemy.transform.position);

		owner.animator.SetTrigger("Attack");
		owner.timer.value = 1;
		owner.GetComponent<AgentDamage>().Damage();
		owner.GetComponentInChildren<Weapon>().isAttacking = true;
	}

	public override void OnExit()
	{
		owner.GetComponentInChildren<Weapon>().isAttacking = false;
	}

	public override void OnUpdate()
	{
		
	}
}
