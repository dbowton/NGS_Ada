using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshMovement))]
public class NavMeshMovement : Movement
{

	//needs reworked
	public NavMeshAgent navMeshAgent;

	int currentPathIndex;
	private List<Vector3> pathVectorList;

	private void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		navMeshAgent.speed = movementData.maxSpeed;
		navMeshAgent.acceleration = movementData.maxForce;
		navMeshAgent.angularSpeed = movementData.turnRate;
	}

	public override void ApplyForce(Vector3 force)
	{

	}

	public override void MoveTowards(Vector3 target)
	{
		navMeshAgent.SetDestination(target);
		velocity = navMeshAgent.velocity;
	}

	public override void Resume()
	{
		navMeshAgent.isStopped = false;
	}

	public override void Stop()
	{
		navMeshAgent.isStopped = true;
	}

	public override Vector3 destination 
	{ 
		get => base.destination; 
		set => base.destination = value; 
	}

	public Vector3 Getposition()
	{
		return transform.position;
	}

	public void SetTargetPosition()
	{
		currentPathIndex = 0;
	}
}
