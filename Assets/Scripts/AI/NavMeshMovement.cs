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

		HandleMovement();
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

	private void HandleMovement()
	{
		if (pathVectorList != null)
		{
			Vector3 targetPosition = pathVectorList[currentPathIndex];
			if (Vector3.Distance(transform.position, targetPosition) > 1f)
			{
				Vector3 moveDir = (targetPosition - transform.position).normalized;

				float distanceBefore = Vector3.Distance(transform.position, targetPosition);
				transform.position = transform.position + moveDir * maxSpeed * Time.deltaTime;
				MoveTowards(targetPosition);
			}
			else
			{
				currentPathIndex++;
				if (currentPathIndex >= pathVectorList.Count)
				{
					StopMoving();
				}
			}
		}
	}

	private void StopMoving()
	{
		pathVectorList = null;
	}

	public Vector3 Getposition()
	{
		return transform.position;
	}

	public void SetTargetPosition()
	{
		currentPathIndex = 0;
		//pathVectorList = PathFind
	}
}
