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

		//navMeshAgent.CalculatePath(target, navMeshAgent.SetDestination(target));

		navMeshAgent.SetDestination(target);
		//Debug.Log(navMeshAgent.SetDestination(target));
		velocity = navMeshAgent.velocity;
		//Debug.Log(velocity);
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

	public float GetPathLength()
	{		
		float length = navMeshAgent.remainingDistance;

		Debug.Log(length);
		return length;
	}

	public float pathToPlayer(Transform playerTransform)
	{
		float length = 0;
		NavMeshPath path = new NavMeshPath();
		navMeshAgent.CalculatePath(playerTransform.position, path);

		for (int i = 0; i < path.corners.Length - 1; i++)
		{
			length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
		}

		return length;
	}
}
