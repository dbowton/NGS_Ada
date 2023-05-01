using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
	[SerializeField] KinematicCharacterMotor player;

	public void Trigger()
	{
		print("triggered respawn");

		player.SetPositionAndRotation(transform.position, Quaternion.identity);


//		player.transform.position = Vector3.zero;// transform.position;
	}
}
